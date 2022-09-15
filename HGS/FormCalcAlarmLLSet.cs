using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GlacialComponents.Controls;
using System.Collections;
using System.Text.RegularExpressions;
using Npgsql;
namespace HGS
{
    public partial class FormCalcAlarmLLSet : Form
    {
        HashSet<int> onlyid = new HashSet<int>();
        public point CalcPoint;
        int PointNums = 0;
        //--------------------------------
        public FormCalcAlarmLLSet()
        {
            InitializeComponent();
        }
        private void DisplayStats()
        {
            tSSLabel_varnums.Text = "变量数：" + PointNums.ToString();
        }
        public void glacialLisint()
        {
            PointNums = 0;
            timer1.Enabled = false;
            List<GLItem> lsItmems = new List<GLItem>();
            if (CalcPoint.lsCalcOrgSubPoint_ll != null)
            {
                foreach (varlinktopoint subpt in CalcPoint.lsCalcOrgSubPoint_ll)
                {
                    GLItem itemn = new GLItem(glacialList1);
                    lsItmems.Add(itemn);
                    itemtag it = new itemtag();

                    point Point = Data.inst().cd_Point[subpt.sub_id];

                    it.id = subpt.sub_id;

                    itemn.SubItems["ND"].Text = Point.nd;
                    itemn.SubItems["PN"].Text = Point.pn;

                    itemn.SubItems["EU"].Text = Point.eu;
                    itemn.SubItems["ED"].Text = Point.ed;
                    //itemn.SubItems[1].Text = Pref.GetInst().GetVarName(Point);
                    it.sisid = Point.id_sis;
                    if (onlyid.Contains(CalcPoint.id)) throw new Exception("不能引用自身！");
                    onlyid.Add(it.id);//唯一性
                                      //
                    itemn.SubItems["VarName"].Text = subpt.varname;
                    itemn.Tag = it;
                    PointNums++;
                }
            }
            glacialList1.Items.AddRange(lsItmems.ToArray());
            onlyid.Add(CalcPoint.id);//排除自已。
            textBoxFormula.Text = CalcPoint.orgformula_ll;
            textBoxmDiscription.Text = CalcPoint.ed;
            comboBox_eu.Text = CalcPoint.eu;
            textBoxPN.Text = CalcPoint.pn;
            checkBoxCalc.Checked = CalcPoint.iscalc;
            numericUpDown.Value = CalcPoint.fm;
            timer1.Enabled = true;
            DisplayStats();
        }
        private void FormPointSet_FormClosed(object sender, FormClosedEventArgs e)
        {
            glacialList1.Dispose();
            timer1.Enabled = false;
            //sisconn.close();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (GLItem item in glacialList1.Items)
            {
                if (glacialList1.IsItemVisible(item))
                {
                    itemtag it = (itemtag)(item.Tag);
                    point pt = Data.inst().cd_Point[it.id];
                    if (pt.av != null)
                    {
                        double dAV = pt.av ?? 0;
                        item.SubItems["AV"].Text = Math.Round(dAV, pt.fm).ToString();
                    }
                    else
                        item.SubItems["AV"].Text = pt.av.ToString();
                    item.SubItems["DS"].Text = pt.ps.ToString();
                }
            }
            DisplayStats();
        }
        private void toolStripButtonAdd_Click_1(object sender, EventArgs e)
        {
            const string cvn = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            FormCalcPointList fcpl = new FormCalcPointList();
            fcpl.glacialLisint(onlyid);
            if (fcpl.ShowDialog() == DialogResult.OK)
            {
                foreach (GLItem item in fcpl.glacialList.SelectedItems)
                {
                    if (!onlyid.Contains(((itemtag)(item.Tag)).sisid))
                    {
                        string t = "";
                        for (int i = 0; i < cvn.Length; i++)
                        {
                            t = cvn.Substring(i, 1);
                            int j = 0;
                            while (j < glacialList1.Items.Count)
                            {
                                if (glacialList1.Items[j].SubItems["VarName"].Text.Trim().Equals(t))
                                    break;
                                j++;
                            }
                            if (j == glacialList1.Items.Count)
                                break;
                        }
                        
                        GLItem itemn = glacialList1.Items.Add("");
                        itemn.SubItems["VarName"].Text = t;
                        itemn.SubItems["ND"].Text = item.SubItems["ND"].Text;
                        itemn.SubItems["PN"].Text = item.SubItems["PN"].Text;
                        itemn.SubItems["EU"].Text = item.SubItems["EU"].Text;
                        itemn.SubItems["ED"].Text = item.SubItems["ED"].Text;
                        itemn.Tag = item.Tag;
                        onlyid.Add(((itemtag)(itemn.Tag)).id);
                    }
                    else
                    {
                        MessageBox.Show(string.Format("点:{0}-{1}已存在！", item.SubItems["ND"].Text, 
                            item.SubItems["PN"].Text), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    };
                    PointNums++;
                }
                DisplayStats();
            }
        }
        private void glacialList1_Leave(object sender, EventArgs e)
        {
            foreach (GLItem item in glacialList1.Items)
            {
                item.Selected = false;
            }
        }
        private void Dovalidity(bool rsl)
        {
            HashSet<string> hsVar = new HashSet<string>();
            CalcEngine.CalcEngine ce = new CalcEngine.CalcEngine();
            point Point = new point();
            Point.lsCalcOrgSubPoint_ll = new List<varlinktopoint>();
            //-----
            //可加内部变量
            //hsVar.Add(???);
            foreach (GLItem item in glacialList1.Items)
            {
                int i = 0;i++;
                if (item.SubItems["VarName"].Text == null )
                {
                    throw new Exception(string.Format("第{0}行,变量不能为空！", i));
                }
                string svar = item.SubItems["VarName"].Text;
                if (hsVar.Contains(svar))
                {
                    throw new Exception(string.Format("第{0}行,变量或点相同！", i));
                }
                hsVar.Add(svar);
                Regex varrgx = new Regex("^[a-zA-Z][A-Za-z0-9_]*$");
                if (!varrgx.IsMatch(svar))
                {
                    throw new Exception(string.Format("第{0}行,变量只能由英文字母开头，只能是英文字母、数字和下划线！",i));
                }
                
                itemtag it = (itemtag)item.Tag;
                varlinktopoint subpt = new varlinktopoint();
                subpt.varname = item.SubItems["VarName"].Text;
                subpt.sub_id = it.id;
                Point.lsCalcOrgSubPoint_ll.Add(subpt);
                ce.Variables[subpt.varname] = Data.inst().cd_Point[it.id].av;//测试用。
            }
            if (textBoxmDiscription.Text.Length < 1)
            {
                throw new Exception("计算点的的描述不能为空！");
            }
            Point.orgformula_ll = textBoxFormula.Text;
            //
            double? orgv = null;
            if(Point.orgformula_ll.Length > 0 ) 
                orgv = Math.Round(Convert.ToDouble(ce.Evaluate(Point.orgformula_ll)), Point.fm); //验证表达式的合法性
                                                                                               //
            ce.Variables = Data.inst().Variables;
            double? expv = null;
            if(Point.orgformula_ll.Length > 0)
                expv = Math.Round(Convert.ToDouble(ce.Evaluate(Data.inst().ExpandOrgFormula_LL(Point))), Point.fm);//验证表达式展开sis点的合法性。
            if (rsl)
                MessageBox.Show(string.Format("原公式计算值＝{0}\n展开公式计算值＝{1}",orgv,expv), 
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                Dovalidity(false);
                CalcPoint.orgformula_ll = textBoxFormula.Text.Trim().Replace("\r\n", ""); ;

                CalcPoint.lsCalcOrgSubPoint_ll = new List<varlinktopoint>();

                foreach (GLItem item in glacialList1.Items)
                {
                    itemtag it = (itemtag)item.Tag;
                    varlinktopoint subpt = new varlinktopoint();
                    subpt.varname = item.SubItems["VarName"].Text;
                    subpt.sub_id = it.id;
                    CalcPoint.lsCalcOrgSubPoint_ll.Add(subpt);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }

        private void glacialList1_DoubleClick(object sender, EventArgs e)
        {
            if (glacialList1.SelectedItems.Count == 1)
            {
                GLItem item = (GLItem)glacialList1.SelectedItems[0];
                textBoxFormula.SelectedText = item.SubItems["VarName"].Text;
            }
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            try
            {
                Dovalidity(true);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tSB_del_Click(object sender, EventArgs e)
        {
            if (glacialList1.SelectedItems.Count == 1)
            {
                GLItem item = (GLItem)glacialList1.SelectedItems[0];
                string vn = item.SubItems["VarName"].Text;
                string pat = string.Format(@"\b{0}\b(?=[^(])|\b{0}$", vn);

                if (Regex.IsMatch(textBoxFormula.Text, pat))
                {
                    MessageBox.Show("公式已引用，不能删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else glacialList1.Items.Remove(item);

            }
        }
    }
}

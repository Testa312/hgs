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
using Npgsql;
using System.Diagnostics;
namespace HGS
{
    public partial class FormPointSet : Form
    {
        Dictionary<GLItem, point> dic_glItemNew = new Dictionary<GLItem, point>();
        HashSet<GLItem> hs_glItemModified = new HashSet<GLItem>();
        HashSet<int> onlysisid = new HashSet<int>();
        HashSet<string> hs_ND = new HashSet<string>();
        int PointNums = 0;
        bool isFirst = true;
        public FormPointSet()
        {
            InitializeComponent();
        }
        private void DisplayHints()
        {
            tSSLabel_count.Text = string.Format("点数：{0}，其中新加点{1}，已修改点{2}。",
               PointNums, dic_glItemNew.Count, hs_glItemModified.Count);
        }
        private void AlarmSubItemSet(GLItem item,point pt)
        {
            if (pt.pointsrc == pointsrc.sis)
            {
                item.SubItems["IsAlarm"].Text = pt.isavalarm || pt.isboolvalarm ? Pref.Inst().strOk : Pref.Inst().strNo;
                if (Data.inst().hs_FormulaErrorPoint.Contains(pt)) item.SubItems["FError"].Text = Pref.Inst().strNo; 
            }
            else
            {
                item.SubItems["IsAlarm"].Text = pt.isavalarm || pt.isboolvalarm ? Pref.Inst().strOk : Pref.Inst().strNo;
                item.SubItems["IsCalc"].Text = pt.iscalc ? Pref.Inst().strOk : Pref.Inst().strNo;
            }
        }
        private void glacialLisint()
        {
            //Stopwatch sw1 = new Stopwatch();//???????????
            //Stopwatch sw2 = new Stopwatch();//???????????

            timerUpdateValue.Enabled = false;
            glacialList1.Items.Clear();
            PointNums = 0;
            this.Cursor = Cursors.WaitCursor;
            //sw1.Start();
            foreach (point ptx in Data.inst().hsAllPoint)
            {

                itemtag it = new itemtag();

                List<GLItem> lsItem = new List<GLItem>();
                if ((ptx.pointsrc == pointsrc.sis || (Auth.GetInst().LoginID == 0 || ptx.ownerid == Auth.GetInst().LoginID || 
                    ptx.ownerid == 0)) &&
                    ptx.nd.Contains(tSCB_ND.Text.Trim()) && ptx.ed.Contains(tSTB_ED.Text.Trim()) &&
                    ptx.pn.Contains(tSTB_PN.Text.Trim()) && ptx.orgformula_main.Contains(tSTB_F.Text.Trim()))
                {
                    // sw2.Start();
                    GLItem itemn = new GLItem(glacialList1);// glacialList1.Items.Add("");//insert 慢
                    lsItem.Add(itemn);
                    it.id = ptx.id;

                    itemn.SubItems["ND"].Text = ptx.nd;
                    itemn.SubItems["PN"].Text = ptx.pn;

                    itemn.SubItems["EU"].Text = ptx.eu;
                    itemn.SubItems["ED"].Text = ptx.ed;
                    itemn.SubItems["TV"].Text = ptx.tv.ToString();
                    itemn.SubItems["BV"].Text = ptx.bv.ToString();
                    itemn.SubItems["LL"].Text = ptx.ll.ToString();
                    itemn.SubItems["HL"].Text = ptx.hl.ToString();
                    itemn.SubItems["ZL"].Text = ptx.zl.ToString();
                    itemn.SubItems["ZH"].Text = ptx.zh.ToString();
                    itemn.SubItems["AlarmInfo"].Text = ptx.alarmininfo;
                    itemn.SubItems["DS"].Text = ptx.ps.ToString();
                    it.sisid = ptx.id_sis;
                    it.fm = ptx.fm;
                    it.PointSrc = ptx.pointsrc;
                    /*
                    if (ptx.pointsrc == pointsrc.sis)
                    {
                        onlysisid.Add(it.sisid);//唯一性
                    }*/
                    AlarmSubItemSet(itemn,ptx);
                    itemn.Tag = it;
                    PointNums++;
                    //sw2.Stop();
                }
                glacialList1.Items.AddRange(lsItem.ToArray());
                if (isFirst) hs_ND.Add(ptx.nd);              
            }
            //sw1.Stop();
            if (isFirst)
            {
                foreach (string citem in hs_ND)
                {
                    tSCB_ND.Items.Add(citem.Trim());
                }
                isFirst = false;
            }
            this.Cursor = Cursors.Default;
            timerUpdateValue.Enabled = true;
            DisplayHints();
        }
        private void toolStripButtonAddSis_Click(object sender, EventArgs e)//加sis点
        {     
            FormSisPointList fspl = new FormSisPointList();
            fspl.onlysisid = onlysisid;
            if (fspl.ShowDialog() == DialogResult.OK)
            {
                toolStripButtonFind.Enabled = fspl.glacialList.SelectedItems.Count == 0;
                foreach (GLItem item in fspl.glacialList.SelectedItems)
                {
                    if (!onlysisid.Contains(((itemtag)(item.Tag)).sisid))
                    {
                        point Point = new point();
                        GLItem itemn;

                        itemn = glacialList1.Items.Insert(0, "");
                        Point.nd = itemn.SubItems["ND"].Text = fspl.tSCBNode.Text;
                        Point.pn = itemn.SubItems["PN"].Text = item.SubItems["PN"].Text;
                        Point.ed = itemn.SubItems["ED"].Text = item.SubItems["ED"].Text;
                        Point.eu = itemn.SubItems["EU"].Text = item.SubItems["EU"].Text;
                        Point.id_sis = ((itemtag)(item.Tag)).sisid;

                        onlysisid.Add(Point.id_sis);
                   
                        Point.id = Data.inst().GetNextPointID();
                        Point.pointsrc = pointsrc.sis;
                        Point.ownerid = Auth.GetInst().LoginID;

                        itemtag it = new itemtag();
                        itemn.Tag = it;
                        Point.fm = it.fm = ((itemtag)(item.Tag)).fm;
                        it.id = Point.id;
                        it.PointSrc = Point.pointsrc;
                        it.sisid = Point.id_sis;
                        //Data.Get().Add(Point);
                        dic_glItemNew.Add(itemn, Point);
                    }
                    else 
                    {
                        MessageBox.Show(string.Format("点:{0}-{1}已存在！",item.SubItems["ND"].Text, 
                            item.SubItems["PN"].Text),"提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                     };
                }
                DisplayHints();
            }
        }
        private void buttonSet_Click(object sender, EventArgs e)
        {
            int glc = glacialList1.SelectedItems.Count;
            if (glc > 0)
            {
                try
                {
                    if (glc == 1 || MessageBox.Show("是否更改所有选择的模拟量报警值？", "提示", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        foreach (GLItem item in glacialList1.SelectedItems)
                        {
                            List<double> lsav = new List<double>();
                            itemtag it = (itemtag)item.Tag;
                            point pt = dic_glItemNew.ContainsKey(item) ? dic_glItemNew[item] : Data.inst().cd_Point[it.id];

                            if (textBoxZL.Text.Length > 0)
                            { pt.zl = double.Parse(textBoxZL.Text); lsav.Add(Convert.ToDouble(pt.zl)); }
                            else pt.zl = null;

                            if (textBoxLL.Text.Length > 0) { pt.ll = double.Parse(textBoxLL.Text); lsav.Add(Convert.ToDouble(pt.ll)); }
                            else pt.ll = null;

                            if (textBoxBV.Text.Length > 0) { pt.bv = double.Parse(textBoxBV.Text); lsav.Add(Convert.ToDouble(pt.bv)); }
                            else pt.bv = null;

                            if (textBoxTV.Text.Length > 0)
                            { pt.tv = double.Parse(textBoxTV.Text);lsav.Add(Convert.ToDouble(pt.tv));}
                            else pt.tv = null;                         

                            if (textBoxHL.Text.Length > 0){pt.hl = double.Parse(textBoxHL.Text); lsav.Add(Convert.ToDouble(pt.hl)); }
                            else pt.hl = null;

                            if (textBoxZH.Text.Length > 0)
                            { pt.zh = double.Parse(textBoxZH.Text); lsav.Add(Convert.ToDouble(pt.zh)); }
                            else pt.zh = null;
                            for(int i = 1; i< lsav.Count;i++)
                            {
                                if (lsav[i - 1] > lsav[i]) { throw new Exception("报警数值大小应按报警高低限值排序！"); }
                            }
                            item.SubItems["TV"].Text = textBoxTV.Text;
                            item.SubItems["BV"].Text = textBoxBV.Text;
                            item.SubItems["LL"].Text = textBoxLL.Text;
                            item.SubItems["HL"].Text = textBoxHL.Text;
                            item.SubItems["ZL"].Text = textBoxZL.Text;
                            item.SubItems["ZH"].Text = textBoxZH.Text;
                            pt.isavalarm = checkBoxAlarm.Checked;
                            pt.isboolvalarm = checkBoxbool.Checked;
                            pt.boolalarminfo = tB_boolAlarmInfo.Text;
                            AlarmSubItemSet(item, pt);
                           
                            if (!dic_glItemNew.ContainsKey(item))
                                hs_glItemModified.Add(item);

                        }
                        toolStripButtonFind.Enabled = glc == 0;
                    }                  
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                DisplayHints();
            }
        }   
        private void Save()
        {
            foreach (GLItem item in hs_glItemModified)
            {
                Data.inst().Update(Data.inst().cd_Point[((itemtag)(item.Tag)).id]);
            }
            foreach (point pt in dic_glItemNew.Values)
            {
                Data.inst().Add(pt);
                if (!hs_ND.Contains(pt.nd))
                {
                    tSCB_ND.Items.Add(pt.nd);
                }
            }
            Data.inst().SavetoPG();
            PointNums += dic_glItemNew.Count;
            hs_glItemModified.Clear();
            dic_glItemNew.Clear();
            toolStripButtonFind.Enabled = true;
        }
        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DisplayHints();
        }

        private void FormPointSet_FormClosed(object sender, FormClosedEventArgs e)
        {
            glacialList1.Dispose();
            timerUpdateValue.Enabled = false;
        }
        private void timerUpdateValue_Tick(object sender, EventArgs e)
        {
            foreach (GLItem item in glacialList1.Items)
            {
                itemtag it = (itemtag)(item.Tag);
                if (glacialList1.IsItemVisible(item))
                {
                    //bool sss = dic_glItemNew.ContainsKey(item);
                    point pt = dic_glItemNew.ContainsKey(item) ? dic_glItemNew[item] : Data.inst().cd_Point[it.id];
                    if (pt.av != null)
                    {
                        double dAV = pt.av ?? 0;
                        item.SubItems["AV"].Text =  Math.Round(dAV,pt.fm).ToString();
                    }
                    else
                        item.SubItems["AV"].Text = "";
                    item.SubItems["DS"].Text = pt.ps.ToString();
                }
            }
            DisplayHints();
        }
        private void glacialList1_Click(object sender, EventArgs e)
        {

            if (glacialList1.SelectedItems.Count == 1)
            {
                GLItem item = (GLItem)glacialList1.SelectedItems[0];
                textBoxTV.Text = item.SubItems["TV"].Text;
                textBoxBV.Text = item.SubItems["BV"].Text;
                textBoxLL.Text = item.SubItems["LL"].Text;
                textBoxHL.Text = item.SubItems["HL"].Text;
                textBoxZL.Text = item.SubItems["ZL"].Text;
                textBoxZH.Text = item.SubItems["ZH"].Text;
                itemtag it = (itemtag)(item.Tag);

                point Point = dic_glItemNew.ContainsKey(item) ? dic_glItemNew[item] : Data.inst().cd_Point[it.id];
                checkBoxAlarm.Checked = Point.isavalarm;
                checkBoxbool.Checked = Point.isboolvalarm;
                tB_boolAlarmInfo.Text = Point.boolalarminfo;
                if(Point.boolalarminfo.Length<=0)
                    tB_boolAlarmInfo.Text = item.SubItems["PN"].Text;
                buttonCalc.Enabled = (it.PointSrc == pointsrc.calc) ? true : false;

                label_formula.Text = Point.orgformula_main;
            }
            tabControl.Enabled = glacialList1.SelectedItems.Count > 0;
        }
        private void toolStripButtonAddNewCalc_Click(object sender, EventArgs e)
        {          
            FormCalcPointSet fcps = new FormCalcPointSet();
            fcps.CalcPoint = new point();
            fcps.Text = "新加计算点";
            //fcps.CellId = cellid.main;
            if (fcps.ShowDialog() == DialogResult.OK)
            {
                toolStripButtonFind.Enabled = false;

                GLItem itemn = glacialList1.Items.Insert(0, "");
                GLColumnCollection glcols = glacialList1.Columns;

                itemn.SubItems["ND"].Text = fcps.CalcPoint.nd;
                itemn.SubItems["ED"].Text = fcps.CalcPoint.ed;
                itemn.SubItems["EU"].Text = fcps.CalcPoint.eu;
                itemn.SubItems["PN"].Text = fcps.CalcPoint.pn;

                itemtag it = new itemtag();
                it.id = fcps.CalcPoint.id;
                it.fm = fcps.CalcPoint.fm;
                it.PointSrc = fcps.CalcPoint.pointsrc ;
                itemn.Tag = it;
                AlarmSubItemSet(itemn, fcps.CalcPoint);
                dic_glItemNew.Add(itemn, fcps.CalcPoint);
                DisplayHints();
            }
        }

        private void buttonCalcSet_Click(object sender, EventArgs e)
        {
            if (glacialList1.SelectedItems.Count == 1)
            {
                FormCalcPointSet fcps = new FormCalcPointSet();
                GLItem itemn = (GLItem)glacialList1.SelectedItems[0];
                itemtag it = (itemtag)itemn.Tag;

                fcps.CalcPoint = dic_glItemNew.ContainsKey(itemn) ? dic_glItemNew[itemn] : Data.inst().cd_Point[it.id];
                fcps.glacialLisint();
                fcps.Text = string.Format("点[{0}]公式",fcps.CalcPoint.ed);
                //fcps.CellId = cellid.main;
                if (fcps.ShowDialog() == DialogResult.OK)
                {
                    toolStripButtonFind.Enabled = false;
                    itemn.SubItems["ED"].Text = fcps.CalcPoint.ed;
                    itemn.SubItems["EU"].Text = fcps.CalcPoint.eu;
                    itemn.SubItems["PN"].Text = fcps.CalcPoint.pn;
                    AlarmSubItemSet(itemn, fcps.CalcPoint);
                }
                //itemn.Tag = it;
                if (!dic_glItemNew.ContainsKey(itemn))
                {
                    hs_glItemModified.Add(itemn);
                    Data.inst().hs_FormulaErrorPoint.Remove(fcps.CalcPoint);
                }
                DisplayHints();
            }
        }
       
        private void toolStripButtonSelect_Click(object sender, EventArgs e)
        {
            glacialLisint();
        }
        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            if (glacialList1.SelectedItems.Count == 1)
            {
                GLItem itemn = (GLItem)glacialList1.SelectedItems[0];
                itemtag it = (itemtag)itemn.Tag;

                List<int> lspid = Data.inst().GetDeletePointIdList(it.id);
                if (dic_glItemNew.ContainsKey(itemn))
                {
                    if (DialogResult.OK == MessageBox.Show(string.Format("是否删除点[{0}]-{1}？",
                        itemn.SubItems["PN"].Text,itemn.SubItems["ED"].Text), "提示",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                    {
                        dic_glItemNew.Remove(itemn);
                        glacialList1.Items.Remove(itemn);
                    }
                    return;
                }
                else if (lspid.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("被下列点引用，不能删除！");
                    foreach (int pid in lspid)
                    {
                        point pt = Data.inst().cd_Point[pid];
                        sb.AppendLine(string.Format("[id:{0}]-{1}", pt.id, pt.ed));
                    }
                    MessageBox.Show(sb.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (DialogResult.OK == MessageBox.Show(string.Format("是否删除点[{0}]-{1}？",
                    itemn.SubItems["PN"].Text,itemn.SubItems["ED"].Text), "提示",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    toolStripButtonFind.Enabled = false;
                    Data.inst().Delete(Data.inst().cd_Point[it.id]);
                    glacialList1.Items.Remove(itemn);
                    PointNums--;
                }
            }
        }
        private void FormPointSet_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!toolStripButtonFind.Enabled &&
                DialogResult.OK == MessageBox.Show("已修改，是否保存？", "提示",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                try
                {
                    Save();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void checkBoxbool_Click(object sender, EventArgs e)
        {
            if (checkBoxbool.Checked)
                checkBoxAlarm.Checked = false;

        }

        private void checkBoxAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAlarm.Checked)
                checkBoxbool.Checked = false;
        }

        private void FormPointSet_Shown(object sender, EventArgs e)
        {
            glacialLisint();
            foreach(point pt in Data.inst().hsSisPoint)
            {
                onlysisid.Add(pt.id_sis);
            }
            //if (tSCB_ND.Items.Count > 0) tSCB_ND.SelectedIndex = 0;
            label_formula.Text = "";
        }

        private void 强制点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (glacialList1.SelectedItems.Count == 1)
            {
                FormForceValue ffv = new FormForceValue();

                GLItem itemn = (GLItem)glacialList1.SelectedItems[0];
                itemtag it = (itemtag)itemn.Tag;

                point Point = dic_glItemNew.ContainsKey(itemn) ? dic_glItemNew[itemn] : Data.inst().cd_Point[it.id];
                ffv.Text = string.Format("强制{0}点",Point.ed);
                ffv.textBoxValue.Text = Point.forceav.ToString();
                ffv.checkBoxForce.Checked = Point.isforce;
                if(DialogResult.OK == ffv.ShowDialog())
                {
                    Point.isforce = ffv.checkBoxForce.Checked;
                    Point.forceav = Convert.ToDouble(ffv.textBoxValue.Text);
                    Point.ps = PointState.Force;
                }
            }
        }

        private void contextMenuStrip_gl_Opening(object sender, CancelEventArgs e)
        {
            强制点ToolStripMenuItem.Visible = glacialList1.SelectedItems.Count == 1;
        }

        private void tSTB_ED_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                glacialLisint();
        }
    }
}

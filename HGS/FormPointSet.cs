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
namespace HGS
{
    public partial class FormPointSet : Form
    {
        OPAPI.Connect sisconn = new OPAPI.Connect(Pref.GetInstance().sisHost, Pref.GetInstance().sisPort, 60,
           Pref.GetInstance().sisUser, Pref.GetInstance().sisPassword);//建立连接

        HashSet<int> onlysisid = new HashSet<int>();

        public FormPointSet()
        {
            InitializeComponent();
            glacialLisint();
        }
        private string getsid(pointsrc cc, long id)
        {
            switch (cc)
            {
                case pointsrc.sis: return "S" + id.ToString();
                case pointsrc.calc: return "C" + id.ToString(); ;
                default: throw new Exception("点计算优先级错误！"); ;
            }
        }
        private void glacialLisint()
        {
            timer1.Enabled = false;
            glacialList1.Items.Clear();

            foreach (int ipt in Data.Get().lsAllPoint)
            {
                GLItem itemn;
                itemtag it = new itemtag();
                point Point = Data.Get().cd_Point[ipt];

                itemn = glacialList1.Items.Add("");
                it.id = Point.id;

                itemn.SubItems[1].Text = Point.nd;
                itemn.SubItems[2].Text = Point.pn;

                itemn.SubItems[4].Text = Point.eu;
                itemn.SubItems[5].Text = Point.ed;
                itemn.SubItems[6].Text = Point.tv.ToString();
                itemn.SubItems[7].Text = Point.bv.ToString();
                itemn.SubItems[8].Text = Point.ll.ToString();
                itemn.SubItems[9].Text = Point.hl.ToString();
                itemn.SubItems[10].Text = Point.zl.ToString();
                itemn.SubItems[11].Text = Point.zh.ToString();
                it.sisID = Point.id_sis;

                //Point.poitsrc = it.PointSrc = (pointsrc)pgreader.GetInt32(pgreader.GetOrdinal("pointsrc"));
                if (Point.pointsrc == pointsrc.sis)
                    onlysisid.Add(it.sisID);//唯一性

                itemn.SubItems[0].Text = getsid(Point.pointsrc, it.id);

                itemn.Tag = it;
            }
            timer1.Enabled = true;

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
                    if (!onlysisid.Contains(((itemtag)(item.Tag)).sisID))
                    {
                        point Point = new point();
                        GLItem itemn;

                        itemn = glacialList1.Items.Add("");
                        Point.nd = itemn.SubItems[1].Text = fspl.tSCBNode.Text;
                        Point.pn = itemn.SubItems[2].Text = item.SubItems[1].Text;
                        Point.ed = itemn.SubItems[5].Text = item.SubItems[6].Text;
                        Point.eu = itemn.SubItems[4].Text = item.SubItems[4].Text;
                        itemn.Tag = item.Tag;
                        ((itemtag)(itemn.Tag)).isNew = true;
                        //((itemtag)(itemn.Tag)).ownerid = 1;//所有人
                        //((itemtag)(itemn.Tag)).PointSrc = pointsrc.sis;
                        Point.id_sis = ((itemtag)(itemn.Tag)).sisID;
                        onlysisid.Add(Point.id_sis);
                        Data.Get().Add(Point);
                    }
                    else 
                    {
                        MessageBox.Show(string.Format("点:{0}-{1}已存在！",item.SubItems[1].Text, 
                            item.SubItems[6].Text),"提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                     };
                }
            }
        }
        private void buttonSet_Click(object sender, EventArgs e)
        {
            int glc = glacialList1.SelectedItems.Count;
            if (glc > 0)
            {
                try
                {
                    if (glc == 1 || MessageBox.Show("是否更改所有选择的项目？", "提示", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        foreach (GLItem item in glacialList1.SelectedItems)
                        {
                            itemtag it = (itemtag)item.Tag;
                            point pt = Data.Get().cd_Point[it.id];
                            if (textBoxTV.Text.Length > 0) pt.tv = double.Parse(textBoxTV.Text); else pt.tv = null;
                            if (textBoxBV.Text.Length > 0) pt.tv = double.Parse(textBoxBV.Text); else pt.tv = null;
                            if (textBoxTV.Text.Length > 0) pt.tv = double.Parse(textBoxLL.Text); else pt.tv = null;
                            if (textBoxTV.Text.Length > 0) pt.tv = double.Parse(textBoxHL.Text); else pt.tv = null;
                            if (textBoxTV.Text.Length > 0) pt.tv = double.Parse(textBoxZL.Text); else pt.tv = null;
                            if (textBoxTV.Text.Length > 0) pt.tv = double.Parse(textBoxZH.Text); else pt.tv = null;
                        
                            item.SubItems[6].Text = textBoxTV.Text;
                            item.SubItems[7].Text = textBoxBV.Text;
                            item.SubItems[8].Text = textBoxLL.Text;
                            item.SubItems[9].Text = textBoxHL.Text;
                            item.SubItems[10].Text = textBoxZL.Text;
                            item.SubItems[11].Text = textBoxZH.Text;
                            pt.isalarm = checkBoxAlarm.Checked; 
                        }
                        toolStripButtonFind.Enabled = glacialList1.SelectedItems.Count == 0;
                    }                  
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }   
        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                Data.Get().SavetoPG();
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormPointSet_FormClosed(object sender, FormClosedEventArgs e)
        {
            glacialList1.Dispose();
            timer1.Enabled = false;
            sisconn.close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (glacialList1.Items.Count == 0) return;
            //if (!conn.isAlive()) return;
            Dictionary<string, GLItem> dic = new Dictionary<string, GLItem>();
            StringBuilder sbid = new StringBuilder();
            bool flag = false;
            foreach (GLItem item in glacialList1.Items)
            {
                itemtag it = (itemtag)(item.Tag);
                if (glacialList1.IsItemVisible(item) &&  it.PointSrc == pointsrc.sis)
                {
                    sbid.Append(it.sisID.ToString());
                    sbid.Append(",");
                    dic.Add(it.sisID.ToString(), item);//??????????????????????
                    flag = it.PointSrc == pointsrc.sis ? false : true;
                    continue;
                }
                if (flag) break;

            }
            if (sbid.Length > 0)
                sbid.Remove(sbid.Length - 1, 1);
            else return;
            string sql = string.Format("select ID,TM,DS,AV from Realtime where ID in ({0})", sbid.ToString());
            try
            {
                OPAPI.ResultSet resultSet =  sisconn.executeQuery(sql);//执行SQL
            
                const short gb1 = -32256;
                const short gb2 = -32768;
                while (resultSet.next())//next()执行一次，游标下移一行
                {
                    string colValue = resultSet.getInt(0).ToString();//获取第i列值
                    GLItem item = dic[colValue];
                    item.SubItems[3].Text = Math.Round(resultSet.getDouble(3), ((itemtag)item.Tag).FM).ToString();
                    short ds = resultSet.getShort(2);
                    if ((ds & gb1) == 0)
                    {
                        item.SubItems[12].Text = "Good";
                    }
                    else if ((ds & gb2) == gb2)
                    {
                        item.SubItems[12].Text = "Timeout";
                    }
                    else
                        item.SubItems[12].Text = "Bad";
                }
                if (resultSet != null)
                {
                    resultSet.close(); //释放内存
                }
            }
            catch (Exception ee)
            {
            }
        }
        private void glacialList1_Click(object sender, EventArgs e)
        {

            if (glacialList1.SelectedItems.Count == 1)
            {
                GLItem item = (GLItem)glacialList1.SelectedItems[0];
                textBoxTV.Text = item.SubItems[6].Text;
                textBoxBV.Text = item.SubItems[7].Text;
                textBoxLL.Text = item.SubItems[8].Text;
                textBoxHL.Text = item.SubItems[9].Text;
                textBoxZL.Text = item.SubItems[10].Text;
                textBoxZH.Text = item.SubItems[11].Text;
                itemtag it = (itemtag)(item.Tag);
                buttonCalc.Enabled = (it.PointSrc == pointsrc.calc) ? true : false;
            }
        }
        private void toolStripButtonAddNewCalc_Click(object sender, EventArgs e)
        {
            FormCalcPointSet fcps = new FormCalcPointSet();
            fcps.CalcPoint = new point();
            if (fcps.ShowDialog() == DialogResult.OK)
            {
                toolStripButtonFind.Enabled = false;

                GLItem itemn = glacialList1.Items.Add("");
                GLColumnCollection glcols = glacialList1.Columns;

                itemn.SubItems[glcols.GetColumnIndex("ND")].Text = fcps.CalcPoint.nd;
                itemn.SubItems[glcols.GetColumnIndex("ED")].Text = fcps.CalcPoint.ed;
                itemn.SubItems[glcols.GetColumnIndex("EU")].Text = fcps.CalcPoint.eu;

                itemtag it = new itemtag();
                it.isNew = true;
                itemn.Tag = it;
            }
        }

        private void buttonCalcSet_Click(object sender, EventArgs e)
        {
            if (glacialList1.SelectedItems.Count == 1)
            {
                FormCalcPointSet fcps = new FormCalcPointSet();
                GLItem itemn = (GLItem)glacialList1.SelectedItems[0];
                itemtag it = (itemtag)itemn.Tag;

                if (it.isNew) fcps.CalcPoint = it.Point;
                else fcps.CalcPoint = Data.Get().cd_Point[it.id];

                if (fcps.ShowDialog() == DialogResult.OK)
                {
                    toolStripButtonFind.Enabled = false;
                    itemn.SubItems[5].Text = fcps.CalcPoint.ed;
                    itemn.SubItems[4].Text = fcps.CalcPoint.eu;                  
                }
            }
        }

        private void toolStripButtonSelect_Click(object sender, EventArgs e)
        {

        }
    }
}

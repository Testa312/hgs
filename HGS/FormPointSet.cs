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
        OPAPI.Connect sisconn = new OPAPI.Connect(Pref.GetInst().sisHost, Pref.GetInst().sisPort, 60,
           Pref.GetInst().sisUser, Pref.GetInst().sisPassword);//建立连接
        Dictionary<GLItem, point> dic_glItemNew = new Dictionary<GLItem, point>();
        //Dictionary<GLItem, point> dic_glItemModified = new Dictionary<GLItem, point>();
        //HashSet<GLItem> hs_glItemNew = new HashSet<GLItem>();
        HashSet<GLItem> hs_glItemModified = new HashSet<GLItem>();
        HashSet<int> onlysisid = new HashSet<int>();
        public FormPointSet()
        {
            InitializeComponent();
            glacialLisint();
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
                it.sisid = Point.id_sis;
                it.fm = Point.fm;
                it.PointSrc = Point.pointsrc;

                //Point.poitsrc = it.PointSrc = (pointsrc)pgreader.GetInt32(pgreader.GetOrdinal("pointsrc"));
                if (Point.pointsrc == pointsrc.sis)
                {
                    onlysisid.Add(it.sisid);//唯一性
                    itemn.SubItems[13].Text = Point.isalarm ? Pref.GetInst().strOk : Pref.GetInst().strNo;
                }
                else
                {
                    itemn.SubItems[13].Text = Point.isalarm ? Pref.GetInst().strOk : Pref.GetInst().strNo;
                    itemn.SubItems[14].Text = Point.iscalc ? Pref.GetInst().strOk : Pref.GetInst().strNo;
                }

                itemn.SubItems[0].Text = Pref.GetInst().GetVarName(Point);

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
                    if (!onlysisid.Contains(((itemtag)(item.Tag)).sisid))
                    {
                        point Point = new point();
                        GLItem itemn;

                        itemn = glacialList1.Items.Add("");
                        Point.nd = itemn.SubItems[1].Text = fspl.tSCBNode.Text;
                        Point.pn = itemn.SubItems[2].Text = item.SubItems[1].Text;
                        Point.ed = itemn.SubItems[5].Text = item.SubItems[6].Text;
                        Point.eu = itemn.SubItems[4].Text = item.SubItems[4].Text;
                        Point.id_sis = ((itemtag)(item.Tag)).sisid;

                        onlysisid.Add(Point.id_sis);
                   
                        Point.id = Data.Get().GetNextPointID();

                        Point.pointsrc = pointsrc.sis;
                        Point.ownerid = Pref.GetInst().Owner;
                        itemn.SubItems[0].Text = Pref.GetInst().GetVarName(Point);
                        //
                        itemtag it = new itemtag();
                        itemn.Tag = it;
                        //it.isNew = true;
                        Point.fm = it.fm = ((itemtag)(item.Tag)).fm;
                        //it.Point = Point;
                        it.sisid = Point.id_sis;
                        //Data.Get().Add(Point);
                        dic_glItemNew.Add(item,Point);
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
                            point pt = dic_glItemNew.ContainsKey(item) ? dic_glItemNew[item] : Data.Get().cd_Point[it.id];
                            if (textBoxTV.Text.Length > 0) pt.tv = double.Parse(textBoxTV.Text); else pt.tv = null;
                            if (textBoxBV.Text.Length > 0) pt.bv = double.Parse(textBoxBV.Text); else pt.bv = null;
                            if (textBoxLL.Text.Length > 0) pt.ll = double.Parse(textBoxLL.Text); else pt.ll = null;
                            if (textBoxHL.Text.Length > 0) pt.hl = double.Parse(textBoxHL.Text); else pt.hl = null;
                            if (textBoxZL.Text.Length > 0) pt.zl = double.Parse(textBoxZL.Text); else pt.zl = null;
                            if (textBoxZH.Text.Length > 0) pt.zh = double.Parse(textBoxZH.Text); else pt.zh = null;
                        
                            item.SubItems[6].Text = textBoxTV.Text;
                            item.SubItems[7].Text = textBoxBV.Text;
                            item.SubItems[8].Text = textBoxLL.Text;
                            item.SubItems[9].Text = textBoxHL.Text;
                            item.SubItems[10].Text = textBoxZL.Text;
                            item.SubItems[11].Text = textBoxZH.Text;
                            pt.isalarm = checkBoxAlarm.Checked;
                            if (pt.pointsrc == pointsrc.sis)
                            {
                                item.SubItems[13].Text = pt.isalarm ? Pref.GetInst().strOk : Pref.GetInst().strNo;
                            }
                            else
                            {
                                item.SubItems[13].Text = pt.isalarm ? Pref.GetInst().strOk : Pref.GetInst().strNo;
                                item.SubItems[14].Text = pt.iscalc ? Pref.GetInst().strOk : Pref.GetInst().strNo;
                            }
                            //glItemModified.Add(item);
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
            }
        }   
        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                foreach(GLItem item in hs_glItemModified)
                {
                    Data.Get().Update(Data.Get().cd_Point[((itemtag)(item.Tag)).id]);
                }
                foreach(point pt in dic_glItemNew.Values)
                {
                    Data.Get().Add(pt);
                }
                Data.Get().SavetoPG();
                hs_glItemModified.Clear();
                dic_glItemNew.Clear();
                toolStripButtonFind.Enabled = true;
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
            foreach (GLItem item in glacialList1.Items)
            {
                if (glacialList1.IsItemVisible(item))
                {
                    itemtag it = (itemtag)(item.Tag);
                    point pt = Data.Get().cd_Point[it.id];
                    item.SubItems[3].Text = pt.av.ToString();
                    item.SubItems[12].Text = pt.ps.ToString();
                }
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
                checkBoxAlarm.Checked = dic_glItemNew.ContainsKey(item) ?  dic_glItemNew[item].isalarm : Data.Get().cd_Point[it.id].isalarm;
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
                itemn.SubItems[glcols.GetColumnIndex("ID")].Text = Pref.GetInst().GetVarName(fcps.CalcPoint);

                itemtag it = new itemtag();
                //it.isNew = true;
                it.id = fcps.CalcPoint.id;
                it.fm = fcps.CalcPoint.fm;
                it.PointSrc = fcps.CalcPoint.pointsrc ;
                //it.Point = fcps.CalcPoint;
                itemn.Tag = it;
                if (fcps.CalcPoint.pointsrc == pointsrc.sis)
                {
                    itemn.SubItems[13].Text = fcps.CalcPoint.isalarm ? Pref.GetInst().strOk : Pref.GetInst().strNo;
                }
                else
                {
                    itemn.SubItems[13].Text = fcps.CalcPoint.isalarm ? Pref.GetInst().strOk : Pref.GetInst().strNo;
                    itemn.SubItems[14].Text = fcps.CalcPoint.iscalc ? Pref.GetInst().strOk : Pref.GetInst().strNo;
                }
                //Data.Get().Add(it.Point);
                dic_glItemNew.Add(itemn, fcps.CalcPoint);
            }
        }

        private void buttonCalcSet_Click(object sender, EventArgs e)
        {
            if (glacialList1.SelectedItems.Count == 1)
            {
                FormCalcPointSet fcps = new FormCalcPointSet();
                GLItem itemn = (GLItem)glacialList1.SelectedItems[0];
                itemtag it = (itemtag)itemn.Tag;

                fcps.CalcPoint = dic_glItemNew.ContainsKey(itemn) ? dic_glItemNew[itemn] : Data.Get().cd_Point[it.id];
                fcps.glacialLisint();
                if (fcps.ShowDialog() == DialogResult.OK)
                {
                    toolStripButtonFind.Enabled = false;
                    itemn.SubItems[5].Text = fcps.CalcPoint.ed;
                    itemn.SubItems[4].Text = fcps.CalcPoint.eu;
                    if (fcps.CalcPoint.pointsrc == pointsrc.sis)
                    {
                        itemn.SubItems[13].Text = fcps.CalcPoint.isalarm ? Pref.GetInst().strOk : Pref.GetInst().strNo;
                    }
                    else
                    {
                        itemn.SubItems[13].Text = fcps.CalcPoint.isalarm ? Pref.GetInst().strOk : Pref.GetInst().strNo;
                        itemn.SubItems[14].Text = fcps.CalcPoint.iscalc ? Pref.GetInst().strOk : Pref.GetInst().strNo;
                    }
                }
                //itemn.Tag = it;
                if(!dic_glItemNew.ContainsKey(itemn))
                    hs_glItemModified.Add(itemn);
                //glItemModified.Add(itemn);
            }
        }
        private void GetSisValue()
        {
            StringBuilder sbid = new StringBuilder();
            foreach (point pt in Data.Get().lsSisPoint )
            {
                sbid.Append(pt.id_sis);
                sbid.Append(",");
            }
            if (sbid.Length > 0)
                sbid.Remove(sbid.Length - 1, 1);
            else return;
            //
            string sql = string.Format("select ID,TM,DS,AV from Realtime where ID in ({0})", sbid.ToString());
            try
            {
                OPAPI.ResultSet resultSet = sisconn.executeQuery(sql);//执行SQL

                const short gb1 = -32256;
                const short gb2 = -32768;
                while (resultSet.next())//next()执行一次，游标下移一行
                {
                    point Point = Data.Get().cd_Point[Data.Get().dic_SisIdtoPointId[resultSet.getInt(0)]];
                    Point.av = Math.Round(resultSet.getDouble(3),Point.fm);
                    Data.Get().Variables[Pref.GetInst().GetVarName(Point)] = Point.av; 
                    short ds = resultSet.getShort(2);
                    if ((ds & gb1) == 0)
                    {
                        Point.ps = PointState.Good;
                    }
                    else if ((ds & gb2) == gb2)
                    {
                        Point.ps = PointState.Timeout;
                    }
                    else
                        Point.ps = PointState.Bad;
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
        private void toolStripButtonSelect_Click(object sender, EventArgs e)
        {

        }

        private void timerCalc_Tick(object sender, EventArgs e)
        {
            GetSisValue();
            foreach (point calcpt in Data.Get().hsCalcPoint)
            {
                if (calcpt.calciserror) continue;
                //point Point = Data.Get().cd_Point[calcid];
                foreach (point pt in calcpt.listSisCalaExpPointID)
                {
                    if (pt.ps != PointState.Good)
                    {
                        calcpt.ps = PointState.Error;
                        break;
                    }
                    calcpt.ps = PointState.Good;
                }
                try
                {
                    calcpt.av = (double)calcpt.expression.Evaluate();
                }
                catch (Exception ee)
                {
                    calcpt.ps = PointState.Error;
                    calcpt.calciserror = true;
                }
            }
        }
    }
}

﻿using System;
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
        OPAPI.Connect sisconn = new OPAPI.Connect(Pref.Inst().sisHost, Pref.Inst().sisPort, 60,
           Pref.Inst().sisUser, Pref.Inst().sisPassword);//建立连接

        Dictionary<GLItem, point> dic_glItemNew = new Dictionary<GLItem, point>();
        HashSet<GLItem> hs_glItemModified = new HashSet<GLItem>();
        HashSet<int> onlysisid = new HashSet<int>();
        int PointNums = 0;
        public FormPointSet()
        {
            InitializeComponent();
            glacialLisint();
            label_formula.Text = "";
        }
        private void DisplayStats()
        {
            tSSLabel_count.Text = string.Format("点数共：{0}个，其中新加点{1}个，已修改点{2}个。",
               PointNums, dic_glItemNew.Count, hs_glItemModified.Count);
        }
        private void AlarmSubItemSet(GLItem item,point pt)
        {
            if (pt.pointsrc == pointsrc.sis)
            {
                item.SubItems["IsAlarm"].Text = pt.isavalarm || pt.isboolv ? Pref.Inst().strOk : Pref.Inst().strNo;
            }
            else
            {
                item.SubItems["IsAlarm"].Text = pt.isavalarm || pt.isboolv ? Pref.Inst().strOk : Pref.Inst().strNo;
                item.SubItems["IsCalc"].Text = pt.iscalc ? Pref.Inst().strOk : Pref.Inst().strNo;
            }
        }
        private void glacialLisint()
        {
            timerUpdateValue.Enabled = false;
            glacialList1.Items.Clear();

            foreach (point ptx in Data.inst().lsAllPoint)
            {
                GLItem itemn;
                itemtag it = new itemtag();
                //point Point = Data.Get().cd_Point[];
                if ((ptx.pointsrc == pointsrc.sis || (Auth.GetInst().LoginID == 0 || ptx.ownerid == Auth.GetInst().LoginID)) &&
                    ptx.nd.Contains(tSCB_ND.Text.Trim()) && ptx.ed.Contains(tSTB_ED.Text.Trim()) &&
                    ptx.pn.Contains(tSTB_PN.Text.Trim()) && ptx.orgformula.Contains(tSTB_F.Text.Trim()))
                {
                    itemn = glacialList1.Items.Add("");
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
                    it.sisid = ptx.id_sis;
                    it.fm = ptx.fm;
                    it.PointSrc = ptx.pointsrc;
                    if (ptx.pointsrc == pointsrc.sis)
                    {
                        onlysisid.Add(it.sisid);//唯一性
                    }
                    AlarmSubItemSet(itemn,ptx);
                    itemn.Tag = it;
                }
                PointNums++;
            }
            timerUpdateValue.Enabled = true;
            DisplayStats();
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
                PointNums++;
                DisplayStats();
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
                            itemtag it = (itemtag)item.Tag;
                            point pt = dic_glItemNew.ContainsKey(item) ? dic_glItemNew[item] : Data.inst().cd_Point[it.id];
                            if (textBoxTV.Text.Length > 0) pt.tv = double.Parse(textBoxTV.Text); else pt.tv = null;
                            if (textBoxBV.Text.Length > 0) pt.bv = double.Parse(textBoxBV.Text); else pt.bv = null;
                            if (textBoxLL.Text.Length > 0) pt.ll = double.Parse(textBoxLL.Text); else pt.ll = null;
                            if (textBoxHL.Text.Length > 0) pt.hl = double.Parse(textBoxHL.Text); else pt.hl = null;
                            if (textBoxZL.Text.Length > 0) pt.zl = double.Parse(textBoxZL.Text); else pt.zl = null;
                            if (textBoxZH.Text.Length > 0) pt.zh = double.Parse(textBoxZH.Text); else pt.zh = null;
                        
                            item.SubItems["TV"].Text = textBoxTV.Text;
                            item.SubItems["BV"].Text = textBoxBV.Text;
                            item.SubItems["LL"].Text = textBoxLL.Text;
                            item.SubItems["HL"].Text = textBoxHL.Text;
                            item.SubItems["ZL"].Text = textBoxZL.Text;
                            item.SubItems["ZH"].Text = textBoxZH.Text;
                            pt.isavalarm = checkBoxAlarm.Checked;
                            pt.isboolv = checkBoxbool.Checked;
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
                DisplayStats();
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
            }
            Data.inst().SavetoPG();
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
            DisplayStats();
        }

        private void FormPointSet_FormClosed(object sender, FormClosedEventArgs e)
        {
            glacialList1.Dispose();
            timerUpdateValue.Enabled = false;
            sisconn.close();
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
                    item.SubItems["AV"].Text = pt.av.ToString();
                    item.SubItems["DS"].Text = pt.ps.ToString();
                }
            }
            DisplayStats();
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
                checkBoxbool.Checked = Point.isboolv;
                tB_boolAlarmInfo.Text = Point.boolalarminfo;
                buttonCalc.Enabled = (it.PointSrc == pointsrc.calc) ? true : false;

                label_formula.Text = Point.orgformula;
            }
            tabControl.Enabled = glacialList1.SelectedItems.Count > 0;
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

                itemn.SubItems["ND"].Text = fcps.CalcPoint.nd;
                itemn.SubItems["ED"].Text = fcps.CalcPoint.ed;
                itemn.SubItems["EU"].Text = fcps.CalcPoint.eu;

                itemtag it = new itemtag();
                it.id = fcps.CalcPoint.id;
                it.fm = fcps.CalcPoint.fm;
                it.PointSrc = fcps.CalcPoint.pointsrc ;
                itemn.Tag = it;
                AlarmSubItemSet(itemn, fcps.CalcPoint);
                dic_glItemNew.Add(itemn, fcps.CalcPoint);
                PointNums++;
                DisplayStats();
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
                if (fcps.ShowDialog() == DialogResult.OK)
                {
                    toolStripButtonFind.Enabled = false;
                    itemn.SubItems["ED"].Text = fcps.CalcPoint.ed;
                    itemn.SubItems["EU"].Text = fcps.CalcPoint.eu;
                    AlarmSubItemSet(itemn, fcps.CalcPoint);
                }
                //itemn.Tag = it;
                if(!dic_glItemNew.ContainsKey(itemn))
                    hs_glItemModified.Add(itemn);
                DisplayStats();
            }
        }
        private void GetSisValue()
        {
            StringBuilder sbid = new StringBuilder();
            foreach (point pt in Data.inst().lsSisPoint )
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
                    point Point = Data.inst().cd_Point[Data.inst().dic_SisIdtoPointId[resultSet.getInt(0)]];
                    Point.av = Math.Round(resultSet.getDouble(3),Point.fm);
                    Data.inst().Variables[Pref.Inst().GetVarName(Point)] = Point.av; 
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
            catch (Exception)
            {
            }

        }
        private void toolStripButtonSelect_Click(object sender, EventArgs e)
        {
            glacialLisint();
        }

        private void timerCalc_Tick(object sender, EventArgs e)
        {
            GetSisValue();//到得sis值；
            foreach (point calcpt in Data.inst().lsAllPoint)
            {
                bool lastAlam = calcpt.alarming;
                //计算计算点。
                if (calcpt.pointsrc == pointsrc.calc)
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
                        calcpt.av = Math.Round(calcpt.expformula.Length > 0 ? (double)calcpt.expression.Evaluate() : -1, calcpt.fm);

                    }
                    catch (Exception)
                    {
                        calcpt.ps = PointState.Error;
                        calcpt.calciserror = true;//需进一步处理?????????????????
                    }
                }
                //加报警
                if (calcpt.AlarmCalc())
                    AlarmSet.GetInst().ssAlarmPoint.Add(calcpt);
                else if (!calcpt.AlarmCalc() && lastAlam)
                    AlarmSet.GetInst().ssAlarmPoint.Remove(calcpt);
            }
            tSSLabel_count.Text = string.Format("点数共：{0}个，其中新加点{1}个，已修改点{2}个。", 
                PointNums,dic_glItemNew.Count, hs_glItemModified.Count);
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
    }
}

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
using System.Diagnostics;
using System.Collections;
namespace HGS
{
    public partial class FormPointSet : Form
    {
        Dictionary<GLItem, point> dic_glItemNew = new Dictionary<GLItem, point>();
        Dictionary<point, GLItem> dic_glItemModified = new Dictionary<point, GLItem>();
        //Dictionary<int, GLItem> NewAddsisid = new Dictionary<int,GLItem>();
        HashSet<string> hs_ND = new HashSet<string>();
        //int PointNums = 0;
        bool isFirst = true;
 
        public FormPointSet()
        {
            InitializeComponent();
            FillMyTreeView();
        }
        private void DisplayHints(int PointNums)
        {
            tSSLabel_count.Text = string.Format("点数：{0}。",PointNums);
        }
        private void AlarmSubItemSymbol(GLItem item,point pt)
        {
            if (pt.pointsrc == pointsrc.sis)
            {
                item.SubItems["IsAlarm"].Text = pt.alarmifav && (pt.isavalarm || pt.isboolvalarm) ? Pref.Inst().strOk : Pref.Inst().strNo;
                if (Data.inst().hs_FormulaErrorPoint.Contains(pt)) item.SubItems["FError"].Text = Pref.Inst().strNo; 
            }
            else
            {
                item.SubItems["IsAlarm"].Text = pt.alarmifav && (pt.isavalarm || pt.isboolvalarm) ? Pref.Inst().strOk : Pref.Inst().strNo;
                item.SubItems["IsCalc"].Text = pt.iscalc ? Pref.Inst().strOk : Pref.Inst().strNo;
            }
        }
        private void glacialLisint()
        {
            timerUpdateValue.Enabled = false;
            glacialList1.Items.Clear();
            //PointNums = 0;
            this.Cursor = Cursors.WaitCursor;
            //NewAddsisid.Clear();
            List<GLItem> lsItem = new List<GLItem>();
            foreach (point ptx in Data.inst().hsAllPoint)
            {
                if (ptx.pointsrc == pointsrc.sis)
                {
                    //NewAddsisid.Add(ptx.id_sis,null);//唯一性
                }
                if ((ptx.pointsrc == pointsrc.sis || (Auth.GetInst().LoginID == 0 || ptx.ownerid == Auth.GetInst().LoginID || 
                    ptx.ownerid == 0)) &&
                    ptx.nd.Contains(tSCB_ND.Text.Trim()) && ptx.ed.Contains(tSTB_ED.Text.Trim()) &&
                    ptx.pn.Contains(tSTB_PN.Text.Trim()) && ptx.orgformula_main.Contains(tSTB_F.Text.Trim()))
                {
                    GLItem item = new GLItem(glacialList1);
                    gllistInitItemText(ptx,item);
                    //NewAddsisid[ptx.id_sis] = item;
                    lsItem.Add(item);
                }
                if (isFirst) hs_ND.Add(ptx.nd);              
            }
            glacialList1.Items.AddRange(lsItem.ToArray());
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
            tabControl.Enabled = false;
            DisplayHints(lsItem.Count);
            glacialList1.Invalidate();
        }
        private void gllistInitItemText(point ptx , GLItem itemn)
        {
            //if (ptx.id != ((itemtag)(itemn).Tag).id) throw new Exception("更换点和项目不同！");

            itemtag it = new itemtag();
            itemn.Tag = it;
            it.id = ptx.id;
            itemn.SubItems["ID"].Text = ptx.id.ToString();
            itemn.SubItems["ND"].Text = ptx.nd;
            itemn.SubItems["PN"].Text = ptx.pn;

            itemn.SubItems["EU"].Text = ptx.eu;
            itemn.SubItems["ED"].Text = ptx.ed;
            itemn.SubItems["TV"].Text = ptx.tv.ToString();
            itemn.SubItems["BV"].Text = ptx.bv.ToString();
            itemn.SubItems["LL"].Text = Functions.NullDoubleRount(ptx.ll, ptx.fm).ToString();

            itemn.SubItems["HL"].Text = Functions.NullDoubleRount(ptx.hl, ptx.fm).ToString();

            itemn.SubItems["ZL"].Text = ptx.zl.ToString();
            itemn.SubItems["ZH"].Text = ptx.zh.ToString();
            itemn.SubItems["AlarmInfo"].Text = ptx.alarmininfo;
            itemn.SubItems["DS"].Text = ptx.ps.ToString();

            it.sisid = ptx.id_sis;
            it.fm = ptx.fm;
            it.PointSrc = ptx.pointsrc;

            AlarmSubItemSymbol(itemn, ptx);
            //PointNums++;
            //DisplayHints();
        }
        private void gllistUpateItemText(GLItem item,point pt)
        {
            if (pt.id != ((itemtag)(item).Tag).id) throw new Exception("更换点和项目不同！");
            item.SubItems["TV"].Text = pt.tv.ToString();
            item.SubItems["BV"].Text = pt.bv.ToString();
            item.SubItems["LL"].Text = pt.ll.ToString();
            item.SubItems["HL"].Text = pt.hl.ToString();
            item.SubItems["ZL"].Text = pt.zl.ToString();
            item.SubItems["ZH"].Text = pt.zh.ToString();
            //item.SubItems["ND"].Text = pt.nd;
            item.SubItems["ED"].Text = pt.ed;
            item.SubItems["EU"].Text = pt.eu;
            item.SubItems["PN"].Text = pt.pn;
            if (!dic_glItemNew.ContainsKey(item))
            {
                dic_glItemModified[pt] = item;
                Data.inst().hs_FormulaErrorPoint.Remove(pt);
            }

            //DisplayHints();
            AlarmSubItemSymbol(item, pt);
        }
        private void toolStripButtonAddSis_Click(object sender, EventArgs e)//加sis点
        {     
            FormSisPointList fspl = new FormSisPointList();
            if (fspl.ShowDialog() == DialogResult.OK)
            {
                //toolStripButtonFind.Enabled = fspl.lsitem.Count == 0;
                List<GLItem> lsItem = new List<GLItem>();
                foreach (point pt in fspl.lsitem)
                {
                    if (!Data.inst().dic_SisIdtoPoint.ContainsKey(pt.id_sis))
                    {
                       
                        GLItem itemn = new GLItem(glacialList1);
                        gllistInitItemText(pt,itemn);
                        lsItem.Add(itemn);
                        //
                        //NewAddsisid.Add(pt.id_sis,itemn);
                        dic_glItemNew.Add(itemn, pt);
                    }
                    else 
                    {
                        MessageBox.Show(string.Format("点:{0}-{1}已存在！",pt.nd, 
                            pt.pn),"提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                     }
                }
                glacialList1.Items.AddRange(lsItem.ToArray());
                glacialList1.ScrolltoBottom();
                //DisplayHints();
                //
                Save();
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

                            if (textBoxLL.Text.Length > 0 && 
                                pt.orgformula_ll.Length == 0) { pt.ll = double.Parse(textBoxLL.Text); lsav.Add(Convert.ToDouble(pt.ll)); }
                            else pt.ll = null;

                            if (textBoxBV.Text.Length > 0) { pt.bv = double.Parse(textBoxBV.Text); /*lsav.Add(Convert.ToDouble(pt.bv));*/ }
                            else pt.bv = null;

                            if (textBoxTV.Text.Length > 0)
                            { pt.tv = double.Parse(textBoxTV.Text);/*lsav.Add(Convert.ToDouble(pt.tv));*/}
                            else pt.tv = null;                         

                            if (textBoxHL.Text.Length > 0 &&
                                pt.orgformula_hl.Length == 0){pt.hl = double.Parse(textBoxHL.Text); lsav.Add(Convert.ToDouble(pt.hl)); }
                            else pt.hl = null;

                            if (textBoxZH.Text.Length > 0)
                            { pt.zh = double.Parse(textBoxZH.Text); lsav.Add(Convert.ToDouble(pt.zh)); }
                            else pt.zh = null;
                            for(int i = 1; i< lsav.Count;i++)
                            {
                                if (lsav[i - 1] > lsav[i]) { throw new Exception("报警数值大小应按报警高低限值排序！"); }
                            }
                            if (pt.bv != null && pt.tv != null)
                            {
                                if (pt.bv > pt.tv) { throw new Exception("量程上限应大于量程下限！"); }
                            }
                            gllistUpateItemText(item, pt);
                           
                            pt.isavalarm = checkBoxAlarm.Checked;
                            if (!pt.isavalarm)
                            {
                                item.SubItems["AlarmInfo"].Text= "";
                                point ppt = Data.inst().cd_Point[it.id];
                                pt.alarmininfo = "";
                                AlarmSet.GetInst().Remove(ppt);
                            }
                            pt.isboolvalarm = checkBoxbool.Checked;
                            pt.boolalarminfo = tB_boolAlarmInfo.Text;
                            pt.boolalarmif = radioButton_true.Checked;

                            if (textBox_pp.Text.Length > 0)
                            { pt.skip_pp = double.Parse(textBox_pp.Text);}
                            else pt.skip_pp = null;
                            pt.isalarmskip = checkBox_isSkip.Checked;
                            pt.isalarmwave = checkBox_isWave.Checked;

                            if ((pt.isalarmwave || pt.isalarmskip) && pt.skip_pp != null)
                            {
                                if (pt.waveDetection == null)
                                {
                                    pt.waveDetection = new WaveDetection();
                                }                               
                            } 
                            else if(pt.waveDetection != null)
                            {                               
                                pt.waveDetection.Clear();
                                pt.waveDetection = null;
                            }
                            if ((pt.isalarmwave || pt.isalarmskip) && pt.skip_pp == null && pt.skip_pp > 0)
                                MessageBox.Show("阈值不应为空且大于0！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        Save();
                        //toolStripButtonFind.Enabled = glc == 0;
                    }                  
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }   
        private void Save()
        {
            try
            {
                foreach (point pt in dic_glItemModified.Keys)
                {
                    Data.inst().Update(pt);
                }
                int ptid = Data.inst().GetNextPointId();
                HashSet<int> hs_ptid = new HashSet<int>();
                foreach (point pt in dic_glItemNew.Values)
                {
                    pt.id = ptid;
                    Data.inst().Add(pt);
                    hs_ptid.Add(ptid);
                    if (!hs_ND.Contains(pt.nd))
                    {
                        tSCB_ND.Items.Add(pt.nd);
                    }
                    ptid++;
                }
                Data.inst().SavetoPG();
                //PointNums += dic_glItemNew.Count;
                dic_glItemModified.Clear();
                dic_glItemNew.Clear();
                //
                TreeNode tn = treeView.SelectedNode;
                if (tn != null && tn.Text != "全部")
                {
                    DeviceInfo tt = (DeviceInfo)tn.Tag;
                    tt.UnionWith(hs_ptid);
                    DataDeviceTree.UpdateNodetoDB(tn);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //timerUpdateValue.Enabled = true;
            }
            if (treeView.SelectedNode != null)
            {
                TreeNodeMouseClickEventArgs ee = new TreeNodeMouseClickEventArgs(treeView.SelectedNode, MouseButtons.Left, 0, 0, 0);
                treeView_NodeMouseClick(null, ee);
            }
        }
        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                //timerUpdateValue.Enabled = false;
                Save();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //timerUpdateValue.Enabled = true;
            }
            glacialLisint();
            DisplayHints();
            */
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
                    item.SubItems["AV"].Text = Functions.NullDoubleRount(pt.Av, pt.fm).ToString();                   
                    item.SubItems["DS"].Text = pt.ps.ToString();
                    item.SubItems["AlarmInfo"].Text = pt.alarmininfo;
                    //
                    if (pt.orgformula_hl.Length > 0)
                    {
                        item.SubItems["HL"].Text = Functions.NullDoubleRount(pt.hl, pt.fm).ToString();
                    }
                    if (pt.orgformula_ll.Length > 0)
                    {
                        item.SubItems["LL"].Text = Functions.NullDoubleRount(pt.ll, pt.fm).ToString();
                    }
                }
            }
            //DisplayHints();
        }
        private void glacialList1_Click(object sender, EventArgs e)
        {
            if (glacialList1.SelectedItems.Count == 1)
            {
                GLItem item = (GLItem)glacialList1.SelectedItems[0];
                itemtag it = (itemtag)(item.Tag);

                point Point = dic_glItemNew.ContainsKey(item) ? dic_glItemNew[item] : Data.inst().cd_Point[it.id];
                textBoxTV.Text = Point.tv.ToString();
                textBoxBV.Text = Point.bv.ToString();
                textBoxLL.Text = Point.ll.ToString();
                textBoxHL.Text = Point.hl.ToString();
                textBoxZL.Text = Point.zl.ToString();
                textBoxZH.Text = Point.zh.ToString();
                checkBoxAlarm.Checked = Point.isavalarm;
                checkBoxbool.Checked = Point.isboolvalarm;
                tB_boolAlarmInfo.Text = Point.boolalarminfo;
                if (Point.boolalarminfo.Length <= 0)
                    tB_boolAlarmInfo.Text = Point.ed;
                buttonCalc.Enabled = (it.PointSrc == pointsrc.calc) ? true : false;

                label_formula.Text = Point.orgformula_main;

                radioButton_true.Checked = Point.boolalarmif;
                radioButton_false.Checked = !Point.boolalarmif;

                checkBox_isSkip.Checked = Point.isalarmskip;
                checkBox_isWave.Checked = Point.isalarmwave;
                textBox_pp.Text = Point.skip_pp.ToString();
                //
                button_HL.ForeColor = Color.Black;
                button_LL.ForeColor = Color.Black;
                if (Point.orgformula_hl.Length > 0)
                {
                    button_HL.ForeColor = Color.Red;
                    //button_HL.Text = Functions.NullDoubleRount(Point.hl, Point.fm).ToString();
                }
                if (Point.orgformula_ll.Length > 0)
                {
                    button_LL.ForeColor = Color.Red;
                    //button_LL.Text = Functions.NullDoubleRount(Point.ll, Point.fm).ToString();
                }
                textBoxHL.Enabled = Point.orgformula_hl.Length == 0;
                textBoxLL.Enabled = Point.orgformula_ll.Length == 0;

                buttonAlarmIf.ForeColor = Color.Black;
                if (Point.alarmif.Length > 0)
                {
                    buttonAlarmIf.ForeColor = Color.Red;
                    //button_HL.Text = Functions.NullDoubleRount(Point.hl, Point.fm).ToString();
                }

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
                //toolStripButtonFind.Enabled = false;
                GLItem item = new GLItem(glacialList1);
                gllistInitItemText(fcps.CalcPoint, item);
                glacialList1.Items.Add(item);
                dic_glItemNew.Add(item, fcps.CalcPoint);

                glacialList1.ScrolltoBottom();
                //
                Save();
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
                    //toolStripButtonFind.Enabled = false;

                    gllistUpateItemText(itemn, fcps.CalcPoint);
                    Save();
                }                
            }
        }
       
        private void toolStripButtonSelect_Click(object sender, EventArgs e)
        {
            glacialLisint();
        }
        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            foreach(GLItem itemn in  glacialList1.SelectedItems)//.Count == 1)
            {
                //GLItem itemn = (GLItem)glacialList1.SelectedItems[0];
                itemtag it = (itemtag)itemn.Tag;

                List<int> lspid = VartoPointTable.GetDeletePointIdList(it.id);
                /*
                if (dic_glItemNew.ContainsKey(itemn))
                {
                    if (DialogResult.OK == MessageBox.Show(string.Format("是否删除点[{0}]-{1}？",
                        itemn.SubItems["PN"].Text,itemn.SubItems["ED"].Text), "提示",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                    {
                        dic_glItemNew.Remove(itemn);
                        glacialList1.Items.Remove(itemn);
                    }
                    continue;
                }
                */
                if (lspid.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("被下列点引用，不能删除！");
                    foreach (int pid in lspid)
                    {
                        point pt = Data.inst().cd_Point[pid];
                        sb.AppendLine(string.Format("[id:{0}]-{1}", pt.id, pt.ed));
                    }
                    MessageBox.Show(sb.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
                if (DialogResult.OK == MessageBox.Show(string.Format("是否删除点[{0}]-{1}？",
                    itemn.SubItems["PN"].Text,itemn.SubItems["ED"].Text), "提示",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    //toolStripButtonFind.Enabled = false;
                    Data.inst().Delete(Data.inst().cd_Point[it.id]);
                    glacialList1.Items.Remove(itemn);
                    //PointNums--;
                }
            }
            Save();
        }
        private void FormPointSet_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            if (!toolStripButtonFind.Enabled &&
                DialogResult.Yes == MessageBox.Show("已修改，是否保存？", "提示",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question))
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
            */
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
            /*
            foreach(point pt in Data.inst().hsSisPoint)
            {
                onlysisid.Add(pt.id_sis);
            }
            */
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

        private void button_HL_Click(object sender, EventArgs e)
        {
            if (glacialList1.SelectedItems.Count == 1)
            {
                FormCalcAlarmHlSet fcps = new FormCalcAlarmHlSet();
                GLItem itemn = (GLItem)glacialList1.SelectedItems[0];
                itemtag it = (itemtag)itemn.Tag;

                fcps.CalcPoint = dic_glItemNew.ContainsKey(itemn) ? dic_glItemNew[itemn] : Data.inst().cd_Point[it.id];
                fcps.glacialLisint();
                fcps.Text = string.Format("点[{0}]高报警值计算", fcps.CalcPoint.ed);
                //fcps.CellId = cellid.main;
                if (fcps.ShowDialog() == DialogResult.OK)
                {
                    if (!dic_glItemNew.ContainsKey(itemn))
                    {
                        dic_glItemModified[fcps.CalcPoint] = itemn;
                        Data.inst().hs_FormulaErrorPoint.Remove(fcps.CalcPoint);
                    }
                    button_HL.ForeColor = Color.Black;
                    if (fcps.CalcPoint.orgformula_hl.Length > 0)
                    {
                        button_HL.ForeColor = Color.Red;
                        //button_HL.Text = Functions.NullDoubleRount(Point.hl, Point.fm).ToString();
                    }
                    textBoxHL.Enabled = fcps.CalcPoint.orgformula_hl.Length == 0;
                    Save();
                }
                //itemn.Tag = it;
                
            }
        }

        private void button_LL_Click(object sender, EventArgs e)
        {
            if (glacialList1.SelectedItems.Count == 1)
            {
                FormCalcAlarmLLSet fcps = new FormCalcAlarmLLSet();
                GLItem itemn = (GLItem)glacialList1.SelectedItems[0];
                itemtag it = (itemtag)itemn.Tag;

                fcps.CalcPoint = dic_glItemNew.ContainsKey(itemn) ? dic_glItemNew[itemn] : Data.inst().cd_Point[it.id];
                fcps.glacialLisint();
                fcps.Text = string.Format("点[{0}]低报警值计算", fcps.CalcPoint.ed);
                //fcps.CellId = cellid.main;
                if (fcps.ShowDialog() == DialogResult.OK)
                {
                    if (!dic_glItemNew.ContainsKey(itemn))
                    {
                        dic_glItemModified[fcps.CalcPoint] = itemn;
                        Data.inst().hs_FormulaErrorPoint.Remove(fcps.CalcPoint);
                    }
                    button_LL.ForeColor = Color.Black;
                    if (fcps.CalcPoint.orgformula_ll.Length > 0)
                    {
                        button_LL.ForeColor = Color.Red;
                        //button_HL.Text = Functions.NullDoubleRount(Point.hl, Point.fm).ToString();
                    }
                    textBoxLL.Enabled = fcps.CalcPoint.orgformula_ll.Length == 0;
                    Save();
                }
                //itemn.Tag = it;

            }
        }
        private void tSB_Cancel_Click(object sender, EventArgs e)
        {
            /*
            dic_glItemModified.Clear();
            dic_glItemNew.Clear();
            //NewAddsisid.Clear();
            Data.inst().DeleteClear();
            glacialLisint();
            toolStripButtonFind.Enabled = true;
            */
        }

        private void tSB_ImportFromFile_Click(object sender, EventArgs e)
        {
            FormImportFromFile fiff = new FormImportFromFile();
            if (DialogResult.OK == fiff.ShowDialog())
            {
                //toolStripButtonFind.Enabled = fiff.lspt.Count == 0;
                List<GLItem> lsItem = new List<GLItem>();
                try
                {
                    foreach (point pt in fiff.lspt)
                    {
                        if (Data.inst().dic_SisIdtoPoint.ContainsKey(pt.id_sis))
                        {
                            if (DialogResult.Yes == MessageBox.Show(string.Format("点[{0}]-{1}已存在，是否修改报警限值？",
                                pt.pn, pt.ed), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            {
                                point ptx = Data.inst().dic_SisIdtoPoint[pt.id_sis];
                                ptx.hl = pt.hl;
                                ptx.ll = pt.ll;
                                dic_glItemModified.Add(ptx, null);//????????????
                            }
                        }
                        else
                        {
                            GLItem itemn = new GLItem(glacialList1);
                            gllistInitItemText(pt, itemn);
                            lsItem.Add(itemn);
                            dic_glItemNew.Add(itemn, pt);
                        }
                    }
                    glacialList1.Items.AddRange(lsItem.ToArray());
                    glacialList1.ScrolltoBottom();
                    Save();
                }
                catch(Exception ee )
                {
                    MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonAlarmIf_Click(object sender, EventArgs e)
        {
            if (glacialList1.SelectedItems.Count == 1)
            {
                FormCalcAlarmIf fcaf = new FormCalcAlarmIf();
                GLItem itemn = (GLItem)glacialList1.SelectedItems[0];
                itemtag it = (itemtag)itemn.Tag;

                fcaf.CalcPoint = dic_glItemNew.ContainsKey(itemn) ? dic_glItemNew[itemn] : Data.inst().cd_Point[it.id];
                fcaf.glacialLisint();
                fcaf.Text = string.Format("点[{0}]报警条件设置", fcaf.CalcPoint.ed);
                //fcps.CellId = cellid.main;
                if (fcaf.ShowDialog() == DialogResult.OK)
                {
                    if (!dic_glItemNew.ContainsKey(itemn))
                    {
                        dic_glItemModified[fcaf.CalcPoint] = itemn;
                        Data.inst().hs_FormulaErrorPoint.Remove(fcaf.CalcPoint);
                    }
                    buttonAlarmIf.ForeColor = Color.Black;
                    if (fcaf.CalcPoint.alarmif.Length > 0)
                    {
                        buttonAlarmIf.ForeColor = Color.Red;

                        itemn.SubItems["AlarmInfo"].Text = "";
                        point pt = Data.inst().cd_Point[it.id];
                        pt.alarmininfo = "";
                        AlarmSet.GetInst().Remove(pt);
                        //button_HL.Text = Functions.NullDoubleRount(Point.hl, Point.fm).ToString();
                    }
                    //buttonAlarmIf.Enabled = fcaf.CalcPoint.alarmif.Length == 0;
                    Save();
                }
                //itemn.Tag = it;

            }
        }
       
        private void FillMyTreeView()
        {
            // Display a wait cursor while the TreeNodes are being created.
            Cursor.Current = Cursors.WaitCursor;

            // Suppress repainting the TreeView until all the objects have been created.
            treeView.BeginUpdate();
            treeView.Nodes.Clear();
            treeView.Nodes.AddRange(DataDeviceTree.GetAllSubNode(@"").ToArray());
            // Clear the TreeView each time the method is called.
            if (treeView.Nodes.Count > 0)
            {
                treeView.Nodes[0].Nodes.AddRange(DataDeviceTree.GetAllSubNode(@"/1").ToArray());
                treeView.Nodes[0].Expand();
            }
            //treeView.Nodes[0].Expand();
            // Reset the cursor to the default for all controls.
            Cursor.Current = Cursors.Default;

            // Begin repainting the TreeView.
            treeView.EndUpdate();
        }

        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {          
            RefreshSubs(e.Node);
        }
        private void RefreshSubs(TreeNode tn)
        {
            if (tn == null) return;
            // Display a wait cursor while the TreeNodes are being created.
            Cursor.Current = Cursors.WaitCursor;

            // Suppress repainting the TreeView until all the objects have been created.
            treeView.BeginUpdate();
            tn.Nodes.Clear();
            tn.Nodes.AddRange(DataDeviceTree.GetAllSubNode(((DeviceInfo)tn.Tag).path).ToArray());
            foreach (TreeNode ttn in tn.Nodes)
            {
                ttn.Nodes.Clear();
                ttn.Nodes.AddRange(DataDeviceTree.GetAllSubNode(((DeviceInfo)ttn.Tag).path).ToArray());
            }
            // Reset the cursor to the default for all controls.
            Cursor.Current = Cursors.Default;

            // Begin repainting the TreeView.
            treeView.EndUpdate();
        }
        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            treeView.SelectedNode = e.Node;
            if (e.Button == MouseButtons.Left)
            {
                DeviceInfo ttg = (DeviceInfo)e.Node.Tag;
                if (e.Node.Text == "全部")
                {
                    glacialLisint();
                }
                else if (ttg == null ||  ttg.Sensors_set().Count <= 0)
                {
                    glacialList1.Items.Clear();
                    glacialList1.Invalidate();
                }
                else if (ttg.Sensors_set().Count > 0)
                {
                    List<GLItem> lsItem = new List<GLItem>();
                    foreach (int id in ttg.Sensors_set())
                    {
                        GLItem item = new GLItem(glacialList1);
                        gllistInitItemText(Data.inst().cd_Point[id], item);
                        lsItem.Add(item);
                    }
                    glacialList1.Items.Clear();
                    glacialList1.Items.AddRange(lsItem.ToArray());
                    glacialList1.Invalidate();
                    DisplayHints(lsItem.Count);
                }
                tabControl.Enabled = false;
            }
        }
        private void 增加节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode stn = treeView.SelectedNode;
                if (stn != null)
                {
                    DeviceInfo tt = new DeviceInfo();
                    FormThSet ftn = new FormThSet(tt);
                    ftn.Text = "增加节点";
                    if (ftn.ShowDialog() == DialogResult.OK)
                    {
                        TreeNode ntn = stn.Nodes.Add(tt.Name);
                        ntn.Tag = tt;
                        DataDeviceTree.InsertNodetoDb(ntn);
                        RefreshSubs(ntn);
                        stn.Expand();

                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误");
            }
        }

        private void 删除节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode tn = treeView.SelectedNode;
                if (tn != null && tn.Text != "全部")
                {
                    if (MessageBox.Show(string.Format("删除节点\"{0}\"?", tn.Text), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        TreeNode ptn = tn.Parent;
                        DataDeviceTree.RemoveNode(tn);
                        tn.Remove();
                        if (ptn != null)
                            RefreshSubs(ptn);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误");
            }

        }

        private void 节点属性ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode tn = treeView.SelectedNode;
                if (tn != null && tn.Text !="全部")
                {
                    FormThSet ftn = new FormThSet((DeviceInfo)tn.Tag);
                    if (ftn.ShowDialog() == DialogResult.OK)
                    {
                        DataDeviceTree.UpdateNodetoDB(tn);
                        TreeNode stn = tn.Parent;
                        RefreshSubs(tn.Parent);
                        foreach (TreeNode ntn in stn.Nodes)
                        {
                            if (((DeviceInfo)ntn.Tag).id == ((DeviceInfo)(tn.Tag)).id)
                            {
                                treeView.SelectedNode = ntn;
                                foreach(int sensor in ((DeviceInfo)ntn.Tag).Sensors_set())
                                {
                                    point pt = Data.inst().cd_Point[sensor];
                                    pt.alarmininfo = "";
                                    AlarmSet.GetInst().Remove(pt);
                                }
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误");
            }
        }

        private void treeView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {

                if ((e.KeyState & 8) == 8 &&
                    (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
                {
                    // CTRL KeyState for copy.
                    e.Effect = DragDropEffects.Copy;
                }
                else
                    e.Effect = DragDropEffects.Move;
            }
            
            else if (e.Data.GetDataPresent(typeof(TreeDragData)))
            {
                if ((e.KeyState & 8) == 8 &&
                    (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
                {
                    // CTRL KeyState for copy.
                    e.Effect = DragDropEffects.Copy;
                }
                else 
                    e.Effect = DragDropEffects.Move;
            }
            else
                e.Effect = DragDropEffects.None;

        }

        private void treeView_DragOver(object sender, DragEventArgs e)
        {
            Point Position = new Point(0, 0);
            Position.X = e.X;
            Position.Y = e.Y;
            Position = treeView.PointToClient(Position);
            TreeNode DropNode = this.treeView.GetNodeAt(Position);

            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                if ((e.KeyState & 8) == 8 &&
                    (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
                {
                    // CTRL KeyState for copy.
                    e.Effect = DragDropEffects.Copy;
                }
                else if((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
                    e.Effect = DragDropEffects.Move;
                treeView.SelectedNode = DropNode;
                if (DropNode != null)
                    DropNode.Expand();
            }
            else if (e.Data.GetDataPresent(typeof(TreeDragData)))
            {
                if ((e.KeyState & 8) == 8 &&
                    (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
                {
                    // CTRL KeyState for copy.
                    e.Effect = DragDropEffects.Copy;
                }
                else if((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
                    e.Effect = DragDropEffects.Move;
                treeView.Focus();
                treeView.SelectedNode = DropNode;
                if (DropNode != null)
                    DropNode.Expand();
            }
            else
                e.Effect = DragDropEffects.None;  
        }

        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode myNode = null;
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                myNode = (TreeNode)(e.Data.GetData(typeof(TreeNode)));

                // if (!myNode.Tag.ToString().Equals("node")) return;
                Point Position = new Point(0, 0);
                Position.X = e.X;
                Position.Y = e.Y;
                Position = treeView.PointToClient(Position);
                TreeNode DropNode = this.treeView.GetNodeAt(Position);
                // 1.目标节点不是空。2.目标节点不是被拖拽接点的字节点。3.目标节点不是被拖拽节点本身
                if (DropNode != null && DropNode != myNode.Parent && DropNode != myNode)
                {
                    if (myNode.Parent != null)
                    {
                        if (e.Effect == DragDropEffects.Move)
                        {
                            myNode.Parent.Nodes.Remove(myNode);
                            DropNode.Nodes.Add(myNode);
                            DataDeviceTree.UpdateAllSubNodes(myNode);
                            RefreshSubs(myNode.Parent);
                        }
                        else if (e.Effect == DragDropEffects.Copy)
                        {
                            TreeNode tn = (TreeNode)myNode.Clone();
                            DropNode.Nodes.Add(tn);
                            DataDeviceTree.InsertNodetoDb(tn);
                            DataDeviceTree.UpdateAllSubNodes(tn.Parent);
                            RefreshSubs(tn.Parent);
                            //tn.Parent.Expand();
                        }
                    }
                   
                }
            }
            else if (e.Data.GetDataPresent(typeof(TreeDragData)))
            {
                TreeDragData myData = (TreeDragData)(e.Data.GetData(typeof(TreeDragData)));

                Point Position = new Point(0, 0);
                Position.X = e.X;
                Position.Y = e.Y;
                Position = treeView.PointToClient(Position);
                TreeNode DropNode = this.treeView.GetNodeAt(Position);
                // 1.目标节点不是空。
                if (DropNode != null)
                {
                    if (e.Effect == DragDropEffects.Move)
                    {
                        if (myData.DragSourceNode != null && myData.DragSourceNode.Text != "全部")
                        {
                            DeviceInfo tt = (DeviceInfo)myData.DragSourceNode.Tag;
                            tt.ExceptWith(myData.pointid_set);
                            DataDeviceTree.UpdateNodetoDB(myData.DragSourceNode);
                        }
                    }
                    if (e.Effect == DragDropEffects.Move || e.Effect == DragDropEffects.Copy)
                    {

                        DeviceInfo ttg = (DeviceInfo)DropNode.Tag;

                        ttg.UnionWith(myData.pointid_set);
                       
                        DataDeviceTree.UpdateNodetoDB(DropNode);
                        TreeNodeMouseClickEventArgs ee = new TreeNodeMouseClickEventArgs(DropNode, MouseButtons.Left, 0, 0, 0);
                        treeView_NodeMouseClick(null, ee);
                        MessageBox.Show("应重新设置设备报警参数");
                    }                  
                }
            }
        }

        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void treeView_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            if (e.EscapePressed)
                e.Action = DragAction.Cancel;
        }

        private void glacialList1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((ModifierKeys & Keys.Shift) == Keys.Shift || (ModifierKeys & Keys.Control) == Keys.Control)
            {
                if (e.Button == MouseButtons.Left && glacialList1.SelectedItems.Count > 0)
                {
                    TreeDragData td = new TreeDragData();
                    foreach (GLItem it in glacialList1.SelectedItems)
                    {
                        itemtag tag = (itemtag)it.Tag;
                        //if (tag.PointSrc == pointsrc.sis)//计算点无曲线，没法比较。
                        td.pointid_set.Add(tag.id);
                        td.DragSourceNode = treeView.SelectedNode;
                    }
                    glacialList1.DoDragDrop(td, DragDropEffects.Copy | DragDropEffects.Move);
                }
            }
        }
        private void 从分组中移除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode tn = treeView.SelectedNode;
            if (tn != null && tn.Text != "全部")
            {
                DeviceInfo tt = (DeviceInfo)tn.Tag;
                foreach (GLItem item in glacialList1.SelectedItems)
                {
                    int did = ((itemtag)item.Tag).id;
                    DeviceInfo di;
                    if(Data_Device.dic_Device.TryGetValue(did,out di))
                    {
                        di.RemoveSensor(did);
                    }
                    tt.RemoveSensor(did);
                }
                DataDeviceTree.UpdateNodetoDB(tn);
                TreeNodeMouseClickEventArgs ee = new TreeNodeMouseClickEventArgs(tn, MouseButtons.Left, 0, 0, 0);
                treeView_NodeMouseClick(null, ee);
                MessageBox.Show("应重新设置设备报警参数");
            }
        }

        private void glacialList1_SelectedIndexChanged(object source, ClickEventArgs e)
        {
            
        }

        private void 显示曲线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HashSet<int> pointid = new HashSet<int>();
            foreach (GLItem item in glacialList1.SelectedItems)
            {
                itemtag it = (itemtag)item.Tag;
                pointid.Add(it.id);
            }
            if (pointid.Count > 0)
            {
                FormCurves fc = new FormCurves(pointid);
                fc.Show();
            }
        }
    }
}

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
namespace HGS
{
    public partial class FormRealTimeAlarm : Form
    {
        HashSet<AlarmInfo> hs_rec_realtime = new HashSet<AlarmInfo>();
        //HashSet<AlarmInfo> hs_rec_history = new HashSet<AlarmInfo>();
        //用于提高性能。
        HashSet<AlarmInfo> hs_realtime = new HashSet<AlarmInfo>();
        //HashSet<AlarmInfo> hs_history = new HashSet<AlarmInfo>();
        public FormRealTimeAlarm()
        {
            InitializeComponent();
            FillMyTreeView();
        }
        private void AitoItemText(AlarmInfo ai, GLItem item,bool isRealtime)
        {
            item.SubItems["ND"].Text = ai._nd;
            item.SubItems["PN"].Text = ai._pn;
            item.SubItems["ED"].Text = ai._ed;
            item.SubItems["AlarmingAV"].Text = ai._av.ToString();
            item.SubItems["EU"].Text = ai._eu;
            item.SubItems["AlarmInfo"].Text = ai._Info;
            item.SubItems["Time"].Text = ai._starttime.ToString("yyyy-MM-dd HH:mm:ss");
            if (ai.stoptime.Year >= 2000 && !isRealtime)
                item.SubItems["StopTime"].Text = ai.stoptime.ToString("yyyy-MM-dd HH:mm:ss");
            item.Tag = ai;
        }
        private void DisplayFilterItem(string path)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                //glacialList1.Items.Clear();
                List<GLItem> lsItems = new List<GLItem>();
                List<AlarmInfo> lsrtiai = AlarmSet.GetInst().GetAlarmRealTimeInfo();
                hs_realtime.Clear();
                foreach (AlarmInfo ai in lsrtiai)
                {
                    string[] filtes = tsTB_ED.Text.Split(' ');
                    bool flag = true;
                    for (int i = 0; i < filtes.Length; i++)
                    {
                        flag = flag && ai._ed.Contains(filtes[i]);
                        if (!flag) break;
                    }
                    /*(pt.OwnerId == tsCB_class.SelectedIndex || tsCB_class.SelectedIndex == 0) &&*/
                    if (flag && ai._nd.Contains(tsCB_ND.Text.Trim()) && flag && ai._pn.Contains(tsTB_PN.Text.Trim()) && 
                        ai._Info.Contains(tsTB_AI.Text.Trim())&& ai._path.Contains(path))
                    {
                        if (!hs_rec_realtime.Contains(ai))
                        {
                            GLItem itemx = new GLItem(glacialList1);
                            AitoItemText(ai, itemx, true);
                            lsItems.Add(itemx);
                            hs_rec_realtime.Add(ai);
                        }
                        hs_realtime.Add(ai);
                    }
                }
                List<GLItem> deleitem = new List<GLItem>();
                foreach (GLItem item in glacialList1.Items)
                {
                    AlarmInfo ai = (AlarmInfo)item.Tag;
                    if (!hs_realtime.Contains(ai))
                    {
                        deleitem.Add(item);
                        hs_rec_realtime.Remove(ai);
                    }

                }
                foreach (GLItem item in deleitem)
                {
                    glacialList1.Items.Remove(item);
                }
                deleitem.Clear();
                foreach (GLItem itm in lsItems)
                {
                    glacialList1.Items.Insert(0,itm);
                }
                glacialList1.Invalidate();
                tSLabel_Nums.Text = string.Format("实时报警总点数：{0}个", lsrtiai.Count);
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                //glacialList2.Items.Clear();
                List<GLItem> lsItems = new List<GLItem>();
                List<AlarmInfo> lshsiai = AlarmSet.GetInst().GetAlarmHistoryInfo();
                hs_realtime.Clear();
                foreach (AlarmInfo ai in lshsiai)
                {
                    string[] filtes = tsTB_ED.Text.Split(' ');
                    bool flag = true;
                    for (int i = 0; i < filtes.Length; i++)
                    {
                        flag = flag && ai._ed.Contains(filtes[i]);
                        if (!flag) break;
                    }
                    /*(pt.OwnerId == tsCB_class.SelectedIndex || tsCB_class.SelectedIndex == 0) &&*/
                    if (flag && ai._nd.Contains(tsCB_ND.Text.Trim()) && flag && ai._pn.Contains(tsTB_PN.Text.Trim()) && 
                        ai._Info.Contains(tsTB_AI.Text.Trim()) && ai._path.Contains(path))
                    {
                        if (!hs_rec_realtime.Contains(ai))
                        {
                            GLItem itemx = new GLItem(glacialList2);
                            AitoItemText(ai, itemx, false);
                            lsItems.Add(itemx);
                            hs_rec_realtime.Add(ai);
                        }
                        hs_realtime.Add(ai);

                    }

                }
                List<GLItem> deleitem = new List<GLItem>();
                foreach (GLItem item in glacialList2.Items)
                {
                    AlarmInfo ai = (AlarmInfo)item.Tag;
                    if (!hs_realtime.Contains(ai))
                    {
                        deleitem.Add(item);
                        hs_rec_realtime.Remove(ai);
                    }

                }
                foreach (GLItem item in deleitem)
                {
                    glacialList2.Items.Remove(item);
                }
                deleitem.Clear();
                foreach (GLItem itm in lsItems)
                {
                    glacialList2.Items.Insert(0, itm);
                }
                glacialList2.Invalidate();
                tSLabel_Nums.Text = string.Format("报警点数：{0}个", lshsiai.Count);
            }
        }
        /*
        private void DisplayTreeItem(TreeNode tn)
        {
            if (tn == null) return;
            if (tabControl1.SelectedIndex == 0)
            {
                glacialList1.Items.Clear();
                List<GLItem> lsItems = new List<GLItem>();
                List<AlarmInfo> lsrtiai = AlarmSet.GetInst().GetAlarmRealTimeInfo();
                foreach (AlarmInfo ai in lsrtiai)
                {
                    if (ai._path.Contains(((DeviceInfo)tn.Tag).path))
                    {
                        GLItem itemx = new GLItem(glacialList1);
                        AitoItemText(ai, itemx, true);
                        lsItems.Add(itemx);
                    }
                }
                glacialList1.Items.AddRange(lsItems.ToArray());
                glacialList1.Invalidate();
                tSLabel_Nums.Text = string.Format("实时报警总点数：{0}个", lsrtiai.Count);
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                glacialList2.Items.Clear();
                List<GLItem> lsItems = new List<GLItem>();
                List<AlarmInfo> lshsiai = AlarmSet.GetInst().GetAlarmHistoryInfo();
                foreach (AlarmInfo ai in lshsiai)
                {

                    if (ai._path.Contains(((DeviceInfo)tn.Tag).path))
                    {
                        GLItem itemx = new GLItem(glacialList2);
                        AitoItemText(ai, itemx, false);
                        lsItems.Add(itemx);
                    }
                }
                glacialList2.Items.AddRange(lsItems.ToArray());
                glacialList2.Invalidate();
                tSLabel_Nums.Text = string.Format("报警点数：{0}个", lshsiai.Count);
            }
        }
        */
        private void timer_GetAlarm_Tick(object sender, EventArgs e)
        {
            if (this.Size.Height <= 100)
                timer_GetAlarm.Interval = 30000;
            else
                timer_GetAlarm.Interval = 5000;

            if (treeView.SelectedNode == null || treeView.SelectedNode.Text == "全部")
            {

                DisplayFilterItem("");
            }
            else
                //DisplayTreeItem(treeView.SelectedNode);
                DisplayFilterItem(((DeviceInfo)treeView.SelectedNode.Tag).path);
            
        }

        private void tsCB_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            hs_rec_realtime.Clear();
            glacialList1.Items.Clear();
            glacialList2.Items.Clear();
            timer_GetAlarm_Tick(sender, e);
        }

        private void FormAlarmSet_Shown(object sender, EventArgs e)
        {
            tsCB_class.Items.Clear();
            foreach (string it in Auth.GetInst().GetUser())
            {
                tsCB_class.Items.Add(it);
            }
            tsCB_class.SelectedIndex = Auth.GetInst().LoginID;
        }

        private void tsTB_PN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                hs_rec_realtime.Clear();
                glacialList1.Items.Clear();
                glacialList2.Items.Clear();
                timer_GetAlarm_Tick(sender, e);
                //glacialList1.Invalidate();
                e.Handled = true;
                e.SuppressKeyPress = true;
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
                glacialList1.Items.Clear();
                glacialList2.Items.Clear();
                hs_rec_realtime.Clear();
                DeviceInfo ttg = (DeviceInfo)e.Node.Tag;
                if (e.Node.Text == "全部" || ttg == null)
                {
                    DisplayFilterItem("");
                }
                else 
                {
                    //DisplayTreeItem(e.Node);
                    DisplayFilterItem(ttg.path);
                }
                //tabControl.Enabled = false;
            }
        }

        private void glacialList1_DoubleClick(object sender, EventArgs e)
        {
            OPAPI.Connect sisconn_temp = new OPAPI.Connect(Pref.Inst().sisHost, Pref.Inst().sisPort, 60,
                                                    Pref.Inst().sisUser, Pref.Inst().sisPassword);//建立连接
            try
            {
                DateTime now = SisConnect.GetSisSystemTime(sisconn_temp);
                if (glacialList1.Items.SelectedItems.Count > 0)
                {
                    AlarmInfo ai = (AlarmInfo)((GLItem)glacialList1.Items.SelectedItems[0]).Tag;
                    TimeSpan ts = now - ai._starttime;

                    if (ts.TotalMinutes >= 5)
                        Plot(ai, ai._starttime.AddMinutes(-5), ai._starttime.AddMinutes(5));
                    else
                        Plot(ai, ai._starttime.AddMinutes(-5), ai._starttime);
                }
            }
            catch (Exception ee)
            {
                FormBugReport.ShowBug(ee);
            }
            finally
            {
                sisconn_temp.close();
            }
        }
        private void Plot(AlarmInfo ai, DateTime begin, DateTime end)
        {
            HashSet<point> point = null;
            if (ai._sensorid != -1)
            {
                point = Functions.set_idtopoint(new HashSet<int>(new int[] { ai._sensorid }));
                //pointid.Add(ai._sensorid);
            }
            else if (ai._deviceid != -1)
            {
                DeviceInfo dv;
                if (Data_Device.dic_Device.TryGetValue(ai._deviceid, out dv))
                {
                    point = Functions.set_idtopoint(dv.Sensors_set());
                    
                }
            }
            if (point != null)
            {
                FormPlotCurves frta = new FormPlotCurves(point, begin, end);
                frta.ShowDialog();
            }
        }
    
        private void glacialList2_DoubleClick(object sender, EventArgs e)
        {
            OPAPI.Connect sisconn_temp = new OPAPI.Connect(Pref.Inst().sisHost, Pref.Inst().sisPort, 60,
                                                   Pref.Inst().sisUser, Pref.Inst().sisPassword);//建立连接
            try
            {
                //历史
                if (glacialList2.Items.SelectedItems.Count > 0)
                {
                    AlarmInfo ai = (AlarmInfo)((GLItem)glacialList2.Items.SelectedItems[0]).Tag;
                    DateTime now = SisConnect.GetSisSystemTime(sisconn_temp);
                    TimeSpan ts = now - ai.stoptime;

                    if (ts.TotalDays >= 7)
                        Plot(ai, ai._starttime.AddMinutes(-5), ai._starttime.AddDays(7));
                    else if (ts.TotalMinutes >= 5)
                        Plot(ai, ai._starttime.AddMinutes(-5), ai.stoptime.AddMinutes(5));

                    else
                        Plot(ai, ai._starttime.AddMinutes(-5), now);
                }
            }
            catch (Exception ee)
            {
                FormBugReport.ShowBug(ee);
            }
            finally
            {
                sisconn_temp.close();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            hs_rec_realtime.Clear();
            glacialList1.Items.Clear();
            glacialList2.Items.Clear();
            timer_GetAlarm_Tick(sender, e);
        }

    }
}

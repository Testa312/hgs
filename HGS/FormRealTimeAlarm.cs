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
        Dictionary<point, GLItem> dic_rec = new Dictionary<point, GLItem>();
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
            item.SubItems["AlarmInfo"].Text = ai._Info;
            item.SubItems["Time"].Text = ai._starttime.ToString();
            if (ai.stoptime.Year >= 2000 && !isRealtime)
                item.SubItems["StopTime"].Text = ai.stoptime.ToString();
            item.Tag = ai;
        }
        private void DisplayFilterItem()
        {
            if (tabControl1.SelectedIndex == 0)
            {
                glacialList1.Items.Clear();
                List<GLItem> lsItems = new List<GLItem>();
                List<AlarmInfo> lsrtiai = AlarmSet.GetInst().GetAlarmRealTimeInfo();
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
                    if (ai._nd.Contains(tsCB_ND.Text.Trim()) && flag && ai._pn.Contains(tsTB_PN.Text.Trim()) && ai._Info.Contains(tsTB_AI.Text.Trim()))
                    {
                        GLItem itemx = new GLItem(glacialList1);
                        AitoItemText(ai, itemx,true);
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
                    string[] filtes = tsTB_ED.Text.Split(' ');
                    bool flag = true;
                    for (int i = 0; i < filtes.Length; i++)
                    {
                        flag = flag && ai._ed.Contains(filtes[i]);
                        if (!flag) break;
                    }
                    /*(pt.OwnerId == tsCB_class.SelectedIndex || tsCB_class.SelectedIndex == 0) &&*/
                    if (ai._nd.Contains(tsCB_ND.Text.Trim()) && flag && ai._pn.Contains(tsTB_PN.Text.Trim()) && ai._Info.Contains(tsTB_AI.Text.Trim()))
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
        private void timer_GetAlarm_Tick(object sender, EventArgs e)
        {
            if (treeView.SelectedNode == null || treeView.SelectedNode.Text == "全部")
            {

                DisplayFilterItem();
            }
            else
                DisplayTreeItem(treeView.SelectedNode);
        }

        private void tsCB_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            dic_rec.Clear();
            glacialList1.Items.Clear();
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
                dic_rec.Clear();
                glacialList1.Items.Clear();
                timer_GetAlarm_Tick(sender, e);
                glacialList1.Invalidate();
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
                DeviceInfo ttg = (DeviceInfo)e.Node.Tag;
                if (e.Node.Text == "全部")
                {
                    DisplayFilterItem();
                }
                else if (ttg == null || ttg.Sensors_set().Count <= 0)
                {
                    glacialList1.Items.Clear();
                    glacialList1.Invalidate();
                }
                else if (ttg.Sensors_set().Count > 0)
                {
                    DisplayTreeItem(e.Node);
                }
                //tabControl.Enabled = false;
            }
        }

        private void glacialList1_DoubleClick(object sender, EventArgs e)
        {
            if (glacialList1.Items.SelectedItems.Count > 0)
            {
                AlarmInfo ai = (AlarmInfo)((GLItem)glacialList1.Items.SelectedItems[0]).Tag;
                if (ai._sensorid != -1)
                {
                    HashSet<int> pid = new HashSet<int>();
                    pid.Add(ai._sensorid);
                    FormPlotCurves frta = new FormPlotCurves(pid);
                    frta.Show();
                }
                else if (ai._deviceid != -1)
                { }
            }
        }
    }
}

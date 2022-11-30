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
using System.Diagnostics;
using OpenPlant = System.IntPtr;
using Npgsql;
namespace HGS
{
    public partial class FormCalcPointList : Form
    {
        bool isFirst = true;
        private HashSet<int> onlyid;
        public FormCalcPointList()
        {
            InitializeComponent();
            FillMyTreeView();
        }
        ~FormCalcPointList()
        {

        }
        public void glacialLisint(HashSet<int> Onlyid,string path)
        {
            Cursor = Cursors.WaitCursor;
            onlyid = Onlyid;
            try
            {
                timer.Enabled = false;
                HashSet<string> hs_ND = new HashSet<string>();
                glacialList.Items.Clear();
                int count = 0;
                List<GLItem> lsItmems = new List<GLItem>();
                foreach (point ptx in Data.inst().hsAllPoint)
                {
                    string[] filtes = tSTBED.Text.Split(' ');
                    bool flag = true;
                    for (int i = 0; i < filtes.Length; i++)
                    {
                        flag = flag && ptx.ed.Contains(filtes[i]);
                        if (!flag) break;
                    }
                    if (!flag) continue;
                    if (ptx.nd.Contains(tSCBNode.Text.Trim()) && flag &&
                        ptx.pn.Contains(tSTBPN.Text.Trim()) && ptx.DevicePath.Contains(path))
                    {
                        if (onlyid.Contains(ptx.id)) continue;

                        GLItem itemn = new GLItem(glacialList);
                        lsItmems.Add(itemn);
                        itemtag it = new itemtag();
                        it.id = ptx.id;

                        itemn.SubItems["ND"].Text = ptx.nd;
                        itemn.SubItems["PN"].Text = ptx.pn;

                        itemn.SubItems["EU"].Text = ptx.eu;
                        itemn.SubItems["ED"].Text = ptx.ed;

                        it.sisid = ptx.Id_sis;

                        it.PointSrc = ptx.pointsrc;

                        itemn.Tag = it;

                        if (isFirst) hs_ND.Add(ptx.nd);
                        count++;
                    }
                }
                glacialList.Items.AddRange(lsItmems.ToArray());
                //
                //tSCBNode.Items.Add("");
                if (isFirst)
                {
                    foreach (string citem in hs_ND)
                    {
                        tSCBNode.Items.Add(citem.Trim());
                    }
                }
                isFirst = false;
                glacialList.Invalidate();
                tSSLabel_nums.Text = string.Format("点数：{0}", count);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            timer.Enabled = true;
        }
        public void tSBFind_Click(object sender, EventArgs e)
        {
            glacialLisint(onlyid,"");
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            foreach (GLItem item in glacialList.Items)
            {
                if (glacialList.IsItemVisible(item))
                {
                    itemtag it = (itemtag)(item.Tag);
                    point pt = Data.inst().cd_Point[it.id];
                    if (pt.av != null)
                    {
                        item.SubItems["AV"].Text = Math.Round(pt.av ?? 0, pt.fm).ToString();
                    }
                    else
                        item.SubItems["AV"].Text = "";
                    item.SubItems["DS"].Text = pt.ps.ToString();
                }
            }
        }
        private void FormSisPointList_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer.Enabled = false;
        }

        private void FormCalcPointList_Shown(object sender, EventArgs e)
        {
            //if (tSCBNode.Items.Count > 0) tSCBNode.SelectedIndex = 0;这样写有问题。
        }

        private void tSTBPN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                glacialLisint(onlyid,"");
                glacialList.Invalidate();
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
                glacialList.Items.Clear();
                DeviceInfo ttg = (DeviceInfo)e.Node.Tag;
                if (e.Node.Text == "全部" || ttg == null)
                {
                    glacialLisint(onlyid, "");
                }
                else
                {
                    //DisplayTreeItem(e.Node);
                    glacialLisint(onlyid,ttg.path);
                }
                //tabControl.Enabled = false;
            }
        }
    }
}

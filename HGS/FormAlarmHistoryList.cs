using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using GlacialComponents.Controls;
namespace HGS
{
    public partial class FormAlarmHistoryList : Form
    {
        public FormAlarmHistoryList()
        {
            InitializeComponent();
            dateTimePickerTo.Value = DateTime.Now.AddHours(1);
            dateTimePickerFrom.Value = DateTime.Now.AddDays(-1);
            FillMyTreeView();
        }
        private void glacialListInit(string path)
        {
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
            try
            {
                string sqlx = string.Format(" nd like'%{0}%' and pn like '%{1}%' and ed like '%{2}%' and device_path like '{3}%'",
                    tsTB_ND.Text, tsTB_PN.Text, tsTB_ED.Text,path);
                string strsql = string.Format("select distinct on (sid) sid,nd,pn,ed,eu,alarmav,alarminfo,alarmtime ,stoptime  " +
                   "from alarmhistory where {0} order by sid, alarmtime desc;", sqlx);

                glacialList_UP.Items.Clear();

                Cursor.Current = Cursors.WaitCursor;
                pgconn.Open();

                var cmd = new NpgsqlCommand(strsql, pgconn);
                NpgsqlDataReader pgreader = cmd.ExecuteReader();
                List<GLItem> lsItems = new List<GLItem>();
                while (pgreader.Read())
                {

                    GLItem itemn = new GLItem(glacialList_UP);
                   
                    lsItems.Add(itemn);
                    //itemn.Tag = pgreader["sid"].ToString();
                   

                    itemn.SubItems["ND"].Text = pgreader["nd"].ToString();
                    itemn.SubItems["PN"].Text = pgreader["pn"].ToString();
                    itemn.SubItems["ED"].Text = pgreader["ed"].ToString();
                    itemn.SubItems["AV"].Text = pgreader["alarmav"].ToString();
                    itemn.SubItems["EU"].Text = pgreader["eu"].ToString();

                    itemn.SubItems["AlarmInfo"].Text = pgreader["alarminfo"].ToString();
                    DateTime start =  (DateTime)pgreader["alarmtime"];
                    itemn.SubItems["AlarmTime"].Text = start.ToString("yyyy-MM-dd HH:mm:ss"); // pgreader["alarmtime"].ToString();
                    DateTime stop = (DateTime)pgreader["stoptime"];
                    //itemn.SubItems["StopTime"].Text = pgreader["stoptime"].ToString();
                   if (stop.Year > 2000)
                        itemn.SubItems["StopTime"].Text = stop.ToString("yyyy-MM-dd HH:mm:ss");
                    //
                    AlarmInfo ai = new AlarmInfo(pgreader["sid"].ToString(), -1, -1, "", "", "", "", 0, "", "", 0);
                    
                    ai._starttime = start;
                    ai.stoptime = stop;
                    itemn.Tag = ai;
                }
                glacialList_UP.Items.AddRange(lsItems.ToArray());
                glacialList_UP.Invalidate();
                pgconn.Close();
            }
            catch (Exception ee)
            {
                FormBugReport.ShowBug(ee);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                glacialList_Down.Items.Clear();
                glacialList_Down.Invalidate();
                pgconn.Close();
            }
        }
        private void toolStripButtonFind_Click(object sender, EventArgs e)
        {
            glacialListInit("");
        }

        private void FormAlarmList_Shown(object sender, EventArgs e)
        {
            tsCB_class.Items.Clear();
            foreach (string it in Auth.GetInst().GetUser())
            {
                tsCB_class.Items.Add(it);
            }
            tsCB_class.SelectedIndex = Auth.GetInst().LoginID;
        }
        private void glacialList_UP_Click(object sender, EventArgs e)
        {
            if (glacialList_UP.SelectedItems.Count == 1)
            {
                var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    glacialList_UP.Enabled = false;
                    glacialList_Down.Items.Clear();
                    string tfrom = dateTimePickerFrom.Value.ToString("yyyy/M/d H:00:00");
                    string tto = dateTimePickerTo.Value.ToString("yyyy/M/d H:00:00");
                    pgconn.Open();
                    string strsql = string.Format("select sid,eu, alarmav, alarminfo, alarmtime,stoptime  from " +
                        "alarmhistory where alarmtime >= '{0}' and alarmtime <= '{1}' and  sid = " +
                        "'{2}' order by alarmtime desc limit 1000;", tfrom, tto,
                        (((AlarmInfo)((GLItem)glacialList_UP.SelectedItems[0]).Tag).sid));
                    var cmd = new NpgsqlCommand(strsql,pgconn);
                    NpgsqlDataReader pgreader = cmd.ExecuteReader();
                    List<GLItem> lsItems = new List<GLItem>();
                    while (pgreader.Read())
                    {
                        GLItem itemn = new GLItem(glacialList_Down);
                        lsItems.Add(itemn);
                        DateTime start = (DateTime)pgreader["alarmtime"];
                        itemn.SubItems["Time"].Text =start.ToString("yyyy-MM-dd HH:mm:ss");
                        DateTime stop = (DateTime)pgreader["stoptime"];
                        if (stop.Year > 2000)
                            itemn.SubItems["StopTime"].Text = stop.ToString("yyyy-MM-dd HH:mm:ss");
                        itemn.SubItems["AlarmInfo"].Text = pgreader["alarminfo"].ToString();
                        itemn.SubItems["eu"].Text = pgreader["eu"].ToString();
                        itemn.SubItems["AlarmAv"].Text = pgreader["alarmav"].ToString();
                        //
                        AlarmInfo ai = new AlarmInfo(pgreader["sid"].ToString(), -1, -1, "", "", "", "", 0, "", "", 0);

                        ai._starttime = start;
                        ai.stoptime = stop;
                        itemn.Tag = ai;
                    }
                    glacialList_Down.Items.AddRange(lsItems.ToArray());
                    glacialList_Down.Invalidate();
                    pgconn.Close();
                }
                catch (Exception ee)
                {
                    FormBugReport.ShowBug(ee);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                    glacialList_UP.Enabled = true;
                    pgconn.Close();
                }
            }
        }

        private void button1H_Click(object sender, EventArgs e)
        {
            dateTimePickerTo.Value = DateTime.Now.AddHours(1); ;
            dateTimePickerFrom.Value = DateTime.Now.AddHours(-1);
            glacialList_UP_Click(sender,e);
        }

        private void button2H_Click(object sender, EventArgs e)
        {
            dateTimePickerTo.Value = DateTime.Now.AddHours(1); ;
            dateTimePickerFrom.Value = DateTime.Now.AddHours(-2);
            glacialList_UP_Click(sender, e);
        }

        private void button8h_Click(object sender, EventArgs e)
        {
            dateTimePickerTo.Value = DateTime.Now.AddHours(1); ;
            dateTimePickerFrom.Value = DateTime.Now.AddHours(-8);
            glacialList_UP_Click(sender, e);
        }

        private void button1D_Click(object sender, EventArgs e)
        {
            dateTimePickerTo.Value = DateTime.Now.AddHours(1); ;
            dateTimePickerFrom.Value = DateTime.Now.AddDays(-1);
            glacialList_UP_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dateTimePickerTo.Value = DateTime.Now.AddHours(1); ;
            dateTimePickerFrom.Value = DateTime.Now.AddDays(-2);
            glacialList_UP_Click(sender, e);
        }

        private void button7D_Click(object sender, EventArgs e)
        {
            dateTimePickerTo.Value = DateTime.Now.AddHours(1); ;
            dateTimePickerFrom.Value = DateTime.Now.AddDays(-7);
            glacialList_UP_Click(sender, e);
        }

        private void tsTB_ND_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                glacialListInit("");
                glacialList_UP.Invalidate();
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
                    glacialListInit("");
                }
                else if (ttg == null)
                {
                    glacialList_UP.Items.Clear();
                    glacialList_UP.Invalidate();
                }
                else
                {
                    glacialListInit(ttg.path);
                }
                //tabControl.Enabled = false;
            }
        }
        private void Plot(AlarmInfo ai)
        {
            if (ai == null) return;
            if (ai.sid.Length <= 2) return;
            string[] temp = ai.sid.Split('-');
            char c = temp[0][0];
            string sid = temp[0].Remove(0, 1);
            DateTime begin = ai._starttime;
            DateTime end = ai.stoptime;
            if (end.Year <= 2000)
                end = DateTime.Now;
            TimeSpan ts = end - begin;
            if (ts.TotalMinutes < 5)
                begin = begin.AddMinutes((ts.TotalMinutes)-10 );
            HashSet<point> hsPoint = null;
            if (c == 'P')
            {
                int pid;
                if (int.TryParse(sid, out pid))
                {
                    point pt;
                    if (Data.inst().cd_Point.TryGetValue(pid, out pt))
                    {
                        hsPoint = new HashSet<point>();
                        hsPoint.Add(pt);
                    }
                }
            }
            else if (c == 'D')
            {
                int did;
                if (int.TryParse(sid, out did))
                {
                    DeviceInfo di;
                    if (Data_Device.dic_Device.TryGetValue(did, out di))
                    {
                        hsPoint = Functions.set_idtopoint(di.Sensors_set());                       
                    }
                    else
                    {
                        try
                        {
                            di = Data_Device.GetDevice(did);
                            hsPoint = Functions.set_idtopoint(di.Sensors_set());
                        }
                        catch (Exception e)
                        {
                            FormBugReport.ShowBug(e);
                        }
                    }
                }
            }
            if (hsPoint != null)
            {
                FormPlotCurves frta = new FormPlotCurves(hsPoint,begin, end);
                frta.ShowDialog();
            }
        }
        private void glacialList_UP_DoubleClick(object sender, EventArgs e)
        {
            //历史
            if (glacialList_UP.Items.SelectedItems.Count > 0)
            {
                AlarmInfo ai = (AlarmInfo)((GLItem)glacialList_UP.Items.SelectedItems[0]).Tag;
                Plot(ai);
            }
        }

        private void glacialList_Down_DoubleClick(object sender, EventArgs e)
        {
            if (glacialList_Down.Items.SelectedItems.Count > 0)
            {
                AlarmInfo ai = (AlarmInfo)((GLItem)glacialList_Down.Items.SelectedItems[0]).Tag;
                Plot(ai);
            }
        }
    }
}

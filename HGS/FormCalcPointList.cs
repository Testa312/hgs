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
        }
         ~FormCalcPointList()
        {
  
        }  
        public void glacialLisint(HashSet<int> Onlyid)
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
                    if (ptx.nd.Contains(tSCBNode.Text.Trim()) && flag &&
                        ptx.pn.Contains(tSTBPN.Text.Trim()))
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
            glacialLisint(onlyid);
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
                glacialLisint(onlyid);
                glacialList.Invalidate();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}

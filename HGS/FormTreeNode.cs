using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HGS
{
    public partial class FormTreeNode : Form
    {
        public TreeTag tt = null;
        public FormTreeNode(TreeTag tt)
        {
            this.tt = tt;
            InitializeComponent();
            InitializeFromTreeTag();
        }
        private void InitializeFromTreeTag()
        {
            if (tt != null) //throw new System.ArgumentException("Parameter cannot be null", "tt");
            {
                tB_nodeName.Text = tt.nodeName;
                mtb_start_th.Text = tt.start_th.ToString();
                mtb_alarm_th_dis.Text = tt.alarm_th_dis.ToString();
                mtb_sort.Text = tt.sort.ToString();
            }
            else
                tt = new TreeTag();
        }
        private void BtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                tt.nodeName = tB_nodeName.Text.Trim();
                if (tt.nodeName.Length == 0)
                    throw new Exception("节点名不能为空！");
                if (mtb_start_th.Text.Length > 0)
                {
                    tt.start_th = float.Parse(mtb_start_th.Text);
                }
                else
                    tt.start_th = null;
                //
                if (mtb_alarm_th_dis.Text.Length > 0)
                {
                    tt.alarm_th_dis = float.Parse(mtb_alarm_th_dis.Text);
                }
                else
                    tt.alarm_th_dis = null;
                //
                if (mtb_sort.Text.Length > 0)
                {
                    tt.sort = int.Parse(mtb_sort.Text);
                }
                else
                    tt.sort = -1;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}
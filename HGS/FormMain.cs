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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            //
            CalcEngine.CalcEngine cd = new CalcEngine.CalcEngine();
            Data.inst().LoadFromPG();
        }

        private void form1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPointSet frm1 = new FormPointSet();     //创建form2窗体的对象

            frm1.MdiParent = this;       //设置mdiparent属性，将当前窗体作为父窗体
            frm1.WindowState = FormWindowState.Maximized;
            frm1.Show();
        }

        private void 点表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSisPointList frm1 = new FormSisPointList();     //创建form2窗体的对象

            frm1.MdiParent = this;       //设置mdiparent属性，将当前窗体作为父窗体
            frm1.WindowState = FormWindowState.Maximized;
            frm1.Show();
        }

        private void 测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTestCE frm1 = new FormTestCE();     //创建form2窗体的对象

            frm1.MdiParent = this;       //设置mdiparent属性，将当前窗体作为父窗体
            frm1.WindowState = FormWindowState.Maximized;
            frm1.Show();
        }

        private void 计算点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCalcPointSet frm1 = new FormCalcPointSet();     //创建form2窗体的对象

            frm1.MdiParent = this;       //设置mdiparent属性，将当前窗体作为父窗体
            frm1.WindowState = FormWindowState.Maximized;
            frm1.Show();
        }

        private void testLuaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTestpgDataset frm1 = new FormTestpgDataset();     //创建form2窗体的对象

            frm1.MdiParent = this;       //设置mdiparent属性，将当前窗体作为父窗体
            frm1.WindowState = FormWindowState.Maximized;
            frm1.Show();
        }
        //登录窗口
        private void FormMain_Shown(object sender, EventArgs e)
        {
            //FormLogin fl = new FormLogin();
            //if (DialogResult.Cancel == fl.ShowDialog()) this.Close();
            //this.Text = string.Format("HGS-{0}", Auth.GetInst().UserName);

            FormPointSet frm1 = new FormPointSet();     //创建form2窗体的对象

            frm1.MdiParent = this;       //设置mdiparent属性，将当前窗体作为父窗体
            frm1.WindowState = FormWindowState.Maximized;
            frm1.Show();

            FormAlarmSet frm2 = new FormAlarmSet();     //创建form2窗体的对象

            frm2.MdiParent = this;       //设置mdiparent属性，将当前窗体作为父窗体
            frm2.WindowState = FormWindowState.Maximized;
            frm2.Show();
        }
        private void 报警信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAlarmSet frm1 = new FormAlarmSet();     //创建form2窗体的对象

            frm1.MdiParent = this;       //设置mdiparent属性，将当前窗体作为父窗体
            frm1.WindowState = FormWindowState.Maximized;
            frm1.Show();
        }
    }
}

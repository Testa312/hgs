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
            Data.Get().LoadFromPG();
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
    }
}

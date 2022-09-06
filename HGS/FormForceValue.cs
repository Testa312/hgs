using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HGS
{
    public partial class FormForceValue : Form
    {
        public FormForceValue()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDouble(textBoxValue.Text);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }

        private void FormForceValue_Activated(object sender, EventArgs e)
        {
            textBoxValue.Focus();
            textBoxValue.SelectionStart = textBoxValue.Text.Length;
        }
    }
}

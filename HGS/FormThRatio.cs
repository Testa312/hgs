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
    public partial class FormThRatio : Form
    {
        public float ratio = 1.1f;
        public bool isMulti = true;
        public FormThRatio(bool  bLL)
        {          
            InitializeComponent();
            maskedTextBox1.Text = ratio.ToString();
            if (bLL)
            {
                ratio = 0.9f;
                radioButtonAdd.Text = "减";
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            ratio = float.Parse(maskedTextBox1.Text.Trim());
            isMulti = radioButtonMulti.Checked;
            if (isMulti && Math.Abs(ratio) < 1e-3)
            {
                MessageBox.Show(string.Format("倍率[{0}]太小！",ratio));
                DialogResult = DialogResult.None;
            }
                
        }
    }
}

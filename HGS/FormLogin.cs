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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            comboBoxUser.DataSource = Auth.GetInst().GetUser();
            label_hint.Text = "";
        }
        private void UserAuthor()
        {
            
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            int id = comboBoxUser.SelectedIndex;
            if (Auth.GetInst().UserAuthorization(id, textBoxPW.Text))
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                return;
            }
            label_hint.Text = "密码输入错误！";
            textBoxPW.Text = "";
            this.DialogResult = System.Windows.Forms.DialogResult.None;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}

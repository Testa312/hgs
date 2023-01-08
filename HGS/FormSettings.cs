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
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
            maskedTextBox_PG_IP.Text = Pref.Inst().pgHost;
            maskedTextBox_PG_PORT.Text = Pref.Inst().pgPort;
            textBox_PG_UserName.Text = Pref.Inst().pgUserName;
            textBox_PG_PW.Text = Pref.Inst().pgPassword;

            //
            maskedTextBox_SIS_IP.Text = Pref.Inst().sisHost;
            maskedTextBox_SIS_PORT.Text = Pref.Inst().sisPort.ToString();
            textBox_SIS_UserName.Text = Pref.Inst().sisUser;
            textBox_SIS_PW.Text = Pref.Inst().sisPassword;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                Pref.Inst().pgHost = maskedTextBox_PG_IP.Text.Trim();
                Pref.Inst().pgPort = maskedTextBox_PG_PORT.Text.Trim();
                Pref.Inst().pgUserName = textBox_PG_UserName.Text;
                Pref.Inst().pgPassword = textBox_PG_PW.Text;


                Pref.Inst().sisHost = maskedTextBox_SIS_IP.Text.Trim();
                Pref.Inst().sisPort = int.Parse(maskedTextBox_SIS_PORT.Text.Trim());
                Pref.Inst().sisUser = textBox_SIS_UserName.Text;
                Pref.Inst().sisPassword = textBox_SIS_PW.Text;
                
                XmlSettings settings1 = new XmlSettings();
                settings1.SetValue(cnstXml.pgHost, Pref.Inst().pgHost);
                settings1.SetValue(cnstXml.pgPort, Pref.Inst().pgPort);
                settings1.SetValue(cnstXml.pgUserName, Pref.Inst().pgUserName);
                settings1.SetValue(cnstXml.pgPassWord, Pref.Inst().pgPassword);


                settings1.SetValue(cnstXml.sisHost, Pref.Inst().sisHost);
                settings1.SetValue(cnstXml.sisPort, Pref.Inst().sisPort);
                settings1.SetValue(cnstXml.sisUserName, Pref.Inst().sisUser);
                settings1.SetValue(cnstXml.sisPassWord, Pref.Inst().sisPassword);
                settings1.Save("settings.cfg");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

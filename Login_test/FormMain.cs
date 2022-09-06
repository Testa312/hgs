using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
namespace Login_test
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        private void FormMain_Shown(object sender, EventArgs e)
        {
            //FormLogin fl = new FormLogin();
            //fl.ShowDialog(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch sW = new Stopwatch();
            HashSet<int> test = new HashSet<int>();
            var rand = new Random();
            sW.Start();

            for (int i = 0; i < 1000000;i++)
            {
                test.Add(rand.Next(100000));
            }
            sW.Stop();
            textBoxRsl.Text = sW.ElapsedMilliseconds.ToString();
           // textBoxRsl.Text = test.Count.ToString();


            sW.Start();
            for (int i = 0; i < 10000; i++)
            {
                bool j;
                j = test.Contains(i);
            }
            sW.Stop();

            textBox2.Text = sW.ElapsedMilliseconds.ToString();

        }
    }
}

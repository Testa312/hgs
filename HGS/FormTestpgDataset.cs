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
using System.Text.RegularExpressions;
namespace HGS
{
    public partial class FormTestpgDataset : Form
    {
        DataSet dsPoint = new DataSet();
        public FormTestpgDataset()
        {
            InitializeComponent();
            //
           
            //
           
            //string strsql = "select * from point,formula_point where point.id=formula_point.id";
            string strsql = "select point.id,formula_point.pointid from point,formula_point where point.id = formula_point.id";
            
            //string strsql = "select * from point";
            NpgsqlDataAdapter daPoint = new NpgsqlDataAdapter(strsql, Pref.Inst().pgConnString);
            daPoint.Fill(dsPoint);
            //
            dataGridView1.DataSource = dsPoint.Tables[0];

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxStr.Text.Length == 0) return;
            //string strexp = string.Format("id1={0}", textBox1.Text);
            string strexp = string.Format("id1={0}", textBoxStr.Text);
            DataRow[] frow = dsPoint.Tables[0].Select(strexp);
            StringBuilder sbx = new StringBuilder();
            foreach (DataRow dr in frow)
            {
                StringBuilder sb = new StringBuilder();
                object[] oba = dr.ItemArray;
                for (int i = 0; i < oba.Length;i++)
                {
                   
                    sb.Append(oba[i].ToString());
                }
                sbx.AppendLine(sb.ToString());

            }
            richTextBox1.Text = sbx.ToString();
        }
        private int AbovePrimes(int n)
        {

            int i=  (n % 2) == 0 ? ++n : n + 2; 
            int j = 0;
            for(; i <= 100003; i = i + 2)
            {
                int k = (int)Math.Sqrt(i);
                for (j = 2; j <= k; j++)
                {
                    if ((i % j) == 0)
                    {
                        break;
                    }
                }

                if (j > k)
                {
                    return i;
                }
            }
            //最大100003
            return 100003;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 1000; i++)
            {
                sb.AppendLine(string.Format("{0}----{1}", i, AbovePrimes(i)));
            }
            richTextBox1.Text = sb.ToString();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                double db;
                double dbb = Convert.ToDouble(null);
            }
            catch(Exception  ee)
            {
                MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            string input = textBoxStr.Text;
            string pattern = textBoxPatten.Text;
            //[a-zA-Z_]+\w+
            foreach (Match match in Regex.Matches(input, pattern))
                sb.AppendLine(match.Value);
            richTextBox1.Text = sb.ToString();

        }
    }
}

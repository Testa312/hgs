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
using System.IO;
using CalcEngine;
namespace HGS
{
    public partial class FormTestCE : Form
    {
        public FormTestCE()
        {
            InitializeComponent();
        }
        private void buttonTestCE_Click(object sender, EventArgs e)
        {
            CalcEngine.CalcEngine ce = new CalcEngine.CalcEngine();
            const string ip = "10.122.18.31";
            const int port = 8200;

            OPAPI.Connect conn = new OPAPI.Connect(ip, port, 60, "sis", "openplant");//建立连接

            string sql = "select ID,DS,GN,AV from Realtime";
            OPAPI.ResultSet resultSet = conn.executeQuery(sql);//执行SQL
            long total = 0;
            //const int flag = 155752;
            const int count = 100000;
            try
            {
                string[] sv = new string[count + 1];
                Stopwatch sw = new Stopwatch();
                StringBuilder sbx = new StringBuilder();
                StringBuilder sbxxx = new StringBuilder();
                sbxxx.Append("9092");
                double ob = 0;
                Random r = new Random();
                while (resultSet.next())//next()执行一次，游标下移一行.总点数155752个。
                {
                    string tmx = resultSet.getInt(0).ToString();
                    string tm = string.Format("a{0}", tmx);
                    ce.Variables.Add(tm, resultSet.getDouble(3));
                    sv[total] = tm;
                    if (total < count)
                    {
                        sbxxx.Append(",");
                        sbxxx.Append(tmx);
                        total++;
                    }
                }
                //性能测试
                StringBuilder sb = new StringBuilder();
             
                CalcEngine.Expression[] cep  = new CalcEngine.Expression[count];
                int number = r.Next(0, count-1);
                sb.Append(sv[number]);
                for (int m = 0; m < count; m++)
                {
                    sb.Clear();
                    for (int j = 0; j < 15; j++)
                    {
                        number = r.Next(0, count-1);
                        sb.Append("+");
                        sb.Append(sv[number]);
                    }
                    cep[m] = ce.Parse(sb.ToString());
                    ob += (double)cep[m].Evaluate();
                    textBoxError.Text = ob.ToString();
                }
                sw.Start();
                {
                    string sqlx = string.Format("select ID,DS,GN,AV from Realtime where ID in ({0})",sbxxx);
                    resultSet = conn.executeQuery(sqlx);//执行SQL
                    while (resultSet.next())//next()执行一次，游标下移一行.变量总数155752个。
                    {
                        string tm = string.Format("a{0}", resultSet.getInt(0).ToString());
                        ce.Variables[tm] = resultSet.getDouble(3);
                    }
                    for (int m = 0; m < count; m++)
                    {
                        ob += (double)cep[m].Evaluate();
                    }
                }
                sw.Stop();
                textBoxCE.Text = sw.ElapsedMilliseconds.ToString();
                textBoxError.Text += ob.ToString();
            }
            catch (Exception ee)
            {
                //Console.WriteLine("error:{0}", ee.Message);
                textBoxError.Text = ee.Message;
            }
            finally
            {
                if (resultSet != null)
                {
                    resultSet.close(); //释放内存
                }
            }
            //textBoxCE.Text = total.ToString();
            
            conn.close(); //关闭连接，千万要记住！！！
        }

        private void buttonTest2_Click(object sender, EventArgs e)
        {
            string f1 = "4 * (4 * Atan(1 / 5.0) - Atan(1 / 239.0)) + a10000000 + b10000000";
            string f2 = "Abs(Sin(Sqrt(a10000000 * a10000000 + b10000000 * b10000000)) * 255)";
            string f3 = "Abs(Sin(Sqrt(a10000000 ^ 2 + b10000000 ^ 2)) * 255)";
            CalcEngine.CalcEngine ce = new CalcEngine.CalcEngine();
            ce.Variables.Add("a10000000", 2);
            ce.Variables.Add("b10000000", 4);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 1; i < 500000; i++)
            {
                object ob = ce.Evaluate(f1).ToString();
                ob = ce.Evaluate(f2).ToString();
                ob = ce.Evaluate(f3).ToString();
            }
            sw.Stop();
            textBoxCE.Text = sw.ElapsedMilliseconds.ToString();
        }

        private void buttonbool_Click(object sender, EventArgs e)
        {
            CalcEngine.CalcEngine ce = new CalcEngine.CalcEngine();
        }
    }
}

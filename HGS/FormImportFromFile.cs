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
using System.IO;
namespace HGS
{
    public partial class FormImportFromFile : Form
    {
        public List<point> lspt = new List<point>();
        private Dictionary<string, point> dicpt = new Dictionary<string, point>();

        OPAPI.Connect sisconn = new OPAPI.Connect(Pref.Inst().sisHost, Pref.Inst().sisPort, 60,
           Pref.Inst().sisUser, Pref.Inst().sisPassword);//建立连接
        public FormImportFromFile()
        {
            InitializeComponent();
        }
        ~FormImportFromFile()
        {
            sisconn.close();
        }
        Dictionary<string, int> dicnodeid = new Dictionary<string, int>();
        //初始化node列表框。
        private void findnode()
        {
            comboBoxND.Items.Clear();
            string sql = "select ID,PN  from Node";
            OPAPI.ResultSet resultSet = sisconn.executeQuery(sql);//执行SQL
            try
            {
                while (resultSet.next())//next()执行一次，游标下移一行
                {
                    string pn = resultSet.getString(1);
                    dicnodeid[pn] = resultSet.getInt(0);
                    comboBoxND.Items.Add(pn.Trim());

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                this.Close();
            }
            finally
            {
                if (resultSet != null)
                {
                    resultSet.close(); //释放内存
                }
            }
            // tSCBNode.SelectedIndex = 0;
            //conn.close(); //关闭连接，千万要记住！！！
        }
        private int findSisid()
        {
            int num = 0;
            dicpt.Clear();
            StringBuilder sb = new StringBuilder();
            for(int i= 0;i<lspt.Count-1;i++)
            {
                sb.Append(string.Format("'{0}',",lspt[i].pn));
                dicpt.Add(lspt[i].pn, lspt[i]);
            }
            if (lspt.Count > 0)
            {
                sb.Append(string.Format("'{0}'", lspt[lspt.Count - 1].pn));
                dicpt.Add(lspt[lspt.Count - 1].pn, lspt[lspt.Count - 1]);
            }

            string sql = string.Format("select ID,PN,EU,FM,ND from Point where nd ={0} and pn in ({1})", 
                dicnodeid[comboBoxND.SelectedItem.ToString()], sb.ToString());
            //bug:如果包含有汉字，查询结果为空。
            OPAPI.ResultSet resultSet = sisconn.executeQuery(sql);//执行SQL
            try
            {
                List<GLItem> lsItem = new List<GLItem>();
                Cursor = Cursors.WaitCursor;
                while (resultSet.next())//next()执行一次，游标下移一行
                {
                    string pn = resultSet.getString(1);
                    point pt = dicpt[pn];
                    pt.pn = pn;
                    pt.id_sis = resultSet.getInt(0);
                    pt.fm = (short)resultSet.getInt(3);
                    pt.eu = resultSet.getString(2);
                    pt.nd = comboBoxND.Text;
                    num++;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                this.Close();
            }
            finally
            {
                if (resultSet != null)
                {
                    resultSet.close(); //释放内存
                }
                Cursor = Cursors.Default;
            }
            return num;
        }
private void buttonOpen_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = openFileDialog.FileName;
                string[] lines = File.ReadAllLines(filePath,Encoding.Default);
                List<GLItem> lsItem = new List<GLItem>();
                lspt.Clear();
               
                glacialList1.Items.Clear();
                int c = 0;
                
                foreach (string l in lines)
                {
                    string[] values = l.Split(',');
                    if (values.Length < 9) continue;
                    if (values[0].Contains("测点")) continue;

                    GLItem itemn = new GLItem(glacialList1);
                    lsItem.Add(itemn);
                    point pt = new point();
                    double max,min;
                    if (checkBox_min.Checked && double.TryParse(values[3], out min))
                    {
                        pt.ll = min;
                        itemn.SubItems["Min"].Text = min.ToString(); 
                    }
                    if (checkBox_min.Checked && double.TryParse(values[5], out max))
                    {
                        pt.hl = max;
                        itemn.SubItems["Max"].Text = max.ToString();
                    }
                    pt.pn  = itemn.SubItems["PN"].Text =values[0];
                    pt.ed = itemn.SubItems["ED"].Text = values[1];
                    //
                    pt.isavalarm = true;
                    pt.pointsrc = pointsrc.sis;
                    //pt.nd = comboBoxND.Text;
                    pt.ownerid = Auth.GetInst().LoginID;
                    lspt.Add(pt);
                    c++;
                }
                glacialList1.Items.AddRange(lsItem.ToArray());
                glacialList1.Invalidate();
                toolStripStatusLabel1.Text = "点数：" + c.ToString();
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (comboBoxND.Text.Trim().Length == 0)
            {
                MessageBox.Show("节点不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }
            if(lspt.Count != findSisid())
            {
                if (DialogResult.No == MessageBox.Show("节点选择可能错误,是否继续？", "错误",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }
            }
            Close();
        }

        private void FormImportFromFile_Shown(object sender, EventArgs e)
        {
            findnode();
            //if (comboBoxND.Items.Count > 0) comboBoxND.SelectedIndex = 0;
            comboBoxND.SelectedIndex = 0;
        }

        private void FormImportFromFile_FormClosed(object sender, FormClosedEventArgs e)
        {
            sisconn.close();
        }
    }
}

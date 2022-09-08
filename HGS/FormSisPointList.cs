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
using System.Diagnostics;
using OpenPlant = System.IntPtr;

namespace HGS
{
    public partial class FormSisPointList : Form
    {
        OPAPI.Connect sisconn = new OPAPI.Connect(Pref.Inst().sisHost, Pref.Inst().sisPort, 60, 
            Pref.Inst().sisUser, Pref.Inst().sisPassword);//建立连接
        public HashSet<int> onlysisid;
        public FormSisPointList()
        {
            InitializeComponent();
        }
         ~FormSisPointList()
        {
            sisconn.close();
        }
        Dictionary<string, string> dicnodeid = new Dictionary<string, string>();
        //初始化node列表框。
        private void findnode()
        {
            tSCBNode.Items.Clear();
            string sql = "select ID,PN  from Node";
            OPAPI.ResultSet resultSet = sisconn.executeQuery(sql);//执行SQL
            try
            {
                while (resultSet.next())//next()执行一次，游标下移一行
                {
                    string pn = resultSet.getObject(1).ToString();
                    dicnodeid[pn] = resultSet.getObject(0).ToString();
                    tSCBNode.Items.Add(pn.Trim());

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
        private void tSBFind_Click(object sender, EventArgs e)
        {
            string wh = string.Format("ND = {0}", dicnodeid[tSCBNode.SelectedItem.ToString()]); ;
            if (tSpCBRT.SelectedIndex == 1)
            {
                wh = string.Format("{0} and RT = 0", wh);

            }
            if (tSpCBRT.SelectedIndex > 1)
            {
                wh = string.Format("{0} and RT > 0", wh); 

            }
            if (tSTBPN.Text.Length > 0)
            {
                wh = string.Format("{0} and PN like '%{1}%'", wh, tSTBPN.Text);
            }
            string sql = string.Format("select PN,RT,EU,KR,ED,ID,FM from Point where {0}", wh);
            //bug:如果包含有汉字，查询结果为空。
            OPAPI.ResultSet resultSet = sisconn.executeQuery(sql);//执行SQL
            try
            {
                long total = 1;

                timer.Enabled = false;
                glacialList.Items.Clear();
                List<GLItem> lsItem = new List<GLItem>();
                Cursor = Cursors.WaitCursor;
                while (resultSet.next())//next()执行一次，游标下移一行
                {
                    string colValue = resultSet.getString(4);
                    int id = resultSet.getInt(5);
                    if (!colValue.Contains(tSTBED.Text) || onlysisid.Contains(id))
                    {
                        continue;
                    }
                    GLItem item = new GLItem(glacialList);
                    lsItem.Add(item);
                    //item.SubItems[0].Text = total.ToString();
                    item.SubItems["ED"].Text = colValue;

                    item.SubItems["PN"].Text = resultSet.getString(0);//速度较慢的原因
                    switch(resultSet.getByte(1))
                    {
                        case 0: item.SubItems["RT"].Text = "AX";break;
                        case 1: item.SubItems["RT"].Text = "DX"; break;
                        case 2: item.SubItems["RT"].Text = "I2"; break;
                        case 3: item.SubItems["RT"].Text = "I4"; break;
                        case 4: item.SubItems["RT"].Text = "R8"; break;
                        default: item.SubItems["RT"].Text = "Error"; break;
                    }         
                    item.SubItems["EU"].Text = resultSet.getString(2);
                    item.SubItems["KR"].Text = resultSet.getString(3);
                    itemtag it = new itemtag();
                    it.sisid = id;
                    it.fm = (byte)resultSet.getInt(6);
                    item.Tag = it;                   
                    total++;
                    /*
                    if (total > 5000)
                    {
                        MessageBox.Show("数量太多，筛选一下！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        while (resultSet.next()) ;//有bug，没有报错。
                            break;
                    }
                    */
                    //this.DialogResult = System.Windows.Forms.DialogResult.None;
                }
                glacialList.Items.AddRange(lsItem.ToArray());
                toolStripStatusLabel1.Text = string.Format("点数：{0}", total.ToString());
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
                timer.Enabled = true;
                Cursor = Cursors.Default;
            }
            // conn.close(); //关闭连接，千万要记住！！！
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (glacialList.Items.Count == 0) return;
            //if (!conn.isAlive()) return;
            Dictionary<string, GLItem> dic = new Dictionary<string, GLItem>();
            StringBuilder sbid = new StringBuilder();
            bool flag = false;
            foreach (GLItem item in glacialList.Items)
            {
                if (glacialList.IsItemVisible(item))
                {
                    sbid.Append(((itemtag)item.Tag).sisid.ToString());
                    sbid.Append(",");
                    dic.Add(((itemtag)item.Tag).sisid.ToString(), item);
                    flag = true;
                    continue;
                }
                if (flag) break;

            }
            if (sbid.Length > 0)
                sbid.Remove(sbid.Length - 1, 1);
            else return;
            string sql = string.Format("select ID,TM,DS,AV from Realtime where ID in ({0})", sbid.ToString());
            try
            {
                OPAPI.ResultSet resultSet = sisconn.executeQuery(sql);//执行SQL
                //const short gb1 = 512;
                const short gb1 = -32256;
                const short gb2 = -32768;
                while (resultSet.next())//next()执行一次，游标下移一行
                {
                    string colValue = resultSet.getInt(0).ToString();//获取第i列值
                    GLItem item = dic[colValue];
                    item.SubItems["AV"].Text = Math.Round(resultSet.getDouble(3), ((itemtag)item.Tag).fm).ToString();
                    short ds = resultSet.getShort(2);
                    GLSubItem sim = item.SubItems["DS"];
                    if ((ds & gb1) == 0)
                    {
                        sim.Text = "Good";
                    }
                    else if((ds & gb2) == gb2)
                    {
                        sim.Text = "Timeout";
                    }
                    else
                        sim.Text = "Bad";
                }
                if (resultSet != null)
                {
                    resultSet.close(); //释放内存
                }
            }
            finally
            {              
            }
           // conn.close(); //关闭连接，千万要记住！！！
        }

        private void FormSisPointList_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer.Enabled = false;
            sisconn.close();
        }

        private void FormSisPointList_Shown(object sender, EventArgs e)
        {
            findnode();
            if (tSCBNode.Items.Count > 0) tSCBNode.SelectedIndex = 0;
            tSpCBRT.SelectedIndex = 1;
        }
    }
}

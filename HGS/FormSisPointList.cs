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
        OPAPI.Connect sisconn = new OPAPI.Connect(Pref.GetInstance().sisHost, Pref.GetInstance().sisPort, 60, 
            Pref.GetInstance().sisUser, Pref.GetInstance().sisPassword);//建立连接
        public HashSet<int> onlysisid;
        public FormSisPointList()
        {
            InitializeComponent();
            findnode();
            tSpCBRT.SelectedIndex = 1;
        }
         ~FormSisPointList()
        {
            sisconn.close();
        }
        Dictionary<string, string> dicnodeid = new Dictionary<string, string>();
        //初始化node列表框。
        private void findnode()
        {
            string sql = "select ID,PN  from Node";
            OPAPI.ResultSet resultSet = sisconn.executeQuery(sql);//执行SQL
            try
            {
                while (resultSet.next())//next()执行一次，游标下移一行
                {
                    string pn = resultSet.getObject(1).ToString();
                    dicnodeid[pn] = resultSet.getObject(0).ToString();
                    tSCBNode.Items.Add(pn);

                }
            }
            catch (Exception ee)
            {
            }
            finally
            {
                if (resultSet != null)
                {
                    resultSet.close(); //释放内存
                }
            }
            tSCBNode.SelectedIndex = 0;
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

                while (resultSet.next())//next()执行一次，游标下移一行
                {
                    GLItem item;
                    string colValue = resultSet.getString(4);
                    int id = resultSet.getInt(5);
                    if (!colValue.Contains(tSCBED.Text) || onlysisid.Contains(id))
                    {
                        continue;
                    }                
                    item = glacialList.Items.Add("");                  
                    item.SubItems[0].Text = total.ToString();
                    item.SubItems[6].Text = colValue;

                    item.SubItems[1].Text = resultSet.getString(0);//速度较慢的原因
                    switch(resultSet.getByte(1))
                    {
                        case 0: item.SubItems[2].Text = "AX";break;
                        case 1: item.SubItems[2].Text = "DX"; break;
                        case 2: item.SubItems[2].Text = "I2"; break;
                        case 3: item.SubItems[2].Text = "I4"; break;
                        case 4: item.SubItems[2].Text = "R8"; break;
                        default: item.SubItems[2].Text = "Error"; break;
                    }         
                    item.SubItems[4].Text = resultSet.getString(2);
                    item.SubItems[5].Text = resultSet.getString(3);
                    itemtag it = new itemtag();
                    it.sisID = id;
                    it.FM = resultSet.getInt(6);
                    item.Tag = it;                   
                    total++;
                }
                toolStripStatusLabel1.Text = string.Format("点数：{0}", total.ToString());
                timer.Enabled = true;
            }
            catch (Exception ee)
            {
            }
            finally
            {
                if (resultSet != null)
                {
                    resultSet.close(); //释放内存
                }
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
                    sbid.Append(((itemtag)item.Tag).sisID.ToString());
                    sbid.Append(",");
                    dic.Add(((itemtag)item.Tag).sisID.ToString(), item);
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
                    item.SubItems[3].Text = Math.Round(resultSet.getDouble(3), ((itemtag)item.Tag).FM).ToString();
                    short ds = resultSet.getShort(2);
                    if ((ds & gb1) == 0)
                    {
                        item.SubItems[7].Text = "Good";
                    }
                    else if((ds & gb2) == gb2)
                    {
                        item.SubItems[7].Text = "Timeout";
                    }
                    else
                        item.SubItems[7].Text = "Bad";
                }
                if (resultSet != null)
                {
                    resultSet.close(); //释放内存
                }
            }
            catch (Exception ee)
            {
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
    }
}

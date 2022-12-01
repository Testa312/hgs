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

namespace HGS
{
    public partial class FormSisPointList : Form
    {
        OPAPI.Connect sisconn = new OPAPI.Connect(Pref.Inst().sisHost, Pref.Inst().sisPort, 60, 
            Pref.Inst().sisUser, Pref.Inst().sisPassword);//建立连接
        //public Dictionary<int, GLItem> onlysisid = new Dictionary<int, GLItem>();
        public List<point> lsitem = new List<point>();
        public FormSisPointList()
        {
            InitializeComponent();
            findnode();
        }
         ~FormSisPointList()
        {
            sisconn.close();
        }
        Dictionary<string, int> dicnodeid = new Dictionary<string, int>();
        //初始化node列表框。
        private void findnode()
        {
            listBox_Node.Items.Clear();
            string sql = "select ID,PN  from Node order by PN";
            OPAPI.ResultSet resultSet = sisconn.executeQuery(sql);//执行SQL
            try
            {
                while (resultSet.next())//next()执行一次，游标下移一行
                {
                    string pn = resultSet.getString(1);
                    dicnodeid[pn] = resultSet.getInt(0);
                    //tSCBNode.Items.Add(pn.Trim());
                    listBox_Node.Items.Add(pn.Trim());

                }
            }
            catch (Exception ee)
            {
                FormBugReport.ShowBug(ee);
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
            string wh = string.Format("ND = {0}", dicnodeid[listBox_Node.SelectedItem.ToString()]); 
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
            string sql = string.Format("select PN,RT,EU,KR,ED,ID,FM,TV,BV from Point where {0}", wh);
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
                    string ed = resultSet.getString(4);
                    int id = resultSet.getInt(5);
                    string[] filtes = tSTBED.Text.Split(' ');

                    bool flag = true;
                    for (int i = 0; i < filtes.Length; i++)
                    {
                        flag = flag && ed.Contains(filtes[i]);
                        if (!flag) break;
                    }

                    if (!flag || Data.inst().dic_SisIdtoPoint.ContainsKey(id))
                    {
                        continue;
                    }
                    GLItem item = new GLItem(glacialList);
                    lsItem.Add(item);
                    //item.SubItems[0].Text = total.ToString();
                    item.SubItems["ED"].Text = ed;

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
                    item.SubItems["TV"].Text = resultSet.getFloat(7).ToString();
                    item.SubItems["BV"].Text = resultSet.getFloat(8).ToString();
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
                glacialList.Invalidate();
            }
            catch (Exception ee)
            {
                FormBugReport.ShowBug(ee);
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
            tSpCBRT.SelectedIndex = 1;
            listBox_Node.SelectedIndex = 0;
            //if (tSCBNode.Items.Count > 0) 
              //  tSCBNode.SelectedIndex = 0;
            tSBFind_Click(null, null);

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            int ptid = Data.inst().GetNextPointId();
            foreach (GLItem item in glacialList.SelectedItems)
            {
                point Point = new point(ptid,pointsrc.sis);
                lsitem.Add(Point);

                Point.nd = listBox_Node.Text;
                Point.pn = item.SubItems["PN"].Text;
                Point.ed = item.SubItems["ED"].Text;
                Point.eu = item.SubItems["EU"].Text;

                string v = item.SubItems["TV"].Text;
                if (v.Length > 0)
                { Point.tv = double.Parse(v); }
                else Point.tv = null;

                v = item.SubItems["BV"].Text;
                if (v.Length > 0)
                { Point.bv = double.Parse(v); }
                else Point.bv = null;

                Point.Id_sis = ((itemtag)(item.Tag)).sisid;

                //Point.pointsrc = pointsrc.sis;
                Point.OwnerId = Auth.GetInst().LoginID;
                //
                ptid++;

            }
        }

        private void listBox_Node_Click(object sender, EventArgs e)
        {
            tSBFind_Click(null, null);
        }
    }
}

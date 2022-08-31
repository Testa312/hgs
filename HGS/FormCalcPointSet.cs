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
using Npgsql;
namespace HGS
{
    public partial class FormCalcPointSet : Form
    {
        OPAPI.Connect sisconn = new OPAPI.Connect(Pref.GetInstance().sisHost, Pref.GetInstance().sisPort, 60,
            Pref.GetInstance().sisUser, Pref.GetInstance().sisPassword);//建立连接
        HashSet<int> onlyid = new HashSet<int>();
       public point CalcPoint;
        //--------------------------------
        public FormCalcPointSet()
        {
            InitializeComponent();
        }
        private string getsid(pointsrc cc, long id)
        {
            switch (cc)
            {
                case pointsrc.sis: return "S" + id.ToString(); 
                case pointsrc.calc: return "C" + id.ToString();;
                default: throw new Exception("点计算优先级错误！"); 
            }
        }
        public void glacialLisint()
        {
            foreach(int ipt in CalcPoint.listSisCalaPointID)
            {
                GLItem itemn;
                itemtag it = new itemtag();
                itemn = glacialList1.Items.Add("");
                point Point = Data.Get().cd_Point[ipt];

                it.id = ipt;
               
                itemn.SubItems[1].Text = Point.nd;
                itemn.SubItems[2].Text = Point.pn;

                itemn.SubItems[4].Text = Point.eu;
                itemn.SubItems[5].Text = Point.ed;
                it.sisID = Point.id_sis;
                if (onlyid.Contains(CalcPoint.id)) throw new Exception("不能引用自身！");
                onlyid.Add(it.id);//唯一性
                //it.PointSrc = (pointsrc)pgreader.GetInt32(pgreader.GetOrdinal("pointsrc"));
                //if (!pgreader.IsDBNull(pgreader.GetOrdinal("ownerid")))
                  //  it.ownerid = (int)pgreader["ownerid"];
                //it.Formula = pgreader["formula"].ToString();
                //it.isNew = true;
                //
                itemn.SubItems[0].Text = getsid(Point.pointsrc, it.id);                
                itemn.Tag = it;
            }
        }
        private void FormPointSet_FormClosed(object sender, FormClosedEventArgs e)
        {
            glacialList1.Dispose();
            timer1.Enabled = false;
            sisconn.close();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (glacialList1.Items.Count == 0) return;
            //if (!conn.isAlive()) return;
            Dictionary<string, GLItem> dic = new Dictionary<string, GLItem>();
            StringBuilder sbid = new StringBuilder();
            bool flag = false;
            foreach (GLItem item in glacialList1.Items)
            {
                itemtag it = (itemtag)item.Tag;
                if (glacialList1.IsItemVisible(item)&&it.PointSrc == pointsrc.sis)
                {
                    sbid.Append(it.sisID.ToString());
                    sbid.Append(",");
                    dic.Add(it.sisID.ToString(), item);
                    flag = true;
                    continue;
                }
                flag = it.PointSrc == pointsrc.sis ? false : true;

            }
            if (sbid.Length > 0)
                sbid.Remove(sbid.Length - 1, 1);
            else return;
            string sql = string.Format("select ID,TM,DS,AV from Realtime where ID in ({0})", sbid.ToString());
            try
            {
                OPAPI.ResultSet resultSet =  sisconn.executeQuery(sql);//执行SQL
            
                const short gb1 = -32256;
                const short gb2 = -32768;
                while (resultSet.next())//next()执行一次，游标下移一行
                {
                    string colValue = resultSet.getInt(0).ToString();//获取第i列值
                    GLItem item = dic[colValue];
                    item.SubItems[3].Text = Math.Round(resultSet.getDouble(3), ((itemtag)item.Tag).FM).ToString();
                    short ds = resultSet.getShort(2);
                    string pas = "Bad";
                    if ((ds & gb1) == 0)
                    {
                        pas = "Good";
                    }
                    else if ((ds & gb2) == gb2)
                    {
                        pas = "Timeout";
                    }
                    item.SubItems[6].Text = pas;
                }
                if (resultSet != null)
                {
                    resultSet.close(); //释放内存
                }
            }
            catch (Exception ee)
            {
            }
        }
        private void glacialList1_Click(object sender, EventArgs e)
        {

            if (glacialList1.SelectedItems.Count == 1)
            {
                GLItem item = (GLItem)glacialList1.SelectedItems[0];
                textBoxFormula.SelectedText = item.SubItems[0].Text;
            }
        }
        private void toolStripButtonAdd_Click_1(object sender, EventArgs e)
        {
            FormCalcPointList fcpl = new FormCalcPointList();
            fcpl.glacialLisint(onlyid);
            if (fcpl.ShowDialog() == DialogResult.OK)
            {
                foreach (GLItem item in fcpl.glacialList.SelectedItems)
                {
                    if (!onlyid.Contains(((itemtag)(item.Tag)).sisID))
                    {
                        GLItem itemn;

                        itemn = glacialList1.Items.Add("");
                        itemn.SubItems[0].Text = item.SubItems[0].Text;
                        itemn.SubItems[1].Text = item.SubItems[1].Text;
                        itemn.SubItems[2].Text = item.SubItems[2].Text;
                        itemn.SubItems[5].Text = item.SubItems[5].Text;
                        itemn.SubItems[4].Text = item.SubItems[4].Text;
                        itemn.Tag = item.Tag;
                        //((itemtag)(itemn.Tag)).isNew = true;
                        //((itemtag)(itemn.Tag)).ownerid = Pref.GetInstance().LoginID;
                        //((itemtag)(itemn.Tag)).PointSrc = pointsrc.sis;
                        onlyid.Add(((itemtag)(itemn.Tag)).id);
                    }
                    else
                    {
                        MessageBox.Show(string.Format("点:{0}-{1}已存在！", item.SubItems[1].Text, item.SubItems[6].Text), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    };
                }
            }
        }
        private void glacialList1_Leave(object sender, EventArgs e)
        {
            foreach (GLItem item in glacialList1.Items)
            {
                item.Selected = false;
            }
        }
 
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (textBoxmDiscription.Text.Length < 1)
            {
                MessageBox.Show("计算的的描述不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = System.Windows.Forms.DialogResult.None;
            }
            CalcPoint.ed = textBoxmDiscription.Text;
            CalcPoint.formula = textBoxFormula.Text;
            CalcPoint.eu = comboBox_eu.Text;
            CalcPoint.ownerid = Pref.GetInstance().LoginID;
            CalcPoint.pointsrc = pointsrc.calc;
            CalcPoint.nd = Pref.GetInstance().CalcPointNodeName;

            CalcPoint.lsOrgCalcPointID.Clear();

            //Pref.GetInstance().cd_Point.Clear();
            foreach (GLItem item in glacialList1.Items)
            {
                itemtag it = (itemtag)item.Tag;
                CalcPoint.lsOrgCalcPointID.Add(it.id);
            }
            try
            {
                itemtag it = new itemtag();
                Data.Get().Add(CalcPoint);
                it.Point = CalcPoint;
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);               
            }
            finally
            {
                this.DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }
    }
}

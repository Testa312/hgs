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
using Npgsql;
namespace HGS
{
    public partial class FormCalcPointList : Form
    {
        OPAPI.Connect sisconn = new OPAPI.Connect(Pref.GetInst().sisHost, Pref.GetInst().sisPort, 60, 
            Pref.GetInst().sisUser, Pref.GetInst().sisPassword);//建立连接
        private HashSet<int> onlyid;
        public FormCalcPointList()
        {
            InitializeComponent();
            InitList();
        }
         ~FormCalcPointList()
        {
            sisconn.close();
        }
        //初始化节点列表。
        public void InitList()
        {
            try
            {
                var pgconn = new NpgsqlConnection(Pref.GetInst().pgConnString);
                pgconn.Open();
                //
                string strsql = "select distinct nd from point";
                var cmd = new NpgsqlCommand(strsql, pgconn);
                NpgsqlDataReader pgreader = cmd.ExecuteReader();
                while (pgreader.Read())
                {
                    tSCBNode.Items.Add(pgreader.GetString(0));
                }
                pgreader.Close();
                pgconn.Close();
                if (tSCBNode.Items.Count > 0) tSCBNode.SelectedIndex = 0;
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void glacialLisint(HashSet<int> Onlyid)
        {
            onlyid = Onlyid;
            try
            {
                var pgconn = new NpgsqlConnection(Pref.GetInst().pgConnString);
                pgconn.Open();
                StringBuilder sb = new StringBuilder();
                if (tSCBNode.Text.Length > 0)
                    sb.Append(string.Format(" nd = '{0}'", tSCBNode.Text));
             
                if (tSTBPN.Text.Length > 0 && sb.Length > 3)
                    sb.Append(string.Format(" and pn like '%{0}%'", tSTBPN.Text));
                else if (tSTBPN.Text.Length > 0)
                    sb.Append(string.Format(" pn like '%{0}%'", tSTBPN.Text));
                if (tSCBED.Text.Length > 0 && sb.Length > 3)
                    sb.Append(string.Format(" and ed like '%{0}%'", tSCBED.Text));
                else if (tSCBED.Text.Length > 0)
                    sb.Append(string.Format(" ed like '%{0}%'", tSCBED.Text));

                string strsql = "select * from point";
                if (sb.Length > 3)
                    strsql = string.Format("select * from point where {0}", sb.ToString());
                var cmd = new NpgsqlCommand(strsql, pgconn);
                NpgsqlDataReader pgreader = cmd.ExecuteReader();

                timer.Enabled = false;
                glacialList.Items.Clear();

                while (pgreader.Read())
                {
                    GLItem itemn;
                    itemtag it = new itemtag();
                    it.id = (int)pgreader["id"];
                    if (onlyid.Contains(it.id)) continue;

                    itemn = glacialList.Items.Add("");
                    it.id = (int)pgreader["id"];
                 
                    itemn.SubItems["ND"].Text = pgreader["nd"].ToString();
                    itemn.SubItems["PN"].Text = pgreader["pn"].ToString();

                    itemn.SubItems["EU"].Text = pgreader["eu"].ToString();
                    itemn.SubItems["ED"].Text = pgreader["ed"].ToString();

                    it.sisid = int.Parse(pgreader["id_sis"].ToString());

                    it.PointSrc = (pointsrc)pgreader.GetInt32(pgreader.GetOrdinal("pointsrc"));
                    //itemn.SubItems[0].Text = Pref.GetInst().GetVarName(it.PointSrc, it.id);                

                    itemn.Tag = it;
                }
                pgconn.Close();
                timer.Enabled = true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void tSBFind_Click(object sender, EventArgs e)
        {
            glacialLisint(onlyid);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            foreach (GLItem item in glacialList.Items)
            {
                if (glacialList.IsItemVisible(item))
                {
                    itemtag it = (itemtag)(item.Tag);
                    point pt = Data.Get().cd_Point[it.id];
                    item.SubItems["AV"].Text = pt.av.ToString();
                    item.SubItems["DS"].Text = pt.ps.ToString();
                }
            }
        }
        private void FormSisPointList_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer.Enabled = false;
            sisconn.close();
        }
    }
}

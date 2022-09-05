﻿using System;
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
        //OPAPI.Connect sisconn = new OPAPI.Connect(Pref.Inst().sisHost, Pref.Inst().sisPort, 60, 
            //Pref.Inst().sisUser, Pref.Inst().sisPassword);//建立连接
        private HashSet<int> onlyid;
        public FormCalcPointList()
        {
            InitializeComponent();
            //InitList();
        }
         ~FormCalcPointList()
        {
            //sisconn.close();
        }
        /*
        //初始化节点列表。
        public void InitList()
        {
            try
            {
                tSCBNode.Items.Add("");
               var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
                pgconn.Open();

                string strsql = "select distinct nd from point order by nd";
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
        }*/
        public void glacialLisint(HashSet<int> Onlyid)
        {
            onlyid = Onlyid;
            try
            {
                /*
                var pgconn = new NpgsqlConnection(Pref.GetInst().pgConnString);
                pgconn.Open();
     
                string strsql = string.Format("select * from point where nd like '%{0}%' and pn like '%{1}%' and ed like '%{2}%'", 
                    tSCBNode.Text.Trim(), tSTBPN.Text.Trim(), tSCBED.Text.Trim());

                var cmd = new NpgsqlCommand(strsql, pgconn);
                NpgsqlDataReader pgreader = cmd.ExecuteReader();
                */
                timer.Enabled = false;
                HashSet<string> hs_ND = new HashSet<string>();
                glacialList.Items.Clear();
                int count = 0;
                foreach (point ptx in Data.inst().hsAllPoint)
                {
                    //point Point = Data.Get().cd_Point[ipt];
                    if (ptx.nd.Contains(tSCBNode.Text.Trim()) && ptx.ed.Contains(tSCBED.Text.Trim()) &&
                        ptx.pn.Contains(tSTBPN.Text.Trim()))
                    {                    
                        if (onlyid.Contains(ptx.id)) continue;

                        GLItem itemn = glacialList.Items.Add("");
                        ;
                        itemtag it = new itemtag();
                        it.id = ptx.id;

                        itemn.SubItems["ND"].Text = ptx.nd;
                        itemn.SubItems["PN"].Text = ptx.pn;

                        itemn.SubItems["EU"].Text = ptx.eu;
                        itemn.SubItems["ED"].Text = ptx.ed;

                        it.sisid = ptx.id_sis;

                        it.PointSrc = ptx.pointsrc;         

                        itemn.Tag = it;

                        hs_ND.Add(ptx.nd);
                        count++;
                    }
                }
                //
                //tSCBNode.Items.Add("");
                tSCBNode.Items.Clear();
                foreach (string citem in hs_ND)
                {
                    tSCBNode.Items.Add(citem.Trim());
                }
                //if (tSCBNode.Items.Count > 0) tSCBNode.SelectedIndex = 1;

                tSSLabel_nums.Text = string.Format("点数：{0}", count);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            timer.Enabled = true;
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
                    point pt = Data.inst().cd_Point[it.id];
                    item.SubItems["AV"].Text = pt.av.ToString();
                    item.SubItems["DS"].Text = pt.ps.ToString();
                }
            }
        }
        private void FormSisPointList_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer.Enabled = false;
            //sisconn.close();
        }

        private void FormCalcPointList_Shown(object sender, EventArgs e)
        {
            if (tSCBNode.Items.Count > 0) tSCBNode.SelectedIndex = 0;
        }
    }
}

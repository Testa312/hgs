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
using GlacialComponents.Controls;
namespace HGS
{
    public partial class FormAlarmHistoryList : Form
    {
        public FormAlarmHistoryList()
        {
            InitializeComponent();
            dateTimePickerTo.Value = DateTime.Now.AddHours(1);
            dateTimePickerFrom.Value = DateTime.Now.AddDays(-1);
        }
        private void glacialListInit()
        {
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
            try
            {
                string sqlx = string.Format(" nd like'%{0}%' and pn like '%{1}%' and ed like '%{2}%'and (ownerid = 0 or ownerid = {3})",
                    tsTB_ND.Text, tsTB_PN.Text, tsTB_ED.Text, tsCB_class.SelectedIndex);
                string strsql = string.Format("select distinct on (point.id) point.id,nd,pn,ed,ownerid,alarminfo,datetime  " +
                   "from point,alarmhistory " +
                   "  where Point.id = alarmhistory.id and {0} order by point.id, datetime desc;", sqlx);

                glacialList_UP.Items.Clear();

                Cursor.Current = Cursors.WaitCursor;
                pgconn.Open();

                var cmd = new NpgsqlCommand(strsql, pgconn);
                NpgsqlDataReader pgreader = cmd.ExecuteReader();
                List<GLItem> lsItems = new List<GLItem>();
                while (pgreader.Read())
                {

                    GLItem itemn = new GLItem(glacialList_UP);
                    lsItems.Add(itemn);
                    itemn.Tag = (int)pgreader["id"];

                    itemn.SubItems["ND"].Text = pgreader["nd"].ToString();
                    itemn.SubItems["PN"].Text = pgreader["pn"].ToString();
                    itemn.SubItems["ED"].Text = pgreader["ed"].ToString();
                    itemn.SubItems["LastAlarmInfo"].Text = pgreader["alarminfo"].ToString();
                    itemn.SubItems["LastTime"].Text = pgreader["datetime"].ToString();
                }
                glacialList_UP.Items.AddRange(lsItems.ToArray());
                pgconn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                glacialList_Down.Items.Clear();
                glacialList_Down.Invalidate();
                pgconn.Close();
            }
        }
        private void toolStripButtonFind_Click(object sender, EventArgs e)
        {
            glacialListInit();
        }

        private void FormAlarmList_Shown(object sender, EventArgs e)
        {
            tsCB_class.Items.Clear();
            foreach (string it in Auth.GetInst().GetUser())
            {
                tsCB_class.Items.Add(it);
            }
            tsCB_class.SelectedIndex = Auth.GetInst().LoginID;
        }
        private void glacialList_UP_Click(object sender, EventArgs e)
        {
            if (glacialList_UP.SelectedItems.Count == 1)
            {
                var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    glacialList_UP.Enabled = false;
                    glacialList_Down.Items.Clear();
                    string tfrom = dateTimePickerFrom.Value.ToString("yyyy/M/d H:00:00");
                    string tto = dateTimePickerTo.Value.ToString("yyyy/M/d H:00:00");
                    pgconn.Open();
                    string strsql = string.Format("select point.id, eu, alarmav, alarminfo, datetime  from point, " +
                        "alarmhistory where datetime >= '{0}' and datetime <= '{1}' and point.id = alarmhistory.id and " +
                        "point.id = {2} order by datetime desc;", tfrom, tto,
                        ((int)((GLItem)glacialList_UP.SelectedItems[0]).Tag));
                    var cmd = new NpgsqlCommand(strsql,pgconn);
                    NpgsqlDataReader pgreader = cmd.ExecuteReader();
                    List<GLItem> lsItems = new List<GLItem>();
                    while (pgreader.Read())
                    {
                        GLItem itemn = new GLItem(glacialList_Down);
                        lsItems.Add(itemn);

                        itemn.SubItems["Time"].Text = pgreader["datetime"].ToString();
                        itemn.SubItems["AlarmInfo"].Text = pgreader["alarminfo"].ToString();
                        itemn.SubItems["eu"].Text = pgreader["eu"].ToString();
                        itemn.SubItems["AlarmAv"].Text = pgreader["alarmav"].ToString();
                    }
                    glacialList_Down.Items.AddRange(lsItems.ToArray());
                    glacialList_Down.Invalidate();
                    pgconn.Close();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                    glacialList_UP.Enabled = true;
                    pgconn.Close();
                }
            }
        }

        private void button1H_Click(object sender, EventArgs e)
        {
            dateTimePickerTo.Value = DateTime.Now.AddHours(1); ;
            dateTimePickerFrom.Value = DateTime.Now.AddHours(-1);
            glacialList_UP_Click(sender,e);
        }

        private void button2H_Click(object sender, EventArgs e)
        {
            dateTimePickerTo.Value = DateTime.Now.AddHours(1); ;
            dateTimePickerFrom.Value = DateTime.Now.AddHours(-2);
            glacialList_UP_Click(sender, e);
        }

        private void button8h_Click(object sender, EventArgs e)
        {
            dateTimePickerTo.Value = DateTime.Now.AddHours(1); ;
            dateTimePickerFrom.Value = DateTime.Now.AddHours(-8);
            glacialList_UP_Click(sender, e);
        }

        private void button1D_Click(object sender, EventArgs e)
        {
            dateTimePickerTo.Value = DateTime.Now.AddHours(1); ;
            dateTimePickerFrom.Value = DateTime.Now.AddDays(-1);
            glacialList_UP_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dateTimePickerTo.Value = DateTime.Now.AddHours(1); ;
            dateTimePickerFrom.Value = DateTime.Now.AddDays(-2);
            glacialList_UP_Click(sender, e);
        }

        private void button7D_Click(object sender, EventArgs e)
        {
            dateTimePickerTo.Value = DateTime.Now.AddHours(1); ;
            dateTimePickerFrom.Value = DateTime.Now.AddDays(-7);
            glacialList_UP_Click(sender, e);
        }
    }
}

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
namespace HGS
{
    public partial class FormAlarmSetList : Form
    {
        Dictionary<point, GLItem> dic_rec = new Dictionary<point, GLItem>();
        public FormAlarmSetList()
        {
            InitializeComponent();
        }
        private void timer_GetAlarm_Tick(object sender, EventArgs e)
        {
            int count = 0;
            HashSet<point> lss = AlarmSet.GetInst().ssAlarmPoint;
            List<GLItem> lsItems = new List<GLItem>();
            foreach (point pt in lss)
            {
                if ((pt.ownerid == tsCB_class.SelectedIndex || tsCB_class.SelectedIndex == 0) &&
                    pt.nd.Contains(tsCB_ND.Text.Trim()) && pt.ed.Contains(tsTB_ED.Text.Trim()) &&
                    pt.pn.Contains(tsTB_PN.Text.Trim()) && pt.alarmininfo.Contains(tsTB_AI.Text.Trim()))
                {
                    GLItem itemn;
                    if (!dic_rec.ContainsKey(pt))
                    {
                        GLItem itemx = new GLItem(glacialList1);
                        dic_rec.Add(pt, itemx);
                        itemx.Tag = pt;
                        itemx.SubItems["ND"].Text = pt.nd;
                        itemx.SubItems["PN"].Text = pt.pn;
                        lsItems.Add(itemx);
                    }
                    itemn = dic_rec[pt];
                    itemn.SubItems["ED"].Text = pt.ed;
                    itemn.SubItems["AlarmingAV"].Text = pt.alarmingav.ToString();
                    itemn.SubItems["AlarmInfo"].Text = pt.alarmininfo;
                    itemn.SubItems["Time"].Text = pt.lastalarmdatetime.ToString();
                    count++;
                }
            }
            glacialList1.Items.AddRange(lsItems.ToArray());
            List<GLItem> deleitem = new List<GLItem>(); 
            foreach(GLItem item in glacialList1.Items)
            {
                if(!AlarmSet.GetInst().ssAlarmPoint.Contains((point)item.Tag))
                    deleitem.Add(item);                   
            }
            foreach (GLItem item in deleitem)
            {
                glacialList1.Items.Remove(item);
                dic_rec.Remove((point)item.Tag);
            }
            deleitem.Clear();

            tSLabel_Nums.Text = string.Format("报警点数：{0}个", AlarmSet.GetInst().ssAlarmPoint.Count);
        }

        private void tsCB_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            dic_rec.Clear();
            glacialList1.Items.Clear();
            timer_GetAlarm_Tick(sender, e);
        }

        private void FormAlarmSet_Shown(object sender, EventArgs e)
        {
            tsCB_class.Items.Clear();
            foreach (string it in Auth.GetInst().GetUser())
            {
                tsCB_class.Items.Add(it);
            }
            tsCB_class.SelectedIndex = Auth.GetInst().LoginID;
        }
    }
}

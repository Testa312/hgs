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
    public partial class FormAlarmSet : Form
    {
        Dictionary<int, GLItem> dic_rec = new Dictionary<int, GLItem>();
        public FormAlarmSet()
        {
            InitializeComponent();
            foreach (string it in Auth.GetInst().GetUser())
            {
                tsCB_class.Items.Add(it);
            }
        }
        private void timer_GetAlarm_Tick(object sender, EventArgs e)
        {
            int count = 0;
            HashSet<point> lss = AlarmSet.GetInst().ssAlarmPoint;
            foreach (point pt in lss)
            {
                StringComparison comp = StringComparison.Ordinal;
                if ((pt.pointsrc == pointsrc.sis || pt.ownerid == tsCB_class.SelectedIndex) &&
                    pt.nd.Contains(tsCB_ND.Text.Trim()) && pt.ed.Contains(tsTB_ED.Text.Trim()) &&
                    pt.pn.Contains(tsTB_PN.Text.Trim()) && pt.alarmininfo.Contains(tsTB_AI.Text.Trim()))
                {
                    GLItem itemn;
                    if (!dic_rec.ContainsKey(pt.id))
                    {
                        itemn = glacialList1.Items.Add("");
                        dic_rec.Add(pt.id, itemn);
                        itemn.Tag = pt.id;
                    }
                    itemn = dic_rec[pt.id];

                    itemn.SubItems["ND"].Text = pt.nd;
                    itemn.SubItems["PN"].Text = pt.pn;

                    itemn.SubItems["ED"].Text = pt.ed;
                    itemn.SubItems["AlarmingAV"].Text = pt.alarmingav.ToString();
                    itemn.SubItems["AlarmInfo"].Text = pt.alarmininfo;
                    itemn.SubItems["Time"].Text = pt.lastalarmdatetime.ToString();
                    count++;
                }
            }
            List<GLItem> deleitem = new List<GLItem>(); 
            foreach(GLItem item in glacialList1.Items)
            {
                if(!AlarmSet.GetInst().ssAlarmPoint.Contains(Data.inst().cd_Point[(int)item.Tag]))
                    deleitem.Add(item);                   
            }
            foreach (GLItem item in deleitem)
            {
                glacialList1.Items.Remove(item);
                dic_rec.Remove((int)item.Tag);
            }
            deleitem.Clear();

            tSLabel_Nums.Text = string.Format("报警点数：{0}个", AlarmSet.GetInst().ssAlarmPoint.Count);
        }
    }
}

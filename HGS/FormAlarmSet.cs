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
        public FormAlarmSet()
        {
            InitializeComponent();
        }
        private void timer_GetAlarm_Tick(object sender, EventArgs e)
        {
            int count = 0;
            SortedSet<point> lss = AlarmSet.GetInst().ssAlarmPoint;
            glacialList1.Items.Clear();
            foreach (point pt in lss)
            {
                if (count > 2000) break;
                GLItem itemn = glacialList1.Items.Add("");
                ;
                itemn.SubItems["ND"].Text = pt.nd;
                itemn.SubItems["PN"].Text = pt.pn;

                itemn.SubItems["ED"].Text = pt.ed;
                itemn.SubItems["AlarmInfo"].Text = pt.alarmininfo;
                itemn.SubItems["Time"].Text = pt.lastalarmdatetime.ToString();
                count++;
            }
            tSLabel_Nums.Text = string.Format("报警点数：{0}个", AlarmSet.GetInst().ssAlarmPoint.Count);
        }
    }
}

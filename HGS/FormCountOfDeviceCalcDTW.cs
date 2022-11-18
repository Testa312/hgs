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
    public partial class FormCountOfDeviceCalcDTW : Form
    {
        public FormCountOfDeviceCalcDTW()
        {
            InitializeComponent();
            refresh();
        }
        private void refresh()
        {
            glacialList1.Items.Clear();
            List<GLItem> lsitem = new List<GLItem>();
            int[] ScanSpan = Pref.Inst().ScanSpan;//分钟
            foreach (DeviceInfo di in Data_Device.dic_Device.Values)
            {
                GLItem item = new GLItem(glacialList1);
                item.SubItems["ID"].Text = di.id.ToString();
                item.SubItems["PN"].Text = "设备";
                item.SubItems["ED"].Text = di.Name;
                item.SubItems["DTW"].Text = di.CountofDTWCalc.ToString();
                lsitem.Add(item);
                //
                for (int i = 0; i < ScanSpan.Length; i++)
                {
                    item.SubItems[string.Format("m{0}", ScanSpan[i])].Text = Math.Round(di.alarm_th_dis_max[i], 3).ToString();
                    item.SubItems[string.Format("m{0}s", ScanSpan[i])].Text = Math.Round(di.Alarm_th_dis[i], 3).ToString();
                }
                foreach (int sid in di.Sensors_set())
                {
                    item = new GLItem(glacialList1);
                    point sensor = Data.inst().cd_Point[sid];
                    item.SubItems["ID"].Text = sensor.id.ToString();
                    item.SubItems["PN"].Text = sensor.pn;
                    item.SubItems["ED"].Text = sensor.ed;
                    for (int i = 0; i < ScanSpan.Length; i++)
                    {
                        item.SubItems[string.Format("m{0}", ScanSpan[i])].Text = Math.Round(sensor.dtw_start_max[i], 3).ToString();
                        item.SubItems[string.Format("m{0}s", ScanSpan[i])].Text = Math.Round(sensor.Dtw_start_th[i], 3).ToString();
                    }
                    lsitem.Add(item);
                }
            }
            glacialList1.Items.AddRange(lsitem.ToArray());
            glacialList1.Invalidate();
        }

        private void toolStripButton_refresh_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            refresh();
        }
    }
}

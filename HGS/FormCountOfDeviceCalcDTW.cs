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
        Dictionary<int, GLItem> dic_device = new Dictionary<int, GLItem>();
        Dictionary<int, GLItem> dic_sensor = new Dictionary<int, GLItem>();
        public FormCountOfDeviceCalcDTW()
        {
            InitializeComponent();
            refresh_dev();
        }
        private void refresh_dev()
        {
            //glacialList1.Items.Clear();
            List<GLItem> lsitem = new List<GLItem>();
            int[] ScanSpan = Pref.Inst().ScanSpan;//分钟
            foreach (DeviceInfo di in Data_Device.dic_Device.Values)
            {
                GLItem item;
                if (!dic_device.TryGetValue(di.id, out item))
                {                    
                    item = new GLItem(glacialList_dev);
                    item.SubItems["ID"].Text = di.id.ToString();
                    item.SubItems["PN"].Text = "设备";
                    item.SubItems["ED"].Text = di.Name;
                    lsitem.Add(item);
                    dic_device.Add(di.id, item);
                    item.Tag = di;
                }
                item.SubItems["DTW"].Text = di.CountofDTWCalc.ToString();
                             
                for (int i = 0; i < ScanSpan.Length; i++)
                {
                    item.SubItems[string.Format("m{0}", ScanSpan[i])].Text = Math.Round(di.alarm_th_dis_max[i], 3).ToString();
                    if (di.Alarm_th_dis != null)
                        item.SubItems[string.Format("m{0}s", ScanSpan[i])].Text = Math.Round(di.Alarm_th_dis[i], 3).ToString();
                }
                
            }
            glacialList_dev.Items.AddRange(lsitem.ToArray());
            glacialList_dev.Invalidate();
        }

        private void toolStripButton_refresh_Click(object sender, EventArgs e)
        {
            glacialList_sensor.Items.Clear();
            glacialList_dev.Items.Clear();
            dic_device.Clear();
            dic_sensor.Clear();
            refresh_dev();
            refresh_sensors();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            refresh_dev();
            refresh_sensors();
        }
        private void refresh_sensors()
        {
            if (glacialList_dev.SelectedItems.Count == 1)
            {
                List<GLItem> lsitem = new List<GLItem>();
                int[] ScanSpan = Pref.Inst().ScanSpan;//分钟
                DeviceInfo di = (DeviceInfo)((GLItem)(glacialList_dev.SelectedItems[0])).Tag;
                foreach (int sid in di.Sensors_set())
                {

                    GLItem item = new GLItem(glacialList_sensor);
                    point sensor = Data.inst().cd_Point[sid];
                    if (!dic_sensor.TryGetValue(sid, out item))
                    {
                        item = new GLItem(glacialList_sensor);
                        item.SubItems["ID"].Text = sensor.id.ToString();
                        item.SubItems["PN"].Text = sensor.pn;
                        item.SubItems["ED"].Text = sensor.ed;
                        lsitem.Add(item);
                        dic_sensor.Add(sid, item);
                    }
                    for (int i = 0; i < ScanSpan.Length; i++)
                    {
                        item.SubItems[string.Format("m{0}", ScanSpan[i])].Text = Math.Round(sensor.dtw_skip_max[i], 3).ToString();
                        if (sensor.Dtw_skip_th != null)
                            item.SubItems[string.Format("m{0}s", ScanSpan[i])].Text = Math.Round(sensor.Dtw_skip_th[i], 3).ToString();
                    }
                }
                glacialList_sensor.Items.AddRange(lsitem.ToArray());
                glacialList_sensor.Invalidate();
            }
        }
        private void glacialList_dev_Click(object sender, EventArgs e)
        {
            dic_sensor.Clear();
            glacialList_sensor.Items.Clear();
            refresh_sensors();
        }
    }
}

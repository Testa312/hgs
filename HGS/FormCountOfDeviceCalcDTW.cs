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
            foreach (DeviceInfo di in Data_Device.dic_Device.Values)
            {
                GLItem item = new GLItem(glacialList1);
                item.SubItems["ID"].Text = di.id.ToString();
                item.SubItems["DN"].Text = di.Name;
                item.SubItems["DTW"].Text = di.CountofDTWCalc.ToString();
                lsitem.Add(item);
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Globalization;
using OxyPlot.Annotations;
using System.Diagnostics;
namespace HGS
{
    public partial class FormThSet : Form
    {
        private TreeTag ttg;
        DateTimePicker dateTimePicker1 = new DateTimePicker();
        DateTimePicker dateTimePicker2 = new DateTimePicker();
        public FormThSet(TreeTag ttg)
        {
            InitializeComponent();
            this.ttg = ttg;
            //
            const string sformat = "yyyy-MM-dd HH:mm:ss";
            Size size = new Size(159, 21);
            //

            dateTimePicker2.Value = Data.GetSisSystemTime().AddSeconds(-5);
            dateTimePicker2.Size = size;
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = sformat;
            //
            dateTimePicker1.Value = dateTimePicker1.Value.AddDays(-1);
            dateTimePicker1.Size = size;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = sformat;
            //           
            ToolStripControlHost host1 = new ToolStripControlHost(dateTimePicker1);
            host1 = new ToolStripControlHost(dateTimePicker1);
            toolStrip1.Items.Insert(1, host1);
            //
            ToolStripControlHost host2 = new ToolStripControlHost(dateTimePicker2);
            host1 = new ToolStripControlHost(dateTimePicker2);
            toolStrip1.Items.Insert(3, host1);
        }
        private PlotModel PlotPoint(Dictionary<int, PointData> dic_pd)
        {
            //
            if (dic_pd == null || dic_pd.Count <= 0) return null;
            string title = "";
           /* List<DateValue> lsdv = dic_pd.Values;
            if (lsdv.Count > 0)
                title = string.Format("{0}---{1}  [{2}]", lsdv[lsdv.Count - 1].Date, lsdv[0].Date,
                                        (lsdv[0].Date - lsdv[lsdv.Count - 1].Date));
           */
            var pm = new PlotModel
            {
                Title = title,
                PlotType = PlotType.XY,
                Background = OxyColors.White
            };
            double maxdv = double.MinValue;
            double mindv = double.MaxValue;

            foreach (PointData pd in dic_pd.Values)
            {
                maxdv = Math.Max(maxdv, pd.MaxAv);
                mindv = Math.Min(mindv, pd.MinAv);
            }
            var dateTimeAxis1 = new DateTimeAxis
            {
                CalendarWeekRule = CalendarWeekRule.FirstFourDayWeek,
                FirstDayOfWeek = DayOfWeek.Monday,
                Position = AxisPosition.Bottom,
                //Minimum = 0
            };
            pm.Axes.Add(dateTimeAxis1);
            double margindv = (maxdv - mindv) * 0.1;

            pm.Axes.Add(new LinearAxis()
            {
                Title = "值",
                Position = AxisPosition.Left,
                IsPanEnabled = false,
                IsZoomEnabled = false,
                Minimum = mindv - margindv,
                Maximum = maxdv + margindv,
                Key = "yaxis_dv",
            });
            foreach (PointData pd in dic_pd.Values)
            {
                var lineSeries = new LineSeries
                {
                    DataFieldX = "Date",
                    DataFieldY = "Value",
                    ItemsSource = pd.data,
                    TrackerFormatString = "{2}\r{4:0.00}",
                };
                pm.Series.Add(lineSeries);
            }
            return pm;
        }
        private void toolStripButton_Find_Click(object sender, EventArgs e)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                for (int c = 0; c < 100; c++)
                {
                    Dictionary<int, PointData> dic_pd = SisConnect.GetsisData(ttg.sisid_set.ToArray(),
                                            dateTimePicker1.Value, dateTimePicker2.Value);
                
                    float cost = 0;
                    List<PointData> lspd = new List<PointData>(dic_pd.Values.ToArray());
                    if (lspd.Count > 0)
                    {
                        PointData pt_main = lspd[0];
                        lspd.RemoveAt(0);
                        while (lspd.Count > 0)
                        {
                            for (int i = 0; i < lspd.Count; i++)
                            {
                                cost = Math.Max(cost, SisConnect.GetDtw(pt_main, lspd[i]));
                            }
                            pt_main = lspd[0];
                            lspd.RemoveAt(0);
                        }
                    }
                }
                sw.Stop();
                toolStripStatusLabel1.Text = sw.ElapsedMilliseconds.ToString();
                //plotView1.Model = PlotPoint(dic_pd);
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void toolStripButton_Multi_Click(object sender, EventArgs e)
        {
            //timespan = (int)Math.Round(timespan / 1.414);
        }

        private void toolStripButton_Divide_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton_Back_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton_First_Click(object sender, EventArgs e)
        {

        }
    }
}

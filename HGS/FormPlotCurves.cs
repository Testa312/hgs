using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Globalization;
using GlacialComponents.Controls;
using System.Diagnostics;
namespace HGS
{
    public partial class FormPlotCurves : Form
    {
        HashSet<point> hsPoint = null;
        DateTimePicker dateTimePicker1 = new DateTimePicker();
        DateTimePicker dateTimePicker2 = new DateTimePicker();
        //
        OPAPI.Connect sisconn_temp = new OPAPI.Connect(Pref.Inst().sisHost, Pref.Inst().sisPort, 60,
        Pref.Inst().sisUser, Pref.Inst().sisPassword);//建立连接

        public delegate void MyDelegate();
        public event MyDelegate MessageEvent;//也可不要event
        public FormPlotCurves(HashSet<point> hsPoint, DateTime begin, DateTime end, bool bAdmin = false )
        {
            this.hsPoint = hsPoint;
            InitializeComponent();
            if (!bAdmin)
                glacialList1.ContextMenuStrip = null;
            dateTimePicker1.Value = begin;
            dateTimePicker2.Value = end;

            if ((end - begin).TotalSeconds < 1)
                MessageBox.Show("时间间隔太短！");
            InitializeFromTreeTag();          
        }
        private void InitializeFromTreeTag()
        {
            try
            {
                const string sformat = "yyyy-MM-dd HH:mm:ss";
                Size size = new Size(159, 21);
                /*
                try
                {
                    dateTimePicker2.Value = SisConnect.GetSisSystemTime(sisconn_temp).AddSeconds(-5);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "错误");
                }*/
                dateTimePicker2.Size = size;
                dateTimePicker2.Format = DateTimePickerFormat.Custom;
                dateTimePicker2.CustomFormat = sformat;
                //
                //dateTimePicker1.Value = dateTimePicker1.Value.AddMinutes(-15);
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
                //

                if (hsPoint != null)
                {
                    plotView1.Model = PlotPoint();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误");
            }
        }
        private PlotModel PlotPoint(int count = 1200)
        {
            Dictionary<int, PointData> dic_pd = null;
            Dictionary<int, PointData> dic_pd_stat = null;
            Dictionary<int, PointData> dic_pd_stat_30s = null;
            try
            {
                dic_pd = SisConnect.GetPointData_dic(sisconn_temp,hsPoint,
                    dateTimePicker1.Value, dateTimePicker2.Value, count);

                dic_pd_stat = SisConnect.GetsisStat(sisconn_temp,hsPoint,
                    dateTimePicker1.Value, dateTimePicker2.Value, (int)(dateTimePicker2.Value - dateTimePicker2.Value).TotalSeconds);
                dic_pd_stat_30s = SisConnect.GetsisStat(sisconn_temp, hsPoint,
                    dateTimePicker1.Value, dateTimePicker2.Value, 30);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误");
            }

            if (dic_pd == null || dic_pd.Count <= 0) return null;
            List<GLItem> lsitem = new List<GLItem>();
            glacialList1.Items.Clear();
            foreach(PointData pd in dic_pd.Values)
            {
                GLItem itm = new GLItem(glacialList1);
                itm.SubItems["PN"].Text = pd.GN;
                itm.SubItems["ED"].Text = pd.ED;
                itm.SubItems["EU"].Text = pd.EU;
                itm.Tag = pd;
                PointData pd_stat;
                if (dic_pd_stat != null && dic_pd_stat.TryGetValue(pd.ID, out pd_stat))
                {
                    pd.MaxAv = pd_stat.MaxAv;
                    pd.MinAv = pd_stat.MinAv;
                }
                if (dic_pd_stat_30s != null && dic_pd_stat_30s.TryGetValue(pd.ID, out pd_stat))
                {
                    pd.DifAV = pd_stat.DifAV;
                }
                itm.SubItems["MAX"].Text = Math.Round(pd.MaxAv * 1.1, 3).ToString();
                itm.SubItems["MIN"].Text = Math.Round(pd.MinAv * 0.9, 3).ToString(); 
                itm.SubItems["Skip_pp"].Text = Math.Round(pd.DifAV * 1.1, 3).ToString();
                lsitem.Add(itm);
            }
            glacialList1.Items.AddRange(lsitem.ToArray());
            glacialList1.Invalidate();

            //string title = string.Format("{0}---{1}  [{2}]", dateTimePicker1.Value, dateTimePicker2.Value,
            //(dateTimePicker2.Value - dateTimePicker1.Value));

            //string title = string.Format("[{0}]", (dateTimePicker2.Value - dateTimePicker1.Value),0);
            var pm = new PlotModel
            {
                Title = (dateTimePicker2.Value - dateTimePicker1.Value).ToString(@"dd\.hh\:mm\:ss"),
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
            double margindv = (maxdv- mindv) * 0.2;

            pm.Axes.Add(new LinearAxis()
            {
                //Title = "值",
                Position = AxisPosition.Left,
                IsPanEnabled = false,
                IsZoomEnabled = false,
                Minimum = mindv - margindv,
                //Minimum = 0,
                Maximum = maxdv + margindv,
                Key = "yaxis_dv",
            });
            foreach (PointData pd in dic_pd.Values)
            {
                var lineSeries = new LineSeries
                {
                    Title = string.Format("[{0}]\r{1}", pd.GN, pd.ED),
                    DataFieldX = "Date",
                    DataFieldY = "Value",
                    ItemsSource = pd.data,
                    TrackerFormatString = "{0}\r{2}\r{4:0.00}",
                };
                pm.Series.Add(lineSeries);
            }
            return pm;
        }

        private void toolStripButton_Multi_Click(object sender, EventArgs e)
        {
            DateTime begin = dateTimePicker1.Value;
            DateTime end = dateTimePicker2.Value;
            if ((end - begin).TotalSeconds >= 900)
            {
                int span = (int)(end - begin).TotalSeconds;
                span = (int)((span - span / 1.414) / 2);
                //
                dateTimePicker1.Value = begin.AddSeconds(span);
                dateTimePicker2.Value = end.AddSeconds(-span);

                plotView1.Model = PlotPoint();
            }
        }
        private void toolStripButton_Divide_Click(object sender, EventArgs e)
        {
            DateTime begin = dateTimePicker1.Value;
            DateTime end = dateTimePicker2.Value;
            DateTime sistime = SisConnect.GetSisSystemTime(sisconn_temp);

            int span = (int)(((end - begin).TotalSeconds * 0.414) / 2);
            end = end.AddSeconds(span);
            if (end > sistime)
            {
                end = sistime;
                begin = end.AddSeconds(-span * 6.831);
            }
            else
                begin = begin.AddSeconds(-span);
            dateTimePicker1.Value = begin;
            dateTimePicker2.Value = end;

            plotView1.Model = PlotPoint();
        }
        private void toolStripButton_BBack_Click(object sender, EventArgs e)
        {
            DateTime begin = dateTimePicker1.Value;
            DateTime end = dateTimePicker2.Value;

            int span = (int)((end - begin).TotalSeconds / 3);

            dateTimePicker1.Value = begin.AddSeconds(-span);
            dateTimePicker2.Value = end.AddSeconds(-span);

            plotView1.Model = PlotPoint();
        }

        private void toolStripButton_FFirst_Click(object sender, EventArgs e)
        {
            DateTime begin = dateTimePicker1.Value;
            DateTime end = dateTimePicker2.Value;
            DateTime sistime = SisConnect.GetSisSystemTime(sisconn_temp);

            int span = (int)((end - begin).TotalSeconds / 3);
            end = end.AddSeconds(span);
            if (end > sistime)
            {
                end = sistime;
                begin = end.AddSeconds(-3 * span);
            }
            else
                begin = begin.AddSeconds(span);

            dateTimePicker1.Value = begin;
            dateTimePicker2.Value = end;

            plotView1.Model = PlotPoint();
        }

        private void toolStripLabel_Back_Click(object sender, EventArgs e)
        {
            DateTime begin = dateTimePicker1.Value;
            DateTime end = dateTimePicker2.Value;

            int span = (int)((end - begin).TotalSeconds / 3);

            dateTimePicker1.Value = begin.AddSeconds(-span);
            dateTimePicker2.Value = end.AddSeconds(-span);

            plotView1.Model = PlotPoint();
        }

        private void toolStripLabel_First_Click(object sender, EventArgs e)
        {

            DateTime begin = dateTimePicker1.Value;
            DateTime end = dateTimePicker2.Value;
            DateTime sistime = SisConnect.GetSisSystemTime(sisconn_temp);

            int span = (int)((end - begin).TotalSeconds / 5);
            end = end.AddSeconds(span);
            if (end > sistime)
            {
                end = sistime;
                begin = end.AddSeconds(-5 * span);
            }
            else
                begin = begin.AddSeconds(span);

            dateTimePicker1.Value = begin;
            dateTimePicker2.Value = end;

            plotView1.Model = PlotPoint();
        }

        private void 接受为报警高低限ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (GLItem item in glacialList1.Items.SelectedItems)
            {
                PointData pd = (PointData)item.Tag;
                point pt;
                if (Data.inst().cd_Point.TryGetValue(pd.ID, out pt))
                {                  
                    pt.ll = Math.Round(pd.MinAv * 0.9,3);
                    pt.hl = Math.Round(pd.MaxAv * 1.1,3);
                    if(pd.DifAV > 0)
                        pt.Skip_pp = Math.Round(pd.DifAV * 1.1,3);
                }
            }
            if (glacialList1.Items.SelectedItems.Count > 0)
            {
                Data.inst().SavetoPG();
                MessageEvent();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SisConnect.GetSisSystemTime(sisconn_temp);//保持连接
        }

        private void FormPlotCurves_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Enabled = false;
            sisconn_temp.close();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            接受为报警高低限ToolStripMenuItem.Visible  = glacialList1.Items.SelectedItems.Count > 0;
        }
    }
}
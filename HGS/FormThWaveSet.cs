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
    public partial class FormThWaveSet : Form
    {
        HashSet<point> hsPoint = null;
        DateTimePicker dateTimePicker1 = new DateTimePicker();
        DateTimePicker dateTimePicker2 = new DateTimePicker();
        //
        OPAPI.Connect sisconn_temp = new OPAPI.Connect(Pref.Inst().sisHost, Pref.Inst().sisPort, 60,
        Pref.Inst().sisUser, Pref.Inst().sisPassword);//建立连接

        readonly int[] step = new int[] { 32, 64, 128, 256, 512, 1024, 2048 };
        const double MULTI = 1.2;
        //------------
        Dictionary<int, PointData> dic_pd = null;
        //---------------
        public FormThWaveSet(HashSet<point> hsPoint, DateTime begin, DateTime end, bool bAdmin = false )
        {
            this.hsPoint = hsPoint;
            InitializeComponent();
            if (!bAdmin)
                glacialList_new.ContextMenuStrip = null;
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
                initgl(glacialList_org);
                plotView1.Model = PlotPoint();

            }
            catch (Exception ee)
            {
                FormBugReport.ShowBug(ee);
            }
        }
        private void initgl(GlacialList gl)
        {
            if (hsPoint != null)
            {
                List<GLItem> lsitem = new List<GLItem>();
                gl.Items.Clear();
                foreach (point pt in hsPoint)
                {
                    GLItem itm = new GLItem(gl);
                    itm.SubItems["PN"].Text = pt.pn;
                    itm.SubItems["ED"].Text = pt.ed;
                    itm.SubItems["EU"].Text = pt.eu;
                    itm.Tag = pt;
                    if (pt.Wd3s_th != null)
                    {
                        for (int i = 0; i < pt.Wd3s_th.Length; i++)
                        {
                            if (pt.Wd3s_th != null && pt.Wd3s_th[i] <= 1e30)
                                itm.SubItems[string.Format("pp{0}s", (1<<i) * 32)].Text =
                                    Math.Round(pt.Wd3s_th[i], 3).ToString();
                        }
                    }

                    lsitem.Add(itm);
                }
                gl.Items.AddRange(lsitem.ToArray());
                gl.Invalidate();
            }
        }
        private PlotModel PlotPoint(int count = 1200)
        {
            try
            {
                dic_pd = SisConnect.GetPointData_dic(sisconn_temp, hsPoint,
                    dateTimePicker1.Value, dateTimePicker2.Value, count);
            }
            catch (Exception ee)
            {
                FormBugReport.ShowBug(ee);
            }
            if (dic_pd == null || dic_pd.Count <= 0) return null;
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
            double margindv = (maxdv) * 0.1;

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
        private void timer1_Tick(object sender, EventArgs e)
        {
            SisConnect.GetSisSystemTime(sisconn_temp);//保持连接
        }

        private void FormPlotCurves_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Enabled = false;
            sisconn_temp.close();
        }
        private void button_ok_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GLItem item in glacialList_new.Items)
                {
                    bool flag = false;
                    float[] th_wave = new float[step.Length];
                    for (int i = 0; i < step.Length; i++)
                    {
                        th_wave[i] = float.MaxValue;
                        string txt_th = item.SubItems[string.Format("pp{0}s", step[i])].Text.Trim();
                        if (txt_th.Length > 0)
                        {
                            float fv;
                            if (float.TryParse(txt_th, out fv))
                            {
                                th_wave[i] = fv;
                                flag = true;
                                if (fv < 0.1)
                                {
                                    MessageBox.Show(string.Format("阈值可能太小[{0}]!", fv));
                                }
                            }
                            else
                                throw new Exception(string.Format("无法解析[{0}]！", txt_th));
                        }
                    }

                    ((point)item.Tag).Wd3s_th = flag ? th_wave : null;
                }
                Data.inst().SavetoPG();
            }
            catch (Exception ee)
            {
                FormBugReport.ShowBug(ee);
                this.DialogResult = DialogResult.None;
            }
        }

        private void button_del_Click(object sender, EventArgs e)
        {
            if (hsPoint != null)
            {
                List<GLItem> lsitem = new List<GLItem>();
                glacialList_new.Items.Clear();
                foreach (point pt in hsPoint)
                {
                    GLItem itm = new GLItem(glacialList_new);
                    itm.SubItems["PN"].Text = pt.pn;
                    itm.SubItems["ED"].Text = pt.ed;
                    itm.SubItems["EU"].Text = pt.eu;
                    itm.Tag = pt;
                    if (pt.Wd3s_th != null)
                    {
                        for (int i = 0; i < pt.Wd3s_th.Length; i++)
                        {
                            itm.SubItems[string.Format("pp{0}s", (1 << i) * 32)].Text = "";
                                //Math.Round(pt.Wd3s_th[i], 3).ToString();
                        }
                    }

                    lsitem.Add(itm);
                }
                glacialList_new.Items.AddRange(lsitem.ToArray());
                glacialList_new.Invalidate();
                tabControl.SelectedIndex = 1;
            }
        }

        private void toolStripButton_stat_th_Click(object sender, EventArgs e)
        {
            if (hsPoint == null) return;
            try
            {
                //
                double dspan = (dateTimePicker2.Value - dateTimePicker1.Value).TotalHours;
                if (dspan < 8)
                    dateTimePicker1.Value = dateTimePicker1.Value.AddHours(dspan - 8);

                //int[] ScanSpan = new int[] { 32, 64, 128, 256, 512, 1024, 2048 };//s
                Dictionary<int, float[]> dic_dtw_th = new Dictionary<int, float[]>();
                //
                HashSet<point> calcpoint = new HashSet<point>();
                foreach (point cp in hsPoint)
                {
                    if (cp.pointsrc == pointsrc.calc)
                        calcpoint.Add(cp);
                }
                List<GLItem> lsItem = new List<GLItem>();
                for (int i = 0; i < step.Length; i++)
                {
                    DateTime end = dateTimePicker2.Value;
                    DateTime begin = end.AddSeconds(-step[i]);

                    Cursor = Cursors.WaitCursor;
                    Dictionary<int, PointData> dic_pd = null;
                    Dictionary<int, PointData> dic_pd_stat = SisConnect.GetsisStat(sisconn_temp, hsPoint,
                        dateTimePicker1.Value, dateTimePicker2.Value, step[i]);
                    //统计计算点。
                    while (begin >= dateTimePicker1.Value && calcpoint.Count > 0)
                    {
                        dic_pd = SisConnect.GetPointData_dic(sisconn_temp, calcpoint, begin, end);

                        foreach (PointData pd in dic_pd.Values)
                        {
                            float[] v;
                            if (!dic_dtw_th.TryGetValue(pd.ID, out v))
                            {
                                v = new float[step.Length];
                                for (int m = 0; m < v.Length; m++)
                                {
                                    v[m] = float.MinValue;
                                }
                                dic_dtw_th.Add(pd.ID, v);
                            }
                            v[i] = Math.Max((float)((pd.MaxAv - pd.MinAv) * MULTI), v[i]);

                        }
                        begin = begin.AddMinutes(-step[i]);
                        end = end.AddMinutes(-step[i]);
                    }
                    foreach (PointData pd in dic_pd_stat.Values)
                    {
                        float[] v;
                        if (!dic_dtw_th.TryGetValue(pd.ID, out v))
                        {
                            v = new float[step.Length];
                            for (int m = 0; m < v.Length; m++)
                            {
                                v[m] = float.MinValue;
                            }
                            dic_dtw_th.Add(pd.ID, v);
                        }
                        v[i] = Math.Max((float)((pd.DifAV) * MULTI), v[i]);
                    }

                }
                foreach (KeyValuePair<int, float[]> th in dic_dtw_th)
                {
                    GLItem item = new GLItem(glacialList_new);
                    point pt = Data.inst().cd_Point[th.Key];

                    item.SubItems["PN"].Text = pt.pn;
                    item.SubItems["ED"].Text = pt.ed;
                    item.SubItems["EU"].Text = pt.eu;
                    item.Tag = pt;

                    for (int i = 0; i < step.Length; i++)
                    {
                        item.SubItems[string.Format("pp{0}s", step[i])].Text = Math.Round(th.Value[i], 3).ToString();
                    }
                    lsItem.Add(item);
                }
                Cursor = Cursors.Default;
                glacialList_new.Items.Clear();
                glacialList_new.Items.AddRange(lsItem.ToArray());
                glacialList_new.Invalidate();
                tabControl.SelectedIndex = 1;
            }
            catch (Exception ee)
            {
                FormBugReport.ShowBug(ee);
            }
        }

        private void 从现阈值复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            glacialList_new.Items.Clear();
            foreach (GLItem item in glacialList_org.Items)
            {
                glacialList_new.Items.Add(item);
            }
            glacialList_new.Invalidate();
        }
    }
}
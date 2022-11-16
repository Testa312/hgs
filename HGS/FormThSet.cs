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
using GlacialComponents.Controls;
using System.Diagnostics;
namespace HGS
{
    public partial class FormThSet : Form
    {
        private TreeTag ttg;
        DateTimePicker dateTimePicker1 = new DateTimePicker();
        DateTimePicker dateTimePicker2 = new DateTimePicker();
        //
        readonly int[] ScanSpan = { 15, 30, 60, 120, 240, 480 };//分钟
        const double MULTI = 1.1;
        public FormThSet(TreeTag ttg)
        {
            InitializeComponent();
            this.ttg = ttg;
            //
            const string sformat = "yyyy-MM-dd HH:mm:ss";
            Size size = new Size(159, 21);
            //

            dateTimePicker2.Value = SisConnect.GetSisSystemTime().AddSeconds(-5);
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
            //
            plotView1.Model = PlotPoint();
            //
            if (ttg != null)
            {
                textBox_Name.Text = ttg.nodeName;
                maskedTextBox_Sort.Text = ttg.sort.ToString();
                if (ttg.alarm_th_dis != null)
                {
                    List<GLItem> lsItem = new List<GLItem>();
                    for (int i = 0; i < ScanSpan.Length; i++)
                    {
                        //                   
                        GLItem item = new GLItem(glacialList1);
                        item.SubItems["TW"].Text = ScanSpan[i].ToString() + "m";
                        item.SubItems["alarm_th"].Text = Math.Round(ttg.alarm_th_dis[i], 3).ToString();
                        lsItem.Add(item);
                    }
                    glacialList1.Items.Clear();
                    glacialList1.Items.AddRange(lsItem.ToArray());
                    glacialList1.Invalidate();
                }
                if (ttg.pointid_set != null)
                {
                    List<GLItem> lsItem = new List<GLItem>();
                    foreach (int id in ttg.pointid_set)
                    {
                        GLItem item = new GLItem(glacialList2);
                        point pt = Data.inst().cd_Point[id];
                        item.SubItems["PN"].Text = pt.pn;
                        item.SubItems["ED"].Text = pt.ed;
                        item.Tag = pt.id;
                        for (int i = 0; i < ScanSpan.Length; i++)
                        {
                            item.SubItems[string.Format("m{0}", ScanSpan[i])].Text = pt.dtw_start_th == null ? "" : Math.Round(pt.dtw_start_th[i], 3).ToString();
                        }
                        lsItem.Add(item);                      
                    }
                    glacialList2.Items.Clear();
                    glacialList2.Items.AddRange(lsItem.ToArray());
                    glacialList2.Invalidate();
                }
            }
            else
                ttg = new TreeTag();
        }
        private PlotModel PlotPoint(int count = 600)
        {
            Dictionary<int, PointData> dic_pd = SisConnect.GetPointData_dic(ttg.pointid_set, dateTimePicker1.Value, dateTimePicker2.Value, count);
            if (dic_pd == null || dic_pd.Count <= 0) return null;

            //string title = string.Format("{0}---{1}  [{2}]", dateTimePicker1.Value, dateTimePicker2.Value,
                                        //(dateTimePicker2.Value - dateTimePicker1.Value));

            string title = string.Format("[{0}h]",Math.Round((dateTimePicker2.Value - dateTimePicker1.Value).TotalHours),2);
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
                    Title = string.Format("[{0}]\r{1}",pd.GN,pd.ED),
                    DataFieldX = "Date",
                    DataFieldY = "Value",
                    ItemsSource = pd.data,
                    TrackerFormatString = "{0}\r{2}\r{4:0.00}",
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
                //int[] span = new int[] {15,30,60,120,240,480 };//分钟
                List<GLItem> lsItem = new List<GLItem>();
                double cost = 0;
                double maxpp = double.MinValue;
                Dictionary<int, float[]> dic_dtw_th = new Dictionary<int, float[]>();
                for (int i = 0; i < ScanSpan.Length; i++)
                {
                    DateTime end = dateTimePicker2.Value;
                    DateTime begin = end.AddMinutes(-ScanSpan[i]);

                    Cursor = Cursors.WaitCursor;

                    cost = 0;
                    maxpp = double.MinValue;
                    Dictionary<int, PointData> dic_pd = null;
                    while (begin >= dateTimePicker1.Value)
                    {
                        dic_pd = SisConnect.GetPointData_dic(ttg.pointid_set,begin,end);
                        List<PointData> lspd = new List<PointData>(dic_pd.Values.ToArray());
                        
                        if (lspd.Count >= 2)
                        {
                            //第1、2条曲线为主进行比较，全部比较会产生组合爆炸。
                            PointData pt_main = lspd[0];
                            lspd.RemoveAt(0);
                            int count = 1;
                            while (lspd.Count > 0)
                            {
                                for (int m = 1; m < lspd.Count; m++)
                                {
                                    cost = Math.Max(cost, SisConnect.GetDtw_dd_diff(pt_main, lspd[m], 0, true));
                                }
                                pt_main = lspd[0];
                                lspd.RemoveAt(0);
                                count++;
                                if (count >= 2) break;//只选两条曲线
                            }
                        }
                        foreach (PointData pd in dic_pd.Values)
                        {
                            maxpp = Math.Max(maxpp, pd.MaxAv - pd.MinAv);
                        }
                        
                        begin = begin.AddMinutes(-ScanSpan[i] / 5);
                        end = end.AddMinutes(-ScanSpan[i] / 5);
                    }
                    //                   
                    GLItem item = new GLItem(glacialList1);
                    item.SubItems["TW"].Text = ScanSpan[i].ToString() + "m";
                    //item.SubItems["start_th"].Text = Math.Round(maxpp * 1.1, 3).ToString();
                    cost *= MULTI;
                    item.SubItems["alarm_th"].Text = Math.Round(cost, 3).ToString();
                    lsItem.Add(item);
                    foreach (PointData pd in dic_pd.Values)
                    {
                        float[] v;
                        if (!dic_dtw_th.TryGetValue(pd.ID, out v))
                        {
                            v = new float[ScanSpan.Length];
                            dic_dtw_th.Add(pd.ID, v);
                        }
                        v[i] =(float) ((pd.MaxAv - pd.MinAv) * MULTI);
                    }
                }
                //
                glacialList1.Items.Clear();
                glacialList1.Items.AddRange(lsItem.ToArray());
                glacialList1.Invalidate();
                //
                lsItem.Clear();
                foreach (KeyValuePair<int, float[]> th in dic_dtw_th)
                {
                    GLItem item = new GLItem(glacialList2);
                    point pt = Data.inst().cd_Point[th.Key];
                    item.SubItems["PN"].Text = pt.pn;
                    item.SubItems["ED"].Text = pt.ed;
                    item.Tag = pt.id;
                    for (int i = 0; i < ScanSpan.Length; i++)
                    {
                        item.SubItems[string.Format("m{0}",ScanSpan[i])].Text = Math.Round(th.Value[i],3).ToString();
                    }
                    lsItem.Add(item);
                }
                glacialList2.Items.Clear();
                glacialList2.Items.AddRange(lsItem.ToArray());
                glacialList2.Invalidate();
                //
                Cursor = Cursors.Default;
                sw.Stop();

                plotView1.Model = PlotPoint();

                toolStripStatusLabel1.Text = string.Format("用时：{0}ms",sw.ElapsedMilliseconds);             
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void toolStripButton_Multi_Click(object sender, EventArgs e)
        {
            DateTime begin = dateTimePicker1.Value;
            DateTime end = dateTimePicker2.Value;
            if ((end - begin).TotalSeconds >= 900)
            {
                int span = (int)(end - begin).TotalSeconds;
                span = (int)((span - span / 1.414) /2);
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
            DateTime sistime = SisConnect.GetSisSystemTime();

            int span = (int)(((end - begin).TotalSeconds * 0.414) / 2);
            end = end.AddSeconds(span);
            if(end > sistime)
            {
                end = sistime;
                begin = end.AddSeconds(-span * 6.831);
            }
            else
                begin = begin.AddSeconds(-span);
            dateTimePicker1.Value = begin;
            dateTimePicker2.Value = end ;

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
            DateTime sistime = SisConnect.GetSisSystemTime();

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

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_Name.Text.Trim().Length == 0)
                    throw new Exception("节点名不能为空！");
                ttg.nodeName = textBox_Name.Text.Trim();
                //
                bool flag = false;
                float[] th = new float[ScanSpan.Length];
                if (glacialList1.Items.Count == 6)
                {
                    
                    for (int i = 0; i < 6; i++)
                    {
                        th[i] = float.MaxValue;
                        GLItem item = glacialList1.Items[i];
                        string txt_th = item.SubItems["alarm_th"].Text;
                        if (txt_th.Length > 0)
                        {
                            float fv;
                            if (float.TryParse(txt_th, out fv))
                            {
                                th[i] = fv;
                                flag = true;
                            }
                            else
                                throw new Exception(string.Format("无法解析[{0}]！", txt_th));
                        }
                    }
                }
                ttg.alarm_th_dis = flag ? th : null;

                if (maskedTextBox_Sort.Text.Length > 0)
                {
                    ttg.sort = int.Parse(maskedTextBox_Sort.Text);
                }
                else
                    ttg.sort = -1;
                //
                foreach (int id in ttg.pointid_set)
                {
                    point pt = Data.inst().cd_Point[id];
                    pt.dtw_start_th =  null;
                }
                foreach(GLItem item in glacialList2.Items)
                {
                    flag = false;
                    float[] th_dtw = new float[ScanSpan.Length];
                    for (int i = 0; i < 6; i++)
                    {
                        th_dtw[i] = float.MaxValue;
                        string txt_th = item.SubItems[string.Format("m{0}", ScanSpan[i])].Text;
                        if (txt_th.Length > 0)
                        {
                            float fv;
                            if (float.TryParse(txt_th, out fv))
                            {
                                th_dtw[i] = fv;
                                flag = true;
                            }
                            else
                                throw new Exception(string.Format("无法解析[{0}]！", txt_th));
                        }
                    }

                    point pt = Data.inst().cd_Point[(int)item.Tag];
                    pt.dtw_start_th = flag ? th_dtw : null;
                    
                }
                foreach (int id in ttg.pointid_set)
                {
                    point pt = Data.inst().cd_Point[id];
                    Data.inst().Update(pt);
                }
                Data.inst().SavetoPG();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
            }
        }

        private void button_dell_Click(object sender, EventArgs e)
        {
            glacialList1.Items.Clear();
            glacialList2.Items.Clear();
            glacialList1.Invalidate();
            glacialList2.Invalidate();
        }
    }
}

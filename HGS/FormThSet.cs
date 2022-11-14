﻿using System;
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
            //
            plotView1.Model = PlotPoint(GetPointData_dic(dateTimePicker1.Value, dateTimePicker2.Value,600));
            //
            if (ttg != null)
            {
                textBox_Name.Text = ttg.nodeName;
                maskedTextBox_Sort.Text = ttg.sort.ToString();
            }
            else
                ttg = new TreeTag();
        }
        private Dictionary<int, PointData> GetPointData_dic(DateTime begin,DateTime end,int count = 120)
        {
            Dictionary<int, PointData> dic_pd = null;
            if (ttg.pointid_set != null && ttg.pointid_set.Count > 0)
            {
                HashSet<object> sisid = new HashSet<object>();
                List<point> calcpt = new List<point>();
                foreach (int id in ttg.pointid_set)
                {
                    point pt = Data.inst().cd_Point[id];
                    if (pt.pointsrc == pointsrc.sis)
                        sisid.Add(Convert.ToInt64(pt.id_sis));
                    else if (pt.pointsrc == pointsrc.calc)
                        calcpt.Add(pt);
                }
                dic_pd = SisConnect.GetsisData(sisid.ToArray(),begin, end, count);

                foreach (point pt in calcpt)
                {
                    PointData ptcalc = SisConnect.GetCalcPointData(pt, begin, end, count);
                    dic_pd.Add(ptcalc.ID, ptcalc);
                }
            }
            return dic_pd;
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
                    Title = pd.GN,
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
                int[] span = new int[] {15,30,60,120,240,480 };//分钟
                List<GLItem> lsItem = new List<GLItem>();
                double cost = 0;
                double maxpp = double.MinValue;
                for (int i = 0; i < span.Length; i++)
                {
                    DateTime end = dateTimePicker2.Value;
                    DateTime begin = end.AddMinutes(-span[i]);

                    Cursor = Cursors.WaitCursor;

                    cost = 0;
                    maxpp = double.MinValue;
                    while (begin >= dateTimePicker1.Value)
                    {
                        Dictionary<int, PointData> dic_pd = GetPointData_dic(begin,end);
                        List<PointData> lspd = new List<PointData>(dic_pd.Values.ToArray());
                        
                        if (lspd.Count >= 2)
                        {
                            PointData pt_main = lspd[0];
                            //lspd.RemoveAt(0);
                            //while (lspd.Count > 0)
                            //{
                                for (int m = 1; m < lspd.Count; m++)
                                {
                                    cost = Math.Max(cost, SisConnect.GetDtw_dd(pt_main, lspd[m],0,true));
                                    //cost = Math.Max(cost, SisConnect.GetFastDtw(pt_main, lspd[m]));
                                }
                                //pt_main = lspd[0];
                                //lspd.RemoveAt(0);
                            //}
                        }
                        foreach (PointData pd in dic_pd.Values)
                        {
                            maxpp = Math.Max(maxpp, pd.MaxAv - pd.MinAv);
                        }
                        
                        begin = begin.AddMinutes(-span[i] / 5);
                        end = end.AddMinutes(-span[i] / 5);
                    }
                    //                   
                    GLItem item = new GLItem(glacialList1);
                    item.SubItems["TW"].Text = span[i].ToString() + "m";
                    item.SubItems["start_th"].Text = Math.Round(maxpp * 1.0, 3).ToString();
                    item.SubItems["alarm_th"].Text = Math.Round(cost * 1.0, 3).ToString();
                    lsItem.Add(item);
                   
                }
                //
                glacialList1.Items.Clear();
                glacialList1.Items.AddRange(lsItem.ToArray());
                glacialList1.Invalidate();
                Cursor = Cursors.Default;
                sw.Stop();

                plotView1.Model = PlotPoint(GetPointData_dic(dateTimePicker1.Value, dateTimePicker2.Value,600));

                toolStripStatusLabel1.Text = string.Format("用时：{0}ms",sw.ElapsedMilliseconds);             
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

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (textBox_Name.Text.Trim().Length == 0)
                    throw new Exception("节点名不能为空！");
                ttg.nodeName = textBox_Name.Text.Trim();
                /*
                if (mtb_start_th.Text.Length > 0)
                {
                    tt.start_th = float.Parse(mtb_start_th.Text);
                }
                else
                    tt.start_th = null;
                //
                if (mtb_alarm_th_dis.Text.Length > 0)
                {
                    tt.alarm_th_dis = float.Parse(mtb_alarm_th_dis.Text);
                }
                else
                    tt.alarm_th_dis = null;
                */
                if (maskedTextBox_Sort.Text.Length > 0)
                {
                    ttg.sort = int.Parse(maskedTextBox_Sort.Text);
                }
                else
                    ttg.sort = -1;
                
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}

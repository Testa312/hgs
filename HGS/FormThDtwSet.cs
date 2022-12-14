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
    public partial class FormThDtwSet : Form
    {
        private DeviceInfo ttg;
        DateTimePicker dateTimePicker1 = new DateTimePicker();
        DateTimePicker dateTimePicker2 = new DateTimePicker();
        //
        OPAPI.Connect sisconn_temp = null; 
        //
        readonly int[] ScanSpan = Pref.Inst().ScanSpan;//分钟
        const double StartthRatio = 1.2;
        const double AlarmThRatio = 1.5;
        public FormThDtwSet(DeviceInfo ttg)
        {
            InitializeComponent();
            sisconn_temp = new OPAPI.Connect(Pref.Inst().sisHost, Pref.Inst().sisPort, 60,
                    Pref.Inst().sisUser, Pref.Inst().sisPassword);//建立连接
            this.ttg = ttg;
            //
            const string sformat = "yyyy-MM-dd HH:mm:ss";
            Size size = new Size(159, 21);
            //

            dateTimePicker2.Value = SisConnect.GetSisSystemTime(sisconn_temp).AddSeconds(-5);
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
            glacialList_dev_new.Items.Clear();
            if (ttg != null)
            {
                textBox_Name.Text = ttg.Name;
                textBox_ND.Text = ttg.nd;
                textBox_PN.Text = ttg.pn;
                maskedTextBox_Sort.Text = ttg.sort.ToString();
                maskedTextBox_delaytime.Text= ttg.DelayAlarmTime.ToString();
                button_AlarmIf.ForeColor = ttg.Orgformula_If.Length > 0 ? Color.Red : Color.Black;
                List<GLItem> lsItem = new List<GLItem>();
                if (ttg.Alarm_th_dis != null)
                {
                   
                    for (int i = 0; i < ScanSpan.Length; i++)
                    {
                        //                   
                        GLItem item = new GLItem(glacialList_dev_org);
                        item.SubItems["TW"].Text = ScanSpan[i].ToString() + "m";
                        if (ttg.Alarm_th_dis != null && ttg.Alarm_th_dis[i] <= 1e30)
                            item.SubItems["alarm_th"].Text = Math.Round(ttg.Alarm_th_dis[i], 3).ToString();
                        lsItem.Add(item);
                    }
                    glacialList_dev_org.Items.Clear();
                    glacialList_dev_org.Items.AddRange(lsItem.ToArray());
                    glacialList_dev_org.Invalidate();
                }

                lsItem.Clear();
                foreach (int id in ttg.Sensors_set())
                {
                    GLItem item = new GLItem(glacialList_sensor_org);
                    point pt = Data.inst().cd_Point[id];
                    item.SubItems["PN"].Text = pt.pn;
                    item.SubItems["ED"].Text = pt.ed;
                    item.Tag = pt.id;
                    for (int i = 0; i < ScanSpan.Length; i++)
                    {
                        if (pt.Dtw_start_th != null && pt.Dtw_start_th[i] <= 1e30)
                            item.SubItems[string.Format("m{0}", ScanSpan[i])].Text = pt.Dtw_start_th == null ?
                                    "" : Math.Round(pt.Dtw_start_th[i], 3).ToString();
                    }
                    lsItem.Add(item);
                }
                comboBox_Sound.SelectedIndex = ttg.Sound;
                glacialList_sensor_org.Items.Clear();
                glacialList_sensor_org.Items.AddRange(lsItem.ToArray());
                glacialList_sensor_org.Invalidate();

            }
            else
                ttg = new DeviceInfo();
        }
        private PlotModel PlotPoint(int count = 600)
        {
            Dictionary<int, PointData> dic_pd = SisConnect.GetPointData_dic(sisconn_temp,Functions.set_idtopoint(ttg.Sensors_set()), 
                dateTimePicker1.Value, dateTimePicker2.Value, count);
            if (dic_pd == null || dic_pd.Count <= 0) return null;

            //string title = string.Format("{0}---{1}  [{2}]", dateTimePicker1.Value, dateTimePicker2.Value,
                                        //(dateTimePicker2.Value - dateTimePicker1.Value));

            //string title = string.Format("[{0}h]",Math.Round((dateTimePicker2.Value - dateTimePicker1.Value).TotalHours),2);
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
            double margindv = (maxdv - mindv) * 0.1;

            pm.Axes.Add(new LinearAxis()
            {
                //Title = "值",
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
                double dspan = (dateTimePicker2.Value - dateTimePicker1.Value).TotalHours;
                if (dspan < 8)
                    dateTimePicker1.Value = dateTimePicker1.Value.AddHours(dspan-8);
                Stopwatch sw = new Stopwatch();
                sw.Start();
                //int[] span = new int[] {15,30,60,120,240,480 };//分钟
                List<GLItem> lsItem = new List<GLItem>();
                double cost = 0;
                int dtw = 0;
                //double maxpp = double.MinValue;
                Dictionary<int, float[]> dic_dtw_th = new Dictionary<int, float[]>();
                for (int i = 0; i < ScanSpan.Length; i++)
                {
                    DateTime end = dateTimePicker2.Value;
                    DateTime begin = end.AddMinutes(-ScanSpan[i]);

                    Cursor = Cursors.WaitCursor;

                    cost = 0;
                    //maxpp = double.MinValue;
                    Dictionary<int, PointData> dic_pd = null;
                    Dictionary<int, PointData> dic_pd_stat = SisConnect.GetsisStat(sisconn_temp,Functions.set_idtopoint(ttg.Sensors_set()), 
                        dateTimePicker1.Value, dateTimePicker2.Value, ScanSpan[i] * 60);
                    while (begin >= dateTimePicker1.Value)
                    {
                        dic_pd = SisConnect.GetPointData_dic(sisconn_temp, Functions.set_idtopoint(ttg.Sensors_set()),begin,end);
                       
                        List<PointData> lspd = new List<PointData>(dic_pd.Values.ToArray());
                        
                        if (lspd.Count >= 2)
                        {
                            //第1、2条曲线为主进行比较，全部比较会产生组合爆炸。
                            PointData pt_main = lspd[0];
                            lspd.RemoveAt(0);
                            int count = 0;
                            while (lspd.Count > 0)
                            {
                                for (int m = 0; m < lspd.Count; m++)
                                {
                                    //cost = Math.Max(cost, SisConnect.GetDtw_dd_diff(pt_main, lspd[m], 0, false));
                                    cost = Math.Max(cost, SisConnect.Get_diff_integral(pt_main, lspd[m]));
                                    dtw++;
                                }
                                pt_main = lspd[0];
                                lspd.RemoveAt(0);
                                count++;
                                if (count >= 2) break;//只选两条曲线
                            }
                        }
                        foreach (PointData pd in dic_pd.Values)
                        {
                            float[] v;
                            if (!dic_dtw_th.TryGetValue(pd.ID, out v))
                            {
                                v = new float[ScanSpan.Length];
                                for (int m = 0; m < v.Length; m++)
                                {
                                    v[m] = float.MinValue;
                                }
                                dic_dtw_th.Add(pd.ID, v);
                            }
                            v[i] = Math.Max((float)((pd.MaxAv - pd.MinAv) * StartthRatio), v[i]);                            
                        }
                        foreach (PointData pd in dic_pd_stat.Values)
                        {
                            float[] v;
                            if (!dic_dtw_th.TryGetValue(pd.ID, out v))
                            {
                                v = new float[ScanSpan.Length];
                                for (int m = 0; m < v.Length; m++)
                                {
                                    v[m] = float.MinValue;
                                }
                                dic_dtw_th.Add(pd.ID, v);
                            }
                            v[i] = Math.Max((float)((pd.DifAV) * StartthRatio), v[i]);
                        }
                        begin = begin.AddMinutes(-ScanSpan[i] / 5);
                        end = end.AddMinutes(-ScanSpan[i] / 5);
                    }
                    //                   
                    GLItem item = new GLItem(glacialList_dev_new);
                    item.SubItems["TW"].Text = ScanSpan[i].ToString() + "m";
                    //item.SubItems["start_th"].Text = Math.Round(maxpp * 1.1, 3).ToString();
                    cost *= AlarmThRatio;
                    item.SubItems["alarm_th"].Text = Math.Round(cost, 3).ToString();
                    lsItem.Add(item);
                    
                }
                //
                glacialList_dev_new.Items.Clear();
                glacialList_dev_new.Items.AddRange(lsItem.ToArray());
                glacialList_dev_new.Invalidate();
                //
                lsItem.Clear();
                foreach (KeyValuePair<int, float[]> th in dic_dtw_th)
                {
                    GLItem item = new GLItem(glacialList_sensor_new);
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
                glacialList_sensor_new.Items.Clear();
                glacialList_sensor_new.Items.AddRange(lsItem.ToArray());
                glacialList_sensor_new.Invalidate();
                //
                Cursor = Cursors.Default;
                sw.Stop();

                plotView1.Model = PlotPoint();
                //
                tabControl1.SelectedIndex = 1;
                tabControl2.SelectedIndex = 1;
                toolStripStatusLabel1.Text = string.Format("用时：{0}ms",sw.ElapsedMilliseconds);
                toolStripStatusLabel_dtw.Text = string.Format("dtw次数：{0}",dtw);
            }
            catch(Exception ee)
            {
                FormBugReport.ShowBug(ee);
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
            DateTime sistime = SisConnect.GetSisSystemTime(sisconn_temp);

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

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_Name.Text.Trim().Length == 0)
                    throw new Exception("节点名不能为空！");
                ttg.Name = textBox_Name.Text.Trim();
                ttg.nd = textBox_ND.Text.Trim();
                ttg.pn = textBox_PN.Text.Trim();
                ttg.Sound = comboBox_Sound.SelectedIndex;
                //设备
                bool flag = false;
                float[] th = new float[ScanSpan.Length];
                if (glacialList_dev_new.Items.Count == 6)
                {
                    
                    for (int i = 0; i < ScanSpan.Length; i++)
                    {
                        th[i] = float.MaxValue;
                        GLItem item = glacialList_dev_new.Items[i];
                        string txt_th = item.SubItems["alarm_th"].Text.Trim();
                        if (txt_th.Length > 0)
                        {
                            float fv;
                            if (float.TryParse(txt_th, out fv))
                            {
                                th[i] = fv;
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
                    //非空加入设备字典，空从字典中去掉。
                    ttg.Alarm_th_dis = flag ? th : null;
                    DeviceInfo di;
                    if (Data_Device.dic_Device.TryGetValue(ttg.id, out di))
                    {
                        di.Alarm_th_dis = ttg.Alarm_th_dis;
                    }
                }
                //
                if (maskedTextBox_Sort.Text.Length > 0)
                {
                    ttg.sort = int.Parse(maskedTextBox_Sort.Text);
                }
                else
                    ttg.sort = -1;
                if (maskedTextBox_delaytime.Text.Length > 0)
                {
                    ttg.DelayAlarmTime = int.Parse(maskedTextBox_delaytime.Text);
                }
                else
                    ttg.DelayAlarmTime = 0;

                ttg.DelayAlarmTime = ttg.DelayAlarmTime >= 0 ? ttg.DelayAlarmTime : 0;
                ttg.DelayAlarmTime = ttg.DelayAlarmTime <= 28800 ? ttg.DelayAlarmTime : 28800;

                //传感器
                if (ttg.Sensors_set().Count >= 2)
                {
                    foreach (GLItem item in glacialList_sensor_new.Items)
                    {
                        flag = false;
                        float[] th_dtw = new float[ScanSpan.Length];
                        for (int i = 0; i < ScanSpan.Length; i++)
                        {
                            th_dtw[i] = float.MaxValue;
                            string txt_th = item.SubItems[string.Format("m{0}", ScanSpan[i])].Text.Trim(); ;
                            if (txt_th.Length > 0)
                            {
                                float fv;
                                if (float.TryParse(txt_th, out fv))
                                {
                                    th_dtw[i] = fv;
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

                        point pt = Data.inst().cd_Point[(int)item.Tag];
                        pt.Dtw_start_th = flag ? th_dtw : null;
                    }

                }
                else if(glacialList_sensor_new.Items.Count>0 && glacialList_dev_new.Items.Count >0)
                {
                    MessageBox.Show("因传感器数不应小于2个，设备阈值无法设置!");
                }
                foreach (int id in ttg.Sensors_set())
                {
                    point pt = Data.inst().cd_Point[id];
                    Data.inst().Update(pt);
                }
                Data.inst().SavetoPG();
            }
            catch (Exception ee)
            {
                FormBugReport.ShowBug(ee);
                this.DialogResult = DialogResult.None;
            }
        }

        private void button_dell_Click(object sender, EventArgs e)
        {
            glacialList_dev_new.Items.Clear();
            List<GLItem> lsItem = new List<GLItem>();

            for (int i = 0; i < ScanSpan.Length; i++)
            {
                //                   
                GLItem item = new GLItem(glacialList_dev_new);
                item.SubItems["TW"].Text = ScanSpan[i].ToString() + "m";
                lsItem.Add(item);
            }
            glacialList_dev_new.Items.AddRange(lsItem.ToArray());
            glacialList_dev_new.Invalidate();
            //
            lsItem.Clear();

            foreach (int id in ttg.Sensors_set())
            {
                GLItem item = new GLItem(glacialList_sensor_new);
                point pt = Data.inst().cd_Point[id];
                item.SubItems["PN"].Text = pt.pn;
                item.SubItems["ED"].Text = pt.ed;
                item.Tag = pt.id;
                for (int i = 0; i < ScanSpan.Length; i++)
                {
                    item.SubItems[string.Format("m{0}", ScanSpan[i])].Text = "";
                }
                lsItem.Add(item);
            }
            //
            glacialList_sensor_new.Items.Clear();
            glacialList_sensor_new.Items.AddRange(lsItem.ToArray());

            glacialList_sensor_new.Invalidate();
            glacialList_sensor_new.Invalidate();
            tabControl1.SelectedIndex = 1;
            tabControl2.SelectedIndex = 1;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SisConnect.GetSisSystemTime(sisconn_temp);//保持连接
        }

        private void button_AlarmIf_Click(object sender, EventArgs e)
        {
            FormCalcDeviceAlarmIfSet fcdai = new FormCalcDeviceAlarmIfSet(ttg);
            fcdai.glacialLisint();
            if (fcdai.ShowDialog() == DialogResult.OK)
            {
                button_AlarmIf.ForeColor = ttg.Orgformula_If.Length > 0 ? Color.Red : Color.Black;
            }
        }

        private void FormThSet_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Enabled = false;
            sisconn_temp.close();
        }

        private void 现阈值复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            glacialList_dev_new.Items.Clear();
            foreach (GLItem item in glacialList_dev_org.Items)
            {
                glacialList_dev_new.Items.Add(item);
            }
            glacialList_dev_new.Invalidate();
        }

        private void 从现阈值复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            glacialList_sensor_new.Items.Clear();
            foreach (GLItem item in glacialList_sensor_org.Items)
            {
                glacialList_sensor_new.Items.Add(item);
            }
            glacialList_sensor_new.Invalidate();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using GlacialComponents.Controls;
using System.Diagnostics;
namespace HGS
{
    public partial class FormMain : Form
    {
        FormRealTimeAlarm formAlarmSet = null;
        FormPointSet formPointSet = null;
        FormAlarmHistoryList formAlarmList = null;
        FormCountOfDeviceCalcDTW formcodcd = null;
        int lastTm = -1;//sis点的更新时间，用于处理断线引起的缓冲数据不连续问题。

        DateTime startdate = DateTime.Now;
        //Stopwatch sW = new Stopwatch();

        TimerState ts = new TimerState();
        //
        public FormMain()
        {
            InitializeComponent();
            tssL_error_nums.Text = "";
            toolStripStatusLabel_startdate.Text = startdate.ToString();
            try
            {
                SisConnect.siscon_keep = new OPAPI.Connect(Pref.Inst().sisHost, Pref.Inst().sisPort, 60,
            Pref.Inst().sisUser, Pref.Inst().sisPassword);//建立连接
                //装入数据
                Data.inst().LoadFromPG();
                //
                ts.siscon_keep = new OPAPI.Connect(Pref.Inst().sisHost, Pref.Inst().sisPort, 60,
            Pref.Inst().sisUser, Pref.Inst().sisPassword);//建立连接
                ts.setControlValue = new TimerState.SetControlValue(SetTextBoxValue);
                ts.threadTimer = new System.Threading.Timer(new TimerCallback(TimerUp), ts, 1000, 1000);

            }
            catch (Exception ee)
            {
                FormBugReport.ShowBug(ee);
                this.Close();
            }
        }

         //定时到点执行的事件
 
        private void TimerUp(object state)
        {
            TimerState ts = (TimerState)state;
            
            lock (Pref.Inst().root)
            {
                
                ts.sW.Restart();

                GetSisValue(ts.siscon_keep);//到得sis值；

                VartoPointTable.DelayClear();//释放表；
                VartoDeviceTable.DelayClear();

                foreach (point calcpt in Data.inst().hsCalcPoint)
                {
                    if (!calcpt.isCalc)
                    {
                        calcpt.ps = PointState.Bad;
                        calcpt.av = null;
                        continue;
                    }
                    //
                    if (calcpt.isForce)
                    {
                        calcpt.av = calcpt.Forceav;
                        calcpt.ps = PointState.Good;
                        continue;
                    }
                    if (Data.inst().hs_FormulaErrorPoint.Contains(calcpt)) continue;

                    calcpt.ps = Functions.GetCalcPointState(calcpt.listSisCalcExpPointID_main);

                    try
                    {
                        if (calcpt.Expression_main != null && calcpt.ps == PointState.Good)
                        {
                            //double rsl = 
                            calcpt.av = (float)Convert.ToDouble(calcpt.Expression_main.Evaluate());
                        }
                        else calcpt.av = null;

                    }
                    catch (Exception)
                    {
                        calcpt.ps = PointState.Error;
                        Data.inst().hs_FormulaErrorPoint.Add(calcpt);
                    }
                }
                //计算报警限值
                foreach (point pt in Data.inst().hsAllPoint)
                {
                    if (pt.orgformula_hl.Trim().Length > 0)
                    {
                        PointState hlps = Functions.GetCalcPointState(pt.listSisCalaExpPointID_hl);

                        try
                        {
                            if (pt.Expression_hl != null && hlps == PointState.Good)
                                pt._HL = Math.Round(Convert.ToDouble(pt.Expression_hl.Evaluate()), 2);
                            else pt._HL = null;

                        }
                        catch (Exception)
                        {
                            //pt.ps = PointState.Error;
                            Data.inst().hs_FormulaErrorPoint.Add(pt);
                        }
                    }
                    //
                    if (pt.Orgformula_ll.Trim().Length > 0)
                    {
                        PointState llps = Functions.GetCalcPointState(pt.listSisCalaExpPointID_ll);

                        try
                        {
                            if (pt.Expression_ll != null && llps == PointState.Good)
                                pt._LL = Math.Round(Convert.ToDouble(pt.Expression_ll.Evaluate()), 2);
                            else pt._LL = null;

                        }
                        catch (Exception)
                        {
                            //pt.ps = PointState.Error;
                            Data.inst().hs_FormulaErrorPoint.Add(pt);
                        }
                    }
                    //
                    pt.Alarmifav = true;
                    if (pt.Alarmif.Trim().Length > 0)
                    {
                        PointState alarmifps = Functions.GetCalcPointState(pt.listSisCalcExpPointID_alarmif);

                        try
                        {
                            if (pt.Expression_alarmif != null && alarmifps == PointState.Good)
                                pt.Alarmifav = Convert.ToBoolean(pt.Expression_alarmif.Evaluate());

                        }
                        catch (Exception)
                        {
                            //pt.ps = PointState.Error;
                            Data.inst().hs_FormulaErrorPoint.Add(pt);
                        }
                    }
                    //}
                    //计算完成，加报警
                    //AlarmSet.GetInst().Add(pt);
                    //
                    try
                    {
                        pt.AlarmCalc();
                    }

                    catch (Exception ee)
                    {
#if DEBUG
                        //timerCalc.Enabled = false;
                        FormBugReport.ShowBug(ee, "报警计算出错！");
                        //MessageBox.Show("报警计算出错！" + ee.ToString(), "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                    };
                }
                Data_Device.AlarmCalc_All_Device();
                ts.Formula_error_nums = Data.inst().hs_FormulaErrorPoint.Count;

                try
                {
                    //#if SERVER
                    AlarmSet.GetInst().SaveAlarmInfoToPG();
                    //#endif
                }
                catch (Exception ee)
                {
#if DEBUG
                    //timerCalc.Enabled = false;
                    FormBugReport.ShowBug(ee, "保存历史出错！");
                    //MessageBox.Show("保存历史出错！" + ee.ToString(), "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);            
#endif
                };

                ts.sW.Stop();
                ts.timeconsum = ts.sW.ElapsedMilliseconds;

                this.BeginInvoke(ts.setControlValue,ts);
                //toolStripStatusLabel_span.Text = (DateTime.Now - startdate).ToString(@"dd\.hh\:mm\:ss");
                //usetime.Text = ts.timeconsum.ToString("d4");
                //tssL_error_nums.Text = ts.Formula_error_nums.ToString("d3");
            }
        }

        
        private void SetTextBoxValue(object value)
        {
            TimerState ts = (TimerState)value;
            toolStripStatusLabel_span.Text = (DateTime.Now - startdate).ToString(@"dd\.hh\:mm\:ss");
            usetime.Text = ts.timeconsum.ToString("d4");
            tssL_error_nums.Text = ts.Formula_error_nums.ToString("d3");
        }
        
    private void 点设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formPointSet == null || formPointSet.IsDisposed) formPointSet = new FormPointSet();

            formPointSet.MdiParent = this;       //设置mdiparent属性，将当前窗体作为父窗体
            formPointSet.WindowState = FormWindowState.Maximized;
            formPointSet.Show();
            

        }
        //登录窗口
        private void FormMain_Shown(object sender, EventArgs e)
        {
            /*
            FormLogin fl = new FormLogin();
            if (DialogResult.OK == fl.ShowDialog()) 
            {
                
                formAlarmSet = new FormAlarmSetList();
                
                //formPointSet = new FormPointSet();

                this.Text = string.Format("HGS-{0}", Auth.GetInst().UserName);
                if (formAlarmSet != null || !formAlarmSet.IsDisposed)
                {
                    formAlarmSet.MdiParent = this;       //设置mdiparent属性，将当前窗体作为父窗体
                    formAlarmSet.WindowState = FormWindowState.Maximized;
                    formAlarmSet.Show();
                }
                

            }
            else this.Close();
            */
        }
        private void 报警信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formAlarmSet == null || formAlarmSet.IsDisposed) formAlarmSet = new FormRealTimeAlarm();

            formAlarmSet.MdiParent = this;       //设置mdiparent属性，将当前窗体作为父窗体
            formAlarmSet.WindowState = FormWindowState.Maximized;
            formAlarmSet.Show();

        }
        private void GetSisValue(OPAPI.Connect siscon_keep)
        {
            StringBuilder sbid = new StringBuilder();
            foreach (point pt in Data.inst().hsSisPoint)
            {
                sbid.Append(pt.Id_sis);
                sbid.Append(",");
            }
            if (sbid.Length > 0)
                sbid.Remove(sbid.Length - 1, 1);
            else return;
            //
            string sql = string.Format("select ID,TM,DS,AV from Realtime where ID in ({0})", sbid.ToString());
            try
            {
                OPAPI.ResultSet resultSet = siscon_keep.executeQuery(sql);//执行SQL

                const short gb1 = -32256;
                const short gb2 = -32768;
  
                while (resultSet.next())//next()执行一次，游标下移一行
                {
                    point Point = Data.inst().dic_SisIdtoPoint[resultSet.getInt(0)];
                    if (Point.isForce)
                    {
                        Point.av = Point.Forceav;
                        Point.ps = PointState.Good;
                        continue;
                    }
                                    
                    short ds = resultSet.getShort(2);
                    if ((ds & gb1) == 0)
                    {
                        Point.ps = PointState.Good;
                    }
                    else if ((ds & gb2) == gb2)
                    {
                        Point.ps = PointState.Timeout;
                    }
                    else
                        Point.ps = PointState.Bad;

                    int Tm = resultSet.getInt(1);
                    if (Tm - lastTm >= 5)//超时为5s
                    {
                       // Point.Av = -1;不能这样。
                        Point.ps = PointState.Bad;
                    }
                    lastTm = Tm;
                    //
                    Data.inst().ce.Variables[Pref.Inst().GetVarName(Point)] = Point.av = resultSet.getFloat(3);
                }
                
                if (resultSet != null)
                {
                    resultSet.close(); //释放内存
                }
            }
            catch (Exception ee)
            {
#if DEBUG
                FormBugReport.ShowBug(ee, "取实时出错！");
                //MessageBox.Show("取实时出错！" + ee.ToString(), "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
            }
        }

        private void 报警记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (formAlarmList == null || formAlarmList.IsDisposed) formAlarmList = new FormAlarmHistoryList();

            formAlarmList.MdiParent = this;       //设置mdiparent属性，将当前窗体作为父窗体
            formAlarmList.WindowState = FormWindowState.Maximized;
            formAlarmList.Show();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        protected override void WndProc(ref Message m)
        {
#if !DEBUG
            const int WM_SYSCOMMAND = 0x112;//命令操作
            const int SC_CLOSE = 0xF060;//命令类型
            const int SC_MINIMIZE = 0xF020;//命令类型
            if (m.Msg == WM_SYSCOMMAND && ((int)m.WParam == SC_CLOSE || (int)m.WParam == SC_MINIMIZE))
            {
                this.Hide();
                return;
            }
#endif
            base.WndProc(ref m);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon1.Dispose();
            //timerCalc.Enabled = false;
            SisConnect.siscon_keep.close();
            ts.siscon_keep.close();
        }

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void dTW计算次数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formcodcd == null || formcodcd.IsDisposed)
                formcodcd = new FormCountOfDeviceCalcDTW();

            formcodcd.MdiParent = this;       //设置mdiparent属性，将当前窗体作为父窗体
            formcodcd.WindowState = FormWindowState.Maximized;
            formcodcd.Show();
        }

        private void 消音ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlarmSet.GetInst().bPlay = !AlarmSet.GetInst().bPlay;
            if (!AlarmSet.GetInst().bPlay)
                AlarmSet.GetInst().SoundStop();
        }

        private void toolStripMenuItem1_DropDownOpening(object sender, EventArgs e)
        {
            if (AlarmSet.GetInst().bPlay)
            {
                静音ToolStripMenuItem.Text = "静音";
            }
            else
            {
                静音ToolStripMenuItem.Text = "取消静音";
            }
        }

        private void 关于aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout fa = new FormAbout();
            fa.ShowDialog();
        }

        private void 简要说明IToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process myprocess = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo("Readme.txt");
            myprocess.StartInfo = startInfo;
            myprocess.StartInfo.UseShellExecute = true;
            myprocess.Start();
        }

        private void timerkeeplive_Tick(object sender, EventArgs e)
        {
            SisConnect.GetSisSystemTime(SisConnect.siscon_keep);
        }
    }
}

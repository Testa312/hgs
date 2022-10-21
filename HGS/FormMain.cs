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
using System.Diagnostics;
namespace HGS
{
    public partial class FormMain : Form
    {
        OPAPI.Connect sisconn = new OPAPI.Connect(Pref.Inst().sisHost, Pref.Inst().sisPort, 60,
          Pref.Inst().sisUser, Pref.Inst().sisPassword);//建立连接
        FormAlarmSetList formAlarmSet = null;
        FormPointSet formPointSet = null;
        FormAlarmHistoryList formAlarmList = null;
        int lastTm = -1;//sis点的更新时间，用于处理断线引起的缓冲数据不连续问题。
        Stopwatch sW = new Stopwatch();
        public FormMain()
        {
            InitializeComponent();
            tssL_error_nums.Text = "";
            try
            {
                Data.inst().LoadFromPG();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
         ~FormMain()
        {
            sisconn.close();
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
        }
        private void 报警信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formAlarmSet == null || formAlarmSet.IsDisposed) formAlarmSet = new FormAlarmSetList();

            formAlarmSet.MdiParent = this;       //设置mdiparent属性，将当前窗体作为父窗体
            formAlarmSet.WindowState = FormWindowState.Maximized;
            formAlarmSet.Show();

        }
        private void GetSisValue()
        {
            StringBuilder sbid = new StringBuilder();
            foreach (point pt in Data.inst().hsSisPoint)
            {
                sbid.Append(pt.id_sis);
                sbid.Append(",");
            }
            if (sbid.Length > 0)
                sbid.Remove(sbid.Length - 1, 1);
            else return;
            //
            string sql = string.Format("select ID,TM,DS,AV from Realtime where ID in ({0})", sbid.ToString());
            try
            {
                OPAPI.ResultSet resultSet = sisconn.executeQuery(sql);//执行SQL

                const short gb1 = -32256;
                const short gb2 = -32768;
  
                while (resultSet.next())//next()执行一次，游标下移一行
                {
                    point Point = Data.inst().dic_SisIdtoPoint[resultSet.getInt(0)];
                    if (Point.isforce)
                    {
                        Point.Av = Point.forceav;
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
                    Data.inst().Variables[Pref.Inst().GetVarName(Point)] = Point.Av = resultSet.getDouble(3);
                }
                
                if (resultSet != null)
                {
                    resultSet.close(); //释放内存
                }
            }
            catch (Exception ee)
            {
#if DEBUG
                MessageBox.Show("取实时出错！" + ee.ToString(), "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
            }
        }
        private PointState GetCalcPointState(List<point> hsp)
        {
            PointState ps = PointState.Good; 
            if (hsp != null)
            {             
                foreach (point pt in hsp)
                {
                    if (pt.ps != PointState.Good)
                    {
                        ps = PointState.Error;
                        break;
                    }
                }
            }
            return ps;
        }
        private void timerCalc_Tick(object sender, EventArgs e)
        {
            sW.Restart();

            GetSisValue();//到得sis值；

            VartoPointTable.DelayClear();//释放表；

            foreach (point calcpt in Data.inst().hsCalcPoint)
            {
                if(!calcpt.iscalc)
                {
                    calcpt.ps = PointState.Bad;
                    calcpt.Av = null;
                    continue;
                }
                //
                if (calcpt.isforce)
                {
                    calcpt.Av = calcpt.forceav;
                    calcpt.ps = PointState.Good;
                    continue;
                }
                if (Data.inst().hs_FormulaErrorPoint.Contains(calcpt)) continue;

                calcpt.ps = GetCalcPointState(calcpt.listSisCalaExpPointID_main);

                try
                {
                    if (calcpt.expression_main != null && calcpt.ps == PointState.Good)
                    {
                        //double rsl = 
                        calcpt.Av = Convert.ToDouble(calcpt.expression_main.Evaluate()); ;
                    }
                    else calcpt.Av = null;

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
                    PointState hlps = GetCalcPointState(pt.listSisCalaExpPointID_hl);

                    try
                    {
                        if (pt.expression_hl != null && hlps == PointState.Good)
                            pt.hl = Convert.ToDouble(pt.expression_hl.Evaluate());
                        else pt.hl = null;

                    }
                    catch (Exception)
                    {
                        pt.ps = PointState.Error;
                        Data.inst().hs_FormulaErrorPoint.Add(pt);
                    }
                }
                //
                if (pt.orgformula_ll.Trim().Length > 0)
                {
                    PointState llps = GetCalcPointState(pt.listSisCalaExpPointID_ll);

                    try
                    {
                        if (pt.expression_ll != null && llps == PointState.Good)
                            pt.ll = Convert.ToDouble(pt.expression_ll.Evaluate());
                        else pt.ll = null;

                    }
                    catch (Exception)
                    {
                        pt.ps = PointState.Error;
                        Data.inst().hs_FormulaErrorPoint.Add(pt);
                    }
                }
                //
                pt.alarmifav = true;
                if (pt.alarmif.Trim().Length > 0)
                {
                    PointState alarmifps = GetCalcPointState(pt.listSisCalaExpPointID_alarmif);

                    try
                    {
                        if (pt.expression_alarmif != null && alarmifps == PointState.Good)
                            pt.alarmifav = Convert.ToBoolean(pt.expression_alarmif.Evaluate());

                    }
                    catch (Exception)
                    {
                        pt.ps = PointState.Error;
                        Data.inst().hs_FormulaErrorPoint.Add(pt);
                    }
                }
                //}
                //计算完成，加报警
                AlarmSet.GetInst().Add(pt);
                //
            }
            tssL_error_nums.Text = Data.inst().hs_FormulaErrorPoint.Count.ToString();

            try
            {
#if SERVER
                AlarmSet.GetInst().SaveAlarmInfo();
#endif
            }
            catch (Exception ee) {
#if DEBUG
                timerCalc.Enabled = false;
                MessageBox.Show("保存历史出错！" + ee.ToString(), "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);            
#endif
            };

            sW.Stop();
            usetime.Text = sW.ElapsedMilliseconds.ToString();
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

        private void 从文件导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon1.Dispose();
        }

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }
    }
}

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
    public partial class FormMain : Form
    {
        OPAPI.Connect sisconn = new OPAPI.Connect(Pref.Inst().sisHost, Pref.Inst().sisPort, 60,
          Pref.Inst().sisUser, Pref.Inst().sisPassword);//建立连接
        FormAlarmSetList formAlarmSet = null;
        FormPointSet formPointSet = null;
        FormAlarmHistoryList formAlarmList = null;
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
                    point Point = Data.inst().dic_SisIdtoPointId[resultSet.getInt(0)];
                    if (Point.isforce)
                    {
                        Point.av = Point.forceav;
                        continue;
                    }
                    //double rsl = resultSet.getDouble(3);
                   // Point.av =rsl;
                    Data.inst().Variables[Pref.Inst().GetVarName(Point)] = Point.av = resultSet.getDouble(3);
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
                    //
                    //加报警
                    AlarmSet.GetInst().Add(Point);
                }
                if (resultSet != null)
                {
                    resultSet.close(); //释放内存
                }
            }
            catch (Exception)
            {
            }
        }
        private void timerCalc_Tick(object sender, EventArgs e)
        {
            GetSisValue();//到得sis值；
            foreach (point calcpt in Data.inst().hsCalcPoint)
            {
                //
                if (calcpt.isforce)
                {
                    calcpt.av = calcpt.forceav;
                    continue;
                }
                //bool b_lastAlarm = calcpt.alarming;
                //计算计算点。
                //if (calcpt.pointsrc == pointsrc.calc)
                //{
                if (Data.inst().hs_FormulaErrorPoint.Contains(calcpt)) continue;
                //point Point = Data.Get().cd_Point[calcid];
                foreach (point pt in calcpt.listSisCalaExpPointID_main)
                {
                    if (pt.ps != PointState.Good)
                    {
                        calcpt.ps = PointState.Error;
                        break;
                    }
                    calcpt.ps = PointState.Good;
                }
                try
                {
                    if (calcpt.expformula_main.Length > 0)
                        calcpt.av = Convert.ToDouble(calcpt.expression_main.Evaluate());
                    else calcpt.av = null;

                }
                catch (Exception)
                {
                    calcpt.ps = PointState.Error;
                    Data.inst().hs_FormulaErrorPoint.Add(calcpt);
                }
                //}
                //加报警
                AlarmSet.GetInst().Add(calcpt);              
                tssL_error_nums.Text = Data.inst().hs_FormulaErrorPoint.Count.ToString();
            }
            try
            {
                AlarmSet.GetInst().SaveAlarmInfo();
            }
            catch(Exception ee) {
#if DEBUG
                timerCalc.Enabled = false;
                MessageBox.Show("保存历史出错！" + ee.ToString(), "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);            
#endif
            };
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
    }
}

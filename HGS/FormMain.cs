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
        public FormMain()
        {
            InitializeComponent();
            //
            CalcEngine.CalcEngine cd = new CalcEngine.CalcEngine();
            Data.inst().LoadFromPG();
            tssL_error_nums.Text = "";
        }
         ~FormMain()
        {
            sisconn.close();
        }
            private void form1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPointSet frm1 = new FormPointSet();     //创建form2窗体的对象

            frm1.MdiParent = this;       //设置mdiparent属性，将当前窗体作为父窗体
            frm1.WindowState = FormWindowState.Maximized;
            frm1.Show();
        }

        private void 点表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSisPointList frm1 = new FormSisPointList();     //创建form2窗体的对象

            frm1.MdiParent = this;       //设置mdiparent属性，将当前窗体作为父窗体
            frm1.WindowState = FormWindowState.Maximized;
            frm1.Show();
        }

        private void 测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTestCE frm1 = new FormTestCE();     //创建form2窗体的对象

            frm1.MdiParent = this;       //设置mdiparent属性，将当前窗体作为父窗体
            frm1.WindowState = FormWindowState.Maximized;
            frm1.Show();
        }

        private void 计算点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCalcPointSet frm1 = new FormCalcPointSet();     //创建form2窗体的对象

            frm1.MdiParent = this;       //设置mdiparent属性，将当前窗体作为父窗体
            frm1.WindowState = FormWindowState.Maximized;
            frm1.Show();
        }

        private void testLuaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFormulaErrorList frm1 = new FormFormulaErrorList();     //创建form2窗体的对象

            frm1.MdiParent = this;       //设置mdiparent属性，将当前窗体作为父窗体
            frm1.WindowState = FormWindowState.Maximized;
            frm1.Show();
        }
        //登录窗口
        private void FormMain_Shown(object sender, EventArgs e)
        {
            //FormLogin fl = new FormLogin();
            //if (DialogResult.Cancel == fl.ShowDialog()) this.Close();
            //this.Text = string.Format("HGS-{0}", Auth.GetInst().UserName);

            FormPointSet frm1 = new FormPointSet();     //创建form2窗体的对象

            frm1.MdiParent = this;       //设置mdiparent属性，将当前窗体作为父窗体
            frm1.WindowState = FormWindowState.Maximized;
            frm1.Show();

            FormAlarmSet frm2 = new FormAlarmSet();     //创建form2窗体的对象

            frm2.MdiParent = this;       //设置mdiparent属性，将当前窗体作为父窗体
            frm2.WindowState = FormWindowState.Maximized;
            frm2.Show();
        }
        private void 报警信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAlarmSet frm1 = new FormAlarmSet();     //创建form2窗体的对象

            frm1.MdiParent = this;       //设置mdiparent属性，将当前窗体作为父窗体
            frm1.WindowState = FormWindowState.Maximized;
            frm1.Show();
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
                    point Point = Data.inst().cd_Point[Data.inst().dic_SisIdtoPointId[resultSet.getInt(0)]];
                    Point.av = Math.Round(resultSet.getDouble(3), Point.fm);
                    Data.inst().Variables[Pref.Inst().GetVarName(Point)] = Point.av;
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
            foreach (point calcpt in Data.inst().hsAllPoint)
            {
                bool lastAlam = calcpt.alarming;
                //计算计算点。
                if (calcpt.pointsrc == pointsrc.calc)
                {
                    if (Data.inst().hs_FormulaErrorPoint.Contains(calcpt)) continue;
                    //point Point = Data.Get().cd_Point[calcid];
                    foreach (point pt in calcpt.listSisCalaExpPointID)
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
                        calcpt.av = Math.Round(calcpt.expformula.Length > 0 ? (double)calcpt.expression.Evaluate() : -1, calcpt.fm);

                    }
                    catch (Exception)
                    {
                        calcpt.ps = PointState.Error;
                        Data.inst().hs_FormulaErrorPoint.Add(calcpt);
                    }
                }
                //加报警
                if (calcpt.AlarmCalc())
                    AlarmSet.GetInst().ssAlarmPoint.Add(calcpt);
                else if (!calcpt.AlarmCalc() && lastAlam)
                    AlarmSet.GetInst().ssAlarmPoint.Remove(calcpt);
                tssL_error_nums.Text = Data.inst().hs_FormulaErrorPoint.Count.ToString();
            }
        }
    }
}

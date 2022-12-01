using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
namespace HGS
{
    public partial class FormBugReport : Form
    {
        #region 全局变量
        Exception _bugInfo;
        #endregion

        #region 构造函数
        /// <summary>
        /// Bug发送窗口
        /// </summary>
        /// <param name="bugInfo">Bug信息</param>
        public FormBugReport(Exception bugInfo)
        {
            InitializeComponent();
            _bugInfo = bugInfo;
            this.txtBugInfo.Text = bugInfo.Message;
            lblErrorCode.Text = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Bug发送窗口
        /// </summary>
        /// <param name="bugInfo">Bug信息</param>
        /// <param name="errorCode">错误号</param>
        public FormBugReport(Exception bugInfo, string errorCode)
        {
            InitializeComponent();
            _bugInfo = bugInfo;
            this.txtBugInfo.Text = bugInfo.Message;
            lblErrorCode.Text = errorCode;
        }
        #endregion

        #region 公开静态方法
        /// <summary>
        /// 提示Bug
        /// </summary>
        /// <param name="bugInfo">Bug信息</param>
        /// <param name="errorCode">错误号</param>
        public static void ShowBug(Exception bugInfo, string errorCode)
        {
            new FormBugReport(bugInfo, errorCode).ShowDialog();
        }

        /// <summary>
        /// 提示Bug
        /// </summary>
        /// <param name="bugInfo">Bug信息</param>
        public static void ShowBug(Exception bugInfo)
        {
            ShowBug(bugInfo, Guid.NewGuid().ToString());
        }
        #endregion

        private void btnDetailsInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("异常详细信息：" + _bugInfo.Message + "\r\n跟踪：" + _bugInfo.StackTrace);
        }
        private string GetHostinfo()
        {
            string hname = Dns.GetHostName();
            string hinfo = "计算机名：" + Dns.GetHostName() + ";"; ;
            IPAddress[] ipadr = Dns.GetHostAddresses(hname);
            foreach (IPAddress ip in ipadr)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    hinfo += "IP:" + ip.ToString() + ";";
                }
            }
            return hinfo;
        }
        private string PackErrorMsg()
        {
            string emsg = "**********时间：" + DateTime.Now.ToString() + "**********\r\n";
            emsg += GetHostinfo() + "\r\n"; 
            emsg +="编号：" + lblErrorCode.Text + "\r\n";
            emsg +="描述：" + txtContentInfo.Text + "\r\n";
            emsg +="编译时间：" + System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location).ToString() + "\r\n";
            emsg +="源：" + _bugInfo.Source + "\r\n";
            emsg +="过程：" + _bugInfo.TargetSite + "\r\n";
            emsg +="异常详细信息：" + _bugInfo.Message + "\r\n";
            emsg +="跟踪:" + _bugInfo.StackTrace + "\r\n";
            return emsg;
        }
        private void SaveMsg()
        {
            string MsgFileName = "ErrMsgLog.txt";
            try
            {
                StreamWriter swf = new StreamWriter(MsgFileName, true, Encoding.Default);//不带BOM
                swf.Write(PackErrorMsg());
                swf.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmBugReport_FormClosed(object sender, FormClosedEventArgs e)
        {           
            easyMail1.MailSubject = "生产可视化工具Bug:" +GetHostinfo();
            easyMail1.MailBody = PackErrorMsg();
            try
            {
                SaveMsg();
                easyMail1.Send();
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.ToString());
#endif

            }
        }
    }
}

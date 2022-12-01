namespace HGS
{
    partial class FormBugReport
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBugReport));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtBugInfo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtContentInfo = new System.Windows.Forms.TextBox();
            this.btnDetailsInfo = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblErrorCode = new System.Windows.Forms.Label();
            this.easyMail1 = new HGS.EasyMail(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 80);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // txtBugInfo
            // 
            this.txtBugInfo.BackColor = System.Drawing.Color.White;
            this.txtBugInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBugInfo.Location = new System.Drawing.Point(98, 38);
            this.txtBugInfo.Multiline = true;
            this.txtBugInfo.Name = "txtBugInfo";
            this.txtBugInfo.ReadOnly = true;
            this.txtBugInfo.Size = new System.Drawing.Size(277, 72);
            this.txtBugInfo.TabIndex = 7;
            this.txtBugInfo.Text = "BugInfo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "错误号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "这个问题是如何出现的？";
            // 
            // txtContentInfo
            // 
            this.txtContentInfo.Location = new System.Drawing.Point(14, 176);
            this.txtContentInfo.Multiline = true;
            this.txtContentInfo.Name = "txtContentInfo";
            this.txtContentInfo.Size = new System.Drawing.Size(358, 43);
            this.txtContentInfo.TabIndex = 1;
            // 
            // btnDetailsInfo
            // 
            this.btnDetailsInfo.Location = new System.Drawing.Point(14, 249);
            this.btnDetailsInfo.Name = "btnDetailsInfo";
            this.btnDetailsInfo.Size = new System.Drawing.Size(106, 23);
            this.btnDetailsInfo.TabIndex = 8;
            this.btnDetailsInfo.Text = ">>错误详细信息";
            this.btnDetailsInfo.UseVisualStyleBackColor = true;
            this.btnDetailsInfo.Click += new System.EventHandler(this.btnDetailsInfo_Click);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(297, 249);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // lblErrorCode
            // 
            this.lblErrorCode.AutoSize = true;
            this.lblErrorCode.Location = new System.Drawing.Point(71, 123);
            this.lblErrorCode.Name = "lblErrorCode";
            this.lblErrorCode.Size = new System.Drawing.Size(23, 12);
            this.lblErrorCode.TabIndex = 10;
            this.lblErrorCode.Text = "...";
            // 
            // easyMail1
            // 
            this.easyMail1.MailAttachments = new string[] {
        null};
            this.easyMail1.MailBody = "";
            this.easyMail1.MailFrom = "fdhaorp@xbppower.com";
            this.easyMail1.MailSubject = "HGS";
            this.easyMail1.MailTo = "fdhaorp@xbppower.com";
            this.easyMail1.SendAsync = false;
            this.easyMail1.SMTPPassword = "hcm1997";
            this.easyMail1.SMTPPort = 25;
            this.easyMail1.SMTPServer = "10.122.18.3";
            this.easyMail1.SMTPSSL = false;
            this.easyMail1.SMTPUsername = "fdhaorp@xbppower.com";
            this.easyMail1.TryAgainDelayTime = 10000;
            this.easyMail1.TryAgianOnFailure = false;
            // 
            // FormBugReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(387, 302);
            this.Controls.Add(this.lblErrorCode);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnDetailsInfo);
            this.Controls.Add(this.txtContentInfo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBugInfo);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormBugReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "错误报送";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmBugReport_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtBugInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtContentInfo;
        private System.Windows.Forms.Button btnDetailsInfo;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblErrorCode;
        private EasyMail easyMail1;
    }
}


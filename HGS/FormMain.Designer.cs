namespace HGS
{
    partial class FormMain
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.报警信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.报警记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.form1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dTW计算次数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.静音ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.简要说明IToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssL_error_nums = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.usetime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_startdate = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_span = new System.Windows.Forms.ToolStripStatusLabel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerkeeplive = new System.Windows.Forms.Timer(this.components);
            this.配置PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.帮助HToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(988, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.报警信息ToolStripMenuItem,
            this.报警记录ToolStripMenuItem,
            this.form1ToolStripMenuItem,
            this.dTW计算次数ToolStripMenuItem,
            this.配置PToolStripMenuItem,
            this.静音ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(58, 21);
            this.toolStripMenuItem1.Text = "文件(&F)";
            this.toolStripMenuItem1.DropDownOpening += new System.EventHandler(this.toolStripMenuItem1_DropDownOpening);
            // 
            // 报警信息ToolStripMenuItem
            // 
            this.报警信息ToolStripMenuItem.Name = "报警信息ToolStripMenuItem";
            this.报警信息ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.报警信息ToolStripMenuItem.Text = "实时报警(&A)...";
            this.报警信息ToolStripMenuItem.Click += new System.EventHandler(this.报警信息ToolStripMenuItem_Click);
            // 
            // 报警记录ToolStripMenuItem
            // 
            this.报警记录ToolStripMenuItem.Name = "报警记录ToolStripMenuItem";
            this.报警记录ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.报警记录ToolStripMenuItem.Text = "报警历史(&H)...";
            this.报警记录ToolStripMenuItem.Click += new System.EventHandler(this.报警记录ToolStripMenuItem_Click);
            // 
            // form1ToolStripMenuItem
            // 
            this.form1ToolStripMenuItem.Name = "form1ToolStripMenuItem";
            this.form1ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.form1ToolStripMenuItem.Text = "点配置(&P)...";
            this.form1ToolStripMenuItem.Click += new System.EventHandler(this.点设置ToolStripMenuItem_Click);
            // 
            // dTW计算次数ToolStripMenuItem
            // 
            this.dTW计算次数ToolStripMenuItem.Name = "dTW计算次数ToolStripMenuItem";
            this.dTW计算次数ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.dTW计算次数ToolStripMenuItem.Text = "DTW计算浏览(&D)...";
            this.dTW计算次数ToolStripMenuItem.Click += new System.EventHandler(this.dTW计算次数ToolStripMenuItem_Click);
            // 
            // 静音ToolStripMenuItem
            // 
            this.静音ToolStripMenuItem.Name = "静音ToolStripMenuItem";
            this.静音ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.静音ToolStripMenuItem.Text = "静音(&S)";
            this.静音ToolStripMenuItem.Click += new System.EventHandler(this.消音ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.退出ToolStripMenuItem.Text = "退出(&X)";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 帮助HToolStripMenuItem
            // 
            this.帮助HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.简要说明IToolStripMenuItem,
            this.关于aToolStripMenuItem});
            this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
            this.帮助HToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.帮助HToolStripMenuItem.Text = "帮助(&H)";
            // 
            // 简要说明IToolStripMenuItem
            // 
            this.简要说明IToolStripMenuItem.Name = "简要说明IToolStripMenuItem";
            this.简要说明IToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.简要说明IToolStripMenuItem.Text = "简要说明(&I)...";
            this.简要说明IToolStripMenuItem.Click += new System.EventHandler(this.简要说明IToolStripMenuItem_Click);
            // 
            // 关于aToolStripMenuItem
            // 
            this.关于aToolStripMenuItem.Name = "关于aToolStripMenuItem";
            this.关于aToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.关于aToolStripMenuItem.Text = "关于(&A)";
            this.关于aToolStripMenuItem.Click += new System.EventHandler(this.关于aToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tssL_error_nums,
            this.toolStripStatusLabel3,
            this.usetime,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel_startdate,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel_span});
            this.statusStrip1.Location = new System.Drawing.Point(0, 410);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(988, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(83, 17);
            this.toolStripStatusLabel1.Text = "计算式错误数:";
            // 
            // tssL_error_nums
            // 
            this.tssL_error_nums.Name = "tssL_error_nums";
            this.tssL_error_nums.Size = new System.Drawing.Size(63, 17);
            this.tssL_error_nums.Text = "errornum";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(93, 17);
            this.toolStripStatusLabel3.Text = "计算用时(ms)：";
            // 
            // usetime
            // 
            this.usetime.Name = "usetime";
            this.usetime.Size = new System.Drawing.Size(25, 17);
            this.usetime.Text = "ms";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(68, 17);
            this.toolStripStatusLabel2.Text = "启动日期：";
            // 
            // toolStripStatusLabel_startdate
            // 
            this.toolStripStatusLabel_startdate.Name = "toolStripStatusLabel_startdate";
            this.toolStripStatusLabel_startdate.Size = new System.Drawing.Size(68, 17);
            this.toolStripStatusLabel_startdate.Text = "启动日期：";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(68, 17);
            this.toolStripStatusLabel4.Text = "运行时间：";
            // 
            // toolStripStatusLabel_span
            // 
            this.toolStripStatusLabel_span.Name = "toolStripStatusLabel_span";
            this.toolStripStatusLabel_span.Size = new System.Drawing.Size(68, 17);
            this.toolStripStatusLabel_span.Text = "运行时间：";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "机组健康扫描";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开OToolStripMenuItem,
            this.退出XToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(133, 48);
            // 
            // 打开OToolStripMenuItem
            // 
            this.打开OToolStripMenuItem.Name = "打开OToolStripMenuItem";
            this.打开OToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.打开OToolStripMenuItem.Text = "打开(&O)";
            this.打开OToolStripMenuItem.Click += new System.EventHandler(this.打开OToolStripMenuItem_Click);
            // 
            // 退出XToolStripMenuItem
            // 
            this.退出XToolStripMenuItem.Name = "退出XToolStripMenuItem";
            this.退出XToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.退出XToolStripMenuItem.Text = "退出（&X）";
            this.退出XToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // timerkeeplive
            // 
            this.timerkeeplive.Enabled = true;
            this.timerkeeplive.Interval = 30000;
            this.timerkeeplive.Tick += new System.EventHandler(this.timerkeeplive_Tick);
            // 
            // 配置PToolStripMenuItem
            // 
            this.配置PToolStripMenuItem.Name = "配置PToolStripMenuItem";
            this.配置PToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.配置PToolStripMenuItem.Text = "配置(&P)...";
            this.配置PToolStripMenuItem.Click += new System.EventHandler(this.配置PToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 432);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HGS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem form1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 报警信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tssL_error_nums;
        private System.Windows.Forms.ToolStripMenuItem 报警记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel usetime;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 退出XToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开OToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dTW计算次数ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 静音ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_startdate;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_span;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于aToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 简要说明IToolStripMenuItem;
        private System.Windows.Forms.Timer timerkeeplive;
        private System.Windows.Forms.ToolStripMenuItem 配置PToolStripMenuItem;
    }
}


﻿namespace HGS
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
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.静音ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssL_error_nums = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.usetime = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerCalc = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
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
            this.静音ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItem1.Text = "文件";
            this.toolStripMenuItem1.DropDownOpening += new System.EventHandler(this.toolStripMenuItem1_DropDownOpening);
            // 
            // 报警信息ToolStripMenuItem
            // 
            this.报警信息ToolStripMenuItem.Name = "报警信息ToolStripMenuItem";
            this.报警信息ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.报警信息ToolStripMenuItem.Text = "实时报警信息(&A)...";
            this.报警信息ToolStripMenuItem.Click += new System.EventHandler(this.报警信息ToolStripMenuItem_Click);
            // 
            // 报警记录ToolStripMenuItem
            // 
            this.报警记录ToolStripMenuItem.Name = "报警记录ToolStripMenuItem";
            this.报警记录ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.报警记录ToolStripMenuItem.Text = "报警历史记录(&H)...";
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
            this.dTW计算次数ToolStripMenuItem.Text = "DTW计算次数...";
            this.dTW计算次数ToolStripMenuItem.Click += new System.EventHandler(this.dTW计算次数ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.退出ToolStripMenuItem.Text = "退出(&X)";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 静音ToolStripMenuItem
            // 
            this.静音ToolStripMenuItem.Name = "静音ToolStripMenuItem";
            this.静音ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.静音ToolStripMenuItem.Text = "静音";
            this.静音ToolStripMenuItem.DropDownOpening += new System.EventHandler(this.静音ToolStripMenuItem_DropDownOpening);
            this.静音ToolStripMenuItem.Click += new System.EventHandler(this.消音ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tssL_error_nums,
            this.toolStripStatusLabel3,
            this.usetime});
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
            // timerCalc
            // 
            this.timerCalc.Enabled = true;
            this.timerCalc.Interval = 1000;
            this.timerCalc.Tick += new System.EventHandler(this.timerCalc_Tick);
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
        private System.Windows.Forms.Timer timerCalc;
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
    }
}


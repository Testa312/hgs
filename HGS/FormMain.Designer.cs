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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.form1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.点表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.计算点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testLuaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.报警信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(988, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
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
            this.form1ToolStripMenuItem,
            this.点表ToolStripMenuItem,
            this.测试ToolStripMenuItem,
            this.计算点ToolStripMenuItem,
            this.testLuaToolStripMenuItem,
            this.报警信息ToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItem1.Text = "文件";
            // 
            // form1ToolStripMenuItem
            // 
            this.form1ToolStripMenuItem.Name = "form1ToolStripMenuItem";
            this.form1ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.form1ToolStripMenuItem.Text = "点设置";
            this.form1ToolStripMenuItem.Click += new System.EventHandler(this.form1ToolStripMenuItem_Click);
            // 
            // 点表ToolStripMenuItem
            // 
            this.点表ToolStripMenuItem.Name = "点表ToolStripMenuItem";
            this.点表ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.点表ToolStripMenuItem.Text = "点表";
            this.点表ToolStripMenuItem.Click += new System.EventHandler(this.点表ToolStripMenuItem_Click);
            // 
            // 测试ToolStripMenuItem
            // 
            this.测试ToolStripMenuItem.Name = "测试ToolStripMenuItem";
            this.测试ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.测试ToolStripMenuItem.Text = "测试";
            this.测试ToolStripMenuItem.Click += new System.EventHandler(this.测试ToolStripMenuItem_Click);
            // 
            // 计算点ToolStripMenuItem
            // 
            this.计算点ToolStripMenuItem.Name = "计算点ToolStripMenuItem";
            this.计算点ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.计算点ToolStripMenuItem.Text = "计算点";
            this.计算点ToolStripMenuItem.Click += new System.EventHandler(this.计算点ToolStripMenuItem_Click);
            // 
            // testLuaToolStripMenuItem
            // 
            this.testLuaToolStripMenuItem.Name = "testLuaToolStripMenuItem";
            this.testLuaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.testLuaToolStripMenuItem.Text = "测试pg 数据源";
            this.testLuaToolStripMenuItem.Click += new System.EventHandler(this.testLuaToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 410);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(988, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // 报警信息ToolStripMenuItem
            // 
            this.报警信息ToolStripMenuItem.Name = "报警信息ToolStripMenuItem";
            this.报警信息ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.报警信息ToolStripMenuItem.Text = "报警信息";
            this.报警信息ToolStripMenuItem.Click += new System.EventHandler(this.报警信息ToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 432);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HGS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem form1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 点表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 测试ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 计算点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testLuaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 报警信息ToolStripMenuItem;
    }
}


namespace HGS
{
    partial class FormPlotCurves
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPlotCurves));
            GlacialComponents.Controls.GLColumn glColumn1 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn2 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn3 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn4 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn5 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn6 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn7 = new GlacialComponents.Controls.GLColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_Multi = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Divide = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_BBack = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Back = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_First = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_FFirst = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.glacialList1 = new GlacialComponents.Controls.GlacialList();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.接受为高限报警值ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.接受为低限报警值ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.plotView1 = new OxyPlot.WindowsForms.PlotView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1040, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripLabel2,
            this.toolStripSeparator1,
            this.toolStripButton_Multi,
            this.toolStripButton_Divide,
            this.toolStripSeparator2,
            this.toolStripButton_BBack,
            this.toolStripButton_Back,
            this.toolStripButton_First,
            this.toolStripButton_FFirst});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1040, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel1.Text = "开始：";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel2.Text = "结束：";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_Multi
            // 
            this.toolStripButton_Multi.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_Multi.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Multi.Image")));
            this.toolStripButton_Multi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Multi.Name = "toolStripButton_Multi";
            this.toolStripButton_Multi.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Multi.Text = "+";
            this.toolStripButton_Multi.Click += new System.EventHandler(this.toolStripButton_Multi_Click);
            // 
            // toolStripButton_Divide
            // 
            this.toolStripButton_Divide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_Divide.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Divide.Image")));
            this.toolStripButton_Divide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Divide.Name = "toolStripButton_Divide";
            this.toolStripButton_Divide.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Divide.Text = "-";
            this.toolStripButton_Divide.Click += new System.EventHandler(this.toolStripButton_Divide_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_BBack
            // 
            this.toolStripButton_BBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_BBack.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_BBack.Image")));
            this.toolStripButton_BBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_BBack.Name = "toolStripButton_BBack";
            this.toolStripButton_BBack.Size = new System.Drawing.Size(30, 22);
            this.toolStripButton_BBack.Text = "<<";
            this.toolStripButton_BBack.Click += new System.EventHandler(this.toolStripButton_BBack_Click);
            // 
            // toolStripButton_Back
            // 
            this.toolStripButton_Back.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_Back.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Back.Image")));
            this.toolStripButton_Back.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Back.Name = "toolStripButton_Back";
            this.toolStripButton_Back.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Back.Text = "<";
            this.toolStripButton_Back.Click += new System.EventHandler(this.toolStripLabel_Back_Click);
            // 
            // toolStripButton_First
            // 
            this.toolStripButton_First.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_First.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_First.Image")));
            this.toolStripButton_First.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_First.Name = "toolStripButton_First";
            this.toolStripButton_First.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_First.Text = ">";
            this.toolStripButton_First.Click += new System.EventHandler(this.toolStripLabel_First_Click);
            // 
            // toolStripButton_FFirst
            // 
            this.toolStripButton_FFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_FFirst.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_FFirst.Image")));
            this.toolStripButton_FFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_FFirst.Name = "toolStripButton_FFirst";
            this.toolStripButton_FFirst.Size = new System.Drawing.Size(30, 22);
            this.toolStripButton_FFirst.Text = ">>";
            this.toolStripButton_FFirst.Click += new System.EventHandler(this.toolStripButton_FFirst_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 521);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1040, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 50);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.glacialList1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.plotView1);
            this.splitContainer1.Size = new System.Drawing.Size(1040, 471);
            this.splitContainer1.SplitterDistance = 119;
            this.splitContainer1.TabIndex = 3;
            // 
            // glacialList1
            // 
            this.glacialList1.AllowColumnResize = true;
            this.glacialList1.AllowMultiselect = true;
            this.glacialList1.AlternateBackground = System.Drawing.Color.DarkGreen;
            this.glacialList1.AlternatingColors = false;
            this.glacialList1.AutoHeight = true;
            this.glacialList1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.glacialList1.BackgroundStretchToFit = true;
            glColumn1.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn1.CheckBoxes = false;
            glColumn1.ImageIndex = -1;
            glColumn1.Name = "PN";
            glColumn1.NumericSort = false;
            glColumn1.Text = "点名";
            glColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn1.Width = 200;
            glColumn2.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn2.CheckBoxes = false;
            glColumn2.ImageIndex = -1;
            glColumn2.Name = "ED";
            glColumn2.NumericSort = false;
            glColumn2.Text = "描述";
            glColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn2.Width = 300;
            glColumn3.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn3.CheckBoxes = false;
            glColumn3.ImageIndex = -1;
            glColumn3.Name = "BV";
            glColumn3.NumericSort = false;
            glColumn3.Text = "下限";
            glColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn3.Width = 80;
            glColumn4.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn4.CheckBoxes = false;
            glColumn4.ImageIndex = -1;
            glColumn4.Name = "TV";
            glColumn4.NumericSort = false;
            glColumn4.Text = "上限";
            glColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn4.Width = 80;
            glColumn5.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn5.CheckBoxes = false;
            glColumn5.ImageIndex = -1;
            glColumn5.Name = "MIN";
            glColumn5.NumericSort = true;
            glColumn5.Text = "最小值";
            glColumn5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn5.Width = 90;
            glColumn6.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn6.CheckBoxes = false;
            glColumn6.ImageIndex = -1;
            glColumn6.Name = "MAX";
            glColumn6.NumericSort = true;
            glColumn6.Text = "最大值";
            glColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn6.Width = 90;
            glColumn7.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn7.CheckBoxes = false;
            glColumn7.ImageIndex = -1;
            glColumn7.Name = "EU";
            glColumn7.NumericSort = false;
            glColumn7.Text = "单位";
            glColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn7.Width = 80;
            this.glacialList1.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn1,
            glColumn2,
            glColumn3,
            glColumn4,
            glColumn5,
            glColumn6,
            glColumn7});
            this.glacialList1.ContextMenuStrip = this.contextMenuStrip1;
            this.glacialList1.ControlStyle = GlacialComponents.Controls.GLControlStyles.Normal;
            this.glacialList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glacialList1.FullRowSelect = true;
            this.glacialList1.GridColor = System.Drawing.Color.LightGray;
            this.glacialList1.GridLines = GlacialComponents.Controls.GLGridLines.gridBoth;
            this.glacialList1.GridLineStyle = GlacialComponents.Controls.GLGridLineStyles.gridSolid;
            this.glacialList1.GridTypes = GlacialComponents.Controls.GLGridTypes.gridOnExists;
            this.glacialList1.HeaderHeight = 22;
            this.glacialList1.HeaderVisible = true;
            this.glacialList1.HeaderWordWrap = false;
            this.glacialList1.HotColumnTracking = false;
            this.glacialList1.HotItemTracking = false;
            this.glacialList1.HotTrackingColor = System.Drawing.Color.LightGray;
            this.glacialList1.HoverEvents = false;
            this.glacialList1.HoverTime = 1;
            this.glacialList1.ImageList = null;
            this.glacialList1.ItemHeight = 19;
            this.glacialList1.ItemWordWrap = false;
            this.glacialList1.Location = new System.Drawing.Point(0, 0);
            this.glacialList1.Name = "glacialList1";
            this.glacialList1.Selectable = true;
            this.glacialList1.SelectedTextColor = System.Drawing.Color.White;
            this.glacialList1.SelectionColor = System.Drawing.Color.DarkBlue;
            this.glacialList1.ShowBorder = true;
            this.glacialList1.ShowFocusRect = false;
            this.glacialList1.Size = new System.Drawing.Size(1040, 119);
            this.glacialList1.SortType = GlacialComponents.Controls.SortTypes.InsertionSort;
            this.glacialList1.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.glacialList1.TabIndex = 0;
            this.glacialList1.Text = "glacialList1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.接受为高限报警值ToolStripMenuItem,
            this.接受为低限报警值ToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(173, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 接受为高限报警值ToolStripMenuItem
            // 
            this.接受为高限报警值ToolStripMenuItem.Name = "接受为高限报警值ToolStripMenuItem";
            this.接受为高限报警值ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.接受为高限报警值ToolStripMenuItem.Text = "接受为高限报警值";
            this.接受为高限报警值ToolStripMenuItem.Click += new System.EventHandler(this.接受为高低限报警值ToolStripMenuItem_Click);
            // 
            // 接受为低限报警值ToolStripMenuItem1
            // 
            this.接受为低限报警值ToolStripMenuItem1.Name = "接受为低限报警值ToolStripMenuItem1";
            this.接受为低限报警值ToolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
            this.接受为低限报警值ToolStripMenuItem1.Text = "接受为低限报警值";
            this.接受为低限报警值ToolStripMenuItem1.Click += new System.EventHandler(this.接受为低限报警值ToolStripMenuItem1_Click);
            // 
            // plotView1
            // 
            this.plotView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotView1.Location = new System.Drawing.Point(0, 0);
            this.plotView1.Name = "plotView1";
            this.plotView1.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotView1.Size = new System.Drawing.Size(1040, 348);
            this.plotView1.TabIndex = 0;
            this.plotView1.Text = "plotView1";
            this.plotView1.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotView1.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotView1.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 30000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormPlotCurves
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 543);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormPlotCurves";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "曲线";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormPlotCurves_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private GlacialComponents.Controls.GlacialList glacialList1;
        private OxyPlot.WindowsForms.PlotView plotView1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton_BBack;
        private System.Windows.Forms.ToolStripButton toolStripButton_Back;
        private System.Windows.Forms.ToolStripButton toolStripButton_First;
        private System.Windows.Forms.ToolStripButton toolStripButton_FFirst;
        private System.Windows.Forms.ToolStripButton toolStripButton_Multi;
        private System.Windows.Forms.ToolStripButton toolStripButton_Divide;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 接受为高限报警值ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 接受为低限报警值ToolStripMenuItem1;
    }
}
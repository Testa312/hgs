
namespace HGS
{
    partial class FormRealTimeAlarm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRealTimeAlarm));
            GlacialComponents.Controls.GLColumn glColumn1 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn2 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn3 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn4 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn5 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn6 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn7 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn8 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn9 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn10 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn11 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn12 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn13 = new GlacialComponents.Controls.GLColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsCB_class = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsCB_ND = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tsTB_PN = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.tsTB_ED = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.tsTB_AI = new System.Windows.Forms.ToolStripTextBox();
            this.tSButton_Find = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tSLabel_Nums = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer_GetAlarm = new System.Windows.Forms.Timer(this.components);
            this.glacialList1 = new GlacialComponents.Controls.GlacialList();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_Realtime = new System.Windows.Forms.TabPage();
            this.tabPage_history = new System.Windows.Forms.TabPage();
            this.glacialList2 = new GlacialComponents.Controls.GlacialList();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage_Realtime.SuspendLayout();
            this.tabPage_history.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tsCB_class,
            this.toolStripLabel2,
            this.tsCB_ND,
            this.toolStripLabel3,
            this.tsTB_PN,
            this.toolStripLabel4,
            this.tsTB_ED,
            this.toolStripLabel5,
            this.tsTB_AI,
            this.tSButton_Find});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1015, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel1.Text = "专业：";
            // 
            // tsCB_class
            // 
            this.tsCB_class.Name = "tsCB_class";
            this.tsCB_class.Size = new System.Drawing.Size(90, 25);
            this.tsCB_class.SelectedIndexChanged += new System.EventHandler(this.tsCB_class_SelectedIndexChanged);
            this.tsCB_class.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tsTB_PN_KeyDown);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel2.Text = "节点：";
            // 
            // tsCB_ND
            // 
            this.tsCB_ND.Name = "tsCB_ND";
            this.tsCB_ND.Size = new System.Drawing.Size(90, 25);
            this.tsCB_ND.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tsTB_PN_KeyDown);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel3.Text = "点名：";
            // 
            // tsTB_PN
            // 
            this.tsTB_PN.Name = "tsTB_PN";
            this.tsTB_PN.Size = new System.Drawing.Size(100, 25);
            this.tsTB_PN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tsTB_PN_KeyDown);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel4.Text = "描述：";
            // 
            // tsTB_ED
            // 
            this.tsTB_ED.Name = "tsTB_ED";
            this.tsTB_ED.Size = new System.Drawing.Size(100, 25);
            this.tsTB_ED.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tsTB_PN_KeyDown);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(68, 22);
            this.toolStripLabel5.Text = "报警信息：";
            // 
            // tsTB_AI
            // 
            this.tsTB_AI.Name = "tsTB_AI";
            this.tsTB_AI.Size = new System.Drawing.Size(100, 25);
            this.tsTB_AI.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tsTB_PN_KeyDown);
            // 
            // tSButton_Find
            // 
            this.tSButton_Find.Image = ((System.Drawing.Image)(resources.GetObject("tSButton_Find.Image")));
            this.tSButton_Find.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSButton_Find.Name = "tSButton_Find";
            this.tSButton_Find.Size = new System.Drawing.Size(52, 22);
            this.tSButton_Find.Text = "查询";
            this.tSButton_Find.Click += new System.EventHandler(this.tsCB_class_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSLabel_Nums});
            this.statusStrip1.Location = new System.Drawing.Point(0, 503);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1015, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tSLabel_Nums
            // 
            this.tSLabel_Nums.Name = "tSLabel_Nums";
            this.tSLabel_Nums.Size = new System.Drawing.Size(44, 17);
            this.tSLabel_Nums.Text = "点数：";
            // 
            // timer_GetAlarm
            // 
            this.timer_GetAlarm.Enabled = true;
            this.timer_GetAlarm.Interval = 5000;
            this.timer_GetAlarm.Tick += new System.EventHandler(this.timer_GetAlarm_Tick);
            // 
            // glacialList1
            // 
            this.glacialList1.AllowColumnResize = true;
            this.glacialList1.AllowMultiselect = false;
            this.glacialList1.AlternateBackground = System.Drawing.Color.DarkGreen;
            this.glacialList1.AlternatingColors = false;
            this.glacialList1.AutoHeight = true;
            this.glacialList1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.glacialList1.BackgroundStretchToFit = true;
            glColumn1.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn1.CheckBoxes = false;
            glColumn1.ImageIndex = -1;
            glColumn1.Name = "ND";
            glColumn1.NumericSort = false;
            glColumn1.Text = "  节点";
            glColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn1.Width = 80;
            glColumn2.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn2.CheckBoxes = false;
            glColumn2.ImageIndex = -1;
            glColumn2.Name = "PN";
            glColumn2.NumericSort = false;
            glColumn2.Text = "  点名";
            glColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn2.Width = 150;
            glColumn3.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn3.CheckBoxes = false;
            glColumn3.ImageIndex = -1;
            glColumn3.Name = "ED";
            glColumn3.NumericSort = false;
            glColumn3.Text = "  描述";
            glColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn3.Width = 250;
            glColumn4.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn4.CheckBoxes = false;
            glColumn4.ImageIndex = -1;
            glColumn4.Name = "AlarmingAV";
            glColumn4.NumericSort = false;
            glColumn4.Text = "实时值";
            glColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn4.Width = 60;
            glColumn5.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn5.CheckBoxes = false;
            glColumn5.ImageIndex = -1;
            glColumn5.Name = "AlarmInfo";
            glColumn5.NumericSort = false;
            glColumn5.Text = "  报警信息";
            glColumn5.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn5.Width = 300;
            glColumn6.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn6.CheckBoxes = false;
            glColumn6.ImageIndex = -1;
            glColumn6.Name = "Time";
            glColumn6.NumericSort = false;
            glColumn6.Text = " 开始时间";
            glColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn6.Width = 150;
            this.glacialList1.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn1,
            glColumn2,
            glColumn3,
            glColumn4,
            glColumn5,
            glColumn6});
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
            this.glacialList1.Location = new System.Drawing.Point(3, 3);
            this.glacialList1.Name = "glacialList1";
            this.glacialList1.Selectable = true;
            this.glacialList1.SelectedTextColor = System.Drawing.Color.White;
            this.glacialList1.SelectionColor = System.Drawing.Color.DarkBlue;
            this.glacialList1.ShowBorder = true;
            this.glacialList1.ShowFocusRect = false;
            this.glacialList1.Size = new System.Drawing.Size(708, 446);
            this.glacialList1.SortType = GlacialComponents.Controls.SortTypes.QuickSort;
            this.glacialList1.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.glacialList1.TabIndex = 2;
            this.glacialList1.Text = "glacialList1";
            this.glacialList1.DoubleClick += new System.EventHandler(this.glacialList1_DoubleClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_Realtime);
            this.tabControl1.Controls.Add(this.tabPage_history);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(722, 478);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage_Realtime
            // 
            this.tabPage_Realtime.Controls.Add(this.glacialList1);
            this.tabPage_Realtime.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Realtime.Name = "tabPage_Realtime";
            this.tabPage_Realtime.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Realtime.Size = new System.Drawing.Size(714, 452);
            this.tabPage_Realtime.TabIndex = 0;
            this.tabPage_Realtime.Text = "实时";
            this.tabPage_Realtime.UseVisualStyleBackColor = true;
            // 
            // tabPage_history
            // 
            this.tabPage_history.Controls.Add(this.glacialList2);
            this.tabPage_history.Location = new System.Drawing.Point(4, 22);
            this.tabPage_history.Name = "tabPage_history";
            this.tabPage_history.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_history.Size = new System.Drawing.Size(714, 452);
            this.tabPage_history.TabIndex = 1;
            this.tabPage_history.Text = "历史";
            this.tabPage_history.UseVisualStyleBackColor = true;
            // 
            // glacialList2
            // 
            this.glacialList2.AllowColumnResize = true;
            this.glacialList2.AllowMultiselect = false;
            this.glacialList2.AlternateBackground = System.Drawing.Color.DarkGreen;
            this.glacialList2.AlternatingColors = false;
            this.glacialList2.AutoHeight = true;
            this.glacialList2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.glacialList2.BackgroundStretchToFit = true;
            glColumn7.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn7.CheckBoxes = false;
            glColumn7.ImageIndex = -1;
            glColumn7.Name = "ND";
            glColumn7.NumericSort = false;
            glColumn7.Text = "节点";
            glColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn7.Width = 80;
            glColumn8.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn8.CheckBoxes = false;
            glColumn8.ImageIndex = -1;
            glColumn8.Name = "PN";
            glColumn8.NumericSort = false;
            glColumn8.Text = "点名";
            glColumn8.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn8.Width = 150;
            glColumn9.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn9.CheckBoxes = false;
            glColumn9.ImageIndex = -1;
            glColumn9.Name = "ED";
            glColumn9.NumericSort = false;
            glColumn9.Text = "描述";
            glColumn9.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn9.Width = 150;
            glColumn10.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn10.CheckBoxes = false;
            glColumn10.ImageIndex = -1;
            glColumn10.Name = "AlarmingAV";
            glColumn10.NumericSort = true;
            glColumn10.Text = "值";
            glColumn10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn10.Width = 60;
            glColumn11.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn11.CheckBoxes = false;
            glColumn11.ImageIndex = -1;
            glColumn11.Name = "AlarmInfo";
            glColumn11.NumericSort = false;
            glColumn11.Text = "报警信息";
            glColumn11.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn11.Width = 300;
            glColumn12.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn12.CheckBoxes = false;
            glColumn12.ImageIndex = -1;
            glColumn12.Name = "Time";
            glColumn12.NumericSort = false;
            glColumn12.Text = "开始时间";
            glColumn12.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn12.Width = 150;
            glColumn13.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn13.CheckBoxes = false;
            glColumn13.ImageIndex = -1;
            glColumn13.Name = "StopTime";
            glColumn13.NumericSort = false;
            glColumn13.Text = "结束时间";
            glColumn13.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn13.Width = 150;
            this.glacialList2.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn7,
            glColumn8,
            glColumn9,
            glColumn10,
            glColumn11,
            glColumn12,
            glColumn13});
            this.glacialList2.ControlStyle = GlacialComponents.Controls.GLControlStyles.Normal;
            this.glacialList2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glacialList2.FullRowSelect = true;
            this.glacialList2.GridColor = System.Drawing.Color.LightGray;
            this.glacialList2.GridLines = GlacialComponents.Controls.GLGridLines.gridBoth;
            this.glacialList2.GridLineStyle = GlacialComponents.Controls.GLGridLineStyles.gridSolid;
            this.glacialList2.GridTypes = GlacialComponents.Controls.GLGridTypes.gridOnExists;
            this.glacialList2.HeaderHeight = 22;
            this.glacialList2.HeaderVisible = true;
            this.glacialList2.HeaderWordWrap = false;
            this.glacialList2.HotColumnTracking = false;
            this.glacialList2.HotItemTracking = false;
            this.glacialList2.HotTrackingColor = System.Drawing.Color.LightGray;
            this.glacialList2.HoverEvents = false;
            this.glacialList2.HoverTime = 1;
            this.glacialList2.ImageList = null;
            this.glacialList2.ItemHeight = 19;
            this.glacialList2.ItemWordWrap = false;
            this.glacialList2.Location = new System.Drawing.Point(3, 3);
            this.glacialList2.Name = "glacialList2";
            this.glacialList2.Selectable = true;
            this.glacialList2.SelectedTextColor = System.Drawing.Color.White;
            this.glacialList2.SelectionColor = System.Drawing.Color.DarkBlue;
            this.glacialList2.ShowBorder = true;
            this.glacialList2.ShowFocusRect = false;
            this.glacialList2.Size = new System.Drawing.Size(708, 446);
            this.glacialList2.SortType = GlacialComponents.Controls.SortTypes.QuickSort;
            this.glacialList2.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.glacialList2.TabIndex = 0;
            this.glacialList2.Text = "glacialList2";
            this.glacialList2.DoubleClick += new System.EventHandler(this.glacialList2_DoubleClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1015, 478);
            this.splitContainer1.SplitterDistance = 289;
            this.splitContainer1.TabIndex = 4;
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(289, 478);
            this.treeView.TabIndex = 0;
            this.treeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeExpand);
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            // 
            // FormRealTimeAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 525);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormRealTimeAlarm";
            this.Text = "实时报警信息";
            this.Shown += new System.EventHandler(this.FormAlarmSet_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage_Realtime.ResumeLayout(false);
            this.tabPage_history.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tSLabel_Nums;
        private GlacialComponents.Controls.GlacialList glacialList1;
        private System.Windows.Forms.Timer timer_GetAlarm;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tsCB_class;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox tsCB_ND;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox tsTB_PN;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox tsTB_ED;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripButton tSButton_Find;
        private System.Windows.Forms.ToolStripTextBox tsTB_AI;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_Realtime;
        private System.Windows.Forms.TabPage tabPage_history;
        private GlacialComponents.Controls.GlacialList glacialList2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView;
    }
}
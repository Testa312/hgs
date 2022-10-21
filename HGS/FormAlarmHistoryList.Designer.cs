
namespace HGS
{
    partial class FormAlarmHistoryList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAlarmHistoryList));
            GlacialComponents.Controls.GLColumn glColumn1 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn2 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn3 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn4 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn5 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn6 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn7 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn8 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn9 = new GlacialComponents.Controls.GLColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tSSLabelNums = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsCB_class = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsTB_ND = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tsTB_PN = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.tsTB_ED = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.button7D = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button1D = new System.Windows.Forms.Button();
            this.button8h = new System.Windows.Forms.Button();
            this.button2H = new System.Windows.Forms.Button();
            this.button1H = new System.Windows.Forms.Button();
            this.buttonAlarmFind = new System.Windows.Forms.Button();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.glacialList_UP = new GlacialComponents.Controls.GlacialList();
            this.glacialList_Down = new GlacialComponents.Controls.GlacialList();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSSLabelNums});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(906, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tSSLabelNums
            // 
            this.tSSLabelNums.Name = "tSSLabelNums";
            this.tSSLabelNums.Size = new System.Drawing.Size(44, 17);
            this.tSSLabelNums.Text = "点数：";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tsCB_class,
            this.toolStripLabel2,
            this.tsTB_ND,
            this.toolStripLabel3,
            this.tsTB_PN,
            this.toolStripLabel4,
            this.tsTB_ED,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(906, 25);
            this.toolStrip1.TabIndex = 1;
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
            this.tsCB_class.Size = new System.Drawing.Size(121, 25);
            this.tsCB_class.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tsTB_ND_KeyDown);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel2.Text = "节点：";
            // 
            // tsTB_ND
            // 
            this.tsTB_ND.Name = "tsTB_ND";
            this.tsTB_ND.Size = new System.Drawing.Size(100, 25);
            this.tsTB_ND.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tsTB_ND_KeyDown);
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
            this.tsTB_PN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tsTB_ND_KeyDown);
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
            this.tsTB_ED.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tsTB_ND_KeyDown);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton1.Text = "查询";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButtonFind_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.glacialList_UP);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(906, 403);
            this.splitContainer1.SplitterDistance = 199;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.button7D);
            this.splitContainer2.Panel1.Controls.Add(this.button1);
            this.splitContainer2.Panel1.Controls.Add(this.button1D);
            this.splitContainer2.Panel1.Controls.Add(this.button8h);
            this.splitContainer2.Panel1.Controls.Add(this.button2H);
            this.splitContainer2.Panel1.Controls.Add(this.button1H);
            this.splitContainer2.Panel1.Controls.Add(this.buttonAlarmFind);
            this.splitContainer2.Panel1.Controls.Add(this.dateTimePickerTo);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.dateTimePickerFrom);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.glacialList_Down);
            this.splitContainer2.Size = new System.Drawing.Size(906, 200);
            this.splitContainer2.SplitterDistance = 40;
            this.splitContainer2.TabIndex = 1;
            // 
            // button7D
            // 
            this.button7D.Location = new System.Drawing.Point(810, 10);
            this.button7D.Name = "button7D";
            this.button7D.Size = new System.Drawing.Size(43, 23);
            this.button7D.TabIndex = 2;
            this.button7D.Text = "1周";
            this.button7D.UseVisualStyleBackColor = true;
            this.button7D.Click += new System.EventHandler(this.button7D_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(758, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(43, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "2天";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button1D
            // 
            this.button1D.Location = new System.Drawing.Point(706, 10);
            this.button1D.Name = "button1D";
            this.button1D.Size = new System.Drawing.Size(43, 23);
            this.button1D.TabIndex = 2;
            this.button1D.Text = "1天";
            this.button1D.UseVisualStyleBackColor = true;
            this.button1D.Click += new System.EventHandler(this.button1D_Click);
            // 
            // button8h
            // 
            this.button8h.Location = new System.Drawing.Point(654, 10);
            this.button8h.Name = "button8h";
            this.button8h.Size = new System.Drawing.Size(43, 23);
            this.button8h.TabIndex = 2;
            this.button8h.Text = "8h";
            this.button8h.UseVisualStyleBackColor = true;
            this.button8h.Click += new System.EventHandler(this.button8h_Click);
            // 
            // button2H
            // 
            this.button2H.Location = new System.Drawing.Point(602, 10);
            this.button2H.Name = "button2H";
            this.button2H.Size = new System.Drawing.Size(43, 23);
            this.button2H.TabIndex = 2;
            this.button2H.Text = "2h";
            this.button2H.UseVisualStyleBackColor = true;
            this.button2H.Click += new System.EventHandler(this.button2H_Click);
            // 
            // button1H
            // 
            this.button1H.Location = new System.Drawing.Point(550, 10);
            this.button1H.Name = "button1H";
            this.button1H.Size = new System.Drawing.Size(43, 23);
            this.button1H.TabIndex = 2;
            this.button1H.Text = "1h";
            this.button1H.UseVisualStyleBackColor = true;
            this.button1H.Click += new System.EventHandler(this.button1H_Click);
            // 
            // buttonAlarmFind
            // 
            this.buttonAlarmFind.Location = new System.Drawing.Point(469, 11);
            this.buttonAlarmFind.Name = "buttonAlarmFind";
            this.buttonAlarmFind.Size = new System.Drawing.Size(75, 23);
            this.buttonAlarmFind.TabIndex = 2;
            this.buttonAlarmFind.Text = "查询";
            this.buttonAlarmFind.UseVisualStyleBackColor = true;
            this.buttonAlarmFind.Click += new System.EventHandler(this.glacialList_UP_Click);
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.CustomFormat = "yyyy年MM月dd日 HH时";
            this.dateTimePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTo.Location = new System.Drawing.Point(282, 12);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(181, 21);
            this.dateTimePickerTo.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(256, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "到：";
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.CustomFormat = "yyyy年MM月dd日 HH时";
            this.dateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFrom.Location = new System.Drawing.Point(40, 12);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(181, 21);
            this.dateTimePickerFrom.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "从：";
            // 
            // glacialList_UP
            // 
            this.glacialList_UP.AllowColumnResize = true;
            this.glacialList_UP.AllowMultiselect = false;
            this.glacialList_UP.AlternateBackground = System.Drawing.Color.DarkGreen;
            this.glacialList_UP.AlternatingColors = false;
            this.glacialList_UP.AutoHeight = true;
            this.glacialList_UP.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.glacialList_UP.BackgroundStretchToFit = true;
            glColumn1.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn1.CheckBoxes = false;
            glColumn1.ImageIndex = -1;
            glColumn1.Name = "ND";
            glColumn1.NumericSort = false;
            glColumn1.Text = "  节点";
            glColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn1.Width = 100;
            glColumn2.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn2.CheckBoxes = false;
            glColumn2.ImageIndex = -1;
            glColumn2.Name = "PN";
            glColumn2.NumericSort = false;
            glColumn2.Text = "   点名";
            glColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn2.Width = 160;
            glColumn3.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn3.CheckBoxes = false;
            glColumn3.ImageIndex = -1;
            glColumn3.Name = "ED";
            glColumn3.NumericSort = false;
            glColumn3.Text = "描述";
            glColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn3.Width = 300;
            glColumn4.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn4.CheckBoxes = false;
            glColumn4.ImageIndex = -1;
            glColumn4.Name = "LastAlarmInfo";
            glColumn4.NumericSort = false;
            glColumn4.Text = "最后报警信息";
            glColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn4.Width = 250;
            glColumn5.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn5.CheckBoxes = false;
            glColumn5.ImageIndex = -1;
            glColumn5.Name = "LastTime";
            glColumn5.NumericSort = false;
            glColumn5.Text = "最后报警时间";
            glColumn5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            glColumn5.Width = 150;
            this.glacialList_UP.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn1,
            glColumn2,
            glColumn3,
            glColumn4,
            glColumn5});
            this.glacialList_UP.ControlStyle = GlacialComponents.Controls.GLControlStyles.Normal;
            this.glacialList_UP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glacialList_UP.FullRowSelect = true;
            this.glacialList_UP.GridColor = System.Drawing.Color.LightGray;
            this.glacialList_UP.GridLines = GlacialComponents.Controls.GLGridLines.gridBoth;
            this.glacialList_UP.GridLineStyle = GlacialComponents.Controls.GLGridLineStyles.gridSolid;
            this.glacialList_UP.GridTypes = GlacialComponents.Controls.GLGridTypes.gridOnExists;
            this.glacialList_UP.HeaderHeight = 22;
            this.glacialList_UP.HeaderVisible = true;
            this.glacialList_UP.HeaderWordWrap = false;
            this.glacialList_UP.HotColumnTracking = false;
            this.glacialList_UP.HotItemTracking = false;
            this.glacialList_UP.HotTrackingColor = System.Drawing.Color.LightGray;
            this.glacialList_UP.HoverEvents = false;
            this.glacialList_UP.HoverTime = 1;
            this.glacialList_UP.ImageList = null;
            this.glacialList_UP.ItemHeight = 19;
            this.glacialList_UP.ItemWordWrap = false;
            this.glacialList_UP.Location = new System.Drawing.Point(0, 0);
            this.glacialList_UP.Name = "glacialList_UP";
            this.glacialList_UP.Selectable = true;
            this.glacialList_UP.SelectedTextColor = System.Drawing.Color.White;
            this.glacialList_UP.SelectionColor = System.Drawing.Color.DarkBlue;
            this.glacialList_UP.ShowBorder = true;
            this.glacialList_UP.ShowFocusRect = false;
            this.glacialList_UP.Size = new System.Drawing.Size(906, 199);
            this.glacialList_UP.SortType = GlacialComponents.Controls.SortTypes.QuickSort;
            this.glacialList_UP.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.glacialList_UP.TabIndex = 2;
            this.glacialList_UP.Text = "glacialList1";
            this.glacialList_UP.Click += new System.EventHandler(this.glacialList_UP_Click);
            // 
            // glacialList_Down
            // 
            this.glacialList_Down.AllowColumnResize = true;
            this.glacialList_Down.AllowMultiselect = false;
            this.glacialList_Down.AlternateBackground = System.Drawing.Color.DarkGreen;
            this.glacialList_Down.AlternatingColors = false;
            this.glacialList_Down.AutoHeight = true;
            this.glacialList_Down.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.glacialList_Down.BackgroundStretchToFit = true;
            glColumn6.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn6.CheckBoxes = false;
            glColumn6.ImageIndex = -1;
            glColumn6.Name = "Time";
            glColumn6.NumericSort = false;
            glColumn6.Text = "时间";
            glColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn6.Width = 150;
            glColumn7.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn7.CheckBoxes = false;
            glColumn7.ImageIndex = -1;
            glColumn7.Name = "AlarmInfo";
            glColumn7.NumericSort = false;
            glColumn7.Text = "报警信息";
            glColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn7.Width = 400;
            glColumn8.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn8.CheckBoxes = false;
            glColumn8.ImageIndex = -1;
            glColumn8.Name = "AlarmAv";
            glColumn8.NumericSort = true;
            glColumn8.Text = "报警值";
            glColumn8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            glColumn8.Width = 80;
            glColumn9.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn9.CheckBoxes = false;
            glColumn9.ImageIndex = -1;
            glColumn9.Name = "eu";
            glColumn9.NumericSort = false;
            glColumn9.Text = "单位";
            glColumn9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            glColumn9.Width = 50;
            this.glacialList_Down.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn6,
            glColumn7,
            glColumn8,
            glColumn9});
            this.glacialList_Down.ControlStyle = GlacialComponents.Controls.GLControlStyles.Normal;
            this.glacialList_Down.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glacialList_Down.FullRowSelect = true;
            this.glacialList_Down.GridColor = System.Drawing.Color.LightGray;
            this.glacialList_Down.GridLines = GlacialComponents.Controls.GLGridLines.gridBoth;
            this.glacialList_Down.GridLineStyle = GlacialComponents.Controls.GLGridLineStyles.gridSolid;
            this.glacialList_Down.GridTypes = GlacialComponents.Controls.GLGridTypes.gridOnExists;
            this.glacialList_Down.HeaderHeight = 22;
            this.glacialList_Down.HeaderVisible = true;
            this.glacialList_Down.HeaderWordWrap = false;
            this.glacialList_Down.HotColumnTracking = false;
            this.glacialList_Down.HotItemTracking = false;
            this.glacialList_Down.HotTrackingColor = System.Drawing.Color.LightGray;
            this.glacialList_Down.HoverEvents = false;
            this.glacialList_Down.HoverTime = 1;
            this.glacialList_Down.ImageList = null;
            this.glacialList_Down.ItemHeight = 19;
            this.glacialList_Down.ItemWordWrap = false;
            this.glacialList_Down.Location = new System.Drawing.Point(0, 0);
            this.glacialList_Down.Name = "glacialList_Down";
            this.glacialList_Down.Selectable = true;
            this.glacialList_Down.SelectedTextColor = System.Drawing.Color.White;
            this.glacialList_Down.SelectionColor = System.Drawing.Color.DarkBlue;
            this.glacialList_Down.ShowBorder = true;
            this.glacialList_Down.ShowFocusRect = false;
            this.glacialList_Down.Size = new System.Drawing.Size(906, 156);
            this.glacialList_Down.SortType = GlacialComponents.Controls.SortTypes.QuickSort;
            this.glacialList_Down.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.glacialList_Down.TabIndex = 0;
            this.glacialList_Down.Text = "glacialList2";
            // 
            // FormAlarmHistoryList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "FormAlarmHistoryList";
            this.Text = "报警查询";
            this.Shown += new System.EventHandler(this.FormAlarmList_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private GlacialComponents.Controls.GlacialList glacialList_UP;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tsCB_class;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox tsTB_PN;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox tsTB_ED;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripStatusLabel tSSLabelNums;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private GlacialComponents.Controls.GlacialList glacialList_Down;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAlarmFind;
        private System.Windows.Forms.Button button7D;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button1D;
        private System.Windows.Forms.Button button8h;
        private System.Windows.Forms.Button button2H;
        private System.Windows.Forms.Button button1H;
        private System.Windows.Forms.ToolStripTextBox tsTB_ND;
    }
}
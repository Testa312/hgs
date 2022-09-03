namespace HGS
{
    partial class FormSisPointList
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
            GlacialComponents.Controls.GLColumn glColumn1 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn2 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn3 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn4 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn5 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn6 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn7 = new GlacialComponents.Controls.GLColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.tSCBNode = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tSpCBRT = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tSTBPN = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tSCBED = new System.Windows.Forms.ToolStripComboBox();
            this.tSBFind = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.glacialList = new GlacialComponents.Controls.GlacialList();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel4,
            this.tSCBNode,
            this.toolStripLabel1,
            this.tSpCBRT,
            this.toolStripLabel2,
            this.tSTBPN,
            this.toolStripLabel3,
            this.tSCBED,
            this.tSBFind});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(760, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel4.Text = "节点：";
            // 
            // tSCBNode
            // 
            this.tSCBNode.Name = "tSCBNode";
            this.tSCBNode.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabel1.Text = "点类型：";
            // 
            // tSpCBRT
            // 
            this.tSpCBRT.Items.AddRange(new object[] {
            "ALL",
            "模拟量",
            "开关量"});
            this.tSpCBRT.Name = "tSpCBRT";
            this.tSpCBRT.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel2.Text = "点名：";
            // 
            // tSTBPN
            // 
            this.tSTBPN.Name = "tSTBPN";
            this.tSTBPN.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel3.Text = "描述：";
            // 
            // tSCBED
            // 
            this.tSCBED.Name = "tSCBED";
            this.tSCBED.Size = new System.Drawing.Size(121, 25);
            // 
            // tSBFind
            // 
            this.tSBFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSBFind.Name = "tSBFind";
            this.tSBFind.Size = new System.Drawing.Size(36, 22);
            this.tSBFind.Text = "查询";
            this.tSBFind.Click += new System.EventHandler(this.tSBFind_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 402);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(760, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "点数：";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(44, 17);
            this.toolStripStatusLabel1.Text = "点数：";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(222, 368);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.Location = new System.Drawing.Point(447, 368);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "确定";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // glacialList
            // 
            this.glacialList.AllowColumnResize = true;
            this.glacialList.AllowMultiselect = true;
            this.glacialList.AlternateBackground = System.Drawing.Color.DarkGreen;
            this.glacialList.AlternatingColors = false;
            this.glacialList.AutoHeight = true;
            this.glacialList.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.glacialList.BackgroundStretchToFit = true;
            glColumn1.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn1.CheckBoxes = false;
            glColumn1.ImageIndex = -1;
            glColumn1.Name = "PN";
            glColumn1.NumericSort = false;
            glColumn1.Text = "点名";
            glColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn1.Width = 150;
            glColumn2.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn2.CheckBoxes = false;
            glColumn2.ImageIndex = -1;
            glColumn2.Name = "RT";
            glColumn2.NumericSort = false;
            glColumn2.Text = "点类型";
            glColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            glColumn2.Width = 60;
            glColumn3.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn3.CheckBoxes = false;
            glColumn3.ImageIndex = -1;
            glColumn3.Name = "AV";
            glColumn3.NumericSort = false;
            glColumn3.Text = "值   ";
            glColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn3.Width = 60;
            glColumn4.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn4.CheckBoxes = false;
            glColumn4.ImageIndex = -1;
            glColumn4.Name = "EU";
            glColumn4.NumericSort = false;
            glColumn4.Text = "单位";
            glColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            glColumn4.Width = 60;
            glColumn5.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn5.CheckBoxes = false;
            glColumn5.ImageIndex = -1;
            glColumn5.Name = "KR";
            glColumn5.NumericSort = false;
            glColumn5.Text = "特征字";
            glColumn5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn5.Width = 100;
            glColumn6.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn6.CheckBoxes = false;
            glColumn6.ImageIndex = -1;
            glColumn6.Name = "ED";
            glColumn6.NumericSort = false;
            glColumn6.Text = "描述";
            glColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn6.Width = 200;
            glColumn7.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn7.CheckBoxes = false;
            glColumn7.ImageIndex = -1;
            glColumn7.Name = "DS";
            glColumn7.NumericSort = false;
            glColumn7.Text = "质量";
            glColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn7.Width = 80;
            this.glacialList.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn1,
            glColumn2,
            glColumn3,
            glColumn4,
            glColumn5,
            glColumn6,
            glColumn7});
            this.glacialList.ControlStyle = GlacialComponents.Controls.GLControlStyles.Normal;
            this.glacialList.FullRowSelect = true;
            this.glacialList.GridColor = System.Drawing.Color.LightGray;
            this.glacialList.GridLines = GlacialComponents.Controls.GLGridLines.gridBoth;
            this.glacialList.GridLineStyle = GlacialComponents.Controls.GLGridLineStyles.gridSolid;
            this.glacialList.GridTypes = GlacialComponents.Controls.GLGridTypes.gridOnExists;
            this.glacialList.HeaderHeight = 22;
            this.glacialList.HeaderVisible = true;
            this.glacialList.HeaderWordWrap = false;
            this.glacialList.HotColumnTracking = false;
            this.glacialList.HotItemTracking = false;
            this.glacialList.HotTrackingColor = System.Drawing.Color.LightGray;
            this.glacialList.HoverEvents = false;
            this.glacialList.HoverTime = 1;
            this.glacialList.ImageList = null;
            this.glacialList.ItemHeight = 19;
            this.glacialList.ItemWordWrap = false;
            this.glacialList.Location = new System.Drawing.Point(11, 38);
            this.glacialList.Name = "glacialList";
            this.glacialList.Selectable = true;
            this.glacialList.SelectedTextColor = System.Drawing.Color.White;
            this.glacialList.SelectionColor = System.Drawing.Color.DarkBlue;
            this.glacialList.ShowBorder = true;
            this.glacialList.ShowFocusRect = false;
            this.glacialList.Size = new System.Drawing.Size(736, 303);
            this.glacialList.SortType = GlacialComponents.Controls.SortTypes.QuickSort;
            this.glacialList.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.glacialList.TabIndex = 3;
            this.glacialList.Text = "glacialList";
            // 
            // FormSisPointList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 424);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.glacialList);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSisPointList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sis点选择";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSisPointList_FormClosed);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tSpCBRT;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox tSTBPN;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox tSCBED;
        private System.Windows.Forms.ToolStripButton tSBFind;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        internal GlacialComponents.Controls.GlacialList glacialList;
        public System.Windows.Forms.ToolStripComboBox tSCBNode;
    }
}
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
            GlacialComponents.Controls.GLColumn glColumn8 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn9 = new GlacialComponents.Controls.GLColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.tSCBNode = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tSpCBRT = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tSTBPN = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tSTBED = new System.Windows.Forms.ToolStripTextBox();
            this.tSBFind = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
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
            this.tSTBED,
            this.tSBFind});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(908, 25);
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
            // tSTBED
            // 
            this.tSTBED.Name = "tSTBED";
            this.tSTBED.Size = new System.Drawing.Size(100, 25);
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
            this.statusStrip1.Size = new System.Drawing.Size(908, 22);
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
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(257, 368);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(479, 368);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
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
            glColumn3.NumericSort = true;
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
            glColumn5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
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
            glColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            glColumn7.Width = 80;
            glColumn8.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn8.CheckBoxes = false;
            glColumn8.ImageIndex = -1;
            glColumn8.Name = "TV";
            glColumn8.NumericSort = true;
            glColumn8.Text = "量程上限";
            glColumn8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn8.Width = 70;
            glColumn9.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn9.CheckBoxes = false;
            glColumn9.ImageIndex = -1;
            glColumn9.Name = "BV";
            glColumn9.NumericSort = true;
            glColumn9.Text = "量程下限";
            glColumn9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn9.Width = 70;
            this.glacialList.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn1,
            glColumn2,
            glColumn3,
            glColumn4,
            glColumn5,
            glColumn6,
            glColumn7,
            glColumn8,
            glColumn9});
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
            this.glacialList.Size = new System.Drawing.Size(881, 303);
            this.glacialList.SortType = GlacialComponents.Controls.SortTypes.QuickSort;
            this.glacialList.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.glacialList.TabIndex = 1;
            this.glacialList.Text = "glacialList";
            // 
            // FormSisPointList
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(908, 424);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.glacialList);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSisPointList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新加SIS点";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSisPointList_FormClosed);
            this.Shown += new System.EventHandler(this.FormSisPointList_Shown);
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
        private System.Windows.Forms.ToolStripButton tSBFind;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.ToolStripComboBox tSCBNode;
        private System.Windows.Forms.ToolStripTextBox tSTBED;
        private GlacialComponents.Controls.GlacialList glacialList;
    }
}
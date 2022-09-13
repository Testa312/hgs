namespace HGS
{
    partial class FormCalcPointList
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
            GlacialComponents.Controls.GLColumn glColumn7 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn8 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn9 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn10 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn11 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn12 = new GlacialComponents.Controls.GLColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.tSCBNode = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tSTBPN = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tSTBED = new System.Windows.Forms.ToolStripTextBox();
            this.tSBFind = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tSSLabel_nums = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.toolStripLabel2,
            this.tSTBPN,
            this.toolStripLabel3,
            this.tSTBED,
            this.tSBFind});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(701, 25);
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
            this.tSCBNode.SelectedIndexChanged += new System.EventHandler(this.tSBFind_Click);
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
            this.tSTBPN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tSTBPN_KeyDown);
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
            this.tSSLabel_nums});
            this.statusStrip1.Location = new System.Drawing.Point(0, 402);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(701, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "点数：";
            // 
            // tSSLabel_nums
            // 
            this.tSSLabel_nums.Name = "tSSLabel_nums";
            this.tSSLabel_nums.Size = new System.Drawing.Size(44, 17);
            this.tSSLabel_nums.Text = "点数：";
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
            this.button1.Location = new System.Drawing.Point(186, 368);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.Location = new System.Drawing.Point(418, 368);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
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
            glColumn7.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn7.CheckBoxes = false;
            glColumn7.ImageIndex = -1;
            glColumn7.Name = "ND";
            glColumn7.NumericSort = false;
            glColumn7.Text = "    节点";
            glColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn7.Width = 100;
            glColumn8.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn8.CheckBoxes = false;
            glColumn8.ImageIndex = -1;
            glColumn8.Name = "PN";
            glColumn8.NumericSort = false;
            glColumn8.Text = "    点名";
            glColumn8.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn8.Width = 150;
            glColumn9.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn9.CheckBoxes = false;
            glColumn9.ImageIndex = -1;
            glColumn9.Name = "AV";
            glColumn9.NumericSort = true;
            glColumn9.Text = "值     ";
            glColumn9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn9.Width = 60;
            glColumn10.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn10.CheckBoxes = false;
            glColumn10.ImageIndex = -1;
            glColumn10.Name = "EU";
            glColumn10.NumericSort = false;
            glColumn10.Text = "单位";
            glColumn10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            glColumn10.Width = 60;
            glColumn11.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn11.CheckBoxes = false;
            glColumn11.ImageIndex = -1;
            glColumn11.Name = "ED";
            glColumn11.NumericSort = false;
            glColumn11.Text = "   描述";
            glColumn11.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn11.Width = 200;
            glColumn12.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn12.CheckBoxes = false;
            glColumn12.ImageIndex = -1;
            glColumn12.Name = "DS";
            glColumn12.NumericSort = false;
            glColumn12.Text = "质量";
            glColumn12.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            glColumn12.Width = 80;
            this.glacialList.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn7,
            glColumn8,
            glColumn9,
            glColumn10,
            glColumn11,
            glColumn12});
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
            this.glacialList.Size = new System.Drawing.Size(678, 303);
            this.glacialList.SortType = GlacialComponents.Controls.SortTypes.QuickSort;
            this.glacialList.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.glacialList.TabIndex = 1;
            this.glacialList.Text = "glacialList";
            // 
            // FormCalcPointList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 424);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.glacialList);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCalcPointList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "计算点选择";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSisPointList_FormClosed);
            this.Shown += new System.EventHandler(this.FormCalcPointList_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox tSTBPN;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripButton tSBFind;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripStatusLabel tSSLabel_nums;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        internal GlacialComponents.Controls.GlacialList glacialList;
        public System.Windows.Forms.ToolStripComboBox tSCBNode;
        private System.Windows.Forms.ToolStripTextBox tSTBED;
    }
}
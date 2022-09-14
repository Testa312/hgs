
namespace ImportFromSisStat
{
    partial class FormImportFromFile
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
            GlacialComponents.Controls.GLColumn glColumn6 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn7 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn8 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn9 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn10 = new GlacialComponents.Controls.GLColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxND = new System.Windows.Forms.ComboBox();
            this.checkBox_min = new System.Windows.Forms.CheckBox();
            this.checkBox_max = new System.Windows.Forms.CheckBox();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.glacialList1 = new GlacialComponents.Controls.GlacialList();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.comboBoxClass = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
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
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 511);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(705, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(44, 17);
            this.toolStripStatusLabel1.Text = "点数：";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.buttonOK);
            this.splitContainer1.Panel2.Controls.Add(this.buttonCancel);
            this.splitContainer1.Size = new System.Drawing.Size(705, 511);
            this.splitContainer1.SplitterDistance = 436;
            this.splitContainer1.TabIndex = 2;
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
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.comboBoxClass);
            this.splitContainer2.Panel1.Controls.Add(this.comboBoxND);
            this.splitContainer2.Panel1.Controls.Add(this.checkBox_min);
            this.splitContainer2.Panel1.Controls.Add(this.checkBox_max);
            this.splitContainer2.Panel1.Controls.Add(this.buttonOpen);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.glacialList1);
            this.splitContainer2.Size = new System.Drawing.Size(705, 436);
            this.splitContainer2.SplitterDistance = 35;
            this.splitContainer2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(256, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "节点：";
            // 
            // comboBoxND
            // 
            this.comboBoxND.FormattingEnabled = true;
            this.comboBoxND.Location = new System.Drawing.Point(297, 11);
            this.comboBoxND.Name = "comboBoxND";
            this.comboBoxND.Size = new System.Drawing.Size(121, 20);
            this.comboBoxND.TabIndex = 2;
            // 
            // checkBox_min
            // 
            this.checkBox_min.AutoSize = true;
            this.checkBox_min.Checked = true;
            this.checkBox_min.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_min.Location = new System.Drawing.Point(536, 12);
            this.checkBox_min.Name = "checkBox_min";
            this.checkBox_min.Size = new System.Drawing.Size(60, 16);
            this.checkBox_min.TabIndex = 1;
            this.checkBox_min.Text = "最小值";
            this.checkBox_min.UseVisualStyleBackColor = true;
            // 
            // checkBox_max
            // 
            this.checkBox_max.AutoSize = true;
            this.checkBox_max.Checked = true;
            this.checkBox_max.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_max.Location = new System.Drawing.Point(602, 12);
            this.checkBox_max.Name = "checkBox_max";
            this.checkBox_max.Size = new System.Drawing.Size(60, 16);
            this.checkBox_max.TabIndex = 1;
            this.checkBox_max.Text = "最大值";
            this.checkBox_max.UseVisualStyleBackColor = true;
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(12, 7);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(51, 23);
            this.buttonOpen.TabIndex = 0;
            this.buttonOpen.Text = "打开";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
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
            glColumn6.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn6.CheckBoxes = false;
            glColumn6.ImageIndex = -1;
            glColumn6.Name = "ND";
            glColumn6.NumericSort = false;
            glColumn6.Text = "父节点";
            glColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn6.Width = 80;
            glColumn7.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn7.CheckBoxes = false;
            glColumn7.ImageIndex = -1;
            glColumn7.Name = "PN";
            glColumn7.NumericSort = false;
            glColumn7.Text = "点名";
            glColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn7.Width = 150;
            glColumn8.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn8.CheckBoxes = false;
            glColumn8.ImageIndex = -1;
            glColumn8.Name = "ED";
            glColumn8.NumericSort = false;
            glColumn8.Text = "描述";
            glColumn8.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn8.Width = 300;
            glColumn9.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn9.CheckBoxes = false;
            glColumn9.ImageIndex = -1;
            glColumn9.Name = "Min";
            glColumn9.NumericSort = false;
            glColumn9.Text = "最小值";
            glColumn9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn9.Width = 70;
            glColumn10.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn10.CheckBoxes = false;
            glColumn10.ImageIndex = -1;
            glColumn10.Name = "Max";
            glColumn10.NumericSort = false;
            glColumn10.Text = "最大值";
            glColumn10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn10.Width = 70;
            this.glacialList1.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn6,
            glColumn7,
            glColumn8,
            glColumn9,
            glColumn10});
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
            this.glacialList1.Size = new System.Drawing.Size(705, 397);
            this.glacialList1.SortType = GlacialComponents.Controls.SortTypes.None;
            this.glacialList1.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.glacialList1.TabIndex = 0;
            this.glacialList1.Text = "glacialList1";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(418, 29);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "导入";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(189, 29);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "取肖";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "\"csv files (*.csv)|*.csv|All files (*.*)|*.*\";";
            this.openFileDialog.Title = "打开sis导出文件";
            // 
            // comboBoxClass
            // 
            this.comboBoxClass.FormattingEnabled = true;
            this.comboBoxClass.Location = new System.Drawing.Point(123, 9);
            this.comboBoxClass.Name = "comboBoxClass";
            this.comboBoxClass.Size = new System.Drawing.Size(121, 20);
            this.comboBoxClass.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "专业：";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 533);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "FormMain";
            this.Text = "从SIS统计导入报警值";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
        private System.Windows.Forms.SplitContainer splitContainer1;
        private GlacialComponents.Controls.GlacialList glacialList1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.CheckBox checkBox_min;
        private System.Windows.Forms.CheckBox checkBox_max;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ComboBox comboBoxND;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxClass;
    }
}


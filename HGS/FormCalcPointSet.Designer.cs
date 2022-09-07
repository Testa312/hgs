namespace HGS
{
    partial class FormCalcPointSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCalcPointSet));
            GlacialComponents.Controls.GLColumn glColumn8 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn9 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn10 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn11 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn12 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn13 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn14 = new GlacialComponents.Controls.GLColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.glacialList1 = new GlacialComponents.Controls.GlacialList();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageCalc = new System.Windows.Forms.TabPage();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.checkBoxCalc = new System.Windows.Forms.CheckBox();
            this.comboBox_eu = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCancell = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonTest = new System.Windows.Forms.Button();
            this.textBoxmDiscription = new System.Windows.Forms.TextBox();
            this.textBoxFormula = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tSSLabel_varnums = new System.Windows.Forms.ToolStripStatusLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPN = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageCalc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAdd});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(728, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAdd.Image")));
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Size = new System.Drawing.Size(52, 22);
            this.toolStripButtonAdd.Text = "填加";
            this.toolStripButtonAdd.Click += new System.EventHandler(this.toolStripButtonAdd_Click_1);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.glacialList1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(728, 498);
            this.splitContainer1.SplitterDistance = 265;
            this.splitContainer1.TabIndex = 1;
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
            glColumn8.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.TextBox;
            glColumn8.CheckBoxes = false;
            glColumn8.ImageIndex = -1;
            glColumn8.Name = "VarName";
            glColumn8.NumericSort = false;
            glColumn8.Text = "  变量名";
            glColumn8.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn8.Width = 80;
            glColumn9.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn9.CheckBoxes = false;
            glColumn9.ImageIndex = -1;
            glColumn9.Name = "ND";
            glColumn9.NumericSort = false;
            glColumn9.Text = "  节点";
            glColumn9.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn9.Width = 80;
            glColumn10.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn10.CheckBoxes = false;
            glColumn10.ImageIndex = -1;
            glColumn10.Name = "PN";
            glColumn10.NumericSort = false;
            glColumn10.Text = "  点名（SIS）";
            glColumn10.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn10.Width = 150;
            glColumn11.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn11.CheckBoxes = false;
            glColumn11.ImageIndex = -1;
            glColumn11.Name = "AV";
            glColumn11.NumericSort = true;
            glColumn11.Text = "值  ";
            glColumn11.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn11.Width = 70;
            glColumn12.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn12.CheckBoxes = false;
            glColumn12.ImageIndex = -1;
            glColumn12.Name = "EU";
            glColumn12.NumericSort = false;
            glColumn12.Text = "单位";
            glColumn12.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            glColumn12.Width = 50;
            glColumn13.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn13.CheckBoxes = false;
            glColumn13.ImageIndex = -1;
            glColumn13.Name = "ED";
            glColumn13.NumericSort = false;
            glColumn13.Text = "  描述";
            glColumn13.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn13.Width = 180;
            glColumn14.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn14.CheckBoxes = false;
            glColumn14.ImageIndex = -1;
            glColumn14.Name = "DS";
            glColumn14.NumericSort = false;
            glColumn14.Text = "质量";
            glColumn14.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            glColumn14.Width = 80;
            this.glacialList1.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn8,
            glColumn9,
            glColumn10,
            glColumn11,
            glColumn12,
            glColumn13,
            glColumn14});
            this.glacialList1.ControlStyle = GlacialComponents.Controls.GLControlStyles.Normal;
            this.glacialList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glacialList1.FullRowSelect = false;
            this.glacialList1.GridColor = System.Drawing.Color.LightGray;
            this.glacialList1.GridLines = GlacialComponents.Controls.GLGridLines.gridBoth;
            this.glacialList1.GridLineStyle = GlacialComponents.Controls.GLGridLineStyles.gridSolid;
            this.glacialList1.GridTypes = GlacialComponents.Controls.GLGridTypes.gridOnExists;
            this.glacialList1.HeaderHeight = 22;
            this.glacialList1.HeaderVisible = true;
            this.glacialList1.HeaderWordWrap = false;
            this.glacialList1.HotColumnTracking = true;
            this.glacialList1.HotItemTracking = true;
            this.glacialList1.HotTrackingColor = System.Drawing.Color.LightGray;
            this.glacialList1.HoverEvents = true;
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
            this.glacialList1.ShowFocusRect = true;
            this.glacialList1.Size = new System.Drawing.Size(728, 265);
            this.glacialList1.SortType = GlacialComponents.Controls.SortTypes.InsertionSort;
            this.glacialList1.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.glacialList1.TabIndex = 1;
            this.glacialList1.Text = "glacialList1";
            this.glacialList1.DoubleClick += new System.EventHandler(this.glacialList1_DoubleClick);
            this.glacialList1.Leave += new System.EventHandler(this.glacialList1_Leave);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageCalc);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(728, 229);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageCalc
            // 
            this.tabPageCalc.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPageCalc.Controls.Add(this.numericUpDown);
            this.tabPageCalc.Controls.Add(this.checkBoxCalc);
            this.tabPageCalc.Controls.Add(this.comboBox_eu);
            this.tabPageCalc.Controls.Add(this.label2);
            this.tabPageCalc.Controls.Add(this.label1);
            this.tabPageCalc.Controls.Add(this.buttonCancell);
            this.tabPageCalc.Controls.Add(this.buttonOK);
            this.tabPageCalc.Controls.Add(this.buttonTest);
            this.tabPageCalc.Controls.Add(this.textBoxPN);
            this.tabPageCalc.Controls.Add(this.textBoxmDiscription);
            this.tabPageCalc.Controls.Add(this.textBoxFormula);
            this.tabPageCalc.Controls.Add(this.label3);
            this.tabPageCalc.Controls.Add(this.label15);
            this.tabPageCalc.Controls.Add(this.label14);
            this.tabPageCalc.Location = new System.Drawing.Point(4, 22);
            this.tabPageCalc.Name = "tabPageCalc";
            this.tabPageCalc.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCalc.Size = new System.Drawing.Size(720, 203);
            this.tabPageCalc.TabIndex = 1;
            this.tabPageCalc.Text = "计算点";
            // 
            // numericUpDown
            // 
            this.numericUpDown.Location = new System.Drawing.Point(422, 53);
            this.numericUpDown.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(53, 21);
            this.numericUpDown.TabIndex = 7;
            this.numericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // checkBoxCalc
            // 
            this.checkBoxCalc.AutoSize = true;
            this.checkBoxCalc.Location = new System.Drawing.Point(153, 57);
            this.checkBoxCalc.Name = "checkBoxCalc";
            this.checkBoxCalc.Size = new System.Drawing.Size(48, 16);
            this.checkBoxCalc.TabIndex = 5;
            this.checkBoxCalc.Text = "计算";
            this.checkBoxCalc.UseVisualStyleBackColor = true;
            // 
            // comboBox_eu
            // 
            this.comboBox_eu.FormattingEnabled = true;
            this.comboBox_eu.Items.AddRange(new object[] {
            "℃",
            "A",
            "kA",
            "V",
            "kV",
            "t/h",
            "Pa",
            "kPa",
            "MPa",
            "t",
            "%",
            "J",
            "kJ",
            "MJ"});
            this.comboBox_eu.Location = new System.Drawing.Point(265, 54);
            this.comboBox_eu.Name = "comboBox_eu";
            this.comboBox_eu.Size = new System.Drawing.Size(53, 20);
            this.comboBox_eu.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(368, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "小数位：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(224, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "单位：";
            // 
            // buttonCancell
            // 
            this.buttonCancell.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancell.Location = new System.Drawing.Point(618, 17);
            this.buttonCancell.Name = "buttonCancell";
            this.buttonCancell.Size = new System.Drawing.Size(75, 23);
            this.buttonCancell.TabIndex = 9;
            this.buttonCancell.Text = "取消";
            this.buttonCancell.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(618, 59);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 8;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(509, 59);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(75, 23);
            this.buttonTest.TabIndex = 6;
            this.buttonTest.Text = "测试";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // textBoxmDiscription
            // 
            this.textBoxmDiscription.Location = new System.Drawing.Point(68, 18);
            this.textBoxmDiscription.Name = "textBoxmDiscription";
            this.textBoxmDiscription.Size = new System.Drawing.Size(250, 21);
            this.textBoxmDiscription.TabIndex = 2;
            // 
            // textBoxFormula
            // 
            this.textBoxFormula.Location = new System.Drawing.Point(23, 88);
            this.textBoxFormula.Multiline = true;
            this.textBoxFormula.Name = "textBoxFormula";
            this.textBoxFormula.Size = new System.Drawing.Size(670, 86);
            this.textBoxFormula.TabIndex = 4;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(21, 69);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 12);
            this.label15.TabIndex = 0;
            this.label15.Text = "公式：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(21, 22);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 0;
            this.label14.Text = "描述：";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSSLabel_varnums});
            this.statusStrip.Location = new System.Drawing.Point(0, 501);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(728, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip";
            // 
            // tSSLabel_varnums
            // 
            this.tSSLabel_varnums.Name = "tSSLabel_varnums";
            this.tSSLabel_varnums.Size = new System.Drawing.Size(56, 17);
            this.tSSLabel_varnums.Text = "变量数：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(328, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "点名：";
            // 
            // textBoxPN
            // 
            this.textBoxPN.Location = new System.Drawing.Point(375, 18);
            this.textBoxPN.Name = "textBoxPN";
            this.textBoxPN.Size = new System.Drawing.Size(100, 21);
            this.textBoxPN.TabIndex = 2;
            // 
            // FormCalcPointSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 523);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormCalcPointSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "计算点配置";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormPointSet_FormClosed);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageCalc.ResumeLayout(false);
            this.tabPageCalc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageCalc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancell;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        public System.Windows.Forms.ComboBox comboBox_eu;
        public System.Windows.Forms.TextBox textBoxmDiscription;
        public System.Windows.Forms.TextBox textBoxFormula;
        private System.Windows.Forms.CheckBox checkBoxCalc;
        private GlacialComponents.Controls.GlacialList glacialList1;
        private System.Windows.Forms.NumericUpDown numericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tSSLabel_varnums;
        public System.Windows.Forms.TextBox textBoxPN;
        private System.Windows.Forms.Label label3;
    }
}
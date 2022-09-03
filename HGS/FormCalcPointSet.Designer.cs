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
            GlacialComponents.Controls.GLColumn glColumn1 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn2 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn3 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn4 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn5 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn6 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn7 = new GlacialComponents.Controls.GLColumn();
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
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageCalc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
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
            glColumn1.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.TextBox;
            glColumn1.CheckBoxes = false;
            glColumn1.ImageIndex = -1;
            glColumn1.Name = "VarName";
            glColumn1.NumericSort = false;
            glColumn1.Text = "  变量名";
            glColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn1.Width = 80;
            glColumn2.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn2.CheckBoxes = false;
            glColumn2.ImageIndex = -1;
            glColumn2.Name = "ND";
            glColumn2.NumericSort = false;
            glColumn2.Text = "  节点";
            glColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn2.Width = 80;
            glColumn3.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn3.CheckBoxes = false;
            glColumn3.ImageIndex = -1;
            glColumn3.Name = "PN";
            glColumn3.NumericSort = false;
            glColumn3.Text = "  点名（SIS）";
            glColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn3.Width = 150;
            glColumn4.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn4.CheckBoxes = false;
            glColumn4.ImageIndex = -1;
            glColumn4.Name = "AV";
            glColumn4.NumericSort = true;
            glColumn4.Text = "值  ";
            glColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn4.Width = 70;
            glColumn5.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn5.CheckBoxes = false;
            glColumn5.ImageIndex = -1;
            glColumn5.Name = "EU";
            glColumn5.NumericSort = false;
            glColumn5.Text = "单位";
            glColumn5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            glColumn5.Width = 50;
            glColumn6.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn6.CheckBoxes = false;
            glColumn6.ImageIndex = -1;
            glColumn6.Name = "ED";
            glColumn6.NumericSort = false;
            glColumn6.Text = "  描述";
            glColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn6.Width = 180;
            glColumn7.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn7.CheckBoxes = false;
            glColumn7.ImageIndex = -1;
            glColumn7.Name = "DS";
            glColumn7.NumericSort = false;
            glColumn7.Text = "质量";
            glColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            glColumn7.Width = 80;
            this.glacialList1.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn1,
            glColumn2,
            glColumn3,
            glColumn4,
            glColumn5,
            glColumn6,
            glColumn7});
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
            this.glacialList1.TabIndex = 0;
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
            this.tabPageCalc.Controls.Add(this.textBoxmDiscription);
            this.tabPageCalc.Controls.Add(this.textBoxFormula);
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
            this.numericUpDown.Location = new System.Drawing.Point(375, 48);
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
            this.checkBoxCalc.Location = new System.Drawing.Point(78, 56);
            this.checkBoxCalc.Name = "checkBoxCalc";
            this.checkBoxCalc.Size = new System.Drawing.Size(48, 16);
            this.checkBoxCalc.TabIndex = 6;
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
            this.comboBox_eu.Location = new System.Drawing.Point(375, 23);
            this.comboBox_eu.Name = "comboBox_eu";
            this.comboBox_eu.Size = new System.Drawing.Size(53, 20);
            this.comboBox_eu.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(298, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "保留小数位：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(333, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "单位：";
            // 
            // buttonCancell
            // 
            this.buttonCancell.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancell.Location = new System.Drawing.Point(474, 14);
            this.buttonCancell.Name = "buttonCancell";
            this.buttonCancell.Size = new System.Drawing.Size(75, 23);
            this.buttonCancell.TabIndex = 3;
            this.buttonCancell.Text = "取消";
            this.buttonCancell.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(579, 15);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(160, 53);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(75, 23);
            this.buttonTest.TabIndex = 3;
            this.buttonTest.Text = "测试";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // textBoxmDiscription
            // 
            this.textBoxmDiscription.Location = new System.Drawing.Point(68, 22);
            this.textBoxmDiscription.Name = "textBoxmDiscription";
            this.textBoxmDiscription.Size = new System.Drawing.Size(250, 21);
            this.textBoxmDiscription.TabIndex = 2;
            // 
            // textBoxFormula
            // 
            this.textBoxFormula.Location = new System.Drawing.Point(23, 82);
            this.textBoxFormula.Multiline = true;
            this.textBoxFormula.Name = "textBoxFormula";
            this.textBoxFormula.Size = new System.Drawing.Size(670, 92);
            this.textBoxFormula.TabIndex = 1;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(21, 57);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 12);
            this.label15.TabIndex = 0;
            this.label15.Text = "公式：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(21, 25);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 0;
            this.label14.Text = "描述：";
            // 
            // FormCalcPointSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 523);
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
    }
}
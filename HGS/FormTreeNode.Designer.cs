namespace HGS
{
    partial class FormTreeNode
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
            GlacialComponents.Controls.GLColumn glColumn1 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn2 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn3 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLItem glItem1 = new GlacialComponents.Controls.GLItem();
            GlacialComponents.Controls.GLSubItem glSubItem1 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem2 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem3 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLItem glItem2 = new GlacialComponents.Controls.GLItem();
            GlacialComponents.Controls.GLSubItem glSubItem4 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem5 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem6 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLItem glItem3 = new GlacialComponents.Controls.GLItem();
            GlacialComponents.Controls.GLSubItem glSubItem7 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem8 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem9 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLItem glItem4 = new GlacialComponents.Controls.GLItem();
            GlacialComponents.Controls.GLSubItem glSubItem10 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem11 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem12 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLItem glItem5 = new GlacialComponents.Controls.GLItem();
            GlacialComponents.Controls.GLSubItem glSubItem13 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem14 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem15 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLItem glItem6 = new GlacialComponents.Controls.GLItem();
            GlacialComponents.Controls.GLSubItem glSubItem16 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem17 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem18 = new GlacialComponents.Controls.GLSubItem();
            this.BtnOK = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.tB_nodeName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ratio = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mtb_alarm_th_dis = new MaskedTextBox.MaskedTextBox();
            this.mtb_sort = new MaskedTextBox.MaskedTextBox();
            this.mtb_start_th = new MaskedTextBox.MaskedTextBox();
            this.glacialList1 = new GlacialComponents.Controls.GlacialList();
            this.btn_Fill_statis = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnOK
            // 
            this.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOK.Location = new System.Drawing.Point(82, 394);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(75, 23);
            this.BtnOK.TabIndex = 12;
            this.BtnOK.Text = "确认";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(262, 394);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 13;
            this.BtnCancel.Text = "取消";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // tB_nodeName
            // 
            this.tB_nodeName.Location = new System.Drawing.Point(139, 12);
            this.tB_nodeName.Name = "tB_nodeName";
            this.tB_nodeName.Size = new System.Drawing.Size(205, 21);
            this.tB_nodeName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "节点名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "排序：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "报警阈值（距离）：";
            // 
            // ratio
            // 
            this.ratio.AutoSize = true;
            this.ratio.Location = new System.Drawing.Point(11, 75);
            this.ratio.Name = "ratio";
            this.ratio.Size = new System.Drawing.Size(125, 12);
            this.ratio.TabIndex = 3;
            this.ratio.Text = "启动阈值（极值差）：";
            this.ratio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_Fill_statis);
            this.panel1.Controls.Add(this.glacialList1);
            this.panel1.Controls.Add(this.mtb_alarm_th_dis);
            this.panel1.Controls.Add(this.mtb_sort);
            this.panel1.Controls.Add(this.mtb_start_th);
            this.panel1.Controls.Add(this.ratio);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tB_nodeName);
            this.panel1.Location = new System.Drawing.Point(21, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(372, 354);
            this.panel1.TabIndex = 0;
            // 
            // mtb_alarm_th_dis
            // 
            this.mtb_alarm_th_dis.Location = new System.Drawing.Point(139, 99);
            this.mtb_alarm_th_dis.Masked = MaskedTextBox.Mask.Decimal;
            this.mtb_alarm_th_dis.Name = "mtb_alarm_th_dis";
            this.mtb_alarm_th_dis.Size = new System.Drawing.Size(148, 21);
            this.mtb_alarm_th_dis.TabIndex = 13;
            // 
            // mtb_sort
            // 
            this.mtb_sort.Location = new System.Drawing.Point(139, 45);
            this.mtb_sort.Masked = MaskedTextBox.Mask.Digit;
            this.mtb_sort.Name = "mtb_sort";
            this.mtb_sort.Size = new System.Drawing.Size(148, 21);
            this.mtb_sort.TabIndex = 13;
            // 
            // mtb_start_th
            // 
            this.mtb_start_th.Location = new System.Drawing.Point(139, 72);
            this.mtb_start_th.Masked = MaskedTextBox.Mask.Decimal;
            this.mtb_start_th.Name = "mtb_start_th";
            this.mtb_start_th.Size = new System.Drawing.Size(148, 21);
            this.mtb_start_th.TabIndex = 13;
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
            glColumn1.Name = "TW";
            glColumn1.NumericSort = false;
            glColumn1.Text = "时间窗";
            glColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn1.Width = 100;
            glColumn2.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn2.CheckBoxes = false;
            glColumn2.ImageIndex = -1;
            glColumn2.Name = "Start";
            glColumn2.NumericSort = true;
            glColumn2.Text = "启动值";
            glColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn2.Width = 100;
            glColumn3.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn3.CheckBoxes = false;
            glColumn3.ImageIndex = -1;
            glColumn3.Name = "AlarmV";
            glColumn3.NumericSort = true;
            glColumn3.Text = "报警值";
            glColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn3.Width = 100;
            this.glacialList1.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn1,
            glColumn2,
            glColumn3});
            this.glacialList1.ControlStyle = GlacialComponents.Controls.GLControlStyles.Normal;
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
            glItem1.BackColor = System.Drawing.Color.White;
            glItem1.ForeColor = System.Drawing.Color.Black;
            glItem1.RowBorderColor = System.Drawing.Color.Black;
            glItem1.RowBorderSize = 0;
            glSubItem1.BackColor = System.Drawing.Color.Empty;
            glSubItem1.Checked = false;
            glSubItem1.ForceText = false;
            glSubItem1.ForeColor = System.Drawing.Color.Black;
            glSubItem1.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem1.ImageIndex = -1;
            glSubItem1.Text = "15m";
            glSubItem2.BackColor = System.Drawing.Color.Empty;
            glSubItem2.Checked = false;
            glSubItem2.ForceText = false;
            glSubItem2.ForeColor = System.Drawing.Color.Black;
            glSubItem2.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem2.ImageIndex = -1;
            glSubItem2.Text = "";
            glSubItem3.BackColor = System.Drawing.Color.Empty;
            glSubItem3.Checked = false;
            glSubItem3.ForceText = false;
            glSubItem3.ForeColor = System.Drawing.Color.Black;
            glSubItem3.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem3.ImageIndex = -1;
            glSubItem3.Text = "";
            glItem1.SubItems.AddRange(new GlacialComponents.Controls.GLSubItem[] {
            glSubItem1,
            glSubItem2,
            glSubItem3});
            glItem1.Text = "15m";
            glItem2.BackColor = System.Drawing.Color.White;
            glItem2.ForeColor = System.Drawing.Color.Black;
            glItem2.RowBorderColor = System.Drawing.Color.Black;
            glItem2.RowBorderSize = 0;
            glSubItem4.BackColor = System.Drawing.Color.Empty;
            glSubItem4.Checked = false;
            glSubItem4.ForceText = false;
            glSubItem4.ForeColor = System.Drawing.Color.Black;
            glSubItem4.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem4.ImageIndex = -1;
            glSubItem4.Text = "30m";
            glSubItem5.BackColor = System.Drawing.Color.Empty;
            glSubItem5.Checked = false;
            glSubItem5.ForceText = false;
            glSubItem5.ForeColor = System.Drawing.Color.Black;
            glSubItem5.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem5.ImageIndex = -1;
            glSubItem5.Text = "";
            glSubItem6.BackColor = System.Drawing.Color.Empty;
            glSubItem6.Checked = false;
            glSubItem6.ForceText = false;
            glSubItem6.ForeColor = System.Drawing.Color.Black;
            glSubItem6.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem6.ImageIndex = -1;
            glSubItem6.Text = "";
            glItem2.SubItems.AddRange(new GlacialComponents.Controls.GLSubItem[] {
            glSubItem4,
            glSubItem5,
            glSubItem6});
            glItem2.Text = "30m";
            glItem3.BackColor = System.Drawing.Color.White;
            glItem3.ForeColor = System.Drawing.Color.Black;
            glItem3.RowBorderColor = System.Drawing.Color.Black;
            glItem3.RowBorderSize = 0;
            glSubItem7.BackColor = System.Drawing.Color.Empty;
            glSubItem7.Checked = false;
            glSubItem7.ForceText = false;
            glSubItem7.ForeColor = System.Drawing.Color.Black;
            glSubItem7.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem7.ImageIndex = -1;
            glSubItem7.Text = "1h";
            glSubItem8.BackColor = System.Drawing.Color.Empty;
            glSubItem8.Checked = false;
            glSubItem8.ForceText = false;
            glSubItem8.ForeColor = System.Drawing.Color.Black;
            glSubItem8.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem8.ImageIndex = -1;
            glSubItem8.Text = "";
            glSubItem9.BackColor = System.Drawing.Color.Empty;
            glSubItem9.Checked = false;
            glSubItem9.ForceText = false;
            glSubItem9.ForeColor = System.Drawing.Color.Black;
            glSubItem9.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem9.ImageIndex = -1;
            glSubItem9.Text = "";
            glItem3.SubItems.AddRange(new GlacialComponents.Controls.GLSubItem[] {
            glSubItem7,
            glSubItem8,
            glSubItem9});
            glItem3.Text = "1h";
            glItem4.BackColor = System.Drawing.Color.White;
            glItem4.ForeColor = System.Drawing.Color.Black;
            glItem4.RowBorderColor = System.Drawing.Color.Black;
            glItem4.RowBorderSize = 0;
            glSubItem10.BackColor = System.Drawing.Color.Empty;
            glSubItem10.Checked = false;
            glSubItem10.ForceText = false;
            glSubItem10.ForeColor = System.Drawing.Color.Black;
            glSubItem10.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem10.ImageIndex = -1;
            glSubItem10.Text = "2h";
            glSubItem11.BackColor = System.Drawing.Color.Empty;
            glSubItem11.Checked = false;
            glSubItem11.ForceText = false;
            glSubItem11.ForeColor = System.Drawing.Color.Black;
            glSubItem11.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem11.ImageIndex = -1;
            glSubItem11.Text = "";
            glSubItem12.BackColor = System.Drawing.Color.Empty;
            glSubItem12.Checked = false;
            glSubItem12.ForceText = false;
            glSubItem12.ForeColor = System.Drawing.Color.Black;
            glSubItem12.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem12.ImageIndex = -1;
            glSubItem12.Text = "";
            glItem4.SubItems.AddRange(new GlacialComponents.Controls.GLSubItem[] {
            glSubItem10,
            glSubItem11,
            glSubItem12});
            glItem4.Text = "2h";
            glItem5.BackColor = System.Drawing.Color.White;
            glItem5.ForeColor = System.Drawing.Color.Black;
            glItem5.RowBorderColor = System.Drawing.Color.Black;
            glItem5.RowBorderSize = 0;
            glSubItem13.BackColor = System.Drawing.Color.Empty;
            glSubItem13.Checked = false;
            glSubItem13.ForceText = false;
            glSubItem13.ForeColor = System.Drawing.Color.Black;
            glSubItem13.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem13.ImageIndex = -1;
            glSubItem13.Text = "4h";
            glSubItem14.BackColor = System.Drawing.Color.Empty;
            glSubItem14.Checked = false;
            glSubItem14.ForceText = false;
            glSubItem14.ForeColor = System.Drawing.Color.Black;
            glSubItem14.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem14.ImageIndex = -1;
            glSubItem14.Text = "";
            glSubItem15.BackColor = System.Drawing.Color.Empty;
            glSubItem15.Checked = false;
            glSubItem15.ForceText = false;
            glSubItem15.ForeColor = System.Drawing.Color.Black;
            glSubItem15.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem15.ImageIndex = -1;
            glSubItem15.Text = "";
            glItem5.SubItems.AddRange(new GlacialComponents.Controls.GLSubItem[] {
            glSubItem13,
            glSubItem14,
            glSubItem15});
            glItem5.Text = "4h";
            glItem6.BackColor = System.Drawing.Color.White;
            glItem6.ForeColor = System.Drawing.Color.Black;
            glItem6.RowBorderColor = System.Drawing.Color.Black;
            glItem6.RowBorderSize = 0;
            glSubItem16.BackColor = System.Drawing.Color.Empty;
            glSubItem16.Checked = false;
            glSubItem16.ForceText = false;
            glSubItem16.ForeColor = System.Drawing.Color.Black;
            glSubItem16.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem16.ImageIndex = -1;
            glSubItem16.Text = "8h";
            glSubItem17.BackColor = System.Drawing.Color.Empty;
            glSubItem17.Checked = false;
            glSubItem17.ForceText = false;
            glSubItem17.ForeColor = System.Drawing.Color.Black;
            glSubItem17.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem17.ImageIndex = -1;
            glSubItem17.Text = "";
            glSubItem18.BackColor = System.Drawing.Color.Empty;
            glSubItem18.Checked = false;
            glSubItem18.ForceText = false;
            glSubItem18.ForeColor = System.Drawing.Color.Black;
            glSubItem18.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem18.ImageIndex = -1;
            glSubItem18.Text = "";
            glItem6.SubItems.AddRange(new GlacialComponents.Controls.GLSubItem[] {
            glSubItem16,
            glSubItem17,
            glSubItem18});
            glItem6.Text = "8h";
            this.glacialList1.Items.AddRange(new GlacialComponents.Controls.GLItem[] {
            glItem1,
            glItem2,
            glItem3,
            glItem4,
            glItem5,
            glItem6});
            this.glacialList1.ItemWordWrap = false;
            this.glacialList1.Location = new System.Drawing.Point(13, 177);
            this.glacialList1.Name = "glacialList1";
            this.glacialList1.Selectable = true;
            this.glacialList1.SelectedTextColor = System.Drawing.Color.White;
            this.glacialList1.SelectionColor = System.Drawing.Color.DarkBlue;
            this.glacialList1.ShowBorder = true;
            this.glacialList1.ShowFocusRect = false;
            this.glacialList1.Size = new System.Drawing.Size(340, 160);
            this.glacialList1.SortType = GlacialComponents.Controls.SortTypes.None;
            this.glacialList1.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.glacialList1.TabIndex = 14;
            this.glacialList1.Text = "glacialList1";
            // 
            // btn_Fill_statis
            // 
            this.btn_Fill_statis.Location = new System.Drawing.Point(278, 148);
            this.btn_Fill_statis.Name = "btn_Fill_statis";
            this.btn_Fill_statis.Size = new System.Drawing.Size(75, 23);
            this.btn_Fill_statis.TabIndex = 15;
            this.btn_Fill_statis.Text = "统计...";
            this.btn_Fill_statis.UseVisualStyleBackColor = true;
            // 
            // FormTreeNode
            // 
            this.AcceptButton = this.BtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(425, 445);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTreeNode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "属性";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Button BtnCancel;
        public System.Windows.Forms.TextBox tB_nodeName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ratio;
        private System.Windows.Forms.Panel panel1;
        private MaskedTextBox.MaskedTextBox mtb_start_th;
        private MaskedTextBox.MaskedTextBox mtb_alarm_th_dis;
        private MaskedTextBox.MaskedTextBox mtb_sort;
        private GlacialComponents.Controls.GlacialList glacialList1;
        private System.Windows.Forms.Button btn_Fill_statis;
    }
}

namespace HGS
{
    partial class FormCountOfDeviceCalcDTW
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCountOfDeviceCalcDTW));
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
            GlacialComponents.Controls.GLColumn glColumn14 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn15 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn16 = new GlacialComponents.Controls.GLColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_refresh = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.glacialList1 = new GlacialComponents.Controls.GlacialList();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.文件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(978, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 21);
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
            this.toolStripButton_refresh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(978, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_refresh
            // 
            this.toolStripButton_refresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_refresh.Image")));
            this.toolStripButton_refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_refresh.Name = "toolStripButton_refresh";
            this.toolStripButton_refresh.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton_refresh.Text = "刷新";
            this.toolStripButton_refresh.Click += new System.EventHandler(this.toolStripButton_refresh_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(978, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
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
            glColumn1.Name = "ID";
            glColumn1.NumericSort = true;
            glColumn1.Text = "ID";
            glColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn1.Width = 80;
            glColumn2.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn2.CheckBoxes = false;
            glColumn2.ImageIndex = -1;
            glColumn2.Name = "PN";
            glColumn2.NumericSort = false;
            glColumn2.Text = "点名";
            glColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn2.Width = 100;
            glColumn3.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn3.CheckBoxes = false;
            glColumn3.ImageIndex = -1;
            glColumn3.Name = "ED";
            glColumn3.NumericSort = false;
            glColumn3.Text = "描述";
            glColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn3.Width = 250;
            glColumn4.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn4.CheckBoxes = false;
            glColumn4.ImageIndex = -1;
            glColumn4.Name = "DTW";
            glColumn4.NumericSort = true;
            glColumn4.Text = "DTW次数";
            glColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn4.Width = 80;
            glColumn5.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn5.CheckBoxes = false;
            glColumn5.ImageIndex = -1;
            glColumn5.Name = "m15";
            glColumn5.NumericSort = true;
            glColumn5.Text = "15m";
            glColumn5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn5.Width = 50;
            glColumn6.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn6.CheckBoxes = false;
            glColumn6.ImageIndex = -1;
            glColumn6.Name = "m15s";
            glColumn6.NumericSort = true;
            glColumn6.Text = "15m-s";
            glColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn6.Width = 50;
            glColumn7.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn7.CheckBoxes = false;
            glColumn7.ImageIndex = -1;
            glColumn7.Name = "m30";
            glColumn7.NumericSort = true;
            glColumn7.Text = "30m";
            glColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn7.Width = 50;
            glColumn8.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn8.CheckBoxes = false;
            glColumn8.ImageIndex = -1;
            glColumn8.Name = "m30s";
            glColumn8.NumericSort = true;
            glColumn8.Text = "30m-s";
            glColumn8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn8.Width = 50;
            glColumn9.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn9.CheckBoxes = false;
            glColumn9.ImageIndex = -1;
            glColumn9.Name = "m60";
            glColumn9.NumericSort = true;
            glColumn9.Text = "60m";
            glColumn9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn9.Width = 50;
            glColumn10.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn10.CheckBoxes = false;
            glColumn10.ImageIndex = -1;
            glColumn10.Name = "m60s";
            glColumn10.NumericSort = true;
            glColumn10.Text = "60m-s";
            glColumn10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn10.Width = 50;
            glColumn11.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn11.CheckBoxes = false;
            glColumn11.ImageIndex = -1;
            glColumn11.Name = "m120";
            glColumn11.NumericSort = true;
            glColumn11.Text = "120m";
            glColumn11.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn11.Width = 50;
            glColumn12.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn12.CheckBoxes = false;
            glColumn12.ImageIndex = -1;
            glColumn12.Name = "m120s";
            glColumn12.NumericSort = true;
            glColumn12.Text = "120m-s";
            glColumn12.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn12.Width = 50;
            glColumn13.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn13.CheckBoxes = false;
            glColumn13.ImageIndex = -1;
            glColumn13.Name = "m240";
            glColumn13.NumericSort = true;
            glColumn13.Text = "240m";
            glColumn13.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn13.Width = 50;
            glColumn14.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn14.CheckBoxes = false;
            glColumn14.ImageIndex = -1;
            glColumn14.Name = "m240s";
            glColumn14.NumericSort = true;
            glColumn14.Text = "240m-s";
            glColumn14.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn14.Width = 50;
            glColumn15.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn15.CheckBoxes = false;
            glColumn15.ImageIndex = -1;
            glColumn15.Name = "m480";
            glColumn15.NumericSort = true;
            glColumn15.Text = "480m";
            glColumn15.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn15.Width = 50;
            glColumn16.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn16.CheckBoxes = false;
            glColumn16.ImageIndex = -1;
            glColumn16.Name = "m480s";
            glColumn16.NumericSort = true;
            glColumn16.Text = "480m-s";
            glColumn16.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            glColumn16.Width = 50;
            this.glacialList1.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn1,
            glColumn2,
            glColumn3,
            glColumn4,
            glColumn5,
            glColumn6,
            glColumn7,
            glColumn8,
            glColumn9,
            glColumn10,
            glColumn11,
            glColumn12,
            glColumn13,
            glColumn14,
            glColumn15,
            glColumn16});
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
            this.glacialList1.Location = new System.Drawing.Point(0, 50);
            this.glacialList1.Name = "glacialList1";
            this.glacialList1.Selectable = true;
            this.glacialList1.SelectedTextColor = System.Drawing.Color.White;
            this.glacialList1.SelectionColor = System.Drawing.Color.DarkBlue;
            this.glacialList1.ShowBorder = true;
            this.glacialList1.ShowFocusRect = false;
            this.glacialList1.Size = new System.Drawing.Size(978, 378);
            this.glacialList1.SortType = GlacialComponents.Controls.SortTypes.QuickSort;
            this.glacialList1.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.glacialList1.TabIndex = 3;
            this.glacialList1.Text = "glacialList1";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormCountOfDeviceCalcDTW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 450);
            this.Controls.Add(this.glacialList1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormCountOfDeviceCalcDTW";
            this.Text = "设备的DTW计算次数";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private GlacialComponents.Controls.GlacialList glacialList1;
        private System.Windows.Forms.ToolStripButton toolStripButton_refresh;
        private System.Windows.Forms.Timer timer1;
    }
}
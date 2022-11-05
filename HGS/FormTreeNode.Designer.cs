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
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnOK
            // 
            this.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOK.Location = new System.Drawing.Point(86, 208);
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
            this.BtnCancel.Location = new System.Drawing.Point(205, 208);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 13;
            this.BtnCancel.Text = "取消";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // tB_nodeName
            // 
            this.tB_nodeName.Location = new System.Drawing.Point(82, 18);
            this.tB_nodeName.Name = "tB_nodeName";
            this.tB_nodeName.Size = new System.Drawing.Size(205, 21);
            this.tB_nodeName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 21);
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
            this.panel1.Size = new System.Drawing.Size(317, 156);
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
            // FormTreeNode
            // 
            this.AcceptButton = this.BtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(364, 247);
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
    }
}
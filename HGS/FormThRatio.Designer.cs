
namespace HGS
{
    partial class FormThRatio
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
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonAdd = new System.Windows.Forms.RadioButton();
            this.radioButtonMulti = new System.Windows.Forms.RadioButton();
            this.button_cancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.maskedTextBox1 = new MaskedTextBox.MaskedTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "系数：";
            // 
            // radioButtonAdd
            // 
            this.radioButtonAdd.AutoSize = true;
            this.radioButtonAdd.Location = new System.Drawing.Point(122, 58);
            this.radioButtonAdd.Name = "radioButtonAdd";
            this.radioButtonAdd.Size = new System.Drawing.Size(35, 16);
            this.radioButtonAdd.TabIndex = 2;
            this.radioButtonAdd.Text = "加";
            this.radioButtonAdd.UseVisualStyleBackColor = true;
            // 
            // radioButtonMulti
            // 
            this.radioButtonMulti.AutoSize = true;
            this.radioButtonMulti.Checked = true;
            this.radioButtonMulti.Location = new System.Drawing.Point(57, 58);
            this.radioButtonMulti.Name = "radioButtonMulti";
            this.radioButtonMulti.Size = new System.Drawing.Size(35, 16);
            this.radioButtonMulti.TabIndex = 2;
            this.radioButtonMulti.TabStop = true;
            this.radioButtonMulti.Text = "乘";
            this.radioButtonMulti.UseVisualStyleBackColor = true;
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(36, 154);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 3;
            this.button_cancel.Text = "取消";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(166, 154);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "确认";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButtonMulti);
            this.panel1.Controls.Add(this.maskedTextBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.radioButtonAdd);
            this.panel1.Location = new System.Drawing.Point(36, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(205, 97);
            this.panel1.TabIndex = 4;
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(57, 18);
            this.maskedTextBox1.Masked = MaskedTextBox.Mask.Decimal;
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(100, 21);
            this.maskedTextBox1.TabIndex = 0;
            // 
            // FormThRatio
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancel;
            this.ClientSize = new System.Drawing.Size(280, 197);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.button_cancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormThRatio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择高限阈值系数";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaskedTextBox.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButtonAdd;
        private System.Windows.Forms.RadioButton radioButtonMulti;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Panel panel1;
    }
}
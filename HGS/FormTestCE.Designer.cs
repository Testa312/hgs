namespace HGS
{
    partial class FormTestCE
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
            this.buttonTestCE = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCE = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxError = new System.Windows.Forms.TextBox();
            this.buttonTest2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonTestCE
            // 
            this.buttonTestCE.Location = new System.Drawing.Point(43, 26);
            this.buttonTestCE.Name = "buttonTestCE";
            this.buttonTestCE.Size = new System.Drawing.Size(75, 23);
            this.buttonTestCE.TabIndex = 0;
            this.buttonTestCE.Text = "性能测试";
            this.buttonTestCE.UseVisualStyleBackColor = true;
            this.buttonTestCE.Click += new System.EventHandler(this.buttonTestCE_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "结果：";
            // 
            // textBoxCE
            // 
            this.textBoxCE.Location = new System.Drawing.Point(107, 110);
            this.textBoxCE.Name = "textBoxCE";
            this.textBoxCE.Size = new System.Drawing.Size(100, 21);
            this.textBoxCE.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            // 
            // textBoxError
            // 
            this.textBoxError.Location = new System.Drawing.Point(43, 190);
            this.textBoxError.Multiline = true;
            this.textBoxError.Name = "textBoxError";
            this.textBoxError.Size = new System.Drawing.Size(233, 132);
            this.textBoxError.TabIndex = 4;
            // 
            // buttonTest2
            // 
            this.buttonTest2.Location = new System.Drawing.Point(162, 26);
            this.buttonTest2.Name = "buttonTest2";
            this.buttonTest2.Size = new System.Drawing.Size(75, 23);
            this.buttonTest2.TabIndex = 5;
            this.buttonTest2.Text = "性能测试2";
            this.buttonTest2.UseVisualStyleBackColor = true;
            this.buttonTest2.Click += new System.EventHandler(this.buttonTest2_Click);
            // 
            // FormTestCE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 391);
            this.Controls.Add(this.buttonTest2);
            this.Controls.Add(this.textBoxError);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxCE);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonTestCE);
            this.Name = "FormTestCE";
            this.Text = "FormTestCE";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonTestCE;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxCE;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxError;
        private System.Windows.Forms.Button buttonTest2;
    }
}
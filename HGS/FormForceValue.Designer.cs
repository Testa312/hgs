
namespace HGS
{
    partial class FormForceValue
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancell = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.checkBoxForce = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(156, 157);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "确认";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancell
            // 
            this.buttonCancell.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancell.Location = new System.Drawing.Point(47, 157);
            this.buttonCancell.Name = "buttonCancell";
            this.buttonCancell.Size = new System.Drawing.Size(75, 23);
            this.buttonCancell.TabIndex = 2;
            this.buttonCancell.Text = "取消";
            this.buttonCancell.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBoxValue);
            this.panel1.Controls.Add(this.checkBoxForce);
            this.panel1.Location = new System.Drawing.Point(12, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(237, 115);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "值：";
            // 
            // textBoxValue
            // 
            this.textBoxValue.AcceptsReturn = true;
            this.textBoxValue.Location = new System.Drawing.Point(73, 43);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(100, 21);
            this.textBoxValue.TabIndex = 0;
            // 
            // checkBoxForce
            // 
            this.checkBoxForce.AutoSize = true;
            this.checkBoxForce.Location = new System.Drawing.Point(81, 84);
            this.checkBoxForce.Name = "checkBoxForce";
            this.checkBoxForce.Size = new System.Drawing.Size(48, 16);
            this.checkBoxForce.TabIndex = 0;
            this.checkBoxForce.Text = "强制";
            this.checkBoxForce.UseVisualStyleBackColor = true;
            // 
            // FormForceValue
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 206);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonCancell);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormForceValue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormForceValue";
            this.Activated += new System.EventHandler(this.FormForceValue_Activated);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancell;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBoxValue;
        public System.Windows.Forms.CheckBox checkBoxForce;
    }
}

namespace HGS
{
    partial class FormSettings
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_SIS_UserName = new System.Windows.Forms.TextBox();
            this.textBox_SIS_PW = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_PG_UserName = new System.Windows.Forms.TextBox();
            this.textBox_PG_PW = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.maskedTextBox_PG_PORT = new MaskedTextBox.MaskedTextBox();
            this.maskedTextBox_PG_IP = new MaskedTextBox.MaskedTextBox();
            this.maskedTextBox_SIS_PORT = new MaskedTextBox.MaskedTextBox();
            this.maskedTextBox_SIS_IP = new MaskedTextBox.MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.buttonCancel);
            this.splitContainer1.Panel2.Controls.Add(this.buttonOk);
            this.splitContainer1.Size = new System.Drawing.Size(388, 363);
            this.splitContainer1.SplitterDistance = 307;
            this.splitContainer1.TabIndex = 0;
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(236, 14);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "确认";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(82, 14);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(388, 307);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(380, 281);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "性能";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(380, 281);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "数据库";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP地址：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "用户名：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(175, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "密码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(175, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "端口：";
            // 
            // textBox_SIS_UserName
            // 
            this.textBox_SIS_UserName.Location = new System.Drawing.Point(59, 60);
            this.textBox_SIS_UserName.Name = "textBox_SIS_UserName";
            this.textBox_SIS_UserName.Size = new System.Drawing.Size(100, 21);
            this.textBox_SIS_UserName.TabIndex = 2;
            // 
            // textBox_SIS_PW
            // 
            this.textBox_SIS_PW.Location = new System.Drawing.Point(213, 62);
            this.textBox_SIS_PW.Name = "textBox_SIS_PW";
            this.textBox_SIS_PW.Size = new System.Drawing.Size(100, 21);
            this.textBox_SIS_PW.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.maskedTextBox_SIS_PORT);
            this.groupBox2.Controls.Add(this.textBox_SIS_PW);
            this.groupBox2.Controls.Add(this.maskedTextBox_SIS_IP);
            this.groupBox2.Controls.Add(this.textBox_SIS_UserName);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(23, 159);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(333, 100);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SIS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "IP地址：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "用户名：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(174, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "密码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(175, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "端口：";
            // 
            // textBox_PG_UserName
            // 
            this.textBox_PG_UserName.Location = new System.Drawing.Point(59, 61);
            this.textBox_PG_UserName.Name = "textBox_PG_UserName";
            this.textBox_PG_UserName.Size = new System.Drawing.Size(100, 21);
            this.textBox_PG_UserName.TabIndex = 2;
            // 
            // textBox_PG_PW
            // 
            this.textBox_PG_PW.Location = new System.Drawing.Point(213, 63);
            this.textBox_PG_PW.Name = "textBox_PG_PW";
            this.textBox_PG_PW.Size = new System.Drawing.Size(100, 21);
            this.textBox_PG_PW.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.maskedTextBox_PG_PORT);
            this.groupBox1.Controls.Add(this.maskedTextBox_PG_IP);
            this.groupBox1.Controls.Add(this.textBox_PG_PW);
            this.groupBox1.Controls.Add(this.textBox_PG_UserName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(23, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据库";
            // 
            // maskedTextBox_PG_PORT
            // 
            this.maskedTextBox_PG_PORT.Location = new System.Drawing.Point(213, 36);
            this.maskedTextBox_PG_PORT.Masked = MaskedTextBox.Mask.Digit;
            this.maskedTextBox_PG_PORT.Name = "maskedTextBox_PG_PORT";
            this.maskedTextBox_PG_PORT.Size = new System.Drawing.Size(60, 21);
            this.maskedTextBox_PG_PORT.TabIndex = 3;
            // 
            // maskedTextBox_PG_IP
            // 
            this.maskedTextBox_PG_IP.Location = new System.Drawing.Point(59, 34);
            this.maskedTextBox_PG_IP.Masked = MaskedTextBox.Mask.IpAddress;
            this.maskedTextBox_PG_IP.Name = "maskedTextBox_PG_IP";
            this.maskedTextBox_PG_IP.Size = new System.Drawing.Size(100, 21);
            this.maskedTextBox_PG_IP.TabIndex = 3;
            // 
            // maskedTextBox_SIS_PORT
            // 
            this.maskedTextBox_SIS_PORT.Location = new System.Drawing.Point(213, 35);
            this.maskedTextBox_SIS_PORT.Masked = MaskedTextBox.Mask.Digit;
            this.maskedTextBox_SIS_PORT.Name = "maskedTextBox_SIS_PORT";
            this.maskedTextBox_SIS_PORT.Size = new System.Drawing.Size(60, 21);
            this.maskedTextBox_SIS_PORT.TabIndex = 3;
            // 
            // maskedTextBox_SIS_IP
            // 
            this.maskedTextBox_SIS_IP.Location = new System.Drawing.Point(59, 33);
            this.maskedTextBox_SIS_IP.Masked = MaskedTextBox.Mask.IpAddress;
            this.maskedTextBox_SIS_IP.Name = "maskedTextBox_SIS_IP";
            this.maskedTextBox_SIS_IP.Size = new System.Drawing.Size(100, 21);
            this.maskedTextBox_SIS_IP.TabIndex = 3;
            // 
            // FormSettings
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 363);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.Text = "配置";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_PG_PW;
        private System.Windows.Forms.TextBox textBox_PG_UserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox_SIS_PW;
        private System.Windows.Forms.TextBox textBox_SIS_UserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private MaskedTextBox.MaskedTextBox maskedTextBox_PG_PORT;
        private MaskedTextBox.MaskedTextBox maskedTextBox_PG_IP;
        private MaskedTextBox.MaskedTextBox maskedTextBox_SIS_PORT;
        private MaskedTextBox.MaskedTextBox maskedTextBox_SIS_IP;
    }
}
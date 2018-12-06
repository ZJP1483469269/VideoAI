namespace GTWS_TASK.UI
{
    partial class frmConfig
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
            this.btnApply = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtVIDEO_PASS = new System.Windows.Forms.TextBox();
            this.txtVIDEO_USER = new System.Windows.Forms.TextBox();
            this.txtVIDEO_PORT = new System.Windows.Forms.TextBox();
            this.txtVIDEO_HOST = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.DFS_URL = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.DFS_PASS = new System.Windows.Forms.TextBox();
            this.DFS_USER = new System.Windows.Forms.TextBox();
            this.DFS_PORT = new System.Windows.Forms.TextBox();
            this.DFS_HOST = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(395, 227);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 31);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "确定";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(476, 227);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 31);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtVIDEO_PASS);
            this.groupBox1.Controls.Add(this.txtVIDEO_USER);
            this.groupBox1.Controls.Add(this.txtVIDEO_PORT);
            this.groupBox1.Controls.Add(this.txtVIDEO_HOST);
            this.groupBox1.Location = new System.Drawing.Point(12, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(536, 84);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "蓝天卫士";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "地址：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(281, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "密码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "账号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(281, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "端口：";
            // 
            // txtVIDEO_PASS
            // 
            this.txtVIDEO_PASS.Location = new System.Drawing.Point(328, 53);
            this.txtVIDEO_PASS.Name = "txtVIDEO_PASS";
            this.txtVIDEO_PASS.Size = new System.Drawing.Size(192, 21);
            this.txtVIDEO_PASS.TabIndex = 12;
            // 
            // txtVIDEO_USER
            // 
            this.txtVIDEO_USER.Location = new System.Drawing.Point(63, 53);
            this.txtVIDEO_USER.Name = "txtVIDEO_USER";
            this.txtVIDEO_USER.Size = new System.Drawing.Size(192, 21);
            this.txtVIDEO_USER.TabIndex = 11;
            // 
            // txtVIDEO_PORT
            // 
            this.txtVIDEO_PORT.Location = new System.Drawing.Point(328, 20);
            this.txtVIDEO_PORT.Name = "txtVIDEO_PORT";
            this.txtVIDEO_PORT.Size = new System.Drawing.Size(192, 21);
            this.txtVIDEO_PORT.TabIndex = 13;
            // 
            // txtVIDEO_HOST
            // 
            this.txtVIDEO_HOST.Location = new System.Drawing.Point(63, 20);
            this.txtVIDEO_HOST.Name = "txtVIDEO_HOST";
            this.txtVIDEO_HOST.Size = new System.Drawing.Size(192, 21);
            this.txtVIDEO_HOST.TabIndex = 14;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.DFS_URL);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.DFS_PASS);
            this.groupBox2.Controls.Add(this.DFS_USER);
            this.groupBox2.Controls.Add(this.DFS_PORT);
            this.groupBox2.Controls.Add(this.DFS_HOST);
            this.groupBox2.Location = new System.Drawing.Point(12, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(536, 121);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "文件服务器";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 20;
            this.label9.Text = "路径：";
            // 
            // DFS_URL
            // 
            this.DFS_URL.Location = new System.Drawing.Point(63, 86);
            this.DFS_URL.Name = "DFS_URL";
            this.DFS_URL.Size = new System.Drawing.Size(457, 21);
            this.DFS_URL.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "地址：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(281, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "密码：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "账号：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(281, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 18;
            this.label8.Text = "端口：";
            // 
            // DFS_PASS
            // 
            this.DFS_PASS.Location = new System.Drawing.Point(328, 52);
            this.DFS_PASS.Name = "DFS_PASS";
            this.DFS_PASS.Size = new System.Drawing.Size(192, 21);
            this.DFS_PASS.TabIndex = 12;
            // 
            // DFS_USER
            // 
            this.DFS_USER.Location = new System.Drawing.Point(63, 52);
            this.DFS_USER.Name = "DFS_USER";
            this.DFS_USER.Size = new System.Drawing.Size(192, 21);
            this.DFS_USER.TabIndex = 11;
            // 
            // DFS_PORT
            // 
            this.DFS_PORT.Location = new System.Drawing.Point(328, 19);
            this.DFS_PORT.Name = "DFS_PORT";
            this.DFS_PORT.Size = new System.Drawing.Size(192, 21);
            this.DFS_PORT.TabIndex = 13;
            // 
            // DFS_HOST
            // 
            this.DFS_HOST.Location = new System.Drawing.Point(63, 19);
            this.DFS_HOST.Name = "DFS_HOST";
            this.DFS_HOST.Size = new System.Drawing.Size(192, 21);
            this.DFS_HOST.TabIndex = 14;
            // 
            // frmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 270);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnApply);
            this.Name = "frmConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统配置";
            this.Load += new System.EventHandler(this.frmConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtVIDEO_PASS;
        private System.Windows.Forms.TextBox txtVIDEO_USER;
        private System.Windows.Forms.TextBox txtVIDEO_PORT;
        private System.Windows.Forms.TextBox txtVIDEO_HOST;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox DFS_PASS;
        private System.Windows.Forms.TextBox DFS_USER;
        private System.Windows.Forms.TextBox DFS_PORT;
        private System.Windows.Forms.TextBox DFS_HOST;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox DFS_URL;
    }
}
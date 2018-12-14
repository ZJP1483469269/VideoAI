namespace WEB_TASK.UI
{
    partial class frmDBConfig
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dbHost = new System.Windows.Forms.TextBox();
            this.dbUser = new System.Windows.Forms.TextBox();
            this.dbPass = new System.Windows.Forms.TextBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cmbDBType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "数据库地址：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "数据库类型：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "数据库用户：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "数据库密码：";
            // 
            // dbHost
            // 
            this.dbHost.Location = new System.Drawing.Point(105, 64);
            this.dbHost.Name = "dbHost";
            this.dbHost.Size = new System.Drawing.Size(209, 21);
            this.dbHost.TabIndex = 14;
            // 
            // dbUser
            // 
            this.dbUser.Location = new System.Drawing.Point(106, 100);
            this.dbUser.Name = "dbUser";
            this.dbUser.Size = new System.Drawing.Size(209, 21);
            this.dbUser.TabIndex = 14;
            // 
            // dbPass
            // 
            this.dbPass.Location = new System.Drawing.Point(106, 140);
            this.dbPass.Name = "dbPass";
            this.dbPass.Size = new System.Drawing.Size(209, 21);
            this.dbPass.TabIndex = 14;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(96, 182);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 29);
            this.btnApply.TabIndex = 15;
            this.btnApply.Text = "确定(&A)";
            this.btnApply.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(201, 182);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 29);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cmbDBType
            // 
            this.cmbDBType.FormattingEnabled = true;
            this.cmbDBType.Location = new System.Drawing.Point(106, 26);
            this.cmbDBType.Name = "cmbDBType";
            this.cmbDBType.Size = new System.Drawing.Size(208, 20);
            this.cmbDBType.TabIndex = 16;
            // 
            // frmDBConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 231);
            this.Controls.Add(this.cmbDBType);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.dbPass);
            this.Controls.Add(this.dbUser);
            this.Controls.Add(this.dbHost);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmDBConfig";
            this.Text = "数据库配置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox dbHost;
        private System.Windows.Forms.TextBox dbUser;
        private System.Windows.Forms.TextBox dbPass;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cmbDBType;
    }
}
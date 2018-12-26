namespace WEB_TASK.UI
{
    partial class frmRule
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
            this.txtUrlID = new System.Windows.Forms.TextBox();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.txtLayer = new System.Windows.Forms.TextBox();
            this.txtPageVal = new System.Windows.Forms.TextBox();
            this.txtMatch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUrlName = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtUrlID
            // 
            this.txtUrlID.Location = new System.Drawing.Point(85, 201);
            this.txtUrlID.Name = "txtUrlID";
            this.txtUrlID.Size = new System.Drawing.Size(510, 21);
            this.txtUrlID.TabIndex = 17;
            this.txtUrlID.Text = "NEWS";
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(85, 90);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(510, 21);
            this.txtPrefix.TabIndex = 16;
            this.txtPrefix.Text = "https://www.henan.gov.cn/";
            // 
            // txtLayer
            // 
            this.txtLayer.Location = new System.Drawing.Point(85, 127);
            this.txtLayer.Name = "txtLayer";
            this.txtLayer.Size = new System.Drawing.Size(140, 21);
            this.txtLayer.TabIndex = 15;
            this.txtLayer.Text = "5";
            // 
            // txtPageVal
            // 
            this.txtPageVal.Location = new System.Drawing.Point(319, 132);
            this.txtPageVal.Name = "txtPageVal";
            this.txtPageVal.Size = new System.Drawing.Size(276, 21);
            this.txtPageVal.TabIndex = 14;
            this.txtPageVal.Text = "{PAGE_NO}<1?\'\':\'_{PAGE_NO}\'";
            // 
            // txtMatch
            // 
            this.txtMatch.Location = new System.Drawing.Point(85, 164);
            this.txtMatch.Name = "txtMatch";
            this.txtMatch.Size = new System.Drawing.Size(140, 21);
            this.txtMatch.TabIndex = 13;
            this.txtMatch.Text = "a";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "层数：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "前缀：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "选择器：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "规则编码：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(236, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "页码计算式：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 18;
            this.label6.Text = "网页名称：";
            // 
            // txtUrlName
            // 
            this.txtUrlName.Location = new System.Drawing.Point(85, 16);
            this.txtUrlName.Name = "txtUrlName";
            this.txtUrlName.Size = new System.Drawing.Size(510, 21);
            this.txtUrlName.TabIndex = 16;
            this.txtUrlName.Text = "百度";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(520, 248);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 29);
            this.btnClose.TabIndex = 22;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(425, 248);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 29);
            this.btnApply.TabIndex = 23;
            this.btnApply.Text = "确定(&A)";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 18;
            this.label7.Text = "网页地址：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(85, 53);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(510, 21);
            this.textBox1.TabIndex = 16;
            this.textBox1.Text = "https://www.henan.gov.cn/";
            // 
            // frmRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 302);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtUrlID);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtUrlName);
            this.Controls.Add(this.txtPrefix);
            this.Controls.Add(this.txtLayer);
            this.Controls.Add(this.txtMatch);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPageVal);
            this.Name = "frmRule";
            this.Text = "frmRule";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnApply;
        public System.Windows.Forms.TextBox txtUrlID;
        public System.Windows.Forms.TextBox txtPrefix;
        public System.Windows.Forms.TextBox txtLayer;
        public System.Windows.Forms.TextBox txtMatch;
        public System.Windows.Forms.TextBox txtUrlName;
        public System.Windows.Forms.TextBox txtPageVal;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox textBox1;
    }
}
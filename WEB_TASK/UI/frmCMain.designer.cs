namespace WEB_TASK
{
    partial class frmCMain
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
            this.button1 = new System.Windows.Forms.Button();
            this.txtMatch = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.txtDepth = new System.Windows.Forms.TextBox();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lstUrls = new System.Windows.Forms.ListBox();
            this.button4 = new System.Windows.Forms.Button();
            this.txtUrlID = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(461, 73);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "打开文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtMatch
            // 
            this.txtMatch.Location = new System.Drawing.Point(461, 44);
            this.txtMatch.Name = "txtMatch";
            this.txtMatch.Size = new System.Drawing.Size(140, 21);
            this.txtMatch.TabIndex = 2;
            this.txtMatch.Text = "a";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(552, 73);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "匹配URL";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnAnalyse_Click);
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(13, 15);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(588, 21);
            this.txtUrl.TabIndex = 1;
            this.txtUrl.Text = "https://www.henan.gov.cn/ywdt/hnyw/";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(705, 15);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "开始分析";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtDepth
            // 
            this.txtDepth.Location = new System.Drawing.Point(13, 44);
            this.txtDepth.Name = "txtDepth";
            this.txtDepth.Size = new System.Drawing.Size(140, 21);
            this.txtDepth.TabIndex = 2;
            this.txtDepth.Text = "5";
            // 
            // txtHeader
            // 
            this.txtHeader.Location = new System.Drawing.Point(179, 44);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(214, 21);
            this.txtHeader.TabIndex = 8;
            this.txtHeader.Text = "https://www.henan.gov.cn/";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(705, 71);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 9;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 73);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(197, 21);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "{PAGE_NO}<1?\'\':\'_{PAGE_NO}\'";
            // 
            // lstUrls
            // 
            this.lstUrls.FormattingEnabled = true;
            this.lstUrls.ItemHeight = 12;
            this.lstUrls.Location = new System.Drawing.Point(13, 102);
            this.lstUrls.Name = "lstUrls";
            this.lstUrls.Size = new System.Drawing.Size(614, 424);
            this.lstUrls.TabIndex = 10;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(705, 122);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 11;
            this.button4.Text = "写入数据库";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtUrlID
            // 
            this.txtUrlID.Location = new System.Drawing.Point(234, 73);
            this.txtUrlID.Name = "txtUrlID";
            this.txtUrlID.Size = new System.Drawing.Size(100, 21);
            this.txtUrlID.TabIndex = 12;
            this.txtUrlID.Text = "NEWS";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(675, 247);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 13;
            this.button6.Text = "button6";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // frmCMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 545);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.txtUrlID);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.lstUrls);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.txtHeader);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txtDepth);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtMatch);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "frmCMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文书下载系统";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtMatch;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtDepth;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.ListBox lstUrls;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtUrlID;
        private System.Windows.Forms.Button button6;
    }
}


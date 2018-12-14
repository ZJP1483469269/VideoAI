namespace WEB_TASK
{
    partial class frmMain
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
            this.txtPageHtml = new System.Windows.Forms.TextBox();
            this.txtPageUrl = new System.Windows.Forms.TextBox();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.txtDepth = new System.Windows.Forms.TextBox();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(535, 71);
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
            this.button2.Location = new System.Drawing.Point(613, 71);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "匹配URL";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnAnalyse_Click);
            // 
            // txtPageHtml
            // 
            this.txtPageHtml.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPageHtml.Location = new System.Drawing.Point(13, 100);
            this.txtPageHtml.Multiline = true;
            this.txtPageHtml.Name = "txtPageHtml";
            this.txtPageHtml.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPageHtml.Size = new System.Drawing.Size(767, 168);
            this.txtPageHtml.TabIndex = 3;
            // 
            // txtPageUrl
            // 
            this.txtPageUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPageUrl.Location = new System.Drawing.Point(13, 274);
            this.txtPageUrl.Multiline = true;
            this.txtPageUrl.Name = "txtPageUrl";
            this.txtPageUrl.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPageUrl.Size = new System.Drawing.Size(754, 118);
            this.txtPageUrl.TabIndex = 4;
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(13, 15);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(588, 21);
            this.txtUrl.TabIndex = 1;
            this.txtUrl.Text = "http://f.mnr.gov.cn/579/581/index_3553{PAGE_NO}.html";
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
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(705, 71);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.btnResult_Click);
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
            this.txtHeader.Size = new System.Drawing.Size(140, 21);
            this.txtHeader.TabIndex = 8;
            this.txtHeader.Text = "http://f.mnr.gov.cn/";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(705, 44);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 9;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(332, 73);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(197, 21);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "{PAGE_NO}<1?\'\':\'_{PAGE_NO}\'";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 545);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.txtHeader);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txtPageUrl);
            this.Controls.Add(this.txtPageHtml);
            this.Controls.Add(this.txtDepth);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtMatch);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "frmMain";
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
        private System.Windows.Forms.TextBox txtPageHtml;
        private System.Windows.Forms.TextBox txtPageUrl;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtDepth;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox1;
    }
}


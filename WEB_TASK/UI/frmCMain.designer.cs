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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.btnDBConfig = new System.Windows.Forms.Button();
            this.geckoWebBrowser1 = new Gecko.GeckoWebBrowser();
            this.btnConfigLayer = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.timAuto = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(721, 155);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "打开文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtUrl
            // 
            this.txtUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUrl.Location = new System.Drawing.Point(13, 15);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(703, 21);
            this.txtUrl.TabIndex = 1;
            this.txtUrl.Text = "https://www.henan.gov.cn/ywdt/hnyw/";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(721, 44);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 28);
            this.button3.TabIndex = 6;
            this.button3.Text = "开始下载";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnDBConfig
            // 
            this.btnDBConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDBConfig.Location = new System.Drawing.Point(721, 118);
            this.btnDBConfig.Name = "btnDBConfig";
            this.btnDBConfig.Size = new System.Drawing.Size(75, 28);
            this.btnDBConfig.TabIndex = 11;
            this.btnDBConfig.Text = "数据库配置";
            this.btnDBConfig.UseVisualStyleBackColor = true;
            this.btnDBConfig.Click += new System.EventHandler(this.button4_Click);
            // 
            // geckoWebBrowser1
            // 
            this.geckoWebBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.geckoWebBrowser1.Location = new System.Drawing.Point(12, 44);
            this.geckoWebBrowser1.Name = "geckoWebBrowser1";
            this.geckoWebBrowser1.Size = new System.Drawing.Size(703, 455);
            this.geckoWebBrowser1.TabIndex = 14;
            this.geckoWebBrowser1.UseHttpActivityObserver = false;
            // 
            // btnConfigLayer
            // 
            this.btnConfigLayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfigLayer.Location = new System.Drawing.Point(721, 81);
            this.btnConfigLayer.Name = "btnConfigLayer";
            this.btnConfigLayer.Size = new System.Drawing.Size(75, 28);
            this.btnConfigLayer.TabIndex = 15;
            this.btnConfigLayer.Text = "匹配规则";
            this.btnConfigLayer.UseVisualStyleBackColor = true;
            this.btnConfigLayer.Click += new System.EventHandler(this.btnConfigLayer_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(723, 13);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 16;
            this.btnOpen.Text = "打开地址";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // txtResult
            // 
            this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResult.Location = new System.Drawing.Point(13, 512);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(703, 21);
            this.txtResult.TabIndex = 17;
            // 
            // timAuto
            // 
            this.timAuto.Enabled = true;
            this.timAuto.Interval = 200;
            this.timAuto.Tick += new System.EventHandler(this.timAuto_Tick);
            // 
            // frmCMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 545);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnConfigLayer);
            this.Controls.Add(this.geckoWebBrowser1);
            this.Controls.Add(this.btnDBConfig);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txtUrl);
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
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnDBConfig;
        private Gecko.GeckoWebBrowser geckoWebBrowser1;
        private System.Windows.Forms.Button btnConfigLayer;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Timer timAuto;
    }
}


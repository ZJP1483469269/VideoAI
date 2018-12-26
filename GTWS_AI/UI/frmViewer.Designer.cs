namespace GTWS_TASK.UI
{
    partial class frmViewer
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
            this.components = new System.ComponentModel.Container();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnUP = new System.Windows.Forms.Button();
            this.LB_MSG = new System.Windows.Forms.Label();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnTake = new System.Windows.Forms.Button();
            this.pnlPlay = new System.Windows.Forms.Panel();
            this.geckoWeb = new Gecko.GeckoWebBrowser();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.timAuto = new System.Windows.Forms.Timer(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnUP);
            this.panel2.Controls.Add(this.LB_MSG);
            this.panel2.Controls.Add(this.btnDown);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnRight);
            this.panel2.Controls.Add(this.btnLeft);
            this.panel2.Controls.Add(this.btnTake);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 525);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(882, 40);
            this.panel2.TabIndex = 0;
            // 
            // btnUP
            // 
            this.btnUP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUP.Location = new System.Drawing.Point(212, 6);
            this.btnUP.Name = "btnUP";
            this.btnUP.Size = new System.Drawing.Size(60, 27);
            this.btnUP.TabIndex = 94;
            this.btnUP.Text = "上";
            this.btnUP.UseVisualStyleBackColor = true;
            // 
            // LB_MSG
            // 
            this.LB_MSG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LB_MSG.AutoSize = true;
            this.LB_MSG.Location = new System.Drawing.Point(-2, 10);
            this.LB_MSG.Name = "LB_MSG";
            this.LB_MSG.Size = new System.Drawing.Size(17, 12);
            this.LB_MSG.TabIndex = 100;
            this.LB_MSG.Text = "[]";
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDown.Location = new System.Drawing.Point(80, 6);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(60, 27);
            this.btnDown.TabIndex = 95;
            this.btnDown.Text = "下";
            this.btnDown.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(811, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(63, 29);
            this.btnClose.TabIndex = 99;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRight
            // 
            this.btnRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRight.Location = new System.Drawing.Point(146, 6);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(60, 27);
            this.btnRight.TabIndex = 93;
            this.btnRight.Text = "右";
            this.btnRight.UseVisualStyleBackColor = true;
            // 
            // btnLeft
            // 
            this.btnLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLeft.Location = new System.Drawing.Point(14, 6);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(60, 27);
            this.btnLeft.TabIndex = 92;
            this.btnLeft.Text = "左";
            this.btnLeft.UseVisualStyleBackColor = true;
            // 
            // btnTake
            // 
            this.btnTake.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTake.Location = new System.Drawing.Point(746, 4);
            this.btnTake.Name = "btnTake";
            this.btnTake.Size = new System.Drawing.Size(59, 29);
            this.btnTake.TabIndex = 96;
            this.btnTake.Text = "拍照";
            this.btnTake.UseVisualStyleBackColor = true;
            this.btnTake.Click += new System.EventHandler(this.btnTake_Click);
            // 
            // pnlPlay
            // 
            this.pnlPlay.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlPlay.Location = new System.Drawing.Point(147, 53);
            this.pnlPlay.Name = "pnlPlay";
            this.pnlPlay.Size = new System.Drawing.Size(81, 69);
            this.pnlPlay.TabIndex = 70;
            // 
            // geckoWeb
            // 
            this.geckoWeb.Dock = System.Windows.Forms.DockStyle.Right;
            this.geckoWeb.Location = new System.Drawing.Point(402, 0);
            this.geckoWeb.Name = "geckoWeb";
            this.geckoWeb.Size = new System.Drawing.Size(480, 525);
            this.geckoWeb.TabIndex = 105;
            this.geckoWeb.UseHttpActivityObserver = false;
            // 
            // picBox
            // 
            this.picBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBox.Location = new System.Drawing.Point(0, 0);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(402, 525);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox.TabIndex = 106;
            this.picBox.TabStop = false;
            // 
            // timAuto
            // 
            this.timAuto.Interval = 200;
            this.timAuto.Tick += new System.EventHandler(this.timAuto_Tick);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(392, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(10, 525);
            this.splitter1.TabIndex = 107;
            this.splitter1.TabStop = false;
            // 
            // frmViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 565);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.picBox);
            this.Controls.Add(this.geckoWeb);
            this.Controls.Add(this.pnlPlay);
            this.Controls.Add(this.panel2);
            this.Name = "frmViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "国土智能监控取证系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmTask_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnUP;
        private System.Windows.Forms.Label LB_MSG;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnTake;
        private System.Windows.Forms.Panel pnlPlay;
        private Gecko.GeckoWebBrowser geckoWeb;
        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.Timer timAuto;
        private System.Windows.Forms.Splitter splitter1;
    }
}
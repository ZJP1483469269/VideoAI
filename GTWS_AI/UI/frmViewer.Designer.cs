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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnEndReal = new System.Windows.Forms.Button();
            this.btnStartReal = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnUP = new System.Windows.Forms.Button();
            this.pnlPlay = new System.Windows.Forms.Panel();
            this.LB_MSG = new System.Windows.Forms.Label();
            this.btnTake = new System.Windows.Forms.Button();
            this.txtCAMERA_NAME = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(725, 525);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(63, 29);
            this.btnClose.TabIndex = 66;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnEndReal
            // 
            this.btnEndReal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEndReal.Location = new System.Drawing.Point(658, 525);
            this.btnEndReal.Name = "btnEndReal";
            this.btnEndReal.Size = new System.Drawing.Size(61, 29);
            this.btnEndReal.TabIndex = 65;
            this.btnEndReal.Text = "停止实况";
            this.btnEndReal.UseVisualStyleBackColor = true;
            this.btnEndReal.Click += new System.EventHandler(this.btnEndReal_Click);
            // 
            // btnStartReal
            // 
            this.btnStartReal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartReal.Location = new System.Drawing.Point(591, 525);
            this.btnStartReal.Name = "btnStartReal";
            this.btnStartReal.Size = new System.Drawing.Size(61, 29);
            this.btnStartReal.TabIndex = 64;
            this.btnStartReal.Text = "开始实况";
            this.btnStartReal.UseVisualStyleBackColor = true;
            this.btnStartReal.Click += new System.EventHandler(this.btnStartReal_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLeft.Location = new System.Drawing.Point(33, 533);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(60, 27);
            this.btnLeft.TabIndex = 50;
            this.btnLeft.Text = "左";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnPTZ_Down);
            this.btnLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnPTZ_UP);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDown.Location = new System.Drawing.Point(99, 533);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(60, 27);
            this.btnDown.TabIndex = 53;
            this.btnDown.Text = "下";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnPTZ_Down);
            this.btnDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnPTZ_UP);
            // 
            // btnRight
            // 
            this.btnRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRight.Location = new System.Drawing.Point(165, 533);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(60, 27);
            this.btnRight.TabIndex = 51;
            this.btnRight.Text = "右";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnPTZ_Down);
            this.btnRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnPTZ_UP);
            // 
            // btnUP
            // 
            this.btnUP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUP.Location = new System.Drawing.Point(231, 533);
            this.btnUP.Name = "btnUP";
            this.btnUP.Size = new System.Drawing.Size(60, 27);
            this.btnUP.TabIndex = 52;
            this.btnUP.Text = "上";
            this.btnUP.UseVisualStyleBackColor = true;
            this.btnUP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnPTZ_Down);
            this.btnUP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnPTZ_UP);
            // 
            // pnlPlay
            // 
            this.pnlPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPlay.Location = new System.Drawing.Point(12, 39);
            this.pnlPlay.Name = "pnlPlay";
            this.pnlPlay.Size = new System.Drawing.Size(777, 480);
            this.pnlPlay.TabIndex = 70;
            // 
            // LB_MSG
            // 
            this.LB_MSG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LB_MSG.AutoSize = true;
            this.LB_MSG.Location = new System.Drawing.Point(10, 533);
            this.LB_MSG.Name = "LB_MSG";
            this.LB_MSG.Size = new System.Drawing.Size(17, 12);
            this.LB_MSG.TabIndex = 77;
            this.LB_MSG.Text = "[]";
            // 
            // btnTake
            // 
            this.btnTake.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTake.Location = new System.Drawing.Point(542, 525);
            this.btnTake.Name = "btnTake";
            this.btnTake.Size = new System.Drawing.Size(43, 29);
            this.btnTake.TabIndex = 64;
            this.btnTake.Text = "拍照";
            this.btnTake.UseVisualStyleBackColor = true;
            this.btnTake.Click += new System.EventHandler(this.btnTake_Click);
            // 
            // txtCAMERA_NAME
            // 
            this.txtCAMERA_NAME.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCAMERA_NAME.Location = new System.Drawing.Point(12, 12);
            this.txtCAMERA_NAME.Name = "txtCAMERA_NAME";
            this.txtCAMERA_NAME.ReadOnly = true;
            this.txtCAMERA_NAME.Size = new System.Drawing.Size(777, 21);
            this.txtCAMERA_NAME.TabIndex = 79;
            // 
            // frmViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 566);
            this.Controls.Add(this.txtCAMERA_NAME);
            this.Controls.Add(this.LB_MSG);
            this.Controls.Add(this.pnlPlay);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnEndReal);
            this.Controls.Add(this.btnTake);
            this.Controls.Add(this.btnStartReal);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnUP);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnDown);
            this.Name = "frmViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "国土智能监控取证系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmTask_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnEndReal;
        private System.Windows.Forms.Button btnStartReal;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnUP;
        private System.Windows.Forms.Panel pnlPlay;
        private System.Windows.Forms.Label LB_MSG;
        private System.Windows.Forms.Button btnTake;
        private System.Windows.Forms.TextBox txtCAMERA_NAME;
    }
}
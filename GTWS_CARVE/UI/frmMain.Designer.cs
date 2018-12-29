namespace GTWS_CARVE.UI
{
    partial class frmMain
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
            this.timTask = new System.Windows.Forms.Timer(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.LB_MSG = new System.Windows.Forms.Label();
            this.timCheck = new System.Windows.Forms.Timer(this.components);
            this.btnMonitor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timTask
            // 
            this.timTask.Enabled = true;
            this.timTask.Interval = 1000;
            this.timTask.Tick += new System.EventHandler(this.timAuto_Tick);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(427, 233);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(63, 28);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // LB_MSG
            // 
            this.LB_MSG.AutoSize = true;
            this.LB_MSG.Location = new System.Drawing.Point(46, 107);
            this.LB_MSG.Name = "LB_MSG";
            this.LB_MSG.Size = new System.Drawing.Size(17, 12);
            this.LB_MSG.TabIndex = 1;
            this.LB_MSG.Text = "[]";
            // 
            // timCheck
            // 
            this.timCheck.Enabled = true;
            this.timCheck.Interval = 1000;
            this.timCheck.Tick += new System.EventHandler(this.timCheck_Tick);
            // 
            // btnMonitor
            // 
            this.btnMonitor.Location = new System.Drawing.Point(358, 233);
            this.btnMonitor.Name = "btnMonitor";
            this.btnMonitor.Size = new System.Drawing.Size(63, 28);
            this.btnMonitor.TabIndex = 2;
            this.btnMonitor.Text = "停止";
            this.btnMonitor.UseVisualStyleBackColor = true;
            this.btnMonitor.Click += new System.EventHandler(this.btnMonitor_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 273);
            this.Controls.Add(this.btnMonitor);
            this.Controls.Add(this.LB_MSG);
            this.Controls.Add(this.btnClose);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "国土卫士图像拆分系统";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timTask;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label LB_MSG;
        private System.Windows.Forms.Timer timCheck;
        private System.Windows.Forms.Button btnMonitor;
    }
}
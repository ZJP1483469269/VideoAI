namespace GTWS_TASK.UI
{
    partial class frmTask
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
            System.Windows.Forms.Button btnLoad;
            System.Windows.Forms.Button btnAutoTake;
            System.Windows.Forms.Button btnClose;
            System.Windows.Forms.Button btnEndReal;
            System.Windows.Forms.Button btnTake;
            System.Windows.Forms.Button btnStartReal;
            this.pnlPlay = new System.Windows.Forms.Panel();
            this.timAfter = new System.Windows.Forms.Timer(this.components);
            this.timPreset = new System.Windows.Forms.Timer(this.components);
            this.timTask = new System.Windows.Forms.Timer(this.components);
            this.YWZ_TXT_LIST = new System.Windows.Forms.ListBox();
            this.txtCameraName = new System.Windows.Forms.TextBox();
            this.LB_KEY = new System.Windows.Forms.Label();
            this.LB_MSG = new System.Windows.Forms.Label();
            this.CameraNameList = new System.Windows.Forms.ListBox();
            this.timCheck = new System.Windows.Forms.Timer(this.components);
            this.btnMonitor = new System.Windows.Forms.Button();
            btnLoad = new System.Windows.Forms.Button();
            btnAutoTake = new System.Windows.Forms.Button();
            btnClose = new System.Windows.Forms.Button();
            btnEndReal = new System.Windows.Forms.Button();
            btnTake = new System.Windows.Forms.Button();
            btnStartReal = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnLoad.Location = new System.Drawing.Point(468, 506);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new System.Drawing.Size(74, 29);
            btnLoad.TabIndex = 87;
            btnLoad.Text = "读取摄像机";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnAutoTake
            // 
            btnAutoTake.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnAutoTake.Location = new System.Drawing.Point(549, 506);
            btnAutoTake.Name = "btnAutoTake";
            btnAutoTake.Size = new System.Drawing.Size(62, 29);
            btnAutoTake.TabIndex = 88;
            btnAutoTake.Text = "自动巡航";
            btnAutoTake.UseVisualStyleBackColor = true;
            btnAutoTake.Click += new System.EventHandler(this.btnAutoTake_Click);
            // 
            // btnClose
            // 
            btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnClose.Location = new System.Drawing.Point(862, 506);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(63, 29);
            btnClose.TabIndex = 85;
            btnClose.Text = "关闭";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnEndReal
            // 
            btnEndReal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnEndReal.Location = new System.Drawing.Point(733, 506);
            btnEndReal.Name = "btnEndReal";
            btnEndReal.Size = new System.Drawing.Size(61, 29);
            btnEndReal.TabIndex = 84;
            btnEndReal.Text = "停止实况";
            btnEndReal.UseVisualStyleBackColor = true;
            btnEndReal.Click += new System.EventHandler(this.btnEndReal_Click);
            // 
            // btnTake
            // 
            btnTake.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnTake.Location = new System.Drawing.Point(617, 506);
            btnTake.Name = "btnTake";
            btnTake.Size = new System.Drawing.Size(43, 29);
            btnTake.TabIndex = 82;
            btnTake.Text = "拍照";
            btnTake.UseVisualStyleBackColor = true;
            btnTake.Click += new System.EventHandler(this.btnTake_Click);
            // 
            // btnStartReal
            // 
            btnStartReal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnStartReal.Location = new System.Drawing.Point(666, 506);
            btnStartReal.Name = "btnStartReal";
            btnStartReal.Size = new System.Drawing.Size(61, 29);
            btnStartReal.TabIndex = 83;
            btnStartReal.Text = "开始实况";
            btnStartReal.UseVisualStyleBackColor = true;
            btnStartReal.Click += new System.EventHandler(this.btnStartReal_Click);
            // 
            // pnlPlay
            // 
            this.pnlPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPlay.Location = new System.Drawing.Point(252, 39);
            this.pnlPlay.Name = "pnlPlay";
            this.pnlPlay.Size = new System.Drawing.Size(673, 459);
            this.pnlPlay.TabIndex = 70;
            // 
            // timAfter
            // 
            this.timAfter.Tick += new System.EventHandler(this.timAfter_Tick);
            // 
            // timPreset
            // 
            this.timPreset.Tick += new System.EventHandler(this.timPreset_Tick);
            // 
            // timTask
            // 
            this.timTask.Enabled = true;
            this.timTask.Interval = 30000;
            this.timTask.Tick += new System.EventHandler(this.timTask_Tick);
            // 
            // YWZ_TXT_LIST
            // 
            this.YWZ_TXT_LIST.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.YWZ_TXT_LIST.FormattingEnabled = true;
            this.YWZ_TXT_LIST.ItemHeight = 12;
            this.YWZ_TXT_LIST.Location = new System.Drawing.Point(14, 386);
            this.YWZ_TXT_LIST.Name = "YWZ_TXT_LIST";
            this.YWZ_TXT_LIST.Size = new System.Drawing.Size(231, 112);
            this.YWZ_TXT_LIST.TabIndex = 89;
            // 
            // txtCameraName
            // 
            this.txtCameraName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCameraName.Location = new System.Drawing.Point(12, 12);
            this.txtCameraName.Name = "txtCameraName";
            this.txtCameraName.Size = new System.Drawing.Size(914, 21);
            this.txtCameraName.TabIndex = 90;
            // 
            // LB_KEY
            // 
            this.LB_KEY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LB_KEY.AutoSize = true;
            this.LB_KEY.Location = new System.Drawing.Point(188, 522);
            this.LB_KEY.Name = "LB_KEY";
            this.LB_KEY.Size = new System.Drawing.Size(29, 12);
            this.LB_KEY.TabIndex = 91;
            this.LB_KEY.Text = "    ";
            // 
            // LB_MSG
            // 
            this.LB_MSG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LB_MSG.AutoSize = true;
            this.LB_MSG.Location = new System.Drawing.Point(12, 522);
            this.LB_MSG.Name = "LB_MSG";
            this.LB_MSG.Size = new System.Drawing.Size(29, 12);
            this.LB_MSG.TabIndex = 91;
            this.LB_MSG.Text = "    ";
            // 
            // CameraNameList
            // 
            this.CameraNameList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.CameraNameList.FormattingEnabled = true;
            this.CameraNameList.ItemHeight = 12;
            this.CameraNameList.Location = new System.Drawing.Point(14, 39);
            this.CameraNameList.Name = "CameraNameList";
            this.CameraNameList.Size = new System.Drawing.Size(231, 340);
            this.CameraNameList.TabIndex = 92;
            this.CameraNameList.DoubleClick += new System.EventHandler(this.CameraNameList_DoubleClick);
            // 
            // timCheck
            // 
            this.timCheck.Enabled = true;
            this.timCheck.Interval = 1000;
            this.timCheck.Tick += new System.EventHandler(this.timCheck_Tick);
            // 
            // btnMonitor
            // 
            this.btnMonitor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMonitor.Location = new System.Drawing.Point(800, 506);
            this.btnMonitor.Name = "btnMonitor";
            this.btnMonitor.Size = new System.Drawing.Size(56, 29);
            this.btnMonitor.TabIndex = 93;
            this.btnMonitor.Text = "停止";
            this.btnMonitor.UseVisualStyleBackColor = true;
            this.btnMonitor.Click += new System.EventHandler(this.btnMonitor_Click);
            // 
            // frmTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 547);
            this.Controls.Add(this.btnMonitor);
            this.Controls.Add(this.CameraNameList);
            this.Controls.Add(this.LB_MSG);
            this.Controls.Add(this.LB_KEY);
            this.Controls.Add(this.txtCameraName);
            this.Controls.Add(this.YWZ_TXT_LIST);
            this.Controls.Add(btnLoad);
            this.Controls.Add(btnAutoTake);
            this.Controls.Add(btnClose);
            this.Controls.Add(btnEndReal);
            this.Controls.Add(btnTake);
            this.Controls.Add(btnStartReal);
            this.Controls.Add(this.pnlPlay);
            this.Name = "frmTask";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "国土智能监控取证系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmTake_FormClosed);
            this.Load += new System.EventHandler(this.frmTask_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlPlay;
        private System.Windows.Forms.Timer timAfter;
        private System.Windows.Forms.Timer timPreset;
        private System.Windows.Forms.Timer timTask;
        private System.Windows.Forms.TextBox txtCameraName;
        public System.Windows.Forms.ListBox YWZ_TXT_LIST;
        private System.Windows.Forms.Label LB_KEY;
        private System.Windows.Forms.Label LB_MSG;
        private System.Windows.Forms.ListBox CameraNameList;
        private System.Windows.Forms.Timer timCheck;
        private System.Windows.Forms.Button btnMonitor;
    }
}
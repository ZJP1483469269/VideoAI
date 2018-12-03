﻿namespace GTWS_TASK.UI
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
            this.YWZ_TXT_LIST = new System.Windows.Forms.ListBox();
            this.CameraNameList = new System.Windows.Forms.ListBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnEndReal = new System.Windows.Forms.Button();
            this.btnStartReal = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLENS_FOCAL_STOP = new System.Windows.Forms.Button();
            this.btnLENS_ZOOM_STOP = new System.Windows.Forms.Button();
            this.btnLENS_APERTURE_STOP = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLENS_FOCAL_FAR = new System.Windows.Forms.Button();
            this.btnLENS_FOCAL_NEAT = new System.Windows.Forms.Button();
            this.btnLENS_ZOOM_OUT = new System.Windows.Forms.Button();
            this.btnLENS_ZOOM_IN = new System.Windows.Forms.Button();
            this.btnLENS_APERTURE_CLOSE = new System.Windows.Forms.Button();
            this.btnLENS_APERTURE_OPEN = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnUP = new System.Windows.Forms.Button();
            this.pnlPlay = new System.Windows.Forms.Panel();
            this.timAfter = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.系统管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统配置ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.视频监控ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.遍历截图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.同步摄像机列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPreset = new System.Windows.Forms.Button();
            this.btnAI = new System.Windows.Forms.Button();
            this.LB_MSG = new System.Windows.Forms.Label();
            this.btnTake = new System.Windows.Forms.Button();
            this.timAI = new System.Windows.Forms.Timer(this.components);
            this.timTake = new System.Windows.Forms.Timer(this.components);
            this.timPreset = new System.Windows.Forms.Timer(this.components);
            this.txtCAMERA_NAME = new System.Windows.Forms.TextBox();
            this.timTask = new System.Windows.Forms.Timer(this.components);
            this.系统配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.任务时间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // YWZ_TXT_LIST
            // 
            this.YWZ_TXT_LIST.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.YWZ_TXT_LIST.FormattingEnabled = true;
            this.YWZ_TXT_LIST.ItemHeight = 12;
            this.YWZ_TXT_LIST.Location = new System.Drawing.Point(12, 340);
            this.YWZ_TXT_LIST.Name = "YWZ_TXT_LIST";
            this.YWZ_TXT_LIST.Size = new System.Drawing.Size(239, 88);
            this.YWZ_TXT_LIST.TabIndex = 62;
            this.YWZ_TXT_LIST.DoubleClick += new System.EventHandler(this.CameraYZWList_DoubleClick);
            // 
            // CameraNameList
            // 
            this.CameraNameList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.CameraNameList.FormattingEnabled = true;
            this.CameraNameList.ItemHeight = 12;
            this.CameraNameList.Location = new System.Drawing.Point(12, 28);
            this.CameraNameList.Name = "CameraNameList";
            this.CameraNameList.Size = new System.Drawing.Size(239, 304);
            this.CameraNameList.TabIndex = 63;
            this.CameraNameList.DoubleClick += new System.EventHandler(this.CameraNameList_DoubleClick);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(906, 571);
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
            this.btnEndReal.Location = new System.Drawing.Point(839, 571);
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
            this.btnStartReal.Location = new System.Drawing.Point(772, 571);
            this.btnStartReal.Name = "btnStartReal";
            this.btnStartReal.Size = new System.Drawing.Size(61, 29);
            this.btnStartReal.TabIndex = 64;
            this.btnStartReal.Text = "开始实况";
            this.btnStartReal.UseVisualStyleBackColor = true;
            this.btnStartReal.Click += new System.EventHandler(this.btnStartReal_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnLENS_FOCAL_STOP);
            this.panel1.Controls.Add(this.btnLENS_ZOOM_STOP);
            this.panel1.Controls.Add(this.btnLENS_APERTURE_STOP);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnLENS_FOCAL_FAR);
            this.panel1.Controls.Add(this.btnLENS_FOCAL_NEAT);
            this.panel1.Controls.Add(this.btnLENS_ZOOM_OUT);
            this.panel1.Controls.Add(this.btnLENS_ZOOM_IN);
            this.panel1.Controls.Add(this.btnLENS_APERTURE_CLOSE);
            this.panel1.Controls.Add(this.btnLENS_APERTURE_OPEN);
            this.panel1.Controls.Add(this.btnLeft);
            this.panel1.Controls.Add(this.btnDown);
            this.panel1.Controls.Add(this.btnRight);
            this.panel1.Controls.Add(this.btnUP);
            this.panel1.Location = new System.Drawing.Point(12, 436);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(239, 129);
            this.panel1.TabIndex = 69;
            // 
            // btnLENS_FOCAL_STOP
            // 
            this.btnLENS_FOCAL_STOP.Location = new System.Drawing.Point(206, 93);
            this.btnLENS_FOCAL_STOP.Name = "btnLENS_FOCAL_STOP";
            this.btnLENS_FOCAL_STOP.Size = new System.Drawing.Size(30, 30);
            this.btnLENS_FOCAL_STOP.TabIndex = 65;
            this.btnLENS_FOCAL_STOP.Text = "X";
            this.btnLENS_FOCAL_STOP.UseVisualStyleBackColor = true;
            this.btnLENS_FOCAL_STOP.Click += new System.EventHandler(this.btnLENS_FOCAL_STOP_Click);
            // 
            // btnLENS_ZOOM_STOP
            // 
            this.btnLENS_ZOOM_STOP.Location = new System.Drawing.Point(170, 93);
            this.btnLENS_ZOOM_STOP.Name = "btnLENS_ZOOM_STOP";
            this.btnLENS_ZOOM_STOP.Size = new System.Drawing.Size(30, 30);
            this.btnLENS_ZOOM_STOP.TabIndex = 64;
            this.btnLENS_ZOOM_STOP.Text = "X";
            this.btnLENS_ZOOM_STOP.UseVisualStyleBackColor = true;
            this.btnLENS_ZOOM_STOP.Click += new System.EventHandler(this.btnLENS_FOCAL_STOP_Click);
            // 
            // btnLENS_APERTURE_STOP
            // 
            this.btnLENS_APERTURE_STOP.Location = new System.Drawing.Point(131, 93);
            this.btnLENS_APERTURE_STOP.Name = "btnLENS_APERTURE_STOP";
            this.btnLENS_APERTURE_STOP.Size = new System.Drawing.Size(30, 30);
            this.btnLENS_APERTURE_STOP.TabIndex = 63;
            this.btnLENS_APERTURE_STOP.Text = "X";
            this.btnLENS_APERTURE_STOP.UseVisualStyleBackColor = true;
            this.btnLENS_APERTURE_STOP.Click += new System.EventHandler(this.btnLENS_FOCAL_STOP_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(204, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 62;
            this.label3.Text = "聚焦";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(168, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 61;
            this.label2.Text = "缩放";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 60;
            this.label1.Text = "光圈";
            // 
            // btnLENS_FOCAL_FAR
            // 
            this.btnLENS_FOCAL_FAR.Location = new System.Drawing.Point(206, 57);
            this.btnLENS_FOCAL_FAR.Name = "btnLENS_FOCAL_FAR";
            this.btnLENS_FOCAL_FAR.Size = new System.Drawing.Size(30, 30);
            this.btnLENS_FOCAL_FAR.TabIndex = 59;
            this.btnLENS_FOCAL_FAR.Text = "-";
            this.btnLENS_FOCAL_FAR.UseVisualStyleBackColor = true;
            this.btnLENS_FOCAL_FAR.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLENS_MINUS_MouseDown);
            // 
            // btnLENS_FOCAL_NEAT
            // 
            this.btnLENS_FOCAL_NEAT.Location = new System.Drawing.Point(206, 21);
            this.btnLENS_FOCAL_NEAT.Name = "btnLENS_FOCAL_NEAT";
            this.btnLENS_FOCAL_NEAT.Size = new System.Drawing.Size(30, 30);
            this.btnLENS_FOCAL_NEAT.TabIndex = 58;
            this.btnLENS_FOCAL_NEAT.Text = "+";
            this.btnLENS_FOCAL_NEAT.UseVisualStyleBackColor = true;
            this.btnLENS_FOCAL_NEAT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLENS_PLUS_MouseDown);
            // 
            // btnLENS_ZOOM_OUT
            // 
            this.btnLENS_ZOOM_OUT.Location = new System.Drawing.Point(170, 57);
            this.btnLENS_ZOOM_OUT.Name = "btnLENS_ZOOM_OUT";
            this.btnLENS_ZOOM_OUT.Size = new System.Drawing.Size(30, 30);
            this.btnLENS_ZOOM_OUT.TabIndex = 57;
            this.btnLENS_ZOOM_OUT.Text = "-";
            this.btnLENS_ZOOM_OUT.UseVisualStyleBackColor = true;
            this.btnLENS_ZOOM_OUT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLENS_MINUS_MouseDown);
            // 
            // btnLENS_ZOOM_IN
            // 
            this.btnLENS_ZOOM_IN.Location = new System.Drawing.Point(170, 21);
            this.btnLENS_ZOOM_IN.Name = "btnLENS_ZOOM_IN";
            this.btnLENS_ZOOM_IN.Size = new System.Drawing.Size(30, 30);
            this.btnLENS_ZOOM_IN.TabIndex = 56;
            this.btnLENS_ZOOM_IN.Text = "+";
            this.btnLENS_ZOOM_IN.UseVisualStyleBackColor = true;
            this.btnLENS_ZOOM_IN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLENS_PLUS_MouseDown);
            // 
            // btnLENS_APERTURE_CLOSE
            // 
            this.btnLENS_APERTURE_CLOSE.Location = new System.Drawing.Point(131, 57);
            this.btnLENS_APERTURE_CLOSE.Name = "btnLENS_APERTURE_CLOSE";
            this.btnLENS_APERTURE_CLOSE.Size = new System.Drawing.Size(30, 30);
            this.btnLENS_APERTURE_CLOSE.TabIndex = 55;
            this.btnLENS_APERTURE_CLOSE.Text = "-";
            this.btnLENS_APERTURE_CLOSE.UseVisualStyleBackColor = true;
            this.btnLENS_APERTURE_CLOSE.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLENS_MINUS_MouseDown);
            // 
            // btnLENS_APERTURE_OPEN
            // 
            this.btnLENS_APERTURE_OPEN.Location = new System.Drawing.Point(131, 21);
            this.btnLENS_APERTURE_OPEN.Name = "btnLENS_APERTURE_OPEN";
            this.btnLENS_APERTURE_OPEN.Size = new System.Drawing.Size(30, 30);
            this.btnLENS_APERTURE_OPEN.TabIndex = 54;
            this.btnLENS_APERTURE_OPEN.Text = "+";
            this.btnLENS_APERTURE_OPEN.UseVisualStyleBackColor = true;
            this.btnLENS_APERTURE_OPEN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLENS_PLUS_MouseDown);
            // 
            // btnLeft
            // 
            this.btnLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLeft.Location = new System.Drawing.Point(3, 50);
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
            this.btnDown.Location = new System.Drawing.Point(29, 83);
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
            this.btnRight.Location = new System.Drawing.Point(69, 50);
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
            this.btnUP.Location = new System.Drawing.Point(29, 17);
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
            this.pnlPlay.Location = new System.Drawing.Point(258, 60);
            this.pnlPlay.Name = "pnlPlay";
            this.pnlPlay.Size = new System.Drawing.Size(711, 505);
            this.pnlPlay.TabIndex = 70;
            // 
            // timAfter
            // 
            this.timAfter.Tick += new System.EventHandler(this.timAfter_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统管理ToolStripMenuItem,
            this.系统配置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(975, 25);
            this.menuStrip1.TabIndex = 72;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 系统管理ToolStripMenuItem
            // 
            this.系统管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统配置ToolStripMenuItem1,
            this.视频监控ToolStripMenuItem,
            this.遍历截图ToolStripMenuItem,
            this.同步摄像机列表ToolStripMenuItem});
            this.系统管理ToolStripMenuItem.Name = "系统管理ToolStripMenuItem";
            this.系统管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.系统管理ToolStripMenuItem.Text = "系统管理";
            // 
            // 系统配置ToolStripMenuItem1
            // 
            this.系统配置ToolStripMenuItem1.Name = "系统配置ToolStripMenuItem1";
            this.系统配置ToolStripMenuItem1.Size = new System.Drawing.Size(160, 22);
            this.系统配置ToolStripMenuItem1.Text = "系统配置";
            this.系统配置ToolStripMenuItem1.Click += new System.EventHandler(this.系统配置ToolStripMenuItem1_Click);
            // 
            // 视频监控ToolStripMenuItem
            // 
            this.视频监控ToolStripMenuItem.Name = "视频监控ToolStripMenuItem";
            this.视频监控ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.视频监控ToolStripMenuItem.Text = "加载摄像机";
            this.视频监控ToolStripMenuItem.Click += new System.EventHandler(this.MUToolStripMenuItem_Click);
            // 
            // 遍历截图ToolStripMenuItem
            // 
            this.遍历截图ToolStripMenuItem.Name = "遍历截图ToolStripMenuItem";
            this.遍历截图ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.遍历截图ToolStripMenuItem.Text = "遍历截图";
            this.遍历截图ToolStripMenuItem.Click += new System.EventHandler(this.遍历截图ToolStripMenuItem_Click);
            // 
            // 同步摄像机列表ToolStripMenuItem
            // 
            this.同步摄像机列表ToolStripMenuItem.Name = "同步摄像机列表ToolStripMenuItem";
            this.同步摄像机列表ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.同步摄像机列表ToolStripMenuItem.Text = "同步摄像机列表";
            this.同步摄像机列表ToolStripMenuItem.Click += new System.EventHandler(this.同步摄像机列表ToolStripMenuItem_Click);
            // 
            // btnPreset
            // 
            this.btnPreset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreset.Location = new System.Drawing.Point(583, 571);
            this.btnPreset.Name = "btnPreset";
            this.btnPreset.Size = new System.Drawing.Size(79, 29);
            this.btnPreset.TabIndex = 73;
            this.btnPreset.Text = "添加预制位";
            this.btnPreset.UseVisualStyleBackColor = true;
            this.btnPreset.Click += new System.EventHandler(this.btnPreset_Click);
            // 
            // btnAI
            // 
            this.btnAI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAI.Location = new System.Drawing.Point(668, 571);
            this.btnAI.Name = "btnAI";
            this.btnAI.Size = new System.Drawing.Size(49, 29);
            this.btnAI.TabIndex = 76;
            this.btnAI.Text = "分析";
            this.btnAI.UseVisualStyleBackColor = true;
            this.btnAI.Click += new System.EventHandler(this.button2_Click);
            // 
            // LB_MSG
            // 
            this.LB_MSG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LB_MSG.AutoSize = true;
            this.LB_MSG.Location = new System.Drawing.Point(10, 579);
            this.LB_MSG.Name = "LB_MSG";
            this.LB_MSG.Size = new System.Drawing.Size(17, 12);
            this.LB_MSG.TabIndex = 77;
            this.LB_MSG.Text = "[]";
            // 
            // btnTake
            // 
            this.btnTake.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTake.Location = new System.Drawing.Point(723, 571);
            this.btnTake.Name = "btnTake";
            this.btnTake.Size = new System.Drawing.Size(43, 29);
            this.btnTake.TabIndex = 64;
            this.btnTake.Text = "拍照";
            this.btnTake.UseVisualStyleBackColor = true;
            this.btnTake.Click += new System.EventHandler(this.btnTake_Click);
            // 
            // timAI
            // 
            this.timAI.Interval = 200;
            this.timAI.Tick += new System.EventHandler(this.timAI_Tick);
            // 
            // timTake
            // 
            this.timTake.Enabled = true;
            this.timTake.Interval = 200;
            this.timTake.Tick += new System.EventHandler(this.timTake_Tick);
            // 
            // timPreset
            // 
            this.timPreset.Tick += new System.EventHandler(this.timPreset_Tick);
            // 
            // txtCAMERA_NAME
            // 
            this.txtCAMERA_NAME.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCAMERA_NAME.Location = new System.Drawing.Point(257, 33);
            this.txtCAMERA_NAME.Name = "txtCAMERA_NAME";
            this.txtCAMERA_NAME.ReadOnly = true;
            this.txtCAMERA_NAME.Size = new System.Drawing.Size(712, 21);
            this.txtCAMERA_NAME.TabIndex = 79;
            // 
            // timTask
            // 
            this.timTask.Enabled = true;
            this.timTask.Interval = 3000;
            this.timTask.Tick += new System.EventHandler(this.timTask_Tick);
            // 
            // 系统配置ToolStripMenuItem
            // 
            this.系统配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.任务时间ToolStripMenuItem});
            this.系统配置ToolStripMenuItem.Name = "系统配置ToolStripMenuItem";
            this.系统配置ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.系统配置ToolStripMenuItem.Text = "系统配置";
            // 
            // 任务时间ToolStripMenuItem
            // 
            this.任务时间ToolStripMenuItem.Name = "任务时间ToolStripMenuItem";
            this.任务时间ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.任务时间ToolStripMenuItem.Text = "定时任务";
            this.任务时间ToolStripMenuItem.Click += new System.EventHandler(this.任务时间ToolStripMenuItem_Click);
            // 
            // frmTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 612);
            this.Controls.Add(this.txtCAMERA_NAME);
            this.Controls.Add(this.LB_MSG);
            this.Controls.Add(this.btnAI);
            this.Controls.Add(this.btnPreset);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pnlPlay);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnEndReal);
            this.Controls.Add(this.btnTake);
            this.Controls.Add(this.btnStartReal);
            this.Controls.Add(this.CameraNameList);
            this.Controls.Add(this.YWZ_TXT_LIST);
            this.Name = "frmTask";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "国土智能监控取证系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmTask_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox YWZ_TXT_LIST;
        private System.Windows.Forms.ListBox CameraNameList;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnEndReal;
        private System.Windows.Forms.Button btnStartReal;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnLENS_FOCAL_STOP;
        private System.Windows.Forms.Button btnLENS_ZOOM_STOP;
        private System.Windows.Forms.Button btnLENS_APERTURE_STOP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLENS_FOCAL_FAR;
        private System.Windows.Forms.Button btnLENS_FOCAL_NEAT;
        private System.Windows.Forms.Button btnLENS_ZOOM_OUT;
        private System.Windows.Forms.Button btnLENS_ZOOM_IN;
        private System.Windows.Forms.Button btnLENS_APERTURE_CLOSE;
        private System.Windows.Forms.Button btnLENS_APERTURE_OPEN;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnUP;
        private System.Windows.Forms.Panel pnlPlay;
        private System.Windows.Forms.Timer timAfter;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 系统管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统配置ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 视频监控ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 遍历截图ToolStripMenuItem;
        private System.Windows.Forms.Button btnPreset;
        private System.Windows.Forms.Button btnAI;
        private System.Windows.Forms.Label LB_MSG;
        private System.Windows.Forms.Button btnTake;
        private System.Windows.Forms.Timer timAI;
        private System.Windows.Forms.Timer timTake;
        private System.Windows.Forms.Timer timPreset;
        private System.Windows.Forms.TextBox txtCAMERA_NAME;
        private System.Windows.Forms.Timer timTask;
        private System.Windows.Forms.ToolStripMenuItem 同步摄像机列表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 任务时间ToolStripMenuItem;
    }
}
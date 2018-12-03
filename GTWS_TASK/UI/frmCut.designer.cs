namespace GTWS_BD
{
    partial class frmCut
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
            this.btnLoad = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.GRAY_MIN = new System.Windows.Forms.NumericUpDown();
            this.GRAY_MAX = new System.Windows.Forms.NumericUpDown();
            this.IMG_MIN = new System.Windows.Forms.NumericUpDown();
            this.IMG_MAX = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GRAY_MIN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GRAY_MAX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IMG_MIN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IMG_MAX)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(853, 13);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "btnLoad";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(853, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(832, 589);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // GRAY_MIN
            // 
            this.GRAY_MIN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GRAY_MIN.Location = new System.Drawing.Point(853, 467);
            this.GRAY_MIN.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.GRAY_MIN.Name = "GRAY_MIN";
            this.GRAY_MIN.Size = new System.Drawing.Size(72, 21);
            this.GRAY_MIN.TabIndex = 4;
            this.GRAY_MIN.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.GRAY_MIN.ValueChanged += new System.EventHandler(this.IMG_MIN_ValueChanged);
            // 
            // GRAY_MAX
            // 
            this.GRAY_MAX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GRAY_MAX.Location = new System.Drawing.Point(853, 494);
            this.GRAY_MAX.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.GRAY_MAX.Name = "GRAY_MAX";
            this.GRAY_MAX.Size = new System.Drawing.Size(72, 21);
            this.GRAY_MAX.TabIndex = 4;
            this.GRAY_MAX.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.GRAY_MAX.ValueChanged += new System.EventHandler(this.IMG_MIN_ValueChanged);
            // 
            // IMG_MIN
            // 
            this.IMG_MIN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.IMG_MIN.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.IMG_MIN.Location = new System.Drawing.Point(853, 291);
            this.IMG_MIN.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.IMG_MIN.Name = "IMG_MIN";
            this.IMG_MIN.Size = new System.Drawing.Size(72, 21);
            this.IMG_MIN.TabIndex = 4;
            this.IMG_MIN.Tag = "0";
            this.IMG_MIN.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.IMG_MIN.ValueChanged += new System.EventHandler(this.IMG_MIN_ValueChanged);
            // 
            // IMG_MAX
            // 
            this.IMG_MAX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.IMG_MAX.Location = new System.Drawing.Point(853, 318);
            this.IMG_MAX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.IMG_MAX.Name = "IMG_MAX";
            this.IMG_MAX.Size = new System.Drawing.Size(72, 21);
            this.IMG_MAX.TabIndex = 4;
            this.IMG_MAX.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.IMG_MAX.ValueChanged += new System.EventHandler(this.IMG_MIN_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(851, 442);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "GRAY";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(851, 276);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "IMG";
            // 
            // frmCut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 613);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IMG_MAX);
            this.Controls.Add(this.GRAY_MAX);
            this.Controls.Add(this.IMG_MIN);
            this.Controls.Add(this.GRAY_MIN);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnLoad);
            this.Name = "frmCut";
            this.Text = "摄像机标定";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GRAY_MIN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GRAY_MAX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IMG_MIN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IMG_MAX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NumericUpDown GRAY_MIN;
        private System.Windows.Forms.NumericUpDown GRAY_MAX;
        private System.Windows.Forms.NumericUpDown IMG_MIN;
        private System.Windows.Forms.NumericUpDown IMG_MAX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

    }
}


namespace GTWS_AI.UI
{
    partial class frmArea
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
            this.btnRect = new System.Windows.Forms.Button();
            this.btnDraw = new System.Windows.Forms.Button();
            this.txtJD = new System.Windows.Forms.TextBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCircle = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtXZ = new System.Windows.Forms.TextBox();
            this.btnLine2 = new System.Windows.Forms.Button();
            this.txtGW = new System.Windows.Forms.TextBox();
            this.btnResult = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRect
            // 
            this.btnRect.Location = new System.Drawing.Point(12, 426);
            this.btnRect.Name = "btnRect";
            this.btnRect.Size = new System.Drawing.Size(75, 23);
            this.btnRect.TabIndex = 1;
            this.btnRect.Text = "坐标系";
            this.btnRect.UseVisualStyleBackColor = true;
            this.btnRect.Click += new System.EventHandler(this.btnRect_Click);
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(399, 453);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(75, 23);
            this.btnDraw.TabIndex = 2;
            this.btnDraw.Text = "DrawLine";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtJD
            // 
            this.txtJD.Location = new System.Drawing.Point(284, 455);
            this.txtJD.Name = "txtJD";
            this.txtJD.Size = new System.Drawing.Size(100, 21);
            this.txtJD.TabIndex = 3;
            this.txtJD.Text = "30";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(62, 455);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(100, 21);
            this.txtHeight.TabIndex = 4;
            this.txtHeight.Text = "200";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(168, 453);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "H";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 458);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "高度";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(249, 458);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "仰角";
            // 
            // btnCircle
            // 
            this.btnCircle.Location = new System.Drawing.Point(94, 426);
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(75, 23);
            this.btnCircle.TabIndex = 7;
            this.btnCircle.Text = "画圆";
            this.btnCircle.UseVisualStyleBackColor = true;
            this.btnCircle.Click += new System.EventHandler(this.btnCircle_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(249, 495);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "旋转";
            // 
            // txtXZ
            // 
            this.txtXZ.Location = new System.Drawing.Point(284, 492);
            this.txtXZ.Name = "txtXZ";
            this.txtXZ.Size = new System.Drawing.Size(100, 21);
            this.txtXZ.TabIndex = 8;
            this.txtXZ.Text = "60";
            // 
            // btnLine2
            // 
            this.btnLine2.Location = new System.Drawing.Point(399, 490);
            this.btnLine2.Name = "btnLine2";
            this.btnLine2.Size = new System.Drawing.Size(75, 23);
            this.btnLine2.TabIndex = 10;
            this.btnLine2.Text = "DrawCircle";
            this.btnLine2.UseVisualStyleBackColor = true;
            this.btnLine2.Click += new System.EventHandler(this.btnLine2_Click);
            // 
            // txtGW
            // 
            this.txtGW.Location = new System.Drawing.Point(480, 455);
            this.txtGW.Name = "txtGW";
            this.txtGW.Size = new System.Drawing.Size(83, 21);
            this.txtGW.TabIndex = 11;
            this.txtGW.Text = "30";
            // 
            // btnResult
            // 
            this.btnResult.Location = new System.Drawing.Point(480, 490);
            this.btnResult.Name = "btnResult";
            this.btnResult.Size = new System.Drawing.Size(83, 23);
            this.btnResult.TabIndex = 12;
            this.btnResult.Text = "DrawLine";
            this.btnResult.UseVisualStyleBackColor = true;
            this.btnResult.Click += new System.EventHandler(this.btnResult_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(569, 490);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // frmArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 545);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnResult);
            this.Controls.Add(this.txtGW);
            this.Controls.Add(this.btnLine2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtXZ);
            this.Controls.Add(this.btnCircle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.txtJD);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.btnRect);
            this.Name = "frmArea";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmArea";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRect;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.TextBox txtJD;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCircle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtXZ;
        private System.Windows.Forms.Button btnLine2;
        private System.Windows.Forms.TextBox txtGW;
        private System.Windows.Forms.Button btnResult;
        private System.Windows.Forms.Button button2;
    }
}
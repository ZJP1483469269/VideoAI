namespace WEB_TASK.UI
{
    partial class frmRule
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
            this.txtUrlID = new System.Windows.Forms.TextBox();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.txtDepth = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtMatch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.edtUrl = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUrlID
            // 
            this.txtUrlID.Location = new System.Drawing.Point(85, 160);
            this.txtUrlID.Name = "txtUrlID";
            this.txtUrlID.Size = new System.Drawing.Size(510, 21);
            this.txtUrlID.TabIndex = 17;
            this.txtUrlID.Text = "NEWS";
            // 
            // txtHeader
            // 
            this.txtHeader.Location = new System.Drawing.Point(85, 52);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(510, 21);
            this.txtHeader.TabIndex = 16;
            this.txtHeader.Text = "https://www.henan.gov.cn/";
            // 
            // txtDepth
            // 
            this.txtDepth.Location = new System.Drawing.Point(85, 88);
            this.txtDepth.Name = "txtDepth";
            this.txtDepth.Size = new System.Drawing.Size(140, 21);
            this.txtDepth.TabIndex = 15;
            this.txtDepth.Text = "5";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(319, 88);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(276, 21);
            this.textBox1.TabIndex = 14;
            this.textBox1.Text = "{PAGE_NO}<1?\'\':\'_{PAGE_NO}\'";
            // 
            // txtMatch
            // 
            this.txtMatch.Location = new System.Drawing.Point(85, 124);
            this.txtMatch.Name = "txtMatch";
            this.txtMatch.Size = new System.Drawing.Size(140, 21);
            this.txtMatch.TabIndex = 13;
            this.txtMatch.Text = "a";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "层数：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "前缀：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "选择器：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "规则编码：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(236, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "页码计算式：";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 188);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(583, 250);
            this.dataGridView1.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 18;
            this.label6.Text = "网页地址：";
            // 
            // edtUrl
            // 
            this.edtUrl.Location = new System.Drawing.Point(85, 16);
            this.edtUrl.Name = "edtUrl";
            this.edtUrl.Size = new System.Drawing.Size(510, 21);
            this.edtUrl.TabIndex = 16;
            this.edtUrl.Text = "https://www.henan.gov.cn/";
            // 
            // frmRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtUrlID);
            this.Controls.Add(this.edtUrl);
            this.Controls.Add(this.txtHeader);
            this.Controls.Add(this.txtDepth);
            this.Controls.Add(this.txtMatch);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "frmRule";
            this.Text = "frmRule";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUrlID;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.TextBox txtDepth;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtMatch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox edtUrl;
    }
}
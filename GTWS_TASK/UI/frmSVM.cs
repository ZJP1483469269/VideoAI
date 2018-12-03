using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TLKJ_IVS;
using Emgu.CV.Util;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.OCR;

namespace GTWS_TASK.UI
{
    public partial class frmSVM : Form
    {
        public frmSVM()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String cFileName = Application.StartupPath + @"\JDNUM.jpg";
            String cStr = IVS_AI.getClip(cFileName);
            this.LB_MSG.Text = cStr;
            cFileName = Application.StartupPath + @"\LS.jpg";
            pictureBox1.Load(cFileName);
        }

        private void btnSetFont_Click(object sender, EventArgs e)
        {
            FontDialog vDialog = new FontDialog();
            if (vDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtKeyWord.Font = vDialog.Font;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        Bitmap ActiveImage = null;
        private void btnImage_Click(object sender, EventArgs e)
        {
            Graphics g = txtKeyWord.CreateGraphics();
            Rectangle rect;
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);

            Bitmap vBitmap = new Bitmap(1, 1);
            g = Graphics.FromImage(vBitmap);
            //计算绘制文字所需的区域大小（根据宽度计算长度），重新创建矩形区域绘图
            SizeF sizef = g.MeasureString(this.txtKeyWord.Text, this.txtKeyWord.Font, PointF.Empty, format);

            int iWidth = (int)(sizef.Width + 1);
            int iHeight = (int)(sizef.Height + 1);
            rect = new Rectangle(0, 0, iWidth, iHeight);
            vBitmap.Dispose();

            ActiveImage = new Bitmap(iWidth, iHeight);
            g = Graphics.FromImage(ActiveImage);

            //使用ClearType字体功能
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.FillRectangle(new SolidBrush(Color.White), rect);
            g.DrawString(txtKeyWord.Text, txtKeyWord.Font, Brushes.Black, rect, format);

            this.pictureBox1.Image = ActiveImage;// IVS_AI.getClip(vBitmap);
        }

        public List<PictureBox> PBList = new List<PictureBox>();
        private void btnCut_Click(object sender, EventArgs e)
        {
            VectorOfVectorOfPoint ObjectList = new VectorOfVectorOfPoint();
            Image<Bgr, Byte> BgrImage = new Image<Bgr, byte>(ActiveImage);
            Image<Gray, Byte> GrayImage = new Image<Gray, byte>(ActiveImage.Width, ActiveImage.Height);
            CvInvoke.CvtColor(BgrImage, GrayImage, ColorConversion.Bgr2Gray);
            Image<Gray, Byte> ResultGrayImage = new Image<Gray, byte>(ActiveImage.Width, ActiveImage.Height);
            CvInvoke.Threshold(GrayImage, ResultGrayImage, 200, 255, ThresholdType.Binary);
            GrayImage = ResultGrayImage;
            //ResultGrayImage = new Image<Gray, byte>(ActiveImage.Width, ActiveImage.Height);

            //        CvInvoke.Dilate(GrayImage, ResultGrayImage, null, null, 1, BorderType.Default, new MCvScalar(0));

            pictureBox2.Image = GrayImage.Bitmap;


            Mat vMat = new Mat();
            //CvInvoke.BitwiseNot(GrayImage, ResultGrayImage, vMat);
            pictureBox2.Image = ResultGrayImage.Bitmap;
            this.ResultText.Text = IVS_AI.getKeyText(ResultGrayImage);

            Mat vHierarchy = new Mat();
            CvInvoke.FindContours(ResultGrayImage, ObjectList, vHierarchy, RetrType.List, ChainApproxMethod.ChainApproxNone);
            for (int i = PBList.Count - 1; i >= 0; i--)
            {
                this.Controls.Remove(PBList[i]);
            }
            int iValidNum = 0;

            for (int i = ObjectList.Size - 1; i >= 0; i--)
            {
                VectorOfPoint PointList = ObjectList[i];
                Rectangle rowRect = CvInvoke.BoundingRectangle(PointList);
                if (rowRect.Width > 2 && rowRect.Height > 2)
                {
                    Image<Gray, byte> rowImage = ResultGrayImage.GetSubRect(rowRect);
                    rowImage.Save(i.ToString() + ".jpg");
                    PictureBox vPB = new PictureBox();
                    vPB.Parent = this;
                    vPB.Name = "PB_" + i.ToString();
                    vPB.Left = iValidNum * 40 + 5;
                    vPB.Top = this.Height - 100;
                    vPB.Width = 40;
                    vPB.Height = 40;
                    vPB.BorderStyle = BorderStyle.Fixed3D;
                    vPB.Image = rowImage.Bitmap;
                    //vPB.SizeMode = PictureBoxSizeMode.StretchImage;
                    PBList.Add(vPB);
                    this.Controls.Add(vPB);
                    iValidNum++;
                }
            }
        }

    }
}

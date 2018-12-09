using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenCvSharp;
using TLKJ.Utils;
using TLKJ.DB;
using System.IO;
using OpenCvSharp.Extensions;
using TLKJ_IVS;
using TLKJAI;

namespace GTWS_BD
{
    public partial class frmCut : Form
    {
        public int ImageCount = 0;
        public frmCut()
        {
            InitializeComponent();
        }
        Mat DefaultMat = null;
        Mat GrayMat = null;
        Mat BinaryMat = null;
        String cFileName = @".\Model\IMG05.jpg";
        private void btnLoad_Click(object sender, EventArgs e)
        {
            DefaultMat = new Mat(cFileName);
            pictureBox1.Image = BitmapConverter.ToBitmap(DefaultMat);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<KeyValue> LS = IMGAI.getImageList(cFileName);
            for (int i = 0; i < LS.Count; i++)
            {
                KeyValue rowKey = LS[i];
                String vImage = rowKey.Text;
            }
            MessageBox.Show(LS.Count.ToString());
            int iMinVal = StringEx.getInt(IMG_MIN.Text);
            int iMaxVal = StringEx.getInt(IMG_MAX.Text);

            int iGrayMinVal = StringEx.getInt(GRAY_MIN.Text);
            int iGrayMaxVal = StringEx.getInt(GRAY_MAX.Text);

            int iEXPORT_IMAGE = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.EXPORT_IMAGE, "0"));

            String cID = "Export";
            Mat BgrImage = null;
            Mat GrayImage = null;
            List<Mat> ImageList = new List<Mat>();
            String cAbsoDir = Path.GetDirectoryName(cFileName);
            String cExportDir = cAbsoDir + @"\" + cID;
            if (!Directory.Exists(cExportDir))
            {
                Directory.CreateDirectory(cExportDir); //不存在文件夹，创建
            }

            BgrImage = Cv2.ImRead(cFileName, ImreadModes.AnyColor);
            GrayImage = Cv2.ImRead(cFileName, ImreadModes.Grayscale);
            GrayImage.SaveImage("IMG02.jpg");
            //Image<Gray, byte> binaryImage = GrayImage.ThresholdBinary(new Gray(thresholdBinary), new Gray(iVal));
            // ThresholdToZeroInv反取零   ThresholdToZero取零     ThresholdBinary二值    ThresholdBinaryInv反二值  ThresholdTrunc截断
            Mat binaryImage = GrayImage.Threshold(iGrayMinVal, iMaxVal, ThresholdTypes.Binary);
            GrayImage.SaveImage("IMG03.jpg");
            try
            {
                OpenCvSharp.Point[][] rvs = new OpenCvSharp.Point[1][];
                HierarchyIndex[] hierarchys = new HierarchyIndex[1];

                Cv2.FindContours(binaryImage, out rvs, out hierarchys, RetrievalModes.CComp, ContourApproximationModes.ApproxNone);

                for (int i = 0; i < rvs.Length; i++)
                {
                    OpenCvSharp.Point[] objItem = rvs[i];
                    Rect vRect = Cv2.BoundingRect(objItem);

                    if ((vRect.Width < iMinVal) || (vRect.Height < iMinVal)) continue;
                    if ((vRect.Width > iMaxVal) || (vRect.Height > iMaxVal)) continue;
                    //j++;
                    //Point p1 = new Point(vRect.X, vRect.Y);
                    //Point p2 = new Point(vRect.X + vRect.Width, vRect.Y + vRect.Height);

                    Mat vResult = new Mat(BgrImage, vRect);
                    ImageList.Add(vResult);
                    //String cFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + i.ToString();
                    //vResult.Save(cExportDir + "\\" + i + ".jpg");
                    //ImageList.Add(cExportDir + "\\" + i + ".jpg");
                    Cv2.Rectangle(BgrImage, vRect, new Scalar(255, 0, 0));
                }
                BgrImage.SaveImage("IMG04.jpg");
                pictureBox1.Image = BitmapConverter.ToBitmap(BgrImage);
            }
            catch (Exception ex)
            {
                log4net.WriteTextLog("报错，原因为：" + ex);
            }
        }

        private void IMG_MIN_ValueChanged(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }

        private void btnGray_Click(object sender, EventArgs e)
        {
            GrayMat = DefaultMat.Clone();
            Cv2.CvtColor(DefaultMat, GrayMat, ColorConversionCodes.BGR2GRAY);
            pictureBox1.Image = BitmapConverter.ToBitmap(GrayMat);
        }

        private void btnBinary_Click(object sender, EventArgs e)
        {
            int iGrayMinVal = StringEx.getInt(GRAY_MIN.Text);
            int iGrayMaxVal = StringEx.getInt(GRAY_MAX.Text);

            //BinaryMat = GrayMat.Threshold(iGrayMinVal, iGrayMaxVal, ThresholdTypes.Binary);
            BinaryMat= GrayMat.AdaptiveThreshold(255, AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, 9, 5);

            pictureBox1.Image = BitmapConverter.ToBitmap(BinaryMat);
       
            int iMinVal = StringEx.getInt(IMG_MIN.Text);
            int iMaxVal = StringEx.getInt(IMG_MAX.Text);

            OpenCvSharp.Point[][] rvs = new OpenCvSharp.Point[1][];
            HierarchyIndex[] hierarchys = new HierarchyIndex[1];

            Cv2.FindContours(BinaryMat, out rvs, out hierarchys, RetrievalModes.CComp, ContourApproximationModes.ApproxSimple);
            for (int i = 0; i < rvs.Length; i++)
            {
                OpenCvSharp.Point[] objItem = rvs[i];
                Rect vRect = Cv2.BoundingRect(objItem);

                if ((vRect.Width < iMinVal) || (vRect.Height < iMinVal)) continue;
                if ((vRect.Width > iMaxVal) || (vRect.Height > iMaxVal)) continue;
                //j++;
                //Point p1 = new Point(vRect.X, vRect.Y);
                //Point p2 = new Point(vRect.X + vRect.Width, vRect.Y + vRect.Height); 
              
                Cv2.Rectangle(DefaultMat, vRect, new Scalar(255, 0, 0));
            } 
            pictureBox1.Image = BitmapConverter.ToBitmap(DefaultMat);

        }
    }
}

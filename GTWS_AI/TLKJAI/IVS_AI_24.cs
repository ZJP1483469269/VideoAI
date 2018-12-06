using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV.OCR;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using TLKJ.Utils;
using System.IO;

namespace TLKJ_IVS
{
    public class IVS_AI
    {
        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        public static extern bool BitBlt(
             IntPtr hdcDest, // handle to destination DC
             int nXDest, // x-coord of destination upper-left corner
             int nYDest, // y-coord of destination upper-left corner
             int nWidth, // width of destination rectangle
             int nHeight, // height of destination rectangle
             IntPtr hdcSrc, // handle to source DC
             int nXSrc, // x-coordinate of source upper-left corner
             int nYSrc, // y-coordinate of source upper-left corner
             System.Int32 dwRop // raster operation code
        );

        public static String getKeyText(Image<Gray, byte> vImage)
        {
            String cStr = "";
            string path = Application.StartupPath + "\\tessdata";  //下载识别文件夹
            string language = "eng";//识别语言
            Tesseract ocr = new Tesseract(path, language, Tesseract.OcrEngineMode.OEM_DEFAULT);//生成OCR对象。 
            ocr.SetVariable("tessedit_char_whitelist", "0123456789XPT.");//此方法表示只识别1234567890与x字母 
            int iCode = 2;
            //iCode = 0;
            ocr.Recognize(vImage);//进行识别图像
            if (iCode == 0)  //判断是否成功，应为返回为0时才是成功，因此开始定义初始化为其他值（非零）。
            {
                Tesseract.Charactor[] tet = ocr.GetCharactors();//得到
                cStr = ocr.GetText();//得到文字 
            }
            return cStr;
        }
        /// <summary>
        /// 遍历截图
        /// </summary>
        /// <param name="thresholdBinary">阈值</param>
        public static List<String> GetImageList(String cID, string cImgDir, int iMinVal, int iMaxVal, int iGrayMinVal, int iGrayMaxVal)
        {
            Mat MainMat = null;
            Image<Bgr, Byte> BgrImage = null;
            Image<Gray, Byte> GrayImage = null;
            List<String> ImageList = new List<string>();
            String cAbsoDir = Path.GetDirectoryName(cImgDir);
            String cExportDir = cAbsoDir + @"\" + cID;
            if (!Directory.Exists(cExportDir))
            {
                Directory.CreateDirectory(cExportDir); //不存在文件夹，创建
            }
            BgrImage = new Image<Bgr, Byte>(cImgDir);

            GrayImage = BgrImage.Convert<Gray, Byte>();
            //Image<Gray, byte> binaryImage = GrayImage.ThresholdBinary(new Gray(thresholdBinary), new Gray(iVal));
            // ThresholdToZeroInv反取零   ThresholdToZero取零     ThresholdBinary二值    ThresholdBinaryInv反二值  ThresholdTrunc截断
            Image<Gray, byte> binaryImage = GrayImage.ThresholdToZeroInv(new Gray(iGrayMaxVal));
            try
            {
                IntPtr Dynstorage = CvInvoke.cvCreateMemStorage(0);
                MCvContour con = new MCvContour();
                VectorOfVectorOfPoint rvs = new VectorOfVectorOfPoint();
                CvInvoke.cvFindContours(binaryImage, Dynstorage, ref rvs, Marshal.SizeOf(con), RETR_TYPE.CV_RETR_LIST, CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_NONE, new Point(0, 0));

                int j = 0;
                for (int i = 0; i < rvs.Size; i++)
                {
                    var contour = rvs[i];
                    Rectangle vRect = CvInvoke.cvBoundingRect(contour, false);

                    if ((vRect.Width < iMinVal) || (vRect.Height < iMinVal)) continue;
                    if ((vRect.Width > iMaxVal) || (vRect.Height > iMaxVal)) continue;
                    j++;
                    Point p1 = new Point(vRect.X, vRect.Y);
                    Point p2 = new Point(vRect.X + vRect.Width, vRect.Y + vRect.Height);

                    CvInvoke.cvRectangle(BgrImage, p1, p2, new MCvScalar(255, 0, 0, 0), 3, LINE_TYPE.FOUR_CONNECTED, 1);
                    Image<Bgr, Byte> vResult = BgrImage.GetSubRect(vRect);
                    String cFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + i.ToString();
                    vResult.Save(cExportDir + "\\" + i + ".jpg");
                    ImageList.Add(cExportDir + "\\" + i + ".jpg");
                }
            }
            catch (Exception ex)
            {
                log4net.WriteTextLog("报错，原因为：" + ex);
            }
            return ImageList;
        }
        public static Bitmap getClip(Bitmap vBitmap)
        {
            VectorOfVectorOfPoint ObjectList = new VectorOfVectorOfPoint();

            Image<Bgr, byte> BgrImage = new Image<Bgr, byte>(vBitmap);
            Image<Gray, byte> GrayImage = new Image<Gray, byte>(vBitmap.Width, vBitmap.Height);
            CvInvoke.cvCvtColor(BgrImage, GrayImage, COLOR_CONVERSION.BGR2GRAY);

            Mat vHierarchy = new Mat();

            // threshold(image, image2, 100, 255, THRESH_BINARY);//二值图像 

            Image<Bgr, Byte> disp = new Image<Bgr, byte>(vBitmap.Width, vBitmap.Height);
            Image<Bgr, Byte> edges = new Image<Bgr, byte>(vBitmap.Width, vBitmap.Height);

            Image<Gray, Byte> vResultImage = new Image<Gray, byte>(vBitmap.Width, vBitmap.Height, new Gray(0));

            // CvInvoke.Canny(ImageItem, edges, 100, 200);
            IntPtr Dynstorage = CvInvoke.cvCreateMemStorage(0);
            CvInvoke.cvFindContours(GrayImage, Dynstorage, ref ObjectList, 10, RETR_TYPE.CV_RETR_CCOMP, CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_NONE);
            for (int i = 0; i < ObjectList.Size; i++)
            {
                CvInvoke.cvDrawContours(disp, ObjectList, i, new MCvScalar(255, 255, 255), 1);
            }
            for (int i = 0; i < ObjectList.Size; i++)
            {
                for (int j = 0; j < ObjectList[i].Size; j++)
                {
                    vResultImage.Data[ObjectList[i][j].Y, ObjectList[i][j].X, 0] = 255;
                }
            }

            return vResultImage.Bitmap;
        }

        public static String getClip(String cFileName)
        {
            VectorOfVectorOfPoint ObjectList = new VectorOfVectorOfPoint();

            Image<Bgr, byte> fromImage = new Image<Bgr, byte>(cFileName);
            Rectangle rect = new Rectangle();
            rect.X = StringEx.getInt(Config.GetAppSettings(AppConfig.RECT_LEFT));
            rect.Y = StringEx.getInt(Config.GetAppSettings(AppConfig.RECT_TOP));
            rect.Width = StringEx.getInt(Config.GetAppSettings(AppConfig.RECT_WIDTH));
            rect.Height = StringEx.getInt(Config.GetAppSettings(AppConfig.RECT_HEIGHT));

            Image<Bgr, byte> TextBlockImage = fromImage.GetSubRect(rect);
            TextBlockImage.Save("LS_1.jpg");
            Image<Gray, byte> GrayImage = new Image<Gray, byte>(TextBlockImage.Width, TextBlockImage.Height);

            CvInvoke.cvCvtColor(TextBlockImage, GrayImage, COLOR_CONVERSION.BGR2GRAY);
            GrayImage.Save("LS_2.jpg");

            Mat vGrayImage = new Mat();//小于maxHue
            CvInvoke.cvThreshold(GrayImage, vGrayImage, 100, 255, THRESH.CV_THRESH_BINARY);
            GrayImage.Save("LS_3.jpg");

            //黑白
            Mat vImage = new Mat();
            CvInvoke.BitwiseNot(vGrayImage, GrayImage, vImage);
            GrayImage.Save("LS_4.jpg");

            //// threshold(image, image2, 100, 255, THRESH_BINARY);//二值图像 

            //Image<Bgr, Byte> disp = new Image<Bgr, byte>(BgrImage.Width, BgrImage.Height);
            //Image<Bgr, Byte> edges = new Image<Bgr, byte>(BgrImage.Width, BgrImage.Height);

            //Image<Gray, Byte> vResultImage = new Image<Gray, byte>(BgrImage.Width, BgrImage.Height, new Gray(0));

            //// CvInvoke.Canny(ImageItem, edges, 100, 200);
            Mat vHierarchy = new Mat();
            CvInvoke.cvFindContours(GrayImage, ObjectList, vHierarchy, 0, RETR_TYPE.CV_RETR_CCOMP, CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_NONE);
            for (int i = 0; i < ObjectList.Size; i++)
            {
                VectorOfPoint PointList = ObjectList[i];
                Rectangle rowRect = CvInvoke.cvBoundingRect(PointList, true);
                Image<Bgr, byte> rowImage = TextBlockImage.GetSubRect(rowRect);
                rowImage.Save(i.ToString() + ".jpg");

                //CvInvoke.DrawContours(TextBlockImage, ObjectList, i, new MCvScalar(255, 255, 255), 1);
            }

            return "";
        }

        public static Image<Gray, byte> getGrayImage(String cFileName)
        {
            Image<Bgr, byte> vImage = new Image<Bgr, byte>(cFileName);
            Rectangle rect = new Rectangle();
            rect.X = StringEx.getInt(Config.GetAppSettings(AppConfig.RECT_LEFT));
            rect.Y = StringEx.getInt(Config.GetAppSettings(AppConfig.RECT_TOP));
            rect.Width = StringEx.getInt(Config.GetAppSettings(AppConfig.RECT_WIDTH));
            rect.Height = StringEx.getInt(Config.GetAppSettings(AppConfig.RECT_HEIGHT));

            rect.Width = StringEx.getInt(Config.GetAppSettings(AppConfig.RECT_WIDTH));
            rect.Height = StringEx.getInt(Config.GetAppSettings(AppConfig.RECT_HEIGHT));

            int iBINARY_MIN = StringEx.getInt(Config.GetAppSettings(AppConfig.BINARY_MIN));
            int iBINARY_MAX = StringEx.getInt(Config.GetAppSettings(AppConfig.BINARY_MAX));

            //裁剪
            Image<Bgr, byte> vRectImage = vImage.GetSubRect(rect);

            //转灰度
            Image<Gray, byte> GrayImage = new Image<Gray, byte>(vRectImage.Width, vRectImage.Height);
            CvInvoke.cvCvtColor(vRectImage, GrayImage, COLOR_CONVERSION.BGR2GRAY);

            //黑白翻转
            Mat vMat = new Mat();
            Image<Gray, byte> ResultGrayImage = GrayImage.CopyBlank();
            CvInvoke.BitwiseNot(GrayImage, ResultGrayImage, vMat);
            GrayImage = ResultGrayImage;

            //二值化
            ResultGrayImage = GrayImage.CopyBlank();
            CvInvoke.cvThreshold(GrayImage, ResultGrayImage, iBINARY_MIN, iBINARY_MAX, THRESH.CV_THRESH_BINARY);

            return ResultGrayImage;
        }

        public String getText(Bitmap vBitmap)
        {
            //下载识别文件夹
            string path = Application.StartupPath + "\\tessdata";
            string language = "eng";//识别语言
            Tesseract ocr = new Tesseract(path, language, Tesseract.OcrEngineMode.OEM_DEFAULT);//生成OCR对象。
            Emgu.CV.Image<Gray, byte> GAY1 = new Image<Gray, byte>(vBitmap);//原图一定为灰阶图片
            int A_LENG = 2;
            A_LENG = 0;
            ocr.Recognize(GAY1);//进行识别图像
            if (A_LENG == 0)  //判断是否成功，应为返回为0时才是成功，因此开始定义初始化为其他值（非零）。
            {
                Tesseract.Charactor[] tet = ocr.GetCharactors();//得到
                string cStr = ocr.GetText();//得到文字
                return cStr;
            }
            else
            {
                return "";
            }
        }
    }
}


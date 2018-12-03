using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using TLKJ.Utils;
using OpenCvSharp;
using System.IO;
using OpenCvSharp.Extensions;
using Tesseract;

namespace TLKJ_IVS
{
    public class TLKJ_AI
    {
        public static String getImageText(String cFileName)
        {
            Bitmap vBmp = getPTXImage(cFileName);
            return getImageText(vBmp);
        }
        public static float getP(String cStr)
        {
            //P10.3 T 0.3 X 0.1
            int idxP = cStr.IndexOf("P");
            int idxT = cStr.IndexOf("T");
            int idxX = cStr.IndexOf("X");
            try
            {
                String cVal = StringEx.getString(cStr.Substring(idxP, idxT));
                return StringEx.getFloat(cVal);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static float getT(String cStr)
        {
            //P10.3 T 0.3 X 0.1
            int idxP = cStr.IndexOf("P");
            int idxT = cStr.IndexOf("T");
            int idxX = cStr.IndexOf("X");
            try
            {
                String cVal = StringEx.getString(cStr.Substring(idxT, idxX));
                return StringEx.getFloat(cVal);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static float getX(String cStr)
        {
            //P10.3 T 0.3 X 0.1
            int idxP = cStr.IndexOf("P");
            int idxT = cStr.IndexOf("T");
            int idxX = cStr.IndexOf("X");
            try
            {
                String cVal = StringEx.getString(cStr.Substring(idxX));
                return StringEx.getFloat(cVal);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public static String getImageText(Bitmap vBmp)
        {
            String cStr = null;
            try
            {
                string path = Application.StartupPath + "\\tessdata";  //下载识别文件夹
                string language = "eng";//识别语言
                TesseractEngine ocr = new TesseractEngine(path, language, EngineMode.Default);//生成OCR对象。 
                ocr.SetVariable("tessedit_char_whitelist", "0123456789XPT.");//此方法表示只识别1234567890与x字母 
                Page vf = ocr.Process(vBmp);//放进图像到OCR对象中
                cStr = vf.GetText();
            }
            catch (Exception ex)
            {
                cStr = null;
            }
            if (!String.IsNullOrEmpty(cStr))
            {
                cStr = cStr.Trim().Replace("\n", "").Replace("\r", "");
            }
            return cStr;
        }

        public static Bitmap getPTXImage(String cFileName)
        {
            Mat fromImage = new Mat(cFileName);
            OpenCvSharp.Rect rect = new OpenCvSharp.Rect();
            rect.X = StringEx.getInt(Config.GetAppSettings(AppConfig.RECT_LEFT));
            rect.Y = StringEx.getInt(Config.GetAppSettings(AppConfig.RECT_TOP));
            rect.Width = StringEx.getInt(Config.GetAppSettings(AppConfig.RECT_WIDTH));
            rect.Height = StringEx.getInt(Config.GetAppSettings(AppConfig.RECT_HEIGHT));

            rect.Width = StringEx.getInt(Config.GetAppSettings(AppConfig.RECT_WIDTH));
            rect.Height = StringEx.getInt(Config.GetAppSettings(AppConfig.RECT_HEIGHT));

            int iBINARY_MIN = StringEx.getInt(Config.GetAppSettings(AppConfig.BINARY_MIN));
            int iBINARY_MAX = StringEx.getInt(Config.GetAppSettings(AppConfig.BINARY_MAX));


            //裁剪
            Mat vRectImage = new Mat(fromImage, rect);

            //转灰度
            Mat GrayImage = new Mat(vRectImage.Width, vRectImage.Height, MatType.CV_8SC1);
            Cv2.CvtColor(vRectImage, GrayImage, ColorConversionCodes.RGB2GRAY);

            //黑白翻转
            Mat vMat = new Mat();
            Mat ResultGrayImage = new Mat(vRectImage.Width, vRectImage.Height, MatType.CV_8SC1);
            Cv2.BitwiseNot(GrayImage, ResultGrayImage, vMat);
            GrayImage = ResultGrayImage;

            //二值化
            ResultGrayImage = new Mat(vRectImage.Width, vRectImage.Height, MatType.CV_8SC1);
            Cv2.Threshold(GrayImage, ResultGrayImage, iBINARY_MIN, iBINARY_MAX, ThresholdTypes.Binary);

            return BitmapConverter.ToBitmap(GrayImage);
        }

        public static List<String> getImageList(String cFileName)
        {
            int iMinVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.IMAGE_MIN, "0"));
            int iMaxVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.IMAGE_MAX, "0"));

            int iGrayMinVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.GRAY_MIN, "0"));
            int iGrayMaxVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.GRAY_MAX, "0"));
            int iEXPORT_IMAGE = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.EXPORT_IMAGE, "0"));
            return getImageList(cFileName, iMinVal, iMaxVal, iGrayMinVal, iGrayMaxVal);
        }

        public static List<String> getImageList(String cFileName, int iMinVal, int iMaxVal, int iGrayMinVal, int iGrayMaxVal)
        {
            String cID = "Export";
            Mat BgrImage = null;
            Mat GrayImage = null;
            List<String> ImageList = new List<String>();
            String cAbsoDir = Path.GetDirectoryName(cFileName);
            String cExportDir = cAbsoDir + @"\" + cID;
            if (!Directory.Exists(cExportDir))
            {
                Directory.CreateDirectory(cExportDir); //不存在文件夹，创建
            }

            BgrImage = Cv2.ImRead(cFileName, ImreadModes.AnyColor);
            GrayImage = Cv2.ImRead(cFileName, ImreadModes.Grayscale);
            Mat vMat = new Mat();
            Cv2.BitwiseAnd(GrayImage, vMat, GrayImage);
            GrayImage.SaveImage("IMG02.jpg");
            //Image<Gray, byte> binaryImage = GrayImage.ThresholdBinary(new Gray(thresholdBinary), new Gray(iVal));
            // ThresholdToZeroInv反取零   ThresholdToZero取零     ThresholdBinary二值    ThresholdBinaryInv反二值  ThresholdTrunc截断
            Mat binaryImage = GrayImage.Threshold(iGrayMinVal, iMaxVal, ThresholdTypes.Binary);
            GrayImage.SaveImage("IMG03.jpg");
            try
            {
                OpenCvSharp.Point[][] rvs = new OpenCvSharp.Point[1][];
                HierarchyIndex[] hierarchys = new HierarchyIndex[1];

                Cv2.FindContours(binaryImage, out rvs, out hierarchys, RetrievalModes.Tree, ContourApproximationModes.ApproxNone);

                for (int i = 0; i < rvs.Length; i++)
                {
                    OpenCvSharp.Point[] objItem = rvs[i];
                    OpenCvSharp.Rect vRect = Cv2.BoundingRect(objItem);

                    if ((vRect.Width < iMinVal) || (vRect.Height < iMinVal)) continue;
                    if ((vRect.Width > iMaxVal) || (vRect.Height > iMaxVal)) continue;
                    //j++;
                    //Point p1 = new Point(vRect.X, vRect.Y);
                    //Point p2 = new Point(vRect.X + vRect.Width, vRect.Y + vRect.Height);

                    Mat vResult = new Mat(BgrImage, vRect);
                    vResult.SaveImage(@"Export\" + Path.GetFileName(cFileName) + "_" + i.ToString() + ".jpg");
                    //ImageList.Add(vResult);
                    //String cFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + i.ToString();
                    //vResult.Save(cExportDir + "\\" + i + ".jpg");
                    //ImageList.Add(cExportDir + "\\" + i + ".jpg");
                    Cv2.Rectangle(BgrImage, vRect, new Scalar(255, 0, 0));
                }
                BgrImage.SaveImage("IMG04.jpg");
            }
            catch (Exception ex)
            {
                log4net.WriteTextLog("报错，原因为：" + ex);
            }
            return ImageList;
        }

    }
}


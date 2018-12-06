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

namespace TLKJAI
{
    public class IMGAI
    {
        public static String getImageText(String cFileName)
        {
            Bitmap vBmp = getPTXImage(cFileName);
            String cStr = getImageText(vBmp);
            if ((cStr.IndexOf("P") != -1) && (cStr.IndexOf("T") != -1) && (cStr.IndexOf("X") != -1))
                return cStr;
            else
            {
                String cPTXName = cFileName.Replace("images", "ptx");
                try
                {
                    vBmp.Save(cPTXName);
                    vBmp.Dispose();
                }
                catch (Exception ex)
                {
                    log4net.WriteTextLog(ex.Message);
                }
                log4net.WriteTextLog(cStr);
                return "";
            }
        }
        public static float getP(String cStr)
        {
            //P10.3 T 0.3 X 0.1
            int idxP = cStr.IndexOf("P");
            int idxT = cStr.IndexOf("T");
            int idxX = cStr.IndexOf("X");
            try
            {
                String cVal = StringEx.getString(cStr.Substring(idxP + 1, idxT - 1));
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
                String cVal = StringEx.getString(cStr.Substring(idxT + 1, idxX - 1));
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
                String cVal = StringEx.getString(cStr.Substring(idxX + 1));
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

        public static List<KeyValue> getImageList(String cFileName)
        {
            int iMinVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.IMAGE_MIN, "0"));
            int iMaxVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.IMAGE_MAX, "0"));

            int iGrayMinVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.GRAY_MIN, "0"));
            int iGrayMaxVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.GRAY_MAX, "0"));
            int iEXPORT_IMAGE = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.EXPORT_IMAGE, "0"));
            return getImageList(cFileName, iMinVal, iMaxVal, iGrayMinVal, iGrayMaxVal);
        }

        public static List<KeyValue> getImageList(String cFileName, int iMinVal, int iMaxVal, int iGrayMinVal, int iGrayMaxVal)
        {
            Mat BgrImage = null;
            Mat GrayImage = new Mat();

            List<KeyValue> ImageList = new List<KeyValue>();
            String cExportDir = Path.GetDirectoryName(cFileName);
            cExportDir = cExportDir.Replace("images", "export") + "\\";
            if (!Directory.Exists(cExportDir))
            {
                Directory.CreateDirectory(cExportDir); //不存在文件夹，创建
            }

            //调取图片
            BgrImage = Cv2.ImRead(cFileName, ImreadModes.AnyColor);

            //转灰度
            Cv2.CvtColor(BgrImage, GrayImage, ColorConversionCodes.BGR2GRAY);

            //转黑白
            Mat BinaryImage = new Mat();

            Mat vMat = new Mat();

            BinaryImage = GrayImage.Threshold(iGrayMinVal, iMaxVal, ThresholdTypes.Binary);

            BinaryImage.SaveImage("D:\\IMG03.jpg");

            String cFileID = Path.GetFileName(cFileName).Replace(".jpg", "");
            try
            {
                OpenCvSharp.Point[][] rvs = new OpenCvSharp.Point[1][];
                HierarchyIndex[] hierarchys = new HierarchyIndex[1];

                Cv2.FindContours(BinaryImage, out rvs, out hierarchys, RetrievalModes.Tree, ContourApproximationModes.ApproxNone);
                int j = 1;
                for (int i = 0; i < rvs.Length; i++)
                {
                    OpenCvSharp.Point[] objItem = rvs[i];
                    OpenCvSharp.Rect vRect = Cv2.BoundingRect(objItem);

                    if ((vRect.Width < iMinVal) || (vRect.Height < iMinVal)) continue;
                    if ((vRect.Width > iMaxVal) || (vRect.Height > iMaxVal)) continue;
                    j++;
                    //Point p1 = new Point(vRect.X, vRect.Y);
                    //Point p2 = new Point(vRect.X + vRect.Width, vRect.Y + vRect.Height);

                    Mat vResult = new Mat(BgrImage, vRect);

                    vResult.SaveImage(cExportDir + cFileID + "_" + j.ToString() + ".jpg");
                    //ImageList.Add(vResult);
                    //String cFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + i.ToString();
                    //vResult.Save(cExportDir + "\\" + i + ".jpg");
                    //ImageList.Add(cExportDir + "\\" + i + ".jpg");
                    Cv2.Rectangle(BgrImage, vRect, new Scalar(255, 0, 0));

                    KeyValue rowKey = new KeyValue();
                    rowKey.Text = cExportDir + cFileID + "_" + j.ToString() + ".jpg";
                    rowKey.Val = JsonLib.ToJSON(vRect);
                    ImageList.Add(rowKey);
                }
                BgrImage.SaveImage("D:\\IMG04.jpg");
            }
            catch (Exception ex)
            {
                log4net.WriteTextLog("报错，原因为：" + ex);
            }
            return ImageList;
        }

    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using TLKJ.Utils;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.OCR;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;

namespace TLKJAI
{
    public class IMGAI
    {
        public static String getImageText(String cFileName)
        {
            try
            {
                Image<Gray, byte> vBmp = getPTXImage(cFileName);
                String cStr = getImageText(vBmp);
                if (String.IsNullOrWhiteSpace(cStr))
                {
                    log4net.WriteLogFile("PTX分析失败:" + Path.GetFileName(cFileName));
                }
                if ((cStr.IndexOf("P") != -1) && (cStr.IndexOf("T") != -1) && (cStr.IndexOf("X") != -1))
                    return cStr;
                else
                {
                    String cPTXName = cFileName.Replace("images", "ptx");
                    String cDir = Path.GetDirectoryName(cPTXName);
                    if (!Directory.Exists(cDir))
                    {
                        Directory.CreateDirectory(cDir);
                    }
                    try
                    {
                        vBmp.Save(cPTXName);
                        vBmp.Dispose();
                    }
                    catch (Exception ex)
                    {
                        log4net.WriteLogFile(ex.Message);
                    }
                    log4net.WriteLogFile(cStr);
                    return "";
                }
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile(ex.Message);
                return "";
            }
        }
        public static Boolean RedrawImage(String cFileName, String cRect)
        {
            try
            {
                Image<Bgr, byte> vImage = new Image<Bgr, byte>(cFileName);
                Rectangle vRect = JsonLib.ToObject<Rectangle>(cRect);
                CvInvoke.Rectangle(vImage, vRect, new MCvScalar(255, 0, 0), 2);
                vImage.Save(cFileName);
                return true;
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile(ex.Message);
                return false;
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
                String cVal = StringEx.getString(cStr.Substring(idxT + 1, idxX - idxT - 1)).Trim();
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

        public static String getImageText(Image<Gray, Byte> vBmp)
        {
            String cStr = null;
            try
            {
                vBmp.Save("PTX.jpg");
                string path = Application.StartupPath + "\\tessdata";  //下载识别文件夹
                string language = "eng";//识别语言
                Tesseract ocr = new Tesseract(path, language, OcrEngineMode.Default);//生成OCR对象。 
                ocr.SetVariable("tessedit_char_whitelist", "0123456789XPT.");//此方法表示只识别1234567890与x字母 
                ocr.SetImage(vBmp);
                cStr = ocr.GetUTF8Text();
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile(ex.Message);
                cStr = null;
            }
            if (!String.IsNullOrEmpty(cStr))
            {
                cStr = cStr.Trim().Replace("\n", "").Replace("\r", "");
            }
            return cStr;
        }

        public static Image<Gray, byte> getPTXImage(String cFileName)
        {
            Image<Bgr, byte> fromImage = new Image<Bgr, byte>(cFileName);
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
            Image<Bgr, byte> vRectImage = fromImage.GetSubRect(rect);

            //转灰度
            Image<Gray, byte> GrayImage = vRectImage.Convert<Gray, byte>();


            //黑白翻转
            Image<Gray, byte> ResultGrayImage = new Image<Gray, byte>(vRectImage.Width, vRectImage.Height);
            CvInvoke.BitwiseNot(GrayImage, ResultGrayImage);
            GrayImage = ResultGrayImage;

            //二值化
            ResultGrayImage = new Image<Gray, byte>(vRectImage.Width, vRectImage.Height);
            CvInvoke.Threshold(GrayImage, ResultGrayImage, iBINARY_MIN, iBINARY_MAX, Emgu.CV.CvEnum.ThresholdType.Binary);

            return ResultGrayImage;
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
        public static Image getImage(String cImageFileName)
        {
            Stream s = new FileStream(cImageFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            try
            {
                Image img = new Bitmap(s);
                s.Close();
                return img;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<Rectangle> getImageRect(String cFileName)
        {
            try
            {
                Image<Bgr, Byte> BgrImage = null;
                Image<Gray, Byte> GrayImage = null;

                List<Rectangle> ImageList = new List<Rectangle>();

                int iMinVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.IMAGE_MIN, "0"));
                int iMaxVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.IMAGE_MAX, "0"));

                int iGrayMinVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.GRAY_MIN, "0"));
                int iGrayMaxVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.GRAY_MAX, "0"));

                //调取图片
                BgrImage = new Image<Bgr, byte>(cFileName);
                GrayImage = new Image<Gray, byte>(BgrImage.Width, BgrImage.Height);

                //转灰度
                CvInvoke.CvtColor(BgrImage, GrayImage, Emgu.CV.CvEnum.ColorConversion.Rgb2Gray);

                //转黑白
                Image<Gray, byte> BinaryImage = GrayImage.ThresholdToZeroInv(new Gray(iGrayMinVal));

                VectorOfVectorOfPoint rvs = new VectorOfVectorOfPoint();
                CvInvoke.FindContours(BinaryImage, rvs, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);

                int j = 10001;
                for (int i = 0; i < rvs.Size; i++)
                {
                    var contour = rvs[i];
                    Rectangle BoundingBox = CvInvoke.BoundingRectangle(contour);
                    ImageList.Add(BoundingBox);
                    if ((BoundingBox.Width < iMinVal) || (BoundingBox.Height < iMinVal)) continue;
                    if ((BoundingBox.Width > iMaxVal) || (BoundingBox.Height > iMaxVal)) continue;
                    j++;
                    CvInvoke.Rectangle(BgrImage, BoundingBox, new MCvScalar(255, 0, 0, 0), 3);
                }
                BgrImage.Save(cFileName.Replace("01.jpg", "02.jpg"));
                return ImageList;

            }
            catch (Exception ex)
            {
                log4net.WriteLogFile("报错，原因为：" + ex);
                return null;
            }
        }
        public static List<KeyValue> getImageList(String cFileName, int iMinVal, int iMaxVal, int iGrayMinVal, int iGrayMaxVal)
        {
            try
            {
                Image<Bgr, Byte> BgrImage = null;
                Image<Gray, Byte> GrayImage = null;

                List<KeyValue> ImageList = new List<KeyValue>();
                String cExportDir = Application.StartupPath + "\\export\\";
                ResetDirectory(cExportDir);

                //调取图片
                BgrImage = new Image<Bgr, byte>(cFileName);
                GrayImage = new Image<Gray, byte>(BgrImage.Width, BgrImage.Height);

                //转灰度
                CvInvoke.CvtColor(BgrImage, GrayImage, Emgu.CV.CvEnum.ColorConversion.Rgb2Gray);

                //转黑白
                Image<Gray, byte> BinaryImage = GrayImage.ThresholdToZeroInv(new Gray(iGrayMinVal));

                BinaryImage.Save("D:\\IMG03.jpg");

                String cREC_ID = Path.GetFileName(cFileName).Replace(".jpg", "");

                VectorOfVectorOfPoint rvs = new VectorOfVectorOfPoint();
                CvInvoke.FindContours(BinaryImage, rvs, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);

                int j = 10001;
                for (int i = 0; i < rvs.Size; i++)
                {
                    var contour = rvs[i];
                    Rectangle BoundingBox = CvInvoke.BoundingRectangle(contour);
                    if ((BoundingBox.Width < iMinVal) || (BoundingBox.Height < iMinVal)) continue;
                    if ((BoundingBox.Width > iMaxVal) || (BoundingBox.Height > iMaxVal)) continue;
                    j++;
                    CvInvoke.Rectangle(BgrImage, BoundingBox, new MCvScalar(255, 0, 0, 0), 3);
                    Image<Bgr, Byte> vResult = BgrImage.GetSubRect(BoundingBox);
                    String cExportFileName = cREC_ID + "_" + j.ToString() + ".jpg";
                    vResult.Save(cExportDir + cExportFileName);

                    KeyValue rowKey = new KeyValue();
                    rowKey.Text = cExportDir + cExportFileName;
                    rowKey.Val = JsonLib.ToJSON(BoundingBox);
                    ImageList.Add(rowKey);
                }

                String cZipFileName = Application.StartupPath + "\\" + cREC_ID + ".zip";
                Zip vo = new Zip();
                vo.ZipDir(cExportDir, cZipFileName);

                BgrImage.Save("D:\\IMG04.jpg");

                return ImageList;

            }
            catch (Exception ex)
            {
                log4net.WriteLogFile("报错，原因为：" + ex);
                return null;
            }
        }
        public static void ResetDirectory(String cDir)
        {
            if (!Directory.Exists(cDir))
            {
                Directory.CreateDirectory(cDir); //不存在文件夹，创建
                return;
            }
            String[] FileList = Directory.GetFiles(cDir);
            for (int i = FileList.Length - 1; i >= 0; i--)
            {
                String cFileName = FileList[i];
                if (File.Exists(cFileName))
                {
                    try
                    {
                        File.Delete(cFileName);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
    }
}


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
            Tesseract ocr = new Tesseract(path, language, Emgu.CV.OCR.OcrEngineMode.Default);//生成OCR对象。 
            ocr.SetVariable("tessedit_char_whitelist", "0123456789XPT.");//此方法表示只识别1234567890与x字母 
            ocr.SetImage(vImage);//放进图像到OCR对象中
            int iCode = 2;
            iCode = ocr.Recognize();//进行识别图像
            if (iCode == 0)  //判断是否成功，应为返回为0时才是成功，因此开始定义初始化为其他值（非零）。
            {
                Tesseract.Character[] tet = ocr.GetCharacters();//得到
                cStr = ocr.GetUTF8Text();//得到文字 
            }
            return cStr;
        }

        public static Bitmap getClip(Bitmap vBitmap)
        {
            VectorOfVectorOfPoint ObjectList = new VectorOfVectorOfPoint();

            Image<Bgr, byte> BgrImage = new Image<Bgr, byte>(vBitmap);
            Image<Gray, byte> GrayImage = new Image<Gray, byte>(vBitmap.Width, vBitmap.Height);
            CvInvoke.CvtColor(BgrImage, GrayImage, ColorConversion.Rgb2Gray);

            Mat vHierarchy = new Mat();

            // threshold(image, image2, 100, 255, THRESH_BINARY);//二值图像 

            Image<Bgr, Byte> disp = new Image<Bgr, byte>(vBitmap.Width, vBitmap.Height);
            Image<Bgr, Byte> edges = new Image<Bgr, byte>(vBitmap.Width, vBitmap.Height);

            Image<Gray, Byte> vResultImage = new Image<Gray, byte>(vBitmap.Width, vBitmap.Height, new Gray(0));

            // CvInvoke.Canny(ImageItem, edges, 100, 200);

            CvInvoke.FindContours(GrayImage, ObjectList, vHierarchy, RetrType.Ccomp, ChainApproxMethod.ChainApproxNone);
            for (int i = 0; i < ObjectList.Size; i++)
            {
                CvInvoke.DrawContours(disp, ObjectList, i, new MCvScalar(255, 255, 255), 1);
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

            CvInvoke.CvtColor(TextBlockImage, GrayImage, ColorConversion.Rgb2Gray);
            GrayImage.Save("LS_2.jpg");

            Mat vGrayImage = new Mat();//小于maxHue
            CvInvoke.Threshold(GrayImage, vGrayImage, 100, 255, ThresholdType.Binary);
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
            CvInvoke.FindContours(GrayImage, ObjectList, vHierarchy, RetrType.Ccomp, ChainApproxMethod.ChainApproxNone);
            for (int i = 0; i < ObjectList.Size; i++)
            {
                VectorOfPoint PointList = ObjectList[i];
                Rectangle rowRect = CvInvoke.BoundingRectangle(PointList);
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
            CvInvoke.CvtColor(vRectImage, GrayImage, ColorConversion.Rgb2Gray);

            //黑白翻转
            Mat vMat = new Mat();
            Image<Gray, byte> ResultGrayImage = GrayImage.CopyBlank();
            CvInvoke.BitwiseNot(GrayImage, ResultGrayImage, vMat);
            GrayImage = ResultGrayImage;

            //二值化
            ResultGrayImage = GrayImage.CopyBlank();
            CvInvoke.Threshold(GrayImage, ResultGrayImage, iBINARY_MIN, iBINARY_MAX, ThresholdType.Binary);

            return ResultGrayImage;
        }

        public String getText(Bitmap vBitmap)
        {
            //下载识别文件夹
            string path = Application.StartupPath + "\\tessdata";
            string language = "eng";//识别语言
            Tesseract ocr = new Tesseract(path, language, Emgu.CV.OCR.OcrEngineMode.Default);//生成OCR对象。
            Emgu.CV.Image<Gray, byte> GAY1 = new Image<Gray, byte>(vBitmap);//原图一定为灰阶图片
            ocr.SetImage(GAY1);//放进图像到OCR对象中
            int A_LENG = 2;
            A_LENG = ocr.Recognize();//进行识别图像
            if (A_LENG == 0)  //判断是否成功，应为返回为0时才是成功，因此开始定义初始化为其他值（非零）。
            {
                Tesseract.Character[] tet = ocr.GetCharacters();//得到
                string cStr = ocr.GetUTF8Text();//得到文字
                return cStr;
            }
            else
            {
                return "";
            }
        }
    }
}


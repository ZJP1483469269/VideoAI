using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using System.IO;
using TLKJ.Utils;

namespace TLKJ_IVS
{
    public class ObjectDetect
    {

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
            MainMat = CvInvoke.Imread(cImgDir, Emgu.CV.CvEnum.ImreadModes.Color); //从文件中读取图像 

            GrayImage = BgrImage.Convert<Gray, Byte>();
            //Image<Gray, byte> binaryImage = GrayImage.ThresholdBinary(new Gray(thresholdBinary), new Gray(iVal));
            // ThresholdToZeroInv反取零   ThresholdToZero取零     ThresholdBinary二值    ThresholdBinaryInv反二值  ThresholdTrunc截断
            Image<Gray, byte> binaryImage = GrayImage.ThresholdToZeroInv(new Gray(iGrayMaxVal));
            try
            {
                VectorOfVectorOfPoint rvs = new VectorOfVectorOfPoint();
                CvInvoke.FindContours(binaryImage, rvs, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);

                int j = 0;
                for (int i = 0; i < rvs.Size; i++)
                {
                    var contour = rvs[i];
                    Rectangle vRect = CvInvoke.BoundingRectangle(contour);

                    if ((vRect.Width < iMinVal) || (vRect.Height < iMinVal)) continue;
                    if ((vRect.Width > iMaxVal) || (vRect.Height > iMaxVal)) continue;
                    j++;
                    CvInvoke.Rectangle(BgrImage, vRect, new MCvScalar(255, 0, 0, 0), 3);
                    Image<Bgr, Byte> vResult = BgrImage.GetSubRect(vRect);
                    String cFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + i.ToString();
                    vResult.Save(cExportDir + "\\" + i + ".jpg");
                    ImageList.Add(cExportDir + "\\" + i + ".jpg");
                }
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile("报错，原因为：" + ex);
            }
            return ImageList;
        }
    }
}
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
    public class TLKJ_AI_24
    {
        public static List<String> GetImageList(String cID, string cImgDir, int iMinVal, int iMaxVal, int iGrayMinVal, int iGrayMaxVal)
        {
            List<String> ImageList = new List<string>();
            try
            {
                String cAbsoDir = Path.GetDirectoryName(cImgDir);
                String cExportDir = cAbsoDir + @"\" + cID;
                if (!Directory.Exists(cExportDir))
                {
                    Directory.CreateDirectory(cExportDir); //不存在文件夹，创建
                }
                Image<Bgr, Byte> Bgr_Image = new Image<Bgr, byte>(cImgDir);
                Image<Gray, Byte> Gray_Image = Bgr_Image.Convert<Gray, Byte>();
                Image<Gray, Byte> dest = new Image<Gray, Byte>(Gray_Image.Width, Gray_Image.Height);

                CvInvoke.cvThreshold(Gray_Image, dest, 30, 255, Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY);

                IntPtr Dyncontour = new IntPtr();
                IntPtr Dynstorage = CvInvoke.cvCreateMemStorage(0);
                MCvContour con = new MCvContour();

                int n = CvInvoke.cvFindContours(dest, Dynstorage, ref Dyncontour, Marshal.SizeOf(con), Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_TREE, Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_LINK_RUNS, new Point(0, 0));
                Seq<Point> vContour = new Seq<Point>(Dyncontour, null);

                IntPtr dst = CvInvoke.cvCreateImage(CvInvoke.cvGetSize(dest), Emgu.CV.CvEnum.IPL_DEPTH.IPL_DEPTH_8U, 3);
                CvInvoke.cvZero(dst);

                int i = -1;
                for (; vContour != null && vContour.Ptr.ToInt32() != 0; vContour = vContour.HNext)
                {
                    Rectangle vRect = vContour.BoundingRectangle;
                    if ((vRect.Width < iMinVal) || (vRect.Height < iMinVal)) continue;
                    if ((vRect.Width > iMaxVal) || (vRect.Height > iMaxVal)) continue;

                    i++;
                    String cKeyID = StringEx.getString(i + 1000);
                    Image<Bgr, Byte> vResult = Bgr_Image.GetSubRect(vRect);
                    String cFileName = cID + "_" + cKeyID.ToString();
                    vResult.Save(cExportDir + "\\" + cKeyID + ".jpg");
                    ImageList.Add(cExportDir + "\\" + cKeyID + ".jpg");
                }
            }
            catch (Exception ex)
            {

            }
            return ImageList;
        }
    }
}


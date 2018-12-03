using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TLKJ.Utils;
using TLKJ.DB;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Runtime.InteropServices;
using Emgu.CV.UI;

namespace GTWS_TASK.UI
{
    public partial class frmImage : Form
    {
        String cFileName = @"D:\VideoAI\GTWS_TASK\bin\Debug\Images\201811161629330000014660000000.jpg";
        private ImageBox ActiveBox = null;
        public frmImage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int iMinVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.IMAGE_MIN, "0"));
            int iMaxVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.IMAGE_MAX, "0"));

            int iGrayMinVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.GRAY_MIN, "0"));
            int iGrayMaxVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.GRAY_MAX, "0"));
            int iEXPORT_IMAGE = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.EXPORT_IMAGE, "0"));

            String cDFS_PATH = INIConfig.ReadString("UPLOAD", "DFS_PATH", "0");

            String cAppDir = Application.StartupPath;
            Boolean isUpload = false;
            JActiveTable aMaster = new JActiveTable();
            JActiveTable aSlave = new JActiveTable();
            aSlave.TableName = "XT_IMG_LIST";
            aMaster.TableName = "XT_IMG_REC";

            String cExportDir = "Export";
            String cREC_ID = "123456";
            Image<Bgr, Byte> Bgr_Image = new Image<Bgr, byte>(cFileName);
            Image<Gray, Byte> Gray_Image = Bgr_Image.Convert<Gray, Byte>();
            Image<Gray, Byte> Binary_Image = new Image<Gray, byte>(Gray_Image.Size);

            //CvInvoke.cvThreshold(Gray_Image, Binary_Image, iGrayMinVal, iGrayMaxVal, Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY);
            ActiveBox.Image = Binary_Image;
            //if (cExportDir.Length > 0)
            //{
            //    return;
            //}

            IntPtr Dyncontour = new IntPtr();
            IntPtr Dynstorage = CvInvoke.cvCreateMemStorage(0);
            MCvContour con = new MCvContour();

            int n = CvInvoke.cvFindContours(Gray_Image, Dynstorage, ref Dyncontour, Marshal.SizeOf(con), Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_CCOMP, Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE, new Point(0, 0));
            Seq<Point> vContour = new Seq<Point>(Dyncontour, null);

            //IntPtr dst = CvInvoke.cvCreateImage(CvInvoke.cvGetSize(Gray_Image), Emgu.CV.CvEnum.IPL_DEPTH.IPL_DEPTH_8U, 3);
            // CvInvoke.cvZero(dst);

            int i = -1;
            for (; vContour != null && vContour.Ptr.ToInt32() != 0; vContour = vContour.HNext)
            {
                Rectangle vRect = vContour.BoundingRectangle;
                if ((vRect.Width < iMinVal) || (vRect.Height < iMinVal)) continue;
                if ((vRect.Width > iMaxVal) || (vRect.Height > iMaxVal)) continue;
                Point ptX = new Point(vRect.X, vRect.Y);
                Point ptY = new Point(vRect.X + vRect.Width, vRect.Y + vRect.Height);
                CvInvoke.cvRectangle(Bgr_Image, ptX, ptY, new MCvScalar(255, 0, 0), 2, Emgu.CV.CvEnum.LINE_TYPE.EIGHT_CONNECTED, 1);
                //i++;
                //String cKeyID = StringEx.getString(i + 1000);
                //Image<Bgr, Byte> vResult = Bgr_Image.GetSubRect(vRect);
                //String cFileName2 = cREC_ID + "_" + cKeyID.ToString();
                //vResult.Save(cExportDir + "\\" + cKeyID + ".jpg"); 
            }
            ActiveBox.Image = Bgr_Image;
        }

        private void frmImage_Load(object sender, EventArgs e)
        {
            ActiveBox = new ImageBox();
            ActiveBox.Parent = this;
            ActiveBox.Left = 10;
            ActiveBox.Top = 10;
            ActiveBox.Height = this.Height - 60;
            ActiveBox.Width = this.Width - 160;
            ActiveBox.BorderStyle = BorderStyle.FixedSingle;
            ActiveBox.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            ActiveBox.Load(cFileName);
        }
    }
}

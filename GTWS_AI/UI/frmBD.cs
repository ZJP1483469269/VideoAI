using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GTWS_TASK.UI;
using OpenCvSharp;
using TLKJ.Utils;

namespace GTWS_BD
{
    public partial class frmBD : frmBase
    {
        public int ImageCount = 0;
        public OpenCvSharp.Size ImageSize;
        public OpenCvSharp.Size BoardSize;
        public String[] FileList = null;
        public Point2f[][] CPointList;
        public Point3f[][] GPointList;
        public frmBD()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog vDialog = new OpenFileDialog();
            vDialog.Multiselect = true;
            if (vDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileList = vDialog.FileNames;
            }

            if (FileList.Length > 0)
            {
                String cFileName = FileList[0];
                Cv2.ImRead(cFileName);
                this.pictureBox1.Load(cFileName);
            }
        }

        private void Read_Corners_Click(object sender, EventArgs e)
        {
            BoardSize = new OpenCvSharp.Size(Convert.ToInt16(Corners_Nx.Text), Convert.ToInt16(Corners_Ny.Text));
            WriteMessage("开始读取交点：" + Corners_Nx.Text + "," + Corners_Ny.Text + ",共：" + FileList.Length.ToString() + "个文件");
            for (int i = 0; i < FileList.Length; i++)
            {
                String cFileName = FileList[i];
                WriteMessage("开始读取第(" + (i + 1) + ")个文件:" + cFileName);
                Mat vImage = Cv2.ImRead(cFileName, ImreadModes.AnyColor);
                Mat vGrayImage = Cv2.ImRead(cFileName, ImreadModes.Grayscale);

                if (i == 0)
                {
                    ImageSize = new OpenCvSharp.Size(vImage.Cols, vImage.Rows);
                    int iImageCount = FileList.Length;
                    int iPointCount = BoardSize.Width * BoardSize.Height;
                    CPointList = new Point2f[iImageCount][];
                    GPointList = new Point3f[iImageCount][];
                }

                Boolean isOK = false;

                Point2f[] ptList = new Point2f[BoardSize.Width * BoardSize.Height];
                if (!Cv2.FindChessboardCorners(vImage, BoardSize, out ptList))
                {
                    WriteMessage("第(" + (i + 1) + ")个文件：FindChessboardCorners..转换失败！");
                    continue;
                }
                else
                {
                    WriteMessage("第(" + (i + 1) + ")个文件,FindChessboardCorners成功");
                }
                Cv2.DrawChessboardCorners(vImage, BoardSize, ptList, true);

                Point2f[] ptSubPixList = new Point2f[BoardSize.Width * BoardSize.Height];

                try
                {
                    ptSubPixList = Cv2.CornerSubPix(vGrayImage, ptList, new OpenCvSharp.Size(ImageSize.Width / 2 - 5, ImageSize.Height / 2 - 5), new OpenCvSharp.Size(-1, -1), new TermCriteria(CriteriaType.MaxIter, 10, 0.1));
                    if (ptSubPixList.Length != BoardSize.Width * BoardSize.Height)
                    {
                        continue;
                    }
                    else
                    {
                        isOK = true;
                    }
                }
                catch (Exception ex)
                {
                    WriteMessage(ex.Message);
                }
                if (isOK)
                {
                    WriteMessage("第(" + (i + 1) + ")个文件,CornerSubPix成功");
                }
                else
                {
                    WriteMessage("第(" + (i + 1) + ")个文件：CornerSubPix..转换失败！");
                }
                CPointList[i] = ptSubPixList;

                if (i == FileList.Length - 1)
                {
                    Cv2.DrawChessboardCorners(vImage, BoardSize, ptSubPixList, true);
                    vImage.SaveImage("LS.jpg");
                    this.pictureBox1.Load("LS.jpg");
                }
            }
            WriteMessage("交点读取完成！");
        }
        public void WriteMessage(String cMessage)
        {
            log4net.WriteTextLog(cMessage);
            lstLogs.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + cMessage);
            Application.DoEvents();
        }
        private void btnCalibrate_Click(object sender, EventArgs e)
        {
            WriteMessage("开始摄像机标定！");
            OpenCvSharp.Size SquareSize = new OpenCvSharp.Size(90, 60);

            double[,] cameraMatrix = new double[3, 3];
            double[] distCoeffs = new double[5];

            Vec3d[] rvecs = new Vec3d[100];
            Vec3d[] tvecs = new Vec3d[100];

            for (int t = 0; t < ImageCount; t++)
            {
                Point3f[] GList = new Point3f[BoardSize.Width * BoardSize.Height];
                for (int i = 0; i < BoardSize.Height; i++)
                {
                    for (int j = 0; j < BoardSize.Width; j++)
                    {
                        Point3f realPoint = new Point3f();
                        realPoint.X = i * SquareSize.Width;     //行
                        realPoint.Y = j * SquareSize.Height;    //列
                        realPoint.Z = 0;
                        GList[BoardSize.Height * i + j] = realPoint;
                    }
                }
                GPointList[t] = GList;
            }

            Cv2.CalibrateCamera(GPointList, CPointList, BoardSize, cameraMatrix, distCoeffs, out rvecs, out tvecs, CalibrationFlags.None, new TermCriteria(CriteriaType.MaxIter, 10, 0.1));


            WriteMessage("定标完成！");
            WriteMessage("标定结果显示");
            WriteMessage("相机内参：intrinsic_matrix");
            for (int h = 0; h < 3; h++)
            {
                WriteMessage("intrinsic_matrix:X:" + cameraMatrix[h, 0] + ",Y:" + cameraMatrix[h, 1] + ",Z:" + cameraMatrix[h, 2]);
            }

            WriteMessage("畸变系数：distortion_coeffs");
            for (int ndis = 0; ndis < 4; ndis++)
            {
                WriteMessage("畸变系数:" + ndis + ":" + distCoeffs[ndis].ToString());
            }

            WriteMessage("rotation_vectors");
            for (int i = 0; i < rvecs.Length; i++)
            {
                WriteMessage("X:" + rvecs[i].Item0 + ",Y:" + rvecs[i].Item1 + ",Z:" + rvecs[i].Item2);
            }

            WriteMessage("translation_vectors");
            for (int i = 0; i < tvecs.Length; i++)
            {
                WriteMessage("X:" + tvecs[i].Item0 + ",Y:" + tvecs[i].Item1 + ",Z:" + tvecs[i].Item2);
            }

            WriteMessage("开始评价定标结果………………");
            double total_err = 0.0;
            double err = 0.0;

            //Matrix<double> image_points2(1,point_counts(0,0,0),2);
            //int temp_num  = point_counts(0,0,0); 
            //cout<<"\t每幅图像的定标误差：\n";
            //fout<<"每幅图像的定标误差：\n";

            for (int i = 0; i < ImageCount; i++)
            {
                Point2f[] imagePoints = new Point2f[100];
                double[,] jacobian = new double[1, 1];
                //Cv2.ProjectPoints(GPointList[i], rvecs.ToArray(), tvecs.ToArray(), cameraMatrix, distCoeffs, out imagePoints, out jacobian, 0);
                //Cv2.ProjectPoints(GPointList.g.get_cols(i * point_counts(0, 0, 0), (i + 1) * point_counts(0, 0, 0) - 1).cvmat,
                //  rotation_vectors.get_col(i).cvmat,
                //  translation_vectors.get_col(i).cvmat,
                //  intrinsic_matrix.cvmat,
                //  distortion_coeffs.cvmat,
                // image_points2.cvmat,
                //  0, 0, 0, 0); 

                //err = cvNorm(image_points.get_cols(i*point_counts(0,0,0),(i+1)*point_counts(0,0,0)-1).cvmat,
                //   image_points2.cvmat,
                //   CV_L1);
                //total_err += err/=point_counts(0,0,0);

                //cout<<"******************************************************************\n";
                //cout<<"\t\t第"<<i+1<<"幅图像的平均误差："<<err<<"像素"<<'\n';
                //fout<<"\t第"<<i+1<<"幅图像的平均误差："<<err<<"像素"<<'\n';

                //cout<<"显示image_point2\n";
                //for(int ih=0;ih<7;ih++)
                //{
                // cout<<"X:"<<image_points2(0,ih,0)<<"\tY:"<<image_points2(0,ih,1)<<"\n";
                //}
                //cout<<"显示object_Points\n";
                //for(int iw=0;iw<7;iw++)
                //{
                // cout<<"X:"<<image_points.get_cols(i*point_counts(0,0,0),(i+1)*point_counts(0,0,0)-1)(0,iw,0)
                //  <<"\tY:"<<image_points.get_cols(i*point_counts(0,0,0),(i+1)*point_counts(0,0,0)-1)(0,iw,1)<<"\n";
                //}

            }
        }

        private void frmBD_Load(object sender, EventArgs e)
        {

        }
    }
}

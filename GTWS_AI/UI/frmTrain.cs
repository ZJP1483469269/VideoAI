using Emgu.CV;
using Emgu.CV.ML;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GTWS_AI.UI
{
    public partial class frmTrain : Form
    {
        public frmTrain()
        {
            InitializeComponent();
        }
        String[] fsPeg = null;
        String[] fsNeg = null;
        Matrix<float> DataMatrix = new Matrix<float>(967, 900);
        Matrix<int> AttrMatrix = new Matrix<int>(967, 1);
        private void frmMain_Load(object sender, EventArgs e)
        {
            this.txtPosPath.Text = Application.StartupPath + @"\POS\Model\";
            this.txtNegPath.Text = Application.StartupPath + @"\NEG\Model\";
            this.txtFileName.Text = Application.StartupPath + @"\Model.cfg";
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            HOGDescriptor hog = new HOGDescriptor(new Size(36, 36), new Size(36, 36), new Size(6, 6), new Size(6, 6));
            fsPeg = Directory.GetFiles(txtPosPath.Text);
            for (int i = 0; i < fsPeg.Length; i++)
            {
                String cFileName = txtPosPath.Text + fsPeg[i];
                Image<Bgr, byte> vImage = new Image<Bgr, byte>(cFileName);
                Image<Gray, byte> vGray = vImage.Convert<Gray, byte>();
                float[] fAttr = hog.Compute(vGray);
                for (int j = 0; j < fAttr.Length; j++)
                {
                    DataMatrix[i, j] = fAttr[j];
                }
                AttrMatrix[i, 0] = 1;
            }

            fsNeg = Directory.GetFiles(txtNegPath.Text);
            for (int i = 0; i < fsNeg.Length; i++)
            {
                String cFileName = txtNegPath.Text + fsNeg[i];
                Image<Bgr, byte> vImage = new Image<Bgr, byte>(cFileName);
                Image<Gray, byte> vGray = vImage.Convert<Gray, byte>();
                float[] fAttr = hog.Compute(vGray);
                for (int j = 0; j < fAttr.Length; j++)
                {
                    DataMatrix[i, j] = fAttr[j];
                }
                AttrMatrix[i, 0] = 0;
            }

            Emgu.CV.ML.SVM vSVM = new Emgu.CV.ML.SVM();
            vSVM.Type = Emgu.CV.ML.SVM.SvmType.CSvc;
            vSVM.SetKernel(Emgu.CV.ML.SVM.SvmKernelType.Linear);
            vSVM.TermCriteria = new MCvTermCriteria(1000, 0.1);
            TrainData td = new TrainData(DataMatrix, Emgu.CV.ML.MlEnum.DataLayoutType.RowSample, AttrMatrix);
            String cExportFileName = txtFileName.Text;
            vSVM.Save(cExportFileName);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            MyRect[] regions = null;
            Emgu.CV.ML.SVM vSVM = new Emgu.CV.ML.SVM();
            String cExportFileName = txtFileName.Text;
            FileStorage fileStorage = new FileStorage(cExportFileName, FileStorage.Mode.Read);
            vSVM.Read(fileStorage.GetFirstTopLevelNode());

            int iHeight = vSVM.GetSupportVectors().Height;
            int iWidth = vSVM.GetSupportVectors().Width;
            var svmMat = new Matrix<float>(iWidth, iHeight);

            Matrix<float> resultMat = new Matrix<float>(1, iWidth);
            Matrix<float> alphaMat = new Matrix<float>(1, iHeight);

            float[] mydetector = new float[iWidth + 1];
            for (int i = 0; i < iWidth; i++)
            {
                mydetector[i] = resultMat[0, i];
            }
            //mydetector[iWidth] = rhoValue;

            Mat vImage = new Mat();
            HOGDescriptor hog = new HOGDescriptor(new Size(36, 36), new Size(36, 36), new Size(6, 6), new Size(6, 6));
            hog.SetSVMDetector(mydetector);
            MCvObjectDetection[] results = hog.DetectMultiScale(vImage);
            regions = new MyRect[results.Length];
            for (int i = 0; i < results.Length; i++)
            {
                regions[i] = new MyRect();
                regions[i].Rect = results[i].Rect;
                regions[i].Score = results[i].Score;
            }

        }
    }
}

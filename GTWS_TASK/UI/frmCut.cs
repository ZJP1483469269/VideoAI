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
using System.IO;
using TLKJ_IVS;
using TLKJAI;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;

namespace GTWS_BD
{
    public partial class frmCut : Form
    {
        public int ImageCount = 0;
        public frmCut()
        {
            InitializeComponent();
        }
        Image<Bgr, byte> DefaultMat = null;
        Image<Gray, byte> GrayMat = null;
        Image<Gray, byte> BinaryMat = null;
        String cFileName = @".\Model\IMG05.jpg";
        private void btnLoad_Click(object sender, EventArgs e)
        {
            DefaultMat = new Image<Bgr, byte>(cFileName);
            pictureBox1.Image = DefaultMat.ToBitmap();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void IMG_MIN_ValueChanged(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }

        private void btnGray_Click(object sender, EventArgs e)
        {
            GrayMat = DefaultMat.Convert<Gray, Byte>();
            pictureBox1.Image = GrayMat.ToBitmap();
        }

        private void btnBinary_Click(object sender, EventArgs e)
        {
            List<KeyValue> LS = IMGAI.getImageList(cFileName);
        }
    }
}

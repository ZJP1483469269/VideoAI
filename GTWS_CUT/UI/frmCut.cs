using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TLKJ.Utils;
using TLKJ_IVS;
using TLKJAI;

namespace GTWS_TASK.UI
{
    public partial class frmCut : Form
    {
        Sunisoft.IrisSkin.SkinEngine iskin = new Sunisoft.IrisSkin.SkinEngine();
        Thread UploadThread = null;
       
        public frmCut()
        {
            InitializeComponent();
        }

        private void frmCut_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitUploadThread();
        }

        public void InitUploadThread()
        {
            int iANALYSE = StringEx.getInt(INIConfig.ReadString("ANALYSE", "DFS_ALLOW", "0"));
            if (iANALYSE > 0)
            {
                UploadThread = new Thread(CutTask.Execute);
                UploadThread.Start();
            }
        } 

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

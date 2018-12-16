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
        Thread UploadThread = null;
        static Boolean isAbort = false;
        public void InitUploadThread()
        {
            int iALARM = StringEx.getInt(INIConfig.ReadString("ALARM", "DFS_ALLOW", "0"));
            int iANALYSE = StringEx.getInt(INIConfig.ReadString("ANALYSE", "DFS_ALLOW", "0"));
            if (iALARM > 0 || iANALYSE > 0)
            {
                UploadThread = new Thread(Upload_Execute);
                UploadThread.Start();
            }
        }

        public void Upload_Execute()
        {
            CutTask.Execute();
        }
    }
}

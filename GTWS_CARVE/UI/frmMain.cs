using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TLKJ.Utils;
using TLKJ_IVS;

namespace GTWS_CARVE.UI
{
    public partial class frmMain : Form
    {
        Sunisoft.IrisSkin.SkinEngine iskin = new Sunisoft.IrisSkin.SkinEngine();
        public frmMain()
        {
            InitializeComponent();
        }

        private void timAuto_Tick(object sender, EventArgs e)
        {
            if (ApplicationEvent.UploadThread == null)
            {
                ApplicationEvent.UploadThread = new Thread(UploadTask.Execute);
                ApplicationEvent.UploadThread.Start();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            iskin.SkinFile = "skins/PageColor2.ssk";
        }
    }
}

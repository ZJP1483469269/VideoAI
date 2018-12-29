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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ApplicationEvent.UploadThread != null)
            {
                try
                {
                    ApplicationEvent.isUploadAbort = true;
                    ApplicationEvent.UploadThread.Abort();
                    try
                    {

                        Thread.Sleep(1000);
                    }
                    catch (Exception ex)
                    {

                    }
                    ApplicationEvent.UploadThread = null;
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void timCheck_Tick(object sender, EventArgs e)
        {
            try
            {
                String cFileName = Application.StartupPath + "\\GTWS_TASK.exe";
                if (File.Exists(cFileName))
                {
                    System.Diagnostics.Process.Start(cFileName);
                }
            }
            catch (Exception ex)
            {


            }
            LB_MSG.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void btnMonitor_Click(object sender, EventArgs e)
        {
            timCheck.Enabled = false;
        }
    }
}

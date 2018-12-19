using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using TLKJ_IVS;
using System.Threading;

namespace GTWS_CUT
{
    partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以启动服务。
        }

        protected override void OnStop()
        {
            if (ApplicationEvent.UploadThread != null)
            {
                ApplicationEvent.isImgCutAbort = true;
                ApplicationEvent.isUploadAbort = true;
                try
                {

                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {

                }
                try
                {
                    ApplicationEvent.UploadThread.Abort();
                    ApplicationEvent.UploadThread = null;
                }
                catch (Exception ex)
                {

                }

            }

            if (ApplicationEvent.CutThread != null)
            {
                try
                {
                    ApplicationEvent.CutThread.Abort();
                    ApplicationEvent.CutThread = null;
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}

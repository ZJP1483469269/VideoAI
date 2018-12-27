using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace GTWS_TASK.UI
{
    partial class GTWS_CARVE : ServiceBase
    {
        public GTWS_CARVE()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以启动服务。
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
        }

        private void timAuto_Tick(object sender, EventArgs e)
        {

        }
    }
}

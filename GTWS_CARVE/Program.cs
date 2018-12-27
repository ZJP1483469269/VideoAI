using GTWS_CARVE.UI;
using GTWS_TASK.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Windows.Forms;
using TLKJ.Utils;

namespace GTWS_TASK
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {  
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new GTWS_TASK.UI.GTWS_CARVE() 
            //};
            //ServiceBase.Run(ServicesToRun);

            INIConfig.setConfigFile(Application.StartupPath + @"\Config.ini");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}

using Gecko;
using GTWS_TASK.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TLKJ.Utils;

namespace GTWS_AI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            string xulrunnerPath = Application.StartupPath + "/xulrunner";
            INIConfig.setConfigFile(Application.StartupPath + @"\Config.ini");
            Xpcom.Initialize(xulrunnerPath);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}

using GTWS_TASK.UI;
using System;
using System.Collections.Generic;
using System.Linq;
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
            INIConfig.setConfigFile(Application.StartupPath + @"\Config.ini");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmTask());
        }
    }
}

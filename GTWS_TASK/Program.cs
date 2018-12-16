
using GTWS_TASK.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TLKJ.Utils;

namespace GTWS_BD
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            INIConfig.setConfigFile(Application.StartupPath + @"\Config.ini");
            Application.Run(new frmTake());
        }
    }
}

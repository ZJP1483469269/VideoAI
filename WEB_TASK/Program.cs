
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TLKJ.Utils;

namespace WEB_TASK
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
            Application.Run(new frmCMain());
        }
    }
}

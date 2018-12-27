using GTWS_TASK.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TLKJ.Utils;

namespace GTWS_TASK
{
    static class Program
    {
        static System.Threading.Mutex _mutex;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        { //是否可以打开新进程
            bool createNew;

            //获取程序集Guid作为唯一标识
            Attribute guid_attr = Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(GuidAttribute));
            string guid = ((GuidAttribute)guid_attr).Value;
            _mutex = new System.Threading.Mutex(true, guid, out createNew);
            if (false == createNew)
            {
                Application.Exit();
            }
            else
            {
                INIConfig.setConfigFile(Application.StartupPath + @"\Config.ini");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmTask());
            }
            _mutex.ReleaseMutex();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;

namespace TLKJ_IVS
{
    public class ApplicationEvent
    {
        public static string Token;
         
        public static Boolean isUploadAbort = false;
        public static Thread UploadThread = null; 

        public static void setUserCode(String cUserCode)
        {

        }

        public static int iSession = 0;

        public static IntPtr ReplayHandle;

        public static ulong ulPlatDownHandle = 0;
        public static ulong ulPUDownHandle = 0;
        public static ulong ulReplayHandle = 0;

        public static string APP_VER = "V2017.12";
    }
}

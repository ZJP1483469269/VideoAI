using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace GTWS
{
   [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
   public  struct IVS_LOGIN_INFO
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string cUserName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string pPWD;

        [MarshalAs(UnmanagedType.Struct)]
        public IVS_IP stIP;

        public System.UInt32 uiPort;

        public System.UInt32 uiLoginType;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cDomainName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string cMachineName;

        public System.UInt32 uiClientType;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cReserve;
    };
}

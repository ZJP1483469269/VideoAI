using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace GTWS
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
   public  struct IVS_IP
    {
        public System.UInt32 uiIPType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cIP;
    };
}

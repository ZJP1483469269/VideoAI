using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace GTWS
{
   [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_DEVICE_GROUP
    {
       [MarshalAs(UnmanagedType.ByValArray,SizeConst=Const.IVS_DEVICE_GROUP_LEN)]
       public char[] cGroupCode;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = Const.IVS_NAME_LEN)]
       public char[] cGroupName;

      [MarshalAs(UnmanagedType.ByValArray, SizeConst = Const.IVS_DEVICE_GROUP_LEN)]
      public char[] cParentGroupCode;

      [MarshalAs(UnmanagedType.ByValArray, SizeConst = Const.IVS_DOMAIN_CODE_LEN)]
      public char[] cDomainCode;

      public bool bIsExDomin;

     [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
      public char[] cReserve;
    }
}

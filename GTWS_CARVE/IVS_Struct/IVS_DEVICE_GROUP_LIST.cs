using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace GTWS
{
     [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_DEVICE_GROUP_LIST
    {
       public uint uiTotal;

       public  IVS_INDEX_RANGE stIndexRange;

      [MarshalAs(UnmanagedType.ByValArray)]
       public IVS_DEVICE_GROUP[] stDeviceGroup;
    }
}

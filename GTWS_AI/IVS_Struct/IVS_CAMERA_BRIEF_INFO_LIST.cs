using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using TLKJ_IVS;

namespace GTWS
{
     [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_CAMERA_BRIEF_INFO_LIST
    {
         public UInt32 uiTotal;

         public IVS_INDEX_RANGE stIndexRange;

         [MarshalAs(UnmanagedType.ByValArray)]
         public IVS_CAMERA_BRIEF_INFO[] stCamerBriefInfo;
    }
}

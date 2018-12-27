using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace GTWS
{
    // 实况参数
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_CAMERA_OSD_CFG
    {
        public bool bEnableOSD; // 是否启用：0-停用 1-启用
        public IVS_OSD_TIME stOSDTime;  // OSD时间信息
        public IVS_OSD_NAME stOSDName;  // OSD文字信息
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cReserve;
    };
}

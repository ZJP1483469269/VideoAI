using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace GTWS
{
    // 实况参数
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)] 
    public struct IVS_OSD_TIME
    {
        public bool bEnableOSDTime;     // 是否显示时间：0-不显示，1-显示
        public uint uiTimeFormat;       // 时间显示格式：？1: XXXX-XX-XX XX:XX:XX(如2009-09-09 09:09:09), 2: XXXX年XX月XX日 XX :XX :XX(2009年09月09日 09 :09 :09；3: UTC时间
        public float fTimeX;             // 时间X坐标，以左上角为原点
        public float fTimeY;             // 时间Y坐标，以左上角为原点
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cReserve;
    };
}

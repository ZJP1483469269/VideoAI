using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace GTWS
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_PTZ_PRESET
    {
        public uint uiPresetIndex;   // 预置位索引
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 84)]
        public string cPresetName;   // 预置位名称，1~20个字符。
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cReserve;
    };
}

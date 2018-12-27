using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace GTWS
{
    // 实况参数
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_REALPLAY_PARAM
    {
        public uint uiStreamType;       // 码流类型，值参考 IVS_STREAM_TYPE
        public uint uiProtocolType;     // 协议类型，1-UDP 2-TCP，默认为1
        public bool bDirectFirst;       // 是否直连优先，0-否 1-是，默认为0
        public bool bMultiCast;         // 是否组播，0-单播，1-组播，默认为0
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cReserve;
    };
}

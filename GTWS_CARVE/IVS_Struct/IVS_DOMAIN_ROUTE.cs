using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using TLKJ_IVS;

namespace GTWS
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_DOMAIN_ROUTE
    {
        public UInt32 uiDomainType;                         // SMU工作模式
        public bool bIsAgent;                             // 是否代理
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public char[] cDomainCode;     // 域编码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string cDomainName;            // 域名称
        public IVS_IP stIP;                                  // 域IP
        public uint uiPort;                               // 域端口
        public uint uiStatus;			                // 设备状态：0-离线，1-在线，2-休眠 参考 IVS_DEV_STATUS
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public char[] cSuperDomain;    // 上级域编码
        public bool bIsLocalDomain;      // 是否本域 0-不是 1-是
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cReserve;

    }
}

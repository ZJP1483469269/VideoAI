using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace TLKJ_IVS
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_CAMERA_BRIEF_INFO
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cCode;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 192)]
        public string cName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string cDevGroupCode;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cParentCode;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cDomainCode;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cDevModelType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cVendorType;

        public uint uiDevFormType;                              // 主设备类型：参考 IVS_MAIN_DEVICE_TYPE

        public uint uiType;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string cCameraLocation;
        public uint uiCameraStatus;		                // 摄像机扩展状态：1 – 正常	2 – 视频丢失

        public uint uiStatus;			                // 设备状态：0-离线，1-在线，2-休眠 参考 IVS_DEV_STATUS

        public uint uiNetType;                          // 网络类型 0-有线  1-无线, 参考 IVS_NET_TYPE
        public bool bSupportIntelligent;                // 是否支持智能分析  0-不支持 1-支持

        public bool bEnableVoice;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cNvrCode;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string cDevCreateTime;

        public bool bIsExDomain;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cDevIp;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cReserve;
    }
}

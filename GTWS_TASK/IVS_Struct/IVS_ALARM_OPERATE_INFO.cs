using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace GTWS
{
    // 实况参数
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)] 
    public struct IVS_ALARM_OPERATE_INFO
    {
        public uint uiOperatorID;	                // 处理人ID 参考 IVS_USER_INFO
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string cOperatorName;	// 处理人名
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string cOperateTime;		// 告警处理时间
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cReserver;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
        public string cOperateInfo;	// 告警处理人员输入的信息
    };
}

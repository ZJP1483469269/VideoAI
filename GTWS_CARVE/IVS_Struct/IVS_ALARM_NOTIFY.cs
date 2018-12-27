using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace GTWS
{
    // 实况参数
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_ALARM_NOTIFY
    {
        public UInt64 ullAlarmEventID;                      // 告警事件ID
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cAlarmInCode;	                        // 告警源编码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cDevDomainCode;                       // 设备所属域编码
        public UInt32 uiAlarmInType;					    // 告警源类型 IVS_ALARM_IN_TYPE
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string cAlarmInName;                         // 告警源名称

        public UInt32 uiAlarmLevelValue;		            // 告警级别权值,1~100
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string cAlarmLevelName;	                    // 告警级别名称,汉字和字母（a-z和A-Z），数字，中划线和下划线，1~20个字符
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string cAlarmLevelColor;			            // 告警级别颜色,使用颜色字符串表示ARGB,例如#FFFF0000 表示红色，不透明
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cAlarmType;	                        // 告警类型
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string cAlarmTypeName;	                    // 告警类型名称，汉字和字母（a-z和A-Z），数字，中划线和下划线，1~64个字符
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string cAlarmCategory;				        // 告警类型类别
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string cOccurTime;                           // 告警发生时间：yyyyMMddHHmmss
        public UInt32 uiOccurNumber;		                // 告警发生次数
        public UInt32 uiAlarmStatus;		                // 告警状态 参考 IVS_ALARM_STATUS

        public bool bIsCommission;	                        // 是否为授权告警信息：0-否，1-是
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string cPreviewUrl;	                        // 在存在联动抓拍或者智能分析时的图片预览URL

        public bool bExistsRecord;	                        // 是否存在告警录像：0-否，1-是
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cNvrCode;                             // NVR编码，可用于更新NVR路由
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cReserver;                            // 保留字段
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
        public string cAlarmDesc;	                        // 告警描述信息，键盘可见字符和中文，0~256字符。
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
        public string cExtParam;                            // 扩展参数
    };
}

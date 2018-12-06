using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace GTWS
{
    // 实况参数
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_LOCAL_RECORD_PARAM
    {
        public uint uiRecordFormat;                 // 录像文件格式，参考 IVS_RECORD_FILE_TYPE
        public uint uiSplitterType;                 // 录像分割方式，参考 IVS_RECORD_SPLITE_TYPE
        public uint uiSplitterValue;                // 录像分割值，文件分割方式是时间时，填入时间，对应单位为分钟，5-60分钟，同时满足文件大小不超过2048MB的限制，文件分割方式是容量时，填入容量，对应单位为M，10-2048MB

        public uint uiDiskWarningValue;             // 本地录像，磁盘空间小于此值告警，单位M（进行“本地录像通用事件上报”2-本地录像告警）
        public uint uiStopRecordValue;              // 本地录像，磁盘空间小于此值停止录像，单位M（进行“本地录像通用事件上报”3-本地录像磁盘满停止）

        public uint uiRecordTime;                   // 录像时长，单位秒，整数，300 ~ 43200（12小时）

        public bool bEncryptRecord;    // 录像是否加密
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string cRecordPWD;   // 本地录像密码

        public uint uiNameRule;                     // 录像文件命名规则，参考 IVS_RECORD_NAME_RULE
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string cSavePath;   // 本地录像存放路径，加上文件名长度不超过256字节
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cReserve;                   // 保留字段
    };
}

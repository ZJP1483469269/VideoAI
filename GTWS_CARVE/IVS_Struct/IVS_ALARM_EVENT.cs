using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace GTWS
{
    // 实况参数
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)] 
    public struct IVS_ALARM_EVENT
    {
        public IVS_ALARM_NOTIFY stAlarmNotify;      // 告警信息
        public IVS_ALARM_OPERATE_INFO stOperateInfo;	    // 告警处理信息
    };
}

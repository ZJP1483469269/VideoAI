using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace GTWS
{
    // 实况参数
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_OSD_NAME
    {
        public bool bEnableOSDName;                     // 是否显示文字：0-不显示，1-显示
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string cOSDNameText; // 文字内容
        public IVS_RECT_FLOAT rectText;                     // 文字区域

        public bool bEnableSwitch;		                // 是否交替显示：0-不交替，1-交替
        public uint uiSwitchInterval;                   // 交替显示时间间隔，单位为秒

        public bool bEnableTextBlink;		            // 是否允许闪烁：0-不闪烁，1-闪烁
        public bool bEnableTextTranslucent;	            // 是否允许透明：0-不透明，1-透明
        public uint uiTextTranslucentPercent;           // 文字透明度：0-100
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cReserve;
    };
}

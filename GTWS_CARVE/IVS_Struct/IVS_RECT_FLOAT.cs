using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace GTWS
{
    // 实况参数
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)] 
    public struct IVS_RECT_FLOAT
    {
        public float left;
        public float top;
        public float right;
        public float bottom;
    };
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace TLKJ.Utils
{
    public class UrlMessage
    {
        public String Code;
        public Double X;
        public Double Y;
        public String DayTime;
        public UrlMessage()
        {
            DayTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}

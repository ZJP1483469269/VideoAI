using System;
using System.Collections.Generic;
using System.Text;

namespace TLKJ.Utils
{
    public class JHref
    {
        public int Layer;
        public string UrlID;
        public string Prefix;
        public string Text;
        public string Url;

        override
        public string ToString()
        {
            return Text;
        }
    }
}

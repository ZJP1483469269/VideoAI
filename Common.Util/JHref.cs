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
        public string UrlName;
        public string Match;
        public string Url;
        public string PageVal; 

        override
        public string ToString()
        {
            return UrlName;
        }
    }
}

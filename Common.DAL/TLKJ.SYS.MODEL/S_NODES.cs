using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class S_NODES
    {
        public String _ID; 
        [FieldAttr(isKey = true, FieldDesc = "类型序号", DBLength = 30)]
        public String id
        {
            get { return _ID; }
            set { _ID = value; }
        }
         
        private String _Url;
        [FieldAttr(isKey = true, FieldDesc = "类型序号", DBLength = 30)]
        public String url
        {
            get { return _Url; }
            set { _Url = value; }
        }

        private String _text;
        [FieldAttr(isKey = true, FieldDesc = "类型序号", DBLength = 30)]
        public String text
        {
            get { return _text; }
            set { _text = value; }
        }
    }
}

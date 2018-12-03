using System;
using System.Collections.Generic;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.Utils
{
    public class FieldAttr : Attribute
    {
        public Boolean isKey = false;
        public Boolean isAuto = false;
        public Boolean isRequire = false;
        public String FieldName = null;
        public String FieldDesc = null;


        public int DBType = ActionField.ftString;
        public int DBLength = 0;

        public String fkTable = null;
        public String fkText = null;
        public String fkValue = null;
        public String fkWhere = null;
    }
}

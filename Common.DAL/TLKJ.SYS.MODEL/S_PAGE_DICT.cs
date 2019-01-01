using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class S_PAGE_DICT
    {
        private String _dict_id;
        [FieldAttr(isKey = true, FieldDesc = "字典编码", DBLength = 60)]
        public String dict_id
        {
            get { return _dict_id; }
            set { _dict_id = value; }
        }
        private String _dict_name;
        [FieldAttr(FieldDesc = "字典名称", DBLength = 200)]
        public String dict_name
        {
            get { return _dict_name; }
            set { _dict_name = value; }
        }

        private String _sql_text;
        [FieldAttr(FieldDesc = "SQL语句", DBLength = 200)]
        public String sql_text
        {
            get { return _sql_text; }
            set { _sql_text = value; }
        }

        private String _org_id;
        [FieldAttr(FieldDesc = "单位编码", DBLength = 60)]
        public String org_id
        {
            get { return _org_id; }
            set { _org_id = value; }
        }


        public S_PAGE_DICT()
        {

        }
    }
}

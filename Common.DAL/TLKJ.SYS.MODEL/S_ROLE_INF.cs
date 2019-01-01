using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class S_ROLE_INF
    {
        private String _role_id;
        [FieldAttr(isKey = true, FieldDesc = "角色编码", DBLength = 60)]
        public String role_id
        {
            get { return _role_id; }
            set { _role_id = value; }
        }

        private String _role_name;
        [FieldAttr(FieldDesc = "角色名称", DBLength = 60)]
        public String role_name
        {
            get { return _role_name; }
            set { _role_name = value; }
        }

        private int _orderby;
        [FieldAttr(FieldDesc = "角色排序", DBType = ActionField.ftInteger)]
        public int orderby
        {
            get { return _orderby; }
            set { _orderby = value; }
        }

        private int _org_type;
        [FieldAttr(FieldDesc = "角色类型", DBType = ActionField.ftInteger)]
        public int org_type
        {
            get { return _org_type; }
            set { _org_type = value; }
        }

        private int _isactive;
        [FieldAttr(FieldDesc = "状态", DBType = ActionField.ftBoolean)]
        public int isactive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public S_ROLE_INF()
        {

        }
    }
}

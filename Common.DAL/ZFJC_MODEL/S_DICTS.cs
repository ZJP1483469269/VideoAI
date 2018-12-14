using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class S_DICTS
    {

        private int _type_id;
        [FieldAttr(isKey = true, FieldDesc = "类型序号", DBLength = 30)]
        public int type_id
        {
            get { return _type_id; }
            set { _type_id = value; }
        }

        private String _type_name;
        [FieldAttr(FieldDesc = "类型名称", DBLength = 30)]
        public String type_name
        {
            get { return _type_name; }
            set { _type_name = value; }
        }

        private String _cls_id;
        [FieldAttr(FieldDesc = "字典类型", DBLength = 30)]
        public String cls_id
        {
            get { return _cls_id; }
            set { _cls_id = value; }
        }

        private int _isactive;
        [FieldAttr(FieldDesc = "是否有效", DBType = ActionField.ftBoolean)]
        public int isactive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }

        private int _orderby;
        [FieldAttr(FieldDesc = "排序", DBType = ActionField.ftInteger)]
        public int orderby
        {
            get { return _orderby; }
            set { _orderby = value; }
        }

        public S_DICTS()
        {

        }
    }
}

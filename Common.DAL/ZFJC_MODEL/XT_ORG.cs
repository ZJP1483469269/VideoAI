using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class XT_ORG
    {
        private int _org_id;
        [FieldAttr(isKey = true, FieldDesc = "用户编码", DBLength = 30)]
        public int org_id
        {
            get { return _org_id; }
            set { _org_id = value; }
        }

        private String _org_name;
        [FieldAttr(FieldDesc = "用户姓名", DBLength = 60)]
        public String org_name
        {
            get { return _org_name; }
            set { _org_name = value; }
        }

        private String _org_full_name;
        [FieldAttr(FieldDesc = "密码", DBLength = 60)]
        public String org_full_name
        {
            get { return _org_full_name; }
            set { _org_full_name = value; }
        }

        private Double _x;
        [FieldAttr(FieldDesc = "所属单位", DBLength = 20)]
        public Double x
        {
            get { return _x; }
            set { _x = value; }
        }

        private Double _y;
        [FieldAttr(FieldDesc = "角色")]
        public Double y
        {
            get { return _y; }
            set { _y = value; }
        }
        private int _isactive;
        [FieldAttr(FieldDesc = "状态", DBType = ActionField.ftBoolean)]
        public int isactive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        private int _org_type;
        [FieldAttr(FieldDesc = "单位等级", DBType = ActionField.ftInteger)]
        public int org_type
        {
            get { return _org_type; }
            set { _org_type = value; }
        }
        private String _ploygn;
        [FieldAttr(FieldDesc = "行政界限", DBLength = int.MaxValue)]
        public String ploygn
        {
            get { return _ploygn; }
            set { _ploygn = value; }
        }

        public XT_ORG()
        {

        }
    }
}

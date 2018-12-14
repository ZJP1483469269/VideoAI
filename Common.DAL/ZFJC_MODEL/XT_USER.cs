using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class XT_USER
    {
        private String _user_id;
        [FieldAttr(isKey = true, FieldDesc = "用户编码", DBLength = 60)]
        public String user_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }

        private int _org_id;
        [FieldAttr(FieldDesc = "单位编码", DBLength = 30)]
        public int org_id
        {
            get { return _org_id; }
            set { _org_id = value; }
        }

        private String _usercode;
        [FieldAttr(FieldDesc = "登录账号", DBLength = 60)]
        public String usercode
        {
            get { return _usercode; }
            set { _usercode = value; }
        }

        private String _username;
        [FieldAttr(FieldDesc = "用户姓名", DBLength = 60)]
        public String username
        {
            get { return _username; }
            set { _username = value; }
        }

        private String _phone_telno;
        [FieldAttr(FieldDesc = "手机号码", DBLength = 12)]
        public String phone_telno
        {
            get { return _phone_telno; }
            set { _phone_telno = value; }
        }

        private String _remark;
        [FieldAttr(FieldDesc = "备注", DBLength = 200)]
        public String remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        private int _isactive;
        [FieldAttr(FieldDesc = "状态", DBType = ActionField.ftBoolean)]
        public int isactive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public XT_USER()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class XT_MOBILE_USER
    {
        private String _user_count;
        [FieldAttr(FieldDesc = "用户账号", DBLength = 11)]
        public String user_count
        {
            get { return _user_count; }
            set { _user_count = value; }
        }
        private String _user_name;
        [FieldAttr(FieldDesc = "用户名称", DBLength = 30)]
        public String user_name
        {
            get { return _user_name; }
            set { _user_name = value; }
        }

        private String _deviceid;
        [FieldAttr(FieldDesc = "类型名称", DBLength = 30)]
        public String deviceid
        {
            get { return _deviceid; }
            set { _deviceid = value; }
        }

        private String _phone_num;
        [FieldAttr(FieldDesc = "手机号码", DBLength = 11)]
        public String phone_num
        {
            get { return _phone_num; }
            set { _phone_num = value; }
        }

        private String _pass_word;
        [FieldAttr(FieldDesc = "密码", DBLength = 36)]
        public String pass_word
        {
            get { return _pass_word; }
            set { _pass_word = value; }
        }

        private String _org_id;
        [FieldAttr(FieldDesc = "单位编码", DBLength = 10)]
        public String org_id
        {
            get { return _org_id; }
            set { _org_id = value; }
        }

        private String _appver;
        [FieldAttr(FieldDesc = "手机号码", DBLength = 30)]
        public String appver
        {
            get { return _appver; }
            set { _appver = value; }
        }
        private int _is_active;
        [FieldAttr(FieldDesc = "是否有效", DBType = ActionField.ftBoolean)]
        public int is_active
        {
            get { return _is_active; }
            set { _is_active = value; }
        }


        public XT_MOBILE_USER()
        {

        }
    }
}

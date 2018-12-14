using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class XT_ORG_WX_CONFIG
    {
        private String _org_id;
        [FieldAttr(isKey = true, FieldDesc = "单位编码", DBLength = 6)]
        public String org_id
        {
            get { return _org_id; }
            set { _org_id = value; }
        }

        private String _wx_id;
        [FieldAttr(FieldDesc = "微信ID", DBLength = 60)]
        public String wx_id
        {
            get { return _wx_id; }
            set { _wx_id = value; }
        }

        private String _wx_appid;
        [FieldAttr(FieldDesc = "开发者ID", DBLength = 60)]
        public String wx_appid
        {
            get { return _wx_appid; }
            set { _wx_appid = value; }
        }

        private String _appsecret;
        [FieldAttr(FieldDesc = "开发者密码", DBLength = 60)]
        public String wx_appsecret
        {
            get { return _appsecret; }
            set { _appsecret = value; }
        }

        private String _token;
        [FieldAttr(FieldDesc = "令牌", DBLength = 60)]
        public String wx_token
        {
            get { return _token; }
            set { _token = value; }
        }
        private String _aeskey;
        [FieldAttr(FieldDesc = "消息加解密密钥", DBLength = 60)]
        public String wx_aeskey
        {
            get { return _aeskey; }
            set { _aeskey = value; }
        }

        private int _isactive;
        [FieldAttr(FieldDesc = "状态", DBType = ActionField.ftBoolean)]
        public int isactive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }

        public XT_ORG_WX_CONFIG()
        {

        }
    }
}

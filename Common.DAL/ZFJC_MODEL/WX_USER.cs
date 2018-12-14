using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class WX_USER
    {
        private String _appid;
        [FieldAttr(isKey = true, FieldDesc = "AppID", DBLength = 30)]
        public String appid
        {
            get { return _appid; }
            set { _appid = value; }
        }
        private String _openid;
        [FieldAttr(FieldDesc = "OPENID", DBLength = 30)]
        public String openid
        {
            get { return _openid; }
            set { _openid = value; }
        }
        private String _subscribe;
        [FieldAttr(FieldDesc = "订阅", DBLength = 30)]
        public String subscribe
        {
            get { return _subscribe; }
            set { _subscribe = value; }
        }

        private String _nickname;
        [FieldAttr(FieldDesc = "类型序号", DBLength = 30)]
        public String nickname
        {
            get { return _nickname; }
            set { _nickname = value; }
        }
        private String _sex;
        [FieldAttr(FieldDesc = "类型序号", DBLength = 30)]
        public String sex
        {
            get { return _sex; }
            set { _sex = value; }
        }
        private String _language;
        [FieldAttr(FieldDesc = "语言", DBLength = 30)]
        public String language
        {
            get { return _language; }
            set { _language = value; }
        }
        private String _city;
        [FieldAttr(FieldDesc = "城市", DBLength = 30)]
        public String city
        {
            get { return _city; }
            set { _city = value; }
        }
        private String _province;
        [FieldAttr(FieldDesc = "省", DBLength = 30)]
        public String province
        {
            get { return _province; }
            set { _province = value; }
        }

        private String _country;
        [FieldAttr(FieldDesc = "国家", DBLength = 30)]
        public String country
        {
            get { return _country; }
            set { _country = value; }
        }
        private String _headimgurl;
        [FieldAttr(FieldDesc = "头像地址", DBLength = 30)]
        public String headimgurl
        {
            get { return _headimgurl; }
            set { _headimgurl = value; }
        }
        private String _subscribe_time;
        [FieldAttr(FieldDesc = "关注事件", DBLength = 30)]
        public String subscribe_time
        {
            get { return _subscribe_time; }
            set { _subscribe_time = value; }
        }
        private String _privilege;
        [FieldAttr(FieldDesc = "类型序号", DBLength = 30)]
        public String privilege
        {
            get { return _privilege; }
            set { _privilege = value; }
        }
        private String _unionid;
        [FieldAttr(FieldDesc = "类型序号", DBLength = 30)]
        public String unionid
        {
            get { return _unionid; }
            set { _unionid = value; }
        }
        private String _blacklist;
        [FieldAttr(FieldDesc = "黑名单", DBLength = 30)]
        public String blacklist
        {
            get { return _blacklist; }
            set { _blacklist = value; }
        }
        private String _lable;
        [FieldAttr(FieldDesc = "类型序号", DBLength = 30)]
        public String lable
        {
            get { return _lable; }
            set { _lable = value; }
        }
    }
}

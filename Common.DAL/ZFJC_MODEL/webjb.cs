using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class webjb
    {
        private String _id;
        [FieldAttr(isKey = true, FieldDesc = "序号", DBLength = 15)]
        public String jb_id
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _xianqu;
        [FieldAttr(FieldDesc = "所属县区", DBLength = 50)]
        public String xianqu
        {
            get { return _xianqu; }
            set { _xianqu = value; }
        }

        private String _danwei;
        [FieldAttr(FieldDesc = "被举报人", DBLength = 30)]
        public String danwei
        {
            get { return _danwei; }
            set { _danwei = value; }
        }
        private String _xmdisc;
        [FieldAttr(FieldDesc = "项目描述", DBLength = 30)]
        public String xmdisc
        {
            get { return _xmdisc; }
            set { _xmdisc = value; }
        }
        private String _people;
        [FieldAttr(FieldDesc = "联系人", DBLength = 30)]
        public String people
        {
            get { return people; }
            set { people = value; }
        }
        private String _phone;
        [FieldAttr(FieldDesc = "联系电话", DBLength = 30)]
        public String  phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        private String _email;
        [FieldAttr(FieldDesc = "邮箱", DBLength = 30)]
        public String email
        {
            get { return _email; }
            set { _email = value; }
        }
        private String _adress;
        [FieldAttr(FieldDesc = "详细地址", DBLength = 30)]
        public String adress
        {
            get { return _adress; }
            set { _adress = value; }
        }
        private String _neirong;
        [FieldAttr(FieldDesc = "内容", DBLength = 30)]
        public String neirong
        {
            get { return _neirong; }
            set { _neirong = value; }
        }
        private String _isSd;
        [FieldAttr(FieldDesc = "审定", DBLength = 5)]
        public String isSd
        {
            get { return _isSd; }
            set { _isSd = value; }
        }
    }
}

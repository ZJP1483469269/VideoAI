using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class XT_JB_SEND
    {
        private String _id;
        [FieldAttr(isKey = true, FieldDesc = "线索编号", DBLength = 36)]
        public String id
        {
            get { return _id; }
            set { _id = value; }
        }
        private String _org_id;
        [FieldAttr(FieldDesc = "org_id", DBLength = 10)]
        public String org_id
        {
            get { return _org_id; }
            set { _org_id = value; }
        }
        private String _user_count;
        [FieldAttr(FieldDesc = "user_count", DBLength = 30)]
        public String user_count
        {
            get { return _user_count; }
            set { _user_count = value; }
        }

        private String _createdatetime;
        [FieldAttr(FieldDesc = "createdatetime", DBLength = 10)]
        public String createdatetime
        {
            get { return _createdatetime; }
            set { _createdatetime = value; }
        }

        private String _remark;
        [FieldAttr(FieldDesc = "备注", DBLength = 600)]
        public String remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class XT_JB_FEEDBACK
    {
        private String _id;
        [FieldAttr(FieldDesc = "ID", DBLength = 26)]
        public String id
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _jb_id;
        [FieldAttr(FieldDesc = "JB_ID", DBLength = 26)]
        public String jb_id
        {
            get { return _jb_id; }
            set { _jb_id = value; }
        }
        private String _day_time;
        [FieldAttr(FieldDesc = "day_time", DBLength = 8)]
        public String day_time
        {
            get { return _day_time; }
            set { _day_time = value; }
        }

        private String _type_name;
        [FieldAttr(FieldDesc = "反馈类型", DBLength = 30)]
        public String type_name
        {
            get { return _type_name; }
            set { _type_name = value; }
        }

        private String _content;
        [FieldAttr(FieldDesc = "反馈内容", DBLength = 30)]
        public String content
        {
            get { return _content; }
            set { _content = value; }
        }
        private String _user_id;
        [FieldAttr(FieldDesc = "反馈人员", DBLength = 12)]
        public String user_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }

    }
}

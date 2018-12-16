using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class XT_NOTICE
    {
        private String _notice_id;
        [FieldAttr(isKey = true, FieldDesc = "通知ID", DBLength = 36)]
        public String notice_id
        {
            get { return _notice_id; }
            set { _notice_id = value; }
        }

        private String _notice_title;
        [FieldAttr(FieldDesc = "通知标题", DBLength = 300)]
        public String notice_title
        {
            get { return _notice_title; }
            set { _notice_title = value; }
        }

        private String _notice_content;
        [FieldAttr(FieldDesc = "通知内容", DBLength = 2000)]
        public String notice_content
        {
            get { return _notice_content; }
            set { _notice_content = value; }
        }

        private String _notice_date;
        [FieldAttr(FieldDesc = "通知时间")]
        public String notice_date
        {
            get { return _notice_date; }
            set { _notice_date = value; }
        }

        private int _isactive;
        [FieldAttr(FieldDesc = "是否激活", DBType = ActionField.ftBoolean)]
        public int isactive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
    }
}

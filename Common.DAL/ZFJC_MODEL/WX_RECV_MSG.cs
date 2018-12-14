using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class WX_RECV_MSG
    {
        private String _id;
        [FieldAttr(isKey = true, FieldDesc = "ID", DBLength = 36)]
        public String id
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _wx_id;
        [FieldAttr(FieldDesc = "WX_ID", DBLength = 6)]
        public String wx_id
        {
            get { return _wx_id; }
            set { _wx_id = value; }
        }

        private String _openid;
        [FieldAttr(FieldDesc = "OPENID", DBLength = 12)]
        public String openid
        {
            get { return _openid; }
            set { _openid = value; }
        }

        private String _msg_type;
        [FieldAttr(FieldDesc = "消息类型", DBLength = 240)]
        public String msg_type
        {
            get { return _msg_type; }
            set { _msg_type = value; }
        }
        private String _msg_text;
        [FieldAttr(FieldDesc = "消息内容", DBLength = 2000)]
        public String msg_text
        {
            get { return _msg_text; }
            set { _msg_text = value; }
        }
        private String _msg_time;
        [FieldAttr(FieldDesc = "接收时间", DBLength = 14)]
        public String receive_time
        {
            get { return _msg_time; }
            set { _msg_time = value; }
        }

        private String _read_flag;
        [FieldAttr(FieldDesc = "是否读取", DBLength = 1, DBType = ActionField.ftBoolean)]
        public String read_flag
        {
            get { return _read_flag; }
            set { _read_flag = value; }
        }

        public WX_RECV_MSG()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class XT_RECEIVE_SMS
    {
        private String _receive_guid;
        [FieldAttr(isKey = true, FieldDesc = "接收GUID", DBLength = 36)]
        public String receive_guid
        {
            get { return _receive_guid; }
            set { _receive_guid = value; }
        }

        private String _org_id;
        [FieldAttr(FieldDesc = "单位编码", DBLength = 6)]
        public String org_id
        {
            get { return _org_id; }
            set { _org_id = value; }
        }

        private String _receive_telno;
        [FieldAttr(FieldDesc = "手机号码", DBLength = 12)]
        public String receive_telno
        {
            get { return _receive_telno; }
            set { _receive_telno = value; }
        }

        private String _receive_message;
        [FieldAttr(FieldDesc = "接收内容", DBLength = 240)]
        public String receive_message
        {
            get { return _receive_message; }
            set { _receive_message = value; }
        }

        private String _receive_time;
        [FieldAttr(FieldDesc = "接收时间", DBLength = 14)]
        public String receive_time
        {
            get { return _receive_time; }
            set { _receive_time = value; }
        }

        private int _read_flag;
        [FieldAttr(FieldDesc = "是否读取", DBLength = 1, DBType = ActionField.ftBoolean)]
        public int read_flag
        {
            get { return _read_flag; }
            set { _read_flag = value; }
        }

        private int _sd_result;
        [FieldAttr(FieldDesc = "审核结果")]
        public int sd_result
        {
            get { return _sd_result; }
            set { _sd_result = value; }
        }

        public XT_RECEIVE_SMS()
        {

        }
    }
}

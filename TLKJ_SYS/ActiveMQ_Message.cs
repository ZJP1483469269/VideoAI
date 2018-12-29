using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace TLKJ.SYS
{
    public class ActiveMQ_Message
    {
        public String FROM_ID;
        public String USER_ID;
        public int CMD_ID;
        public String MESSAGE;
        public String CREATE_TIME;
    }

    public class ActiveMQ_MessageType
    {
        public static readonly int VIDEO_LIVE = 1;
        public static readonly int VIDEO_QUERY = 2;
    }
}
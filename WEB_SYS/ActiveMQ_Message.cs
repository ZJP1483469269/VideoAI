using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace TLKJ.WebSys
{
    public class ActiveMQ_Message
    {
        public int MessageType;
        public String USER_ID;
        public String ORG_ID;
        public String MESSAGE;
    }

    public class ActiveMQ_MessageType
    {
        public static readonly int VIDEO_LIVE = 1;
        public static readonly int VIDEO_QUERY = 2;
    }
}
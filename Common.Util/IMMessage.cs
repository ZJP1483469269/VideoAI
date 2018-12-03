using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace TLKJ.Utils
{
    /// <summary>
    /// Message：消息
    /// </summary>
    public class IMMessage
    { 
        public string ID { get; set; } 
        public string MSG_TYPE { get; set; }
        public string MSG_TEXT { get; set; } 
        public DateTime CreateDateTime { get; set; } 
        public String OPENID { get; set; }

        public IMMessage(DateTime dateTime, string cFromUser, string cMsgType, string cMsgBody)
        {
            this.ID = AutoID.getAutoID(); 
            this.CreateDateTime = dateTime;
            this.OPENID = cFromUser;
            this.MSG_TYPE = cMsgType;
            this.MSG_TEXT = cMsgBody;
        }

        /// <summary>
        /// 返回参数列表
        /// </summary>
        /// <returns></returns>
        public List<DbParameter> getParmList()
        {
            List<DbParameter> parameters = new List<DbParameter>(4);
            SqlParameter p = new SqlParameter("@ID", SqlDbType.VarChar, 26);
            p.Value = ID;
            parameters.Add(p);

            p = new SqlParameter("@MSG_TYPE", SqlDbType.NVarChar);
            p.Value = MSG_TYPE;
            parameters.Add(p);

            p = new SqlParameter("@MSG_TEXT", SqlDbType.NVarChar);
            p.Value = MSG_TEXT;
            parameters.Add(p);

            p = new SqlParameter("@CreateDateTime", SqlDbType.DateTime, 8);
            p.Value = CreateDateTime;
            parameters.Add(p);

            p = new SqlParameter("@OPENID", SqlDbType.NVarChar, 8);
            p.Value = KeyInfo.OPENID;
            parameters.Add(p);

            return parameters;
        }


    }
}
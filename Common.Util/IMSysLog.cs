using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace TLKJ.Utils
{
    /// <summary>
    /// IMSysLog：消息
    /// </summary>
    public class IMSysLog
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public IMMessageType MSG_TYPE { get; set; }

        public string MSG_TEXT { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime CreateDateTime { get; set; } 

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="content"></param>
        /// <param name="time"></param>
        public IMSysLog(string vOpenID, string cMessageID, IMMessageType vType, string content, DateTime time)
        {
            ID = cMessageID;
            MSG_TYPE = vType;
            MSG_TEXT = content;
            CreateDateTime = time;
        }

        public IMSysLog(IMMessageType vType, string content)
            : this("SYS", AutoID.getAutoID(), vType, content, DateTime.Now)
        {

        }
  
        /// <summary>
        /// 返回参数列表
        /// </summary>
        /// <returns></returns>
        public List<DbParameter> GetParmList()
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
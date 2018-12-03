using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace TLKJ.Utils
{
    /// <summary>
    /// IMRecvMSG：消息
    /// </summary>
    public class IMRecvMSG
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string WX_ID { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string MSG_TYPE { get; set; }

        public string MSG_TEXT { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public string MSG_TIME { get; set; }

        public String OPENID { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="content"></param>
        /// <param name="time"></param>
        public IMRecvMSG(String vType, String cWX_ID, String cOPENID, string vContent)
        {
            ID = AutoID.getAutoID();
            WX_ID = cWX_ID;
            OPENID = cOPENID;
            MSG_TYPE = vType;
            MSG_TEXT = vContent;
            MSG_TIME = StringEx.getString(DateUtils.getDayTimeNum());
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

            p = new SqlParameter("@WX_ID", SqlDbType.NVarChar);
            p.Value = WX_ID;
            parameters.Add(p);

            p = new SqlParameter("@OPENID", SqlDbType.NVarChar);
            p.Value = OPENID;
            parameters.Add(p);

            p = new SqlParameter("@MSG_TYPE", SqlDbType.NVarChar);
            p.Value = MSG_TYPE;
            parameters.Add(p);

            p = new SqlParameter("@MSG_TEXT", SqlDbType.NVarChar);
            p.Value = MSG_TEXT;
            parameters.Add(p);

            p = new SqlParameter("@MSG_TIME", SqlDbType.Char, 14);
            p.Value = MSG_TIME;
            parameters.Add(p);

            return parameters;
        }


    }
}
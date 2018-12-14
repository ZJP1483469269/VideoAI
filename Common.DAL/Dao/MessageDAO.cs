using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TLKJ.Utils;
using TLKJ.DB;
using System.Data.Common;

namespace TLKJ.DAO
{
    public class MessageDAO
    {
        /// <summary>
        /// 插入新消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns>返回插入是否成功</returns>
        public static bool Log(IMSysLog message)
        {
            string sql = "insert into XT_LOG (ID,WX_ID,MSG_TYPE,MSG_TEXT,MSG_TIME) "
                + " values (@ID,@WX_ID,@MSG_TYPE,@MSG_TEXT,@MSG_TIME)";
            List<DbParameter> ParmList = message.getParmList();
            DbParameter[] vParmList = new DbParameter[ParmList.Count];
            for (int i = 0; i < ParmList.Count; i++)
            {
                vParmList[i] = ParmList[i];
            }
            return DbManager.ExecSQL(sql, vParmList) > 0;
        }

        public static bool RECV_MSG(IMRecvMSG msg)
        {
            string sql = "insert into WX_RECV_MSG (ID,WX_ID,OPENID,MSG_TYPE,MSG_TEXT,MSG_TIME,READ_FLAG) "
                + " values (@ID,@WX_ID,@OPENID,@MSG_TYPE,@MSG_TEXT,@MSG_TIME,0)";
            List<DbParameter> ParmList = msg.getParmList();
            DbParameter[] vParmList = new DbParameter[ParmList.Count];
            for (int i = 0; i < ParmList.Count; i++)
            {
                vParmList[i] = ParmList[i];
            }
            return DbManager.ExecSQL(sql, vParmList) > 0;
        }

        public static bool SEND_MSG(IMRecvMSG msg)
        {
            string sql = "insert into WX_RECV_MSG (ID,WX_ID,OPENID,MSG_TYPE,MSG_TEXT,MSG_TIME,READ_FLAG) "
                + " values (@ID,@WX_ID,@OPENID,@MSG_TYPE,@MSG_TEXT,@MSG_TIME,0)";
            List<DbParameter> ParmList = msg.getParmList();
            DbParameter[] vParmList = new DbParameter[ParmList.Count];
            for (int i = 0; i < ParmList.Count; i++)
            {
                vParmList[i] = ParmList[i];
            }
            return DbManager.ExecSQL(sql, vParmList) > 0;
        }
    }
}
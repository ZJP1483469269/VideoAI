using Novacode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Text;
using TLKJ.DB;

namespace TLKJ.WebSys
{
    /// <summary>
    /// DBConfig 的摘要说明
    /// </summary>
    public class DBConfig
    {
        public String getOrgKey(String cORG_ID, String cKeyName)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT KEYVALUE ");
            sql.Append(" FROM XT_ORG_CONFIG ");
            sql.Append(" WHERE ORG_ID='" + cORG_ID + "' ");
            sql.Append(" and KEYNAME='" + cKeyName + "'");
            return DbManager.GetStrValue(sql.ToString());
        }
    }
}
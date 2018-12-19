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
            sql.Append(" FROM S_ORG_CONFIG ");
            sql.Append(" WHERE ORG_ID='" + cORG_ID + "' ");
            sql.Append(" and KEYNAME='" + cKeyName + "'");
            DataTable dtRows = DbManager.QueryData(sql.ToString());
            if (dtRows != null && dtRows.Rows.Count > 0)
            {
                return DbManager.GetStrValue(sql.ToString());
            }
            else
            {
                DbManager.ExecSQL("INSERT INTO S_ORG_CONFIG(ORG_ID,KEYNAME) VALUES('" + cORG_ID + "','" + cKeyName + "')");
                return "";
            }
        }
    }
}
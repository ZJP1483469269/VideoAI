using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TLKJ.Utils;
using TLKJ.DB;
using System.Data;

namespace TLKJ.DAO
{
    public class OrgConfigService : IService
    {
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
            readQueryString();
        }

        public void InitOrgConfig(String cOrgID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" INSERT INTO XT_ORG_CONFIG(ORG_ID,KEYNAME,KEYVALUE) ");
            sql.Append(" SELECT '" + cOrgID + "',KEYNAME,DEFVAL ");
            sql.Append(" FROM  S_CONSTANT T ");
            sql.Append(" WHERE NOT EXISTS(SELECT 1 FROM XT_ORG_CONFIG  X WHERE T.KEYNAME= X.KEYNAME) ");
            DbManager.ExecSQL(sql.ToString());
        }

        public void query()
        {
            String cORG_ID = StringEx.getString(request["ORG_ID"]);
            InitOrgConfig(cORG_ID);

            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            DataTable dtRows = DbManager.QueryData("SELECT A.*,B.DEFVAL,KEYDESC,TYPE_ID FROM XT_ORG_CONFIG A,S_CONSTANT B WHERE A.KEYNAME=B.KEYNAME AND A.ORG_ID='" + cORG_ID + "'");
            vret = ActiveResult.Query(dtRows);
            response.Write(vret.toJSONString());
        }

        public void save()
        {
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cORG_ID = StringEx.getString(request["ORG_ID"]);
            String cKEYNAME = StringEx.getString(request["KEYNAME"]);
            String cKEYVALUE = StringEx.getString(request["KEYVALUE"]);
            if (String.IsNullOrEmpty(cORG_ID))
            {
                vret = ActiveResult.Valid("单位编码参数不能为空！");
            }
            else if (String.IsNullOrEmpty(cKEYNAME))
            {
                vret = ActiveResult.Valid("配置项目不能为空！");
            }
            else if (String.IsNullOrEmpty(cKEYVALUE))
            {
                vret = ActiveResult.Valid("配置值不能为空！");
            }
            else
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" UPDATE XT_ORG_CONFIG ");
                sql.Append(" SET KEYVALUE='" + cKEYVALUE + "'");
                sql.Append(" WHERE ORG_ID='" + cORG_ID + "'");
                sql.Append(" AND KEYNAME='" + cKEYNAME + "'");
                int iCode = DbManager.ExecSQL(sql.ToString());
                vret = ActiveResult.returnObject(iCode);
            }
            response.Write(vret.toJSONString());
        }
    }
}

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
    public class SYSService : IService
    {
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
            readQueryString();
        }

        /// <summary>
        /// 根据单位类型和上级模块，查询可管理模块。
        /// </summary>
        public void MENU()
        {
            ActiveResult vret = new ActiveResult();
            String cTYPE_ID = StringEx.getString(request["TYPE_ID"]);
            String cORG_CLS = StringEx.getString(request["ORG_CLS"]);
            if (cTYPE_ID.Length == 0)
            {
                vret = ActiveResult.Valid("单位序号不能为空！");
            }
            else if (cORG_CLS.Length == 0)
            {
                vret = ActiveResult.Valid("单位类型不能为空！");
            }
            else
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT ITEM_ID,ITEM_NAME,EVENT,PARENTID,ALLOW_ORGS ");
                sql.Append(" FROM S_NODES ");
                sql.Append(" WHERE PARENTID='" + cTYPE_ID + "' and ALLOW_ORGS LIKE  '"+"%"+cORG_CLS+"%"+"'");
                  
                DataTable dtRows = DbManager.QueryData(sql.ToString());
                vret = ActiveResult.Query(dtRows);
            }
            response.Write(vret.toJSONString());
        }

        /// <summary>
        /// 用户登录模块
        /// </summary>
        public void UserLogin()
        {
            ActiveResult vret = new ActiveResult();
            String cUserID = StringEx.getString(request["USERID"]);
            String cKeyWord = StringEx.getString(request["KEYWORD"]);
            if (cUserID.Length == 0)
            {
                vret = ActiveResult.Valid("usercode", "登录账号不能为空！");
            }
            else if (cKeyWord.Length == 0)
            {
                vret = ActiveResult.Valid("userpass", "登录密码不能为空！");
            }
            else
            {

                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM  S_USER_INF u");
                sql.Append(" WHERE USER_ID='" + cUserID + "' and USER_PASSW='" + cKeyWord + "'");
                DataTable dtRows = DbManager.QueryData(sql.ToString());
                if ((dtRows != null) && (dtRows.Rows.Count > 0))
                {
                    LoginUserInfo vUserInf = new LoginUserInfo();
                    vUserInf.USER_ID = cUserID;
                    vUserInf.USER_NAME = StringEx.getString(dtRows, 0, "USER_NAME");

                    vUserInf.ORG_ID = StringEx.getString(dtRows, 0, "ORG_ID");

                    vUserInf.ORG_NAME = DbManager.GetStrValue("SELECT * FROM  S_ORG_INF o , S_USER_INF u WHERE o.ORG_ID = u.ORG_ID and o.GXQ_ID='" + StringEx.getString(dtRows, 0, "GXQ_ID") + "'" );
                   string sqlstr="select ORG_TYPE FROM S_ORG_INF o, S_USER_INF u WHERE  o.ORG_ID = u.ORG_ID and u.GXQ_ID='" + StringEx.getString(dtRows, 0, "GXQ_ID") + "'";
                 
                    vUserInf.ORG_TYPE = DbManager.GetStrValue(sqlstr);

                    vUserInf.ROLE_ID = StringEx.getString(dtRows, 0, "ROLEID");
                    vret = ActiveResult.returnObject(vUserInf);
                }
                else
                {
                    vret = ActiveResult.Valid("账号或密码错误！");
                }
            }
            response.Write(vret.toJSONString());
        }
    }
}

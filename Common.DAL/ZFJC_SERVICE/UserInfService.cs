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
    public class UserInfService : IService
    {
        XT_USER_Dao dao = new XT_USER_Dao();
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
            readQueryString();
        }

        public void query()
        {
            String cORG_ID = StringEx.getString(request["ORG_ID"]);
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cWhereParm = "ORG_ID like '" + cORG_ID + "%'";
            String cOrderBy = "ORDER BY USER_ID ASC";
            DBResult dbret = dao.Query(cWhereParm, cOrderBy, iPageNo, iPageSize);
            DataTable dtRows = dbret.dtrows;
            int iRowsCount = dbret.ROW_COUNT;
            vret = ActiveResult.Query(dtRows);
            vret.total = iRowsCount;

            response.Write(vret.toJSONString());
        }

        public void USER_LOGIN()
        {
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String vUserCount = StringEx.getString(request["user_id"]);
            String vPassWord = StringEx.getString(request["user_pass"]);
            String sql = "SELECT COUNT(1) FROM XT_USER WHERE USERCODE='" + vUserCount + "' AND PASSWORD = " + vPassWord;

            String sqlValue = DbManager.GetStrValue(sql);
            int iRSCount = StringEx.getInt(sqlValue);
            if (iRSCount > 0)
            {
                HttpCookie vUserInf = new HttpCookie(AppConfig.SESSION_USER_ID, vUserCount);
                HttpCookie ckUser = new HttpCookie(AppConfig.SESSION_USER_ID, vUserCount);
                request.Cookies.Add(ckUser);
                HttpContext.Current.Session[AppConfig.SESSION_USER_ID] = vUserCount;
                request.Cookies.Add(vUserInf);
            }
            vret = ActiveResult.Valid(iRSCount);
            response.Write(vret.toJSONString());
        }

        public void find()
        {
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cORG_ID = StringEx.getString(request["USER_ID"]);
            if (String.IsNullOrEmpty(cORG_ID))
            {
                vret = ActiveResult.Valid("用户编码参数不能为空！");
            }
            else
            {
                XT_USER vInfo = dao.FindOne(cORG_ID);
                vret = ActiveResult.returnObject(vInfo);
            }
            response.Write(vret.toJSONString());
        }

        public void del_item()
        {
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cORG_ID = StringEx.getString(request["USER_ID"]);
            if (String.IsNullOrEmpty(cORG_ID))
            {
                vret = ActiveResult.Valid("用户编码参数不能为空！");
            }
            else
            {
                int iCode = dao.del_item(cORG_ID);
                vret = ActiveResult.Valid(iCode > 0);
            }
            response.Write(vret.toJSONString());
        }
    }
}

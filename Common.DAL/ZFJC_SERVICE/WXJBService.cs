using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TLKJ.Utils;
using TLKJ.DB;
using System.Data;
using System.IO;
using System.Web.UI;

namespace TLKJ.DAO
{
    public class WXJBService : IService
    {

        XT_JB_Dao dao = new XT_JB_Dao();
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
            String cWhereParm = " (ORG_ID ='" + cORG_ID + "')  AND (JBTYPE='WEIXIN') ";
            String cOrderBy = "ORDER BY ID ASC";
            DBResult dbret = dao.Query(cWhereParm, cOrderBy, iPageNo, iPageSize);
            DataTable dtRows = dbret.dtrows;
            int iRowsCount = dbret.ROW_COUNT;
            vret = ActiveResult.Query(dtRows);
            vret.total = iRowsCount;

            response.Write(vret.toJSONString());
        }

        /// <summary>
        /// 
        /// </summary>
        public void export_query()
        {
            String cORG_ID = Config.GetAppSettings("ORG_ID");
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cWhereParm = " (ORG_ID ='" + cORG_ID + "') AND (SD_RESULT=1) ";
            String cOrderBy = "ORDER BY ID ASC";
            DBResult dbret = dao.QueryResult(cWhereParm, cOrderBy, iPageNo, iPageSize);
            DataTable dtRows = dbret.dtrows;
            int iRowsCount = dbret.ROW_COUNT;
            vret = ActiveResult.Query(dtRows);
            vret.total = iRowsCount;

            response.Write(vret.toJSONString());
        }

        public void find()
        {
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cKeyID = StringEx.getString(request["ID"]);
            if (String.IsNullOrEmpty(cKeyID))
            {
                vret = ActiveResult.Valid("单位编码参数不能为空！");
            }
            else
            {
                XT_JB vInfo = dao.FindOne(cKeyID);
                vret = ActiveResult.returnObject(vInfo);
            }
            response.Write(vret.toJSONString());
        }
        public void saves()
        {
            ActiveResult vret = new ActiveResult();
            XT_JB vo = new XT_JB();
            String cKeyID = StringEx.getString(request[AppConfig.__DBKEY]);
            vo = (XT_JB)RequestUtil.readFromRequest(request, vo);
            int iCode = dao.save(vo, cKeyID);
            vret = ActiveResult.Valid(iCode);
            response.Write(vret.toJSONString());
        }
        public void check()
        {
            ActiveResult vret = new ActiveResult();
            XT_JB vo = new XT_JB();
            String cKEY_ID = StringEx.getString(request["ID"]);
            int iTYPE_ID = StringEx.getInt(request["TYPE_ID"]);
            int iCode = dao.Check(cKEY_ID, iTYPE_ID);
            vret = ActiveResult.Valid(iCode);
            response.Write(vret.toJSONString());
        }

        public void savegtws()
        {
            ActiveResult vret = new ActiveResult();
            XT_JB vo = new XT_JB();
            vo.jbtype = "GTWSWEB";
            String cKeyID = StringEx.getString(request[AppConfig.__DBKEY]);
            vo = (XT_JB)RequestUtil.readFromRequest(request, vo);
            int iCode = dao.save(vo, cKeyID);
            vret = ActiveResult.Valid(iCode);
            response.Write(vret.toJSONString());
        }

    }
}

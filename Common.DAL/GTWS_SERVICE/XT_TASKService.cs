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
    public class XT_TASKService : IService
    {
        XT_TASK_Dao dao = new XT_TASK_Dao();
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

            String cWhereParm = "";
            String cOrderBy = "ORDER BY REC_ID ASC";
            DBResult dbret = dao.Query(cWhereParm, cOrderBy, iPageNo, iPageSize);
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
                vret = ActiveResult.Valid("用户账号不能为空！");
            }
            else
            {
                XT_TASK vInfo = dao.FindOne(cKeyID);
                vret = ActiveResult.returnObject(vInfo);
            }
            response.Write(vret.toJSONString());
        }

        public void report()
        {
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cORG_ID = StringEx.getString(request["ORG_ID"]);
            if (String.IsNullOrEmpty(cORG_ID))
            {
                vret = ActiveResult.Valid("用户账号不能为空！");
            }
            else
            {
                String cAreaID = AppManager.getAreaHeader(cORG_ID);
                StringBuilder sql = new StringBuilder();
                sql.Append("  SELECT ORG_ID ");
                sql.Append("  , (SELECT ORG_NAME FROM S_ORG_INF X WHERE X.ORG_ID = T.ORG_ID) AS ORG_NAME ");
                sql.Append("  , COUNT(1) AS ALL_TASK ");
                sql.Append("  , (SELECT COUNT(1) FROM XT_TASK_LIST X WHERE X.ORG_ID = T.ORG_ID) AS LEFT_TASK ");
                sql.Append("  FROM XT_CAMERA T ");
                sql.Append("  WHERE ORG_ID like  '" + cAreaID + "%'");
                sql.Append("  GROUP BY ORG_ID ");
                DataTable dtRows = DbManager.QueryData(sql.ToString());
                vret = ActiveResult.Query(dtRows);
            }
            response.Write(vret.toJSONString());
        }
        public void del_item()
        {
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cUSER_COUNT = StringEx.getString(request["dbkey"]);
            if (String.IsNullOrEmpty(cUSER_COUNT))
            {
                vret = ActiveResult.Valid("编码不能为空！");
            }
            else
            {
                int iCode = dao.del_item(cUSER_COUNT);
                vret = ActiveResult.Valid(iCode > 0);
            }
            response.Write(vret.toJSONString());
        }

        public void save()
        {
            ActiveResult vret = ActiveResult.Valid(AppConfig.SUCCESS);
            String cKeyID = StringEx.getString(request["ID"]);
            String cORG_ID = StringEx.getString(request["org_id"]);
            String cCLS_ID = StringEx.getString(request["cls_id"]);

            XT_TASK vo = new XT_TASK();
            vo = (XT_TASK)RequestUtil.readFromRequest(request, vo);
            vo.cls_id = cCLS_ID;
            vo.update_time = StringEx.getString(DateUtils.getDayTimeNum());
            if (String.IsNullOrEmpty(cORG_ID))
            {
                vret = ActiveResult.Valid("单位编码不能为空！");
            }
            else if (String.IsNullOrEmpty(cCLS_ID))
            {
                vret = ActiveResult.Valid("类型不能为空！");
            }

            if (vret.result == AppConfig.SUCCESS)
            {
                int iCode = dao.save(vo, cKeyID);
                vret = ActiveResult.Valid(iCode);
            }
            response.Write(vret.toJSONString());
        }
    }
}
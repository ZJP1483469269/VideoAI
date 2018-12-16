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
    public class OrgWXConfigService : IService
    {
        S_ORG_INF_WX_CONFIG_Dao dao = new S_ORG_INF_WX_CONFIG_Dao();
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
            readQueryString();
        }

        public void query()
        {
            String cORG_ID = StringEx.getString(request["ORG_ID"]);
            ActiveResult vret = new ActiveResult();
            if (cORG_ID.Length == 0)
            {
                vret = ActiveResult.Valid("错误，参数ORG_ID未正常传递！");
            }
            else
            {
                String cWhereParm = "ORG_ID like '" + cORG_ID + "%'";
                String cOrderBy = " order by ORG_ID desc ";
                DBResult dbret = dao.Query(cWhereParm, cOrderBy, iPageNo, iPageSize);
                DataTable dtRows = dbret.dtrows;
                vret = ActiveResult.Query(dtRows);
                vret.total = dtRows.Rows.Count;
            }
            response.Write(vret.toJSONString());
        }

        public void find()
        {
            ActiveResult vret = new ActiveResult();
            String cORG_ID = StringEx.getString(request["ORG_ID"]);
            if (cORG_ID.Length == 0)
            {
                vret = ActiveResult.Valid("错误，参数ORG_ID未正常传递！");
            }
            else
            {
                S_ORG_INF_WX_CONFIG vo = dao.FindOne(cORG_ID);
                vret = ActiveResult.returnObject(vo);
            }
            response.Write(vret.toJSONString());
        }

        public void save()
        {
            ActiveResult vret = new ActiveResult();
            String cKeyID = StringEx.getString(request["ORG_ID"]);
            S_ORG_INF_WX_CONFIG vo = new S_ORG_INF_WX_CONFIG();
            vo = (S_ORG_INF_WX_CONFIG)RequestUtil.readFromRequest(request, vo);
            int iCode = dao.save(vo, cKeyID);
            vret = ActiveResult.Valid(iCode);
            response.Write(vret.toJSONString());
        }

        public void del_item()
        {
            ActiveResult vret = new ActiveResult();
            String cDBKey = StringEx.getString(request[DEL_ITEM_KEY]);
            int iCode = dao.del_item(cDBKey);
            vret = ActiveResult.Valid(iCode);
            response.Write(vret.toJSONString());
        }

        public void del_list()
        {
            ActiveResult vret = new ActiveResult();
            String cDBKey = StringEx.getString(request[DEL_LIST_KEY]);
            int iCode = dao.del_list(cDBKey);
            vret = ActiveResult.Valid(iCode);
            response.Write(vret.toJSONString());
        }

        public void finditem()
        {
            ActiveResult vret = new ActiveResult();
            String cDBKey = StringEx.getString(request[AppConfig.__DBKEY]);
            S_ORG_INF_WX_CONFIG vo = dao.FindOne(cDBKey);
            vret = ActiveResult.returnObject(vo);
            response.Write(vret.toJSONString());
        }
    }
}

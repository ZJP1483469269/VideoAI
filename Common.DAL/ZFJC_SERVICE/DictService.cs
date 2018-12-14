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
    public class DictService : IService
    {
        S_DICTS_Dao dao = new S_DICTS_Dao();
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
            readQueryString();
        }

        public void query()
        {
            ActiveResult vret = new ActiveResult();
            String cCLS_ID = StringEx.getString(request["cls_id"]);
            String cWhereParm = " CLS_ID='" + cCLS_ID + "'";
            String cOrderBy = " order by TYPE_ID asc";
            DBResult dbret = dao.Query(cWhereParm, cOrderBy, iPageNo, iPageSize);
            DataTable dtRows = dbret.dtrows;
            int iRowCount = (dtRows == null) ? 0 : dtRows.Rows.Count;
            vret = ActiveResult.Query(dtRows);
            vret.total = iRowCount;
            response.Write(vret.toJSONString());
        }

        public void save()
        {
            ActiveResult vret = new ActiveResult();
            String cKeyID = StringEx.getString(request[AppConfig.__DBKEY]);
            S_DICTS vo = new S_DICTS();
            vo = (S_DICTS)RequestUtil.readFromRequest(request, vo);
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
            S_DICTS vo = dao.FindOne(cDBKey);
            vret = ActiveResult.returnObject(vo);
            response.Write(vret.toJSONString());
        }
    }
}

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
    public class FEEDBACKService : IService
    {
        XT_JB_FEEDBACK_Dao dao = new XT_JB_FEEDBACK_Dao();
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
            readQueryString();
        }

        public void query()
        {
            String cKeyGuid = StringEx.getString(request["ID"]);
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cWhereParm = " (JB_ID ='" + cKeyGuid + "') ";
            String cOrderBy = "ORDER BY ID ASC";
            DataTable dtRows = dao.Query(cWhereParm, cOrderBy);
            int iRowsCount = dtRows.Rows.Count;
            vret = ActiveResult.Query(dtRows);
            vret.total = iRowsCount;

            response.Write(vret.toJSONString());
        }

        /// <summary>
        /// 保存反馈数据
        /// </summary>
        public void save()
        {
            ActiveResult vret = new ActiveResult();
            String cKeyID = StringEx.getString(request[AppConfig.__DBKEY]);
            XT_JB_FEEDBACK vo = new XT_JB_FEEDBACK();
            vo = (XT_JB_FEEDBACK)RequestUtil.readFromRequest(request, vo);
            vo.id = AutoID.getAutoID();
            int iCode = dao.save(vo, "");
            vret = ActiveResult.Valid(iCode);
            response.Write(vret.toJSONString());
        }

        public void find()
        {
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cKEY_ID = StringEx.getString(request["ID"]);
            if (String.IsNullOrEmpty(cKEY_ID))
            {
                vret = ActiveResult.Valid("错误，ID参数为空！");
            }
            else
            {
                XT_JB_FEEDBACK vInfo = dao.FindOne(cKEY_ID);
                vret = ActiveResult.returnObject(vInfo);
            }
            response.Write(vret.toJSONString());
        }


    }
}

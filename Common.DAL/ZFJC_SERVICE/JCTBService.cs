using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TLKJ.Utils;
using TLKJ.DB;
using System.Data;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class JCTBService : IService
    {
        JCTBDao dao = new JCTBDao();
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
        }

        public void query()
        {
            ActiveResult vret = new ActiveResult();
            String cORG_ID = StringEx.getString(request["ORG_ID"]);
            String cWhereParm = "ORG_ID='" + cORG_ID + "'";
            DataTable dtRows = dao.QueryData(cWhereParm);
            vret = ActiveResult.Query(dtRows);
            response.Write(vret.toJSONString());
        }

        public void city_gxsd()
        {
            ActiveResult vret = new ActiveResult();
            String cXZQDM = StringEx.getString(request["XZQDM"]);
            String cORG_ID = StringEx.getString(request["ORG_ID"]);
            if (String.IsNullOrEmpty(cORG_ID))
            {
                vret = ActiveResult.Query(null);
                response.Write(vret.toJSONString());
                return;
            }

            cORG_ID = cORG_ID.Replace("00", "");
            StringBuilder sql = new StringBuilder();

            if (cORG_ID.Length == 4)
            {
                sql.Append(" (XZQDM LIKE '" + cORG_ID + "%') ");
            }

            if (cXZQDM.Length > 0)
            {
                sql.Append(" AND (XZQDM='" + cXZQDM + "') ");
            }

            DBResult dbret = dao.Query("JCTB_GUID,XZQDM,XMC AS XZQMC,JCBH,AREA_XZQDM AS GXQDM,(SELECT GXQ_NAME FROM S_GXQ_INF X WHERE X.GXQ_ID=AREA_XZQDM) AS GXQMC ", sql.ToString(), " order by xzqdm,jcbh asc ", iPageNo, iPageSize);
            vret = ActiveResult.Query(dbret.dtrows);
            int iRowCount = (dbret == null) ? 0 : dbret.ROW_COUNT;
            vret.total = iRowCount;
            response.Write(vret.toJSONString());
        }

        public void jctb_gxsd()
        {
            ActiveResult vret = new ActiveResult();
            String cGXQ_ID = StringEx.getString(request["GXQ_ID"]);
            String cJCTB_GUID = StringEx.getString(request["JCTB_GUID"]);
            if ((String.IsNullOrEmpty(cGXQ_ID)) || (String.IsNullOrEmpty(cJCTB_GUID)))
            {
                vret = ActiveResult.Valid(AppConfig.FAILURE);
                response.Write(vret.toJSONString());
                return;
            }

            StringBuilder sql = new StringBuilder();
            sql.Append(" UPDATE XT_JCTB ");
            sql.Append(" SET AREA_XZQDM='" + cGXQ_ID + "'");
            sql.Append(" WHERE JCTB_GUID='" + cJCTB_GUID + "'");

            int iCode = DbManager.ExecSQL(sql.ToString());

            vret = ActiveResult.Valid(iCode);
            response.Write(vret.toJSONString());
        }

        public void list()
        {
            ActiveResult vret = new ActiveResult();
            String cGXQ_ID = StringEx.getString(request["GXQ_COUNTY_ID"]);
            String cXZQ_ID = StringEx.getString(request["XZQ_COUNTY_ID"]);
            if ((String.IsNullOrEmpty(cGXQ_ID)) && (String.IsNullOrEmpty(cXZQ_ID)))
            {
                vret = ActiveResult.Query(null);
                response.Write(vret.toJSONString());
            }
            else
            {
                String cORDER_BY = StringEx.getString(request["ORDERBY"]);

                String cJCBH = StringEx.getString(request["JCBH"]);

                String cTAG = StringEx.getString(request["TAG"]);

                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT JCTB_GUID,XZQDM,XZQMC,JCBH ");

                if (cGXQ_ID.Length > 0)
                {
                    sql.Append(" WHERE (AREA_XZQDM='" + cGXQ_ID + "') ");
                }

                if (cXZQ_ID.Length > 0)
                {
                    sql.Append(" WHERE (XZQDM='" + cXZQ_ID + "') ");
                }
                if (cTAG.Equals("2"))
                {
                    sql.Append(" AND (DX_LX=2) ");
                }

                if (cJCBH.Length > 0)
                {
                    sql.Append(" AND JCBH='" + cJCBH + "' ");
                }

                if (cORDER_BY.Length > 0)
                {
                    sql.Append(" ORDER BY " + cORDER_BY);
                }
                else
                {
                    sql.Append(" ORDER BY JCBH ");
                }

                DataTable dtRows = DbManager.QueryData(sql.ToString());
                vret = ActiveResult.Query(dtRows);
                response.Write(vret.toJSONString());
            }
        }
    }
}

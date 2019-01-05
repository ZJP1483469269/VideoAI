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
    public class XT_IMG_RECService : IService
    {
        XT_IMG_REC_Dao dao = new XT_IMG_REC_Dao();
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

        public void query_alarm()
        {
            String cORG_ID = StringEx.getString(request["ORG_ID"]);
            String cCAMERA_NAME = StringEx.getString(request["CAMERA_NAME"]);
            String cALARM_CHECKED = StringEx.getString(request["ALARM_CHECKED"]);
            String cALARM_FLAG = StringEx.getString(request["ALARM_FLAG"]);

            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);

            String cWhereParm = " EXISTS( SELECT 1 FROM XT_CAMERA X WHERE X.ORG_ID='" + cORG_ID + "' AND X.DEVICE_ID=CAMERA_ID ";

            if (!String.IsNullOrWhiteSpace(cCAMERA_NAME))
            {
                cWhereParm = cWhereParm + " AND (CAMERA_NAME LIKE '%" + cCAMERA_NAME + "%')";
            }
            cWhereParm = cWhereParm + " )";

            if (!String.IsNullOrWhiteSpace(cALARM_FLAG))
            {
                cWhereParm = cWhereParm + " AND (ALARM_FLAG=" + cALARM_FLAG + ") ";
            }
            if (!String.IsNullOrWhiteSpace(cALARM_CHECKED))
            {
                cWhereParm = cWhereParm + " AND ALARM_CHECKED='" + cALARM_CHECKED + "'";
            }

            String cOrderBy = "ORDER BY REC_ID ASC";
            DBResult dbret = dao.Query("*"
                    + ",(SELECT CAMERA_NAME FROM XT_CAMERA X WHERE X.DEVICE_ID=CAMERA_ID) AS CAMERA_NAME"
                    + ",(SELECT ADDR FROM XT_CAMERA X WHERE X.DEVICE_ID=CAMERA_ID) AS ADDR ", "XT_IMG_REC T", cWhereParm, cOrderBy, iPageNo, iPageSize);
            DataTable dtRows = dbret.dtrows;
            int iRowsCount = dbret.ROW_COUNT;
            vret = ActiveResult.Query(dtRows);
            vret.total = iRowsCount;

            response.Write(vret.toJSONString());
        }

        public void query_list()
        {
            String cREC_ID = StringEx.getString(request["REC_ID"]);

            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            DataTable dtInfo = DbManager.QueryData("SELECT CAMERA_ID,PRESET_ID FROM XT_IMG_REC WHERE REC_ID='" + cREC_ID + "'");
            String cCAMERA_ID = StringEx.getString(dtInfo, 0, "CAMERA_ID");
            String cPRESET_ID = StringEx.getString(dtInfo, 0, "PRESET_ID");

            String cWhereParm = " CAMERA_ID='" + cCAMERA_ID + "' AND PRESET_ID='" + cPRESET_ID + "'";

            String cOrderBy = "ORDER BY REC_ID ASC";
            DBResult dbret = dao.Query("*,(SELECT ADDR FROM XT_CAMERA X WHERE X.DEVICE_ID=CAMERA_ID) AS ADDR ", "XT_IMG_REC T", cWhereParm, cOrderBy, iPageNo, iPageSize);
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
            String cCAMERA_ID = StringEx.getString(request["REC_ID"]);

            DataTable dtInfo = DbManager.QueryData(" select * from xt_CAMERA where ID = '" + cCAMERA_ID + "'");
            vret = ActiveResult.Query(dtInfo);

            response.Write(vret.toJSONString());
        }

        public void del_item()
        {
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cUSER_COUNT = StringEx.getString(request["dbkey"]);
            String cREC_ID = StringEx.getString(request["REC_ID"]);

            int Code = DbManager.ExecSQL(" delete XT_IMG_REC  WHERE REC_ID = '" + cREC_ID + "'");
            vret.total = Code;
            response.Write(vret.toJSONString());
        }

        public void save()
        {
            ActiveResult vret = ActiveResult.Valid(AppConfig.SUCCESS);
            String cKeyID = StringEx.getString(request["ID"]);
            String cORG_ID = StringEx.getString(request["org_id"]);
            String cCLS_ID = StringEx.getString(request["cls_id"]);
            String cALARM_FLAG = StringEx.getString(request["ALARM_FLAG"]);
            String cREC_ID = StringEx.getString(request["REC_ID"]);
            String Ccamera_name = StringEx.getString(request["CAMERA_NAME"]);
            String CADDR = StringEx.getString(request["ADDR"]);
            String CX = StringEx.getString(request["X"]);
            String CY = StringEx.getString(request["Y"]);
            String ID = (System.DateTime.Now).ToFileTime().ToString();

            if (!String.IsNullOrWhiteSpace(Ccamera_name) && !String.IsNullOrWhiteSpace(CADDR) && !String.IsNullOrWhiteSpace(CX) && !String.IsNullOrWhiteSpace(CY))
            {
                DbManager.ExecSQL(" update XT_IMG_REC SET ALARM_FLAG = '" + cALARM_FLAG + "' and ALARM_CHECKED =1 WHERE REC_ID = '" + cREC_ID + "'  ");
                int Code = DbManager.ExecSQL(" INSERT INTO XT_JB (ID,ADRESS,X,Y)VALUES('" + ID + "','" + CADDR + "','" + CX + "','" + CY + "') ");
                vret.total = Code;
            }
            else
            {
                int Code = DbManager.ExecSQL(" update XT_IMG_REC SET ALARM_FLAG = '" + cALARM_FLAG + "' and ALARM_CHECKED =1 WHERE REC_ID = '" + cREC_ID + "' ");
                vret.total = Code;
            }

            response.Write(vret.toJSONString());
        }
    }
}
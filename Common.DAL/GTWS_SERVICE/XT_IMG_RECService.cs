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

            String cWhereParm = " EXISTS( SELECT 1 FROM XT_CAMERA X WHERE X.ORG_ID='" + cORG_ID + "' AND X.DEVICE_ID=CAMERA_ID AND ALARM_FLAG = '0' AND ALARM_CHECKED = '0' ";

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
                if (cALARM_CHECKED.Equals("0"))
                {
                    cWhereParm = cWhereParm + " AND ALARM_CHECKED='" + cALARM_CHECKED + "'";
                }
                else {
                    cWhereParm = cWhereParm + " AND ALARM_CHECKED> 0 ";
                }
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
            String cCamera_ID = DbManager.GetStrValue(" select CAMERA_ID from XT_IMG_REC where REC_ID = '" + cCAMERA_ID + "'");
            DataTable dtInfo = DbManager.QueryData(" SELECT * FROM XT_CAMERA WHERE DEVICE_ID = '" + cCamera_ID + "' ");
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
            String cREC_ID = StringEx.getString(request["REC_ID"]);
            if (String.IsNullOrWhiteSpace(cREC_ID))
            {
                vret = ActiveResult.Valid("参数传递错误！");
            }
            else
            {
                String cDeviceID = DbManager.GetStrValue("SELECT CAMERA_ID FROM XT_IMG_REC WHERE REC_ID='" + cREC_ID + "'");
                XT_CAMERA_Dao dao = new XT_CAMERA_Dao();
                XT_CAMERA mv = dao.FindItem(cDeviceID);

                String cAddress = mv.addr;
                String CX = StringEx.getString(mv.x); 
                String CY = StringEx.getString(mv.y);
                String ID = AutoID.getAutoID();

                List<String> sqls = new List<string>();
                sqls.Add("  update XT_IMG_REC SET ALARM_CHECKED =1 WHERE REC_ID = '" + cREC_ID + "' ");
                JActiveTable aTable = new JActiveTable();
                aTable.TableName = "XT_JB";
                aTable.AddField("ID", ID);
                aTable.AddField("ADRESS", cAddress);
                aTable.AddField("X", CX);
                aTable.AddField("Y", CY);
                sqls.Add(aTable.getInsertSQL());

                int iCode = DbManager.ExecSQL(sqls);
                vret = ActiveResult.Valid(iCode);
            }

            response.Write(vret.toJSONString());
        }

        public void clear_alarm()
        {
            ActiveResult vret = ActiveResult.Valid(AppConfig.SUCCESS);
            String cREC_ID = StringEx.getString(request["ID"]);
            if (String.IsNullOrWhiteSpace(cREC_ID))
            {
                vret = ActiveResult.Valid("参数传递错误！");
            }
            else
            {
                int iCode = DbManager.ExecSQL(" update XT_IMG_REC SET  ALARM_CHECKED =2 WHERE REC_ID = '" + cREC_ID + "' ");
                vret = ActiveResult.Valid(iCode);
            }

            response.Write(vret.toJSONString());
        }
    }
}
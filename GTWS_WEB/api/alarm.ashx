<%@ WebHandler Language="C#" Class="alarm" %>

using System;
using System.Web;
using System.Data;
using System.Reflection;
using System.Web.SessionState;
using TLKJ.Utils;
using TLKJ.DB;
using TLKJ.DAO;

public class alarm : IHttpHandler
{

    protected HttpRequest Request = null;
    protected HttpResponse Response = null;
    protected HttpSessionState Session = null;

    public void ProcessRequest(HttpContext ctx)
    {
        ctx.Response.ContentType = "text/plain";
        Request = ctx.Request;
        Response = ctx.Response;
        Session = ctx.Session;

        String cActionType = StringEx.getString(Request["action_type"]);
        String cActionMethod = StringEx.getString(Request["action_method"]);

        ActiveResult ret = null;
        if (cActionType.Length == 0)
        {
            log4net.WriteLogFile("参数传递错误:action_type" + cActionType);
            ret = ActiveResult.Valid("参数传递错误:action_type" + cActionType);
            Response.Write(ret.toJSONString());
            return;
        }

        if (cActionMethod.Length == 0)
        {
            log4net.WriteLogFile("参数传递错误:action_method" + cActionMethod);
            ret = ActiveResult.Valid("参数传递错误:action_method" + cActionMethod);
            Response.Write(ret.toJSONString());
            return;
        }

        if (cActionMethod.Equals("query_list"))
        {
            query_list();
        }
    }
    public void query_list()
    {
        XT_IMG_REC_Dao dao = new XT_IMG_REC_Dao();
        String cREC_ID = StringEx.getString(Request["REC_ID"]);

        ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
        DataTable dtInfo = DbManager.QueryData("SELECT CAMERA_ID,PRESET_ID FROM XT_IMG_REC WHERE REC_ID='" + cREC_ID + "'");
        String cCAMERA_ID = StringEx.getString(dtInfo, 0, "CAMERA_ID");
        String cPRESET_ID = StringEx.getString(dtInfo, 0, "PRESET_ID");

        int iPageNo = 1;
        int iPageSize = 5;
        String cWhereParm = " UPLOAD_FLAG=1 AND CAMERA_ID='" + cCAMERA_ID + "' AND PRESET_ID='" + cPRESET_ID + "'";

        String cOrderBy = "ORDER BY REC_ID ASC";
        DBResult dbret = dao.Query("*,(SELECT ADDR FROM XT_CAMERA X WHERE X.DEVICE_ID=CAMERA_ID) AS ADDR ", "XT_IMG_REC T", cWhereParm, cOrderBy, iPageNo, iPageSize);
        DataTable dtRows = dbret.dtrows;
        for (int i = 0; i < dtRows.Rows.Count; i++)
        {
            int iALARM_FLAG = StringEx.getInt(dtRows, i, "ALARM_FLAG");
            int iIMAGE_REDRAW = StringEx.getInt(dtRows, i, "IMAGE_REDRAW");
            if (iALARM_FLAG > 0 && iIMAGE_REDRAW == 0)
            {
                String cID = StringEx.getString(dtRows, i, "REC_ID");
                String cFILE_URL = StringEx.getString(dtRows, i, "FILE_URL");
                if (cID.Equals(cREC_ID))
                {
                    String cFileName = Request.MapPath(cFILE_URL);
                    System.Collections.Generic.List<System.Drawing.Rectangle> KeyList = dao.FindRectangle(cID);
                    AppManager.RedrawImage(cFileName, KeyList);
                }
            }
        }
        int iRowsCount = dbret.ROW_COUNT;
        vret = ActiveResult.Query(dtRows);
        vret.total = iRowsCount;

        Response.Write(vret.toJSONString());
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Text;
using System.Data;
using TLKJ.DB;
using TLKJ.Utils;
using System.Collections;
using System.Collections.Generic;
using TLKJ.WebSys;


public class Handler : IHttpHandler, IRequiresSessionState
{
    private HttpRequest Request;
    private HttpResponse Response;

    public void ProcessRequest(HttpContext ctx)
    {
        Request = ctx.Request;
        Response = ctx.Response;
        ctx.Response.ContentType = "text/plain";
        String cActionMethod = StringEx.getString(Request["action_method"]);
        cActionMethod = cActionMethod.ToUpper();
        ActiveResult vret = new ActiveResult();
        String sql = "";

        if (cActionMethod.Equals("USER_LOGIN"))
        {
            String vUserCount = StringEx.getString(Request["user_id"]);
            String vPassWord = StringEx.getString(Request["user_pass"]);
            sql = "SELECT COUNT(1) FROM S_USER_INF WHERE USER_CODE='" + vUserCount + "' AND USER_PASS = " + vPassWord;
            String sqlValue = DbManager.GetStrValue(sql);
            int iRSCount = StringEx.getInt(sqlValue);
            if (iRSCount > 0)
            {
                HttpCookie vUserInf = new HttpCookie(AppConfig.SESSION_USER_ID, vUserCount);
                ctx.Session[AppConfig.SESSION_USER_ID] = vUserCount;
                Request.Cookies.Add(vUserInf);
            }
            Response.Write(sqlValue);
        }
        else if (cActionMethod.Equals("WLJB_LIST"))
        {
            String cVals = StringEx.getString(Request["vals"]);
            String cORG_ID = Config.GetAppSettings("ORG_ID");
            String cWhereParm = " WHERE (ORG_ID='" + cORG_ID + "') ";
            if (cVals != "" && cVals != null && cVals != "undefined")
            {
                cWhereParm += "  AND (JBTYPE = 'WEB') and a.ID='" + cVals + "'";
            }
            sql = "select a.ID as a_id,a.XIANQU  as a_xianqu,a.ADRESS as a_adress, a.DANWEI as a_danwei "
                + " ,a.NEIRONG as a_neirong,a.PEOPLE as a_people,a.PHONE as a_phone,a.EMAIL as a_email,a.JBTYPE as a_jbtype "
                + " ,a.XMDISC as a_xmdisc,a.TIME as a_time ,a.SD_RESULT as a_sd_reslut, a.FILES_ID as a_files_id,b.URL as url "
                + " ,b.TEXT as texts from XT_JB a left join S_UPLOAD b on CHARINDEX ( Rtrim(b.ID),Rtrim(a.FILES_ID))>0 " + cWhereParm;
            DataTable dt_table = DbManager.QueryData(sql);
            vret = ActiveResult.Query(dt_table);
            Response.Write(vret.toJSONString());
        }
        else if (cActionMethod.Equals("WLJB_CHECK"))
        {
            String cCVal = StringEx.getString(Request["cVal"]);
            String vSelectVal = StringEx.getString(Request["shenheEffect"]);
            sql = "update xt_jb set SD_RESULT='" + vSelectVal + "' where id='" + cCVal + "'";
            int iCode = DbManager.ExecSQL(sql);
            Response.Write(iCode);
        }
        else if (cActionMethod.Equals("WLJB_SAVE"))
        {
            String vArea = StringEx.getString(Request["area"]);
            String vFiles_Id = StringEx.getString(Request["getFiles_Id"]);
            String vBaserAdress = StringEx.getString(Request["baseAdress"]);
            String vSuspectPerson = StringEx.getString(Request["suspectPerson"]);
            String vLinkName = StringEx.getString(Request["linkName"]);
            String vLinkPhone = StringEx.getString(Request["linkPhone"]);
            String vEamil = StringEx.getString(Request["eamil"]);
            String vJbText = StringEx.getString(Request["jbText"]);
            String vXmmc = StringEx.getString(Request["xmmc"]);
            String vOtherDataType = StringEx.getString(Request["ltwsJb"]);
            String vImg_id = StringEx.getString(Request["img_id"]);
            if (vOtherDataType == "")
            {
                vOtherDataType = "WEB";
            }
            else if (vOtherDataType == "ltwsJb")
            {
                vOtherDataType = "lt";
                String cFileID = AutoID.getAutoID();
                vFiles_Id = cFileID;
            }
            String cUrls = StringEx.getString(Request["urls"]);
            String dateNow = DateTime.Now.ToString("yyyy-MM-ddhh:mm:ss");
            String guid = dateNow.Replace("-", "");
            guid = guid.Replace(":", "");
            Random ran = new Random();
            guid = guid + ran.Next(1, 10);
            String cORG_ID = Config.GetAppSettings("ORG_ID");
            string[] delStr = new string[3];
            delStr[0] = "insert into XT_JB(ID,ORG_ID,XIANQU,ADRESS,DANWEI,PEOPLE,PHONE,EMAIL,NEIRONG,JBTYPE,XMDISC,TIME,FILES_ID,SD_RESULT) "
                + " values('" + guid + "','" + cORG_ID + "','" + vArea + "','" + vBaserAdress + "','" + vSuspectPerson
                + "','" + vLinkName + "','" + vLinkPhone + "','" + vEamil + "','" + vJbText + "','" + vOtherDataType + "','" + vXmmc + "','" + dateNow + "','" + vFiles_Id + "','0')";
            if (vOtherDataType == "lt")
            {
                delStr[1] = "insert into S_UPLOAD (ID,TEXT,URL,ORG_ID,RES_ID) VALUES ('" + vFiles_Id + "','蓝天卫士举报'," + cUrls + ",'" + cORG_ID + "','" + vFiles_Id + "')";
                delStr[2] = "update XT_IMAGE_LIST set ISHANDS ='1' where id ='" + vImg_id + "' ";
            }
            int iCode= DbManager.ExecSQL(delStr);
            if (iCode > 0)
            {
                Response.Write(guid);
            }
            else
            {
                int backvalue = -1;
                Response.Write(backvalue);
            }
        }
        else if (cActionMethod.Equals("UNEFFECTCLICK"))
        {
            try
            {
                String vImg_id = StringEx.getString(Request["img_id"]);
                sql = "update XT_IMAGE_LIST set ISHANDS ='1' where id ='" + vImg_id + "' ";
                int iCode= DbManager.ExecSQL(sql);
                Response.Write(iCode);
            }
            catch(Exception ex) {
                 log4net.WriteLogFile("api/handler-uneffectclick-sql" + sql + ex.Message);
            } 
        } 
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
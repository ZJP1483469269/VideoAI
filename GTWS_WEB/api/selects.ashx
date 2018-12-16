<%@ WebHandler Language="C#" Class="selects" %>

using System;
using System.Web;
using TLKJ.DB;
using TLKJ.Utils;
using System.Data;
public class selects : IHttpHandler {

    public DataTable dt_table = null;
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        String sql = "";
        sql = "select ORG_ID,ORG_NAME from S_ORG_INF where  ORG_ID like'4114__' and ORG_TYPE = '6'";
        dt_table =DbManager.QueryData(sql);
        String ret = "";
        ret = JsonLib.ToJSON(dt_table);
        context.Response.Write(ret);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}
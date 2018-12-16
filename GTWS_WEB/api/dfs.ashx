<%@ WebHandler Language="C#" Class="dfs" %>

using System;
using System.Web;
using System.Web.SessionState;
using TLKJ.WebSys;
using System.Text;
using System.Collections;
using TLKJ.DB;
using TLKJ.Utils;
using System.IO;
using System.Collections.Generic;

public class dfs : IHttpHandler, IRequiresSessionState
{
    protected HttpRequest Request = null;
    protected HttpResponse Response = null;
    protected HttpSessionState Session = null;
    public void ProcessRequest(HttpContext ctx)
    {
        try
        {
            ctx.Response.ContentType = "text/plain";
            Request = ctx.Request;
            Response = ctx.Response;
            Session = ctx.Session;

            int iLength = (Request.Files == null) ? 0 : Request.Files.Count;
            string sql = null;
            int iCode = 0;
            if (iLength > 0)
            {
                String cFilePath = Request.Files[0].FileName;
                String cFileExt = System.IO.Path.GetExtension(cFilePath);
                String cRootDir = ctx.Server.MapPath("~/dfs");
                AppConfig.CheckDir(cRootDir);

                String cFileName = Path.GetFileName(cFilePath);
                String cREC_ID = cFileName.Replace(cFileExt, "");
                Request.Files[0].SaveAs(cRootDir + @"\" + cFileName);
                sql = "UPDATE XT_IMG_REC SET UPLOAD_FLAG=1 WHERE REC_ID='" + cREC_ID + "'";
                iCode = DbManager.ExecSQL(sql);
            }

            if (iCode > 0)
            {
                Response.Clear();
                Response.Write(AppConfig.SUCCESS);
                Response.End();
            }
            else
            {
                Response.Clear();
                Response.Write(AppConfig.FAILURE);
                Response.End();
            }
        }
        catch (Exception ex)
        {
            Response.Clear();
            Response.Write(AppConfig.FAILURE);
            Response.End();
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
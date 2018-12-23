<%@ WebHandler Language="C#" Class="dfs" %>

using System;
using System.Web;
using System.Web.SessionState;
using TLKJ.WEB;
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
                String cUploadField = StringEx.getString(Request.QueryString["UPLOAD_FIELD"]);
                String cFileName = Request.Files[0].FileName;
                cFileName = Path.GetFileName(cFileName);
                String cFileExt = System.IO.Path.GetExtension(cFileName);
                String cAbsUrl = ctx.Server.MapPath("~/dfs");
                AppConfig.CheckDir(cFileExt);

                if (File.Exists(cAbsUrl + @"\" + cFileName))
                {
                    try
                    {
                        File.Delete(cAbsUrl + @"\" + cFileName);
                    }
                    catch (Exception ex)
                    {
                        log4net.WriteLogFile(ex.Message);
                    }

                }
                String cREC_ID = cFileName.Replace(cFileExt, "");
                Request.Files[0].SaveAs(cAbsUrl + @"\" + cFileName);

                String cFilePath = cAbsUrl + @"\" + cREC_ID.Substring(0, 8);
                AppConfig.CheckDir(cFilePath);

                if (File.Exists(cFilePath + "\\" + cFileName))
                {
                    try
                    {
                        File.Delete(cFilePath + "\\" + cFileName);
                    }
                    catch (Exception ex)
                    {
                        log4net.WriteLogFile(ex.Message);
                    }
                }

                File.Copy(cAbsUrl + @"\" + cFileName, cFilePath + "\\" + cFileName);
                sql = "UPDATE XT_IMG_REC SET " + cUploadField + "=1 WHERE REC_ID='" + cREC_ID + "'";
                iCode = DbManager.ExecSQL(sql);
                Response.Write(AppConfig.SUCCESS);
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
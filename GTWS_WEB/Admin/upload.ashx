<%@ WebHandler Language="C#" Class="Upload" %>

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

public class Upload : IHttpHandler, IRequiresSessionState
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
        String cResID = StringEx.getSafeQueryString(Request.Params["id"]);
        String cCamera_ID=StringEx.getString(Request["CAMERA_ID"]);
        if(!String.IsNullOrEmpty(cCamera_ID))
        {
            UPLOAD_IMAGE(ctx);
        }
    }
    
    public void UPLOAD_FILE(HttpContext ctx)
    {
        int iLength = (Request.Files == null) ? 0 : Request.Files.Count;
        List<string> sqls = new List<string>();
        String cResID = StringEx.getSafeQueryString(Request.Params["id"]);
        UploadFileInfo vf = new UploadFileInfo();
        if (iLength > 0)
        {
            String cFilePath = "";
            String cFileID = "";
            String cUrl = "";

            for (int i = 0; i < iLength; i++)
            {
                cFilePath = Request.Files[i].FileName;
                String cFileExt = System.IO.Path.GetExtension(cFilePath);

                String cRootDir = ctx.Server.MapPath("~/Upload");
                String cDayNum = DateUtils.getDayNum();
                String cFileDir = cRootDir + Path.DirectorySeparatorChar + cDayNum;
                AppConfig.CheckDir(cFileDir);

                cFileID = AutoID.getAutoID();
                String cFileUrl = cFileDir + "\\" + cFileID + cFileExt.ToLower();
                cUrl = "/Upload/" + cDayNum + "/" + cFileID + cFileExt.ToLower();

                vf.FileName = Path.GetFileName(cFilePath);
                vf.ID = cFileID;
                vf.Url = cUrl;
                if (cResID.Length == 0)
                    vf.ResID = "0";
                else
                    vf.ResID = cResID;

                int iFileLength = Request.Files[i].ContentLength;
                Request.Files[i].SaveAs(cFileUrl);
                //arr.Add(vf);
                if (cResID.Length == 0)
                {
                    sqls.Add("insert into S_UPLOAD(ID,TEXT,ORG_ID,URL,RES_ID) "
                        + " VALUES('" + cFileID + "','" + cFilePath + "','','" + cUrl + "','" + cResID + "')");
                }
                else
                {
                    //   sqls.Add("delete from S_UPLOAD where RES_ID='" + cResID + "'");
                    sqls.Add("insert into S_UPLOAD(ID,TEXT,ORG_ID,URL,RES_ID) VALUES('" + cFileID + "','" + cFilePath + "','','" + cUrl + "','" + cResID + "')");
                }
            }
        }
        int iCode = DbManager.ExeSql(sqls).Code;

        if (iCode > 0)
        {
            String cRetStr = Newtonsoft.Json.JsonConvert.SerializeObject(vf);
            Response.Write(cRetStr);
            Response.End();
        }
        else
        {
            String cRetStr = Newtonsoft.Json.JsonConvert.SerializeObject(vf);
            Response.Write(cRetStr);
            Response.End();
        }
    }
    public void UPLOAD_IMAGE(HttpContext ctx)
    {
        ctx.Response.ContentType = "text/plain";
        Request = ctx.Request;
        Response = ctx.Response;
        Session = ctx.Session;

        int iLength = (Request.Files == null) ? 0 : Request.Files.Count;
        List<string> sqls = new List<string>();

        String cCameraID = StringEx.getSafeQueryString(Request.Params["CAMERA_ID"]);
        String cORG_ID = StringEx.getSafeQueryString(Request.Params["ORG_ID"]);
        //ArrayList arr = new ArrayList();
        UploadFileInfo vf = new UploadFileInfo();
        if (iLength > 0)
        {
            String cFilePath = "";
            String cFileID = "";
            String cUrl = "";

            for (int i = 0; i < iLength; i++)
            {
                cFilePath = Request.Files[i].FileName;
                String cFileExt = System.IO.Path.GetExtension(cFilePath);

                String cRootDir = ctx.Server.MapPath("~/Upload");
                String cDayNum = DateUtils.getDayNum();
                String cFileDir = cRootDir + Path.DirectorySeparatorChar + cDayNum;
                AppConfig.CheckDir(cFileDir);

                cFileID = AutoID.getAutoID();
                String cFileUrl = cFileDir + "\\" + cFilePath;
                cUrl = "/Upload/" + cDayNum + "/" + cFilePath;

                vf.FileName = Path.GetFileName(cFilePath);
                vf.ID = cFileID;
                vf.Url = cUrl;

                vf.ResID = "0";

                int iFileLength = Request.Files[i].ContentLength;
                Request.Files[i].SaveAs(cFileUrl);

                sqls.Add("insert into XT_IMAGE_LIST(ID,CAMERA_ID,ORG_ID,URL,CREATETIME) "
                    + " VALUES('" + cFileID + "','" + cCameraID + "','" + cORG_ID + "','" + cUrl + "','" + cFileID.Substring(0, 14) + "')");
            }
        }
        int iCode = DbManager.ExeSql(sqls).Code;

        if (iCode > 0)
        {
            String cRetStr = Newtonsoft.Json.JsonConvert.SerializeObject(vf);
            Response.Write(cRetStr);
            Response.End();
        }
        else
        {
            String cRetStr = Newtonsoft.Json.JsonConvert.SerializeObject(vf);
            Response.Write(cRetStr);
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
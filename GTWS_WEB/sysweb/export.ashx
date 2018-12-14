<%@ WebHandler Language="C#" Class="export" %>

using System;
using System.Web;
using System.Data;
using System.Text;
using TLKJ.Utils;
using TLKJ.DB;
using TLKJ.DAO;

public class export : IHttpHandler
{
    public void ProcessRequest(HttpContext ctx)
    {
        HttpRequest request = ctx.Request;
        HttpResponse response = ctx.Response;
        ctx.Response.ContentType = "text/plain";
        ctx.Response.Buffer = true;
        ctx.Response.Clear();
        ctx.Response.ContentType = "application/download";
        string downFile = System.IO.Path.GetFileName("Export.xml");             //这里也可以随便取名
        string EncodeFileName = HttpUtility.UrlEncode(downFile, System.Text.Encoding.UTF8);//防止中文出现乱码
        ctx.Response.AddHeader("Content-Disposition", "attachment;filename=" + EncodeFileName + ";");

        String cKeyID = StringEx.getString(request["IDS"]);
        String cTABLE_ID = StringEx.getString(request["TABLE_ID"]);
        string[] KeyList = cKeyID.Split(',');
        StringBuilder sb = new StringBuilder();
        String cWhereParm = "";
        for (int i = 0; i < KeyList.Length; i++)
        {
            if (i == 0)
                sb.Append("'" + KeyList[i] + "'");
            else
                sb.Append(cWhereParm + ",'" + KeyList[i] + "'");
        }
        if (sb.Length > 0)
        {
            cWhereParm = " ID IN (" + sb.ToString() + ")";
        }

        String cAppDir = ctx.Server.MapPath("~/");
        DataTable dtRows = DbManager.QueryData("*", "xt_jb", cWhereParm);
        String JBList = TLKJ.DAO.PackUtil.Pack(cTABLE_ID, dtRows, cAppDir, "WEB");
        ctx.Response.Write(JBList);                                    //返回文件数据给客户端下载
        ctx.Response.Flush();
        ctx.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
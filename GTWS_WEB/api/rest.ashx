<%@ WebHandler Language="C#" Class="rest" %>

using System;
using System.Web;
using System.Reflection;
using System.Web.SessionState;
using TLKJ.Utils;
using TLKJ.DAO;

public class rest : IHttpHandler, IRequiresSessionState
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
             log4net.WriteLogFile("参数传递错误:action_method" + cActionType);
            ret = ActiveResult.Valid("参数传递错误:action_method" + cActionType);
            Response.Write(ret.toJSONString());
            return;
        }
        Assembly assembly = Assembly.GetExecutingAssembly();
        String cActionClass = "TLKJ.DAO." + cActionType + "Service"; 
        Type ActionType = assembly.GetType(cActionClass);
        Assembly asm = Assembly.Load("TLKJ.DAO");
        ActionType = asm.GetType(cActionClass);
        if (ActionType == null)
        {
             log4net.WriteLogFile("未找到对应的类" + cActionClass + "错误!");
            ret = ActiveResult.Valid("未找到对应的类" + cActionClass + "错误!");
            Response.Write(ret.toJSONString());
            return;
        }
        System.Reflection.MethodInfo vMethod = null;
        Object obj = null;
        try
        {
            obj = asm.CreateInstance(cActionClass, true);
            vMethod = obj.GetType().GetMethod("init");
            object[] ParmList = { Request, Response };
            vMethod.Invoke(obj, ParmList);
        }
        catch (Exception ex)
        {
            ret = ActiveResult.Valid("未找到对应初始化方法inits错误!");
            Response.Write(ret.toJSONString());
            return;
        }
        try
        {
            vMethod = obj.GetType().GetMethod(cActionMethod);
            vMethod.Invoke(obj, null);
        }
        catch (Exception ex)
        {
             log4net.WriteLogFile(cActionType + "未找到对应 " + cActionMethod + "方法错误!");
            ret = ActiveResult.Valid(cActionType + "未找到对应 " + cActionMethod + "方法错误!");
            return;
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
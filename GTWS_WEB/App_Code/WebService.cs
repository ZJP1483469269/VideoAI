using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using TLKJ.DB;
using System.Data;

/// <summary>
///WebService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class WebDB : System.Web.Services.WebService
{

    public WebDB()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string GetStrValue(String vSQL)
    {
        return DbManager.GetStrValue(vSQL);
    }

    [WebMethod]
    public DataTable QueryData(String vSQL)
    {
        return DbManager.QueryData(vSQL);
    }

    [WebMethod]
    public DBResult Query(String cFileList, String cTableName, String cWhereParm, String cOrderBy, int iPageNo, int iPageSize)
    {
        return DbManager.Query(cFileList, cTableName, cWhereParm, cOrderBy, iPageNo, iPageSize);
    }

    [WebMethod]
    public int ExecSQL(List<String> sqls, List<Object[]> ParmList)
    {
        return DbManager.ExecSQL(sqls, ParmList);
    } 
}

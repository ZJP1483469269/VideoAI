using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLKJ.WebSys;
using TLKJ.Utils;
using System.Data;
using TLKJ.DB;
using TLKJ.DAO;

public partial class gtws_Default : PageEx
{
    public String cHOST;
    public String cPORT;
    public String cUSER;
    public String cPASS;
    public String ORG_ID;
    public String ROOT_LIST;
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogin();
        LoginUserInfo vUserInfo = getLoginUserInfo();

        DBConfig vConf = new DBConfig();
        ORG_ID = vUserInfo.ORG_ID;
        cHOST = vConf.getOrgKey(ORG_ID, "GTWS_HOST");
        cPORT = vConf.getOrgKey(ORG_ID, "GTWS_PORT");
        cUSER = vConf.getOrgKey(ORG_ID, "GTWS_USER");
        cPASS = vConf.getOrgKey(ORG_ID, "GTWS_PASS");
        XT_CAMERA_Dao dao = new XT_CAMERA_Dao();

        DataTable CountyList = dao.QueryData("DISTINCT ORG_ID,COUNTY", "ORG_ID LIKE '" + AppManager.getAreaHeader(ORG_ID) + "%'", " ORDER BY ORG_ID");
        List<Dictionary<String, string>> KeyList = new List<Dictionary<string, string>>();

        DataTable VillageList = dao.QueryData("DISTINCT VILLAGE_ID,VILLAGE,ORG_ID", "ORG_ID LIKE '" + AppManager.getAreaHeader(ORG_ID) + "%'", " ORDER BY ORG_ID");

        List<string> ORG_LIST = new List<string>();
        for (int i = 0; i < CountyList.Rows.Count; i++)
        {
            String cID = StringEx.getString(CountyList, i, "ORG_ID");
            String cCOUNTY = StringEx.getString(CountyList, i, "COUNTY");
            Dictionary<string, string> vo = new Dictionary<string, string>();
            vo.Add("id", cID);
            vo.Add("name", cCOUNTY);
            vo.Add("value", cID);
            vo.Add("pid", "0");
            vo.Add("isParent", "true");
            KeyList.Add(vo);
        }
        ROOT_LIST = JsonLib.toJSONString(KeyList);
    }
}
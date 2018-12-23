using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLKJ.WEB;
using TLKJ.Utils;

public partial class Default2 : PageEx
{
    public String cHOST;
    public String cPORT;
    public String cUSER;
    public String cPASS;
    public String ORG_ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        //CheckLogin();
        LoginUserInfo vUserInfo = getLoginUserInfo();

        DBConfig vConf = new DBConfig();
        ORG_ID = vUserInfo.ORG_ID;
        cHOST = vConf.getOrgKey(ORG_ID, "GTWS_HOST");
        cPORT = vConf.getOrgKey(ORG_ID, "GTWS_PORT");
        cUSER = vConf.getOrgKey(ORG_ID, "GTWS_USER");
        cPASS = vConf.getOrgKey(ORG_ID, "GTWS_PASS");
    }
}
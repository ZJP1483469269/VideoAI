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
    public String DEVICE_ID = "";
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
        String cKeyID = StringEx.getString(Request.QueryString["ID"]);
        if (cKeyID.Length > 0)
        {
            XT_CAMERA vInfo = dao.FindOne(cKeyID);
            DEVICE_ID = vInfo.device_id;
        }
    }
}
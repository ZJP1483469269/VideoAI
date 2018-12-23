using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLKJ.WEB;
using TLKJ.Utils;
using System.Data;
using TLKJ.DB;
using TLKJ.DAO;

public partial class Player : PageEx
{
    public String cHOST;
    public String cPORT;
    public String cUSER;
    public String cPASS;
    public String ORG_ID;
    public String DEVICE_ID;
    public XT_CAMERA Info = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        //CheckLogin();
        LoginUserInfo vUserInfo = getLoginUserInfo();

        String cKEYID = StringEx.getString(Request.QueryString["ID"]);
        DBConfig vConf = new DBConfig();
        ORG_ID = vUserInfo.ORG_ID;
        cHOST = vConf.getOrgKey(ORG_ID, "GTWS_HOST");
        cPORT = vConf.getOrgKey(ORG_ID, "GTWS_PORT");
        cUSER = vConf.getOrgKey(ORG_ID, "GTWS_USER");
        cPASS = vConf.getOrgKey(ORG_ID, "GTWS_PASS");
        if (!string.IsNullOrEmpty(cKEYID))
        {
            XT_CAMERA_Dao dao = new XT_CAMERA_Dao();
            Info = dao.FindOne(cKEYID);
            DEVICE_ID = Info.device_id;
        }
    }
}
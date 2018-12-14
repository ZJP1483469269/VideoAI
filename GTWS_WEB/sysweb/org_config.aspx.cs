using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLKJ.WebSys;
using TLKJ.Utils;
using System.Data;
using TLKJ.DB;

public partial class sysweb_org_config : PageEx
{
    public DataTable dtRows = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogin();
        if (!Page.IsPostBack)
        {
            LoginUserInfo vUserInf = getLoginUserInfo();
            //XM
            StringBuilder sql = new StringBuilder();
            sql.Append(" INSERT INTO XT_ORG_CONFIG(ORG_ID,KEYNAME,KEYVALUE) ");
            sql.Append(" SELECT '" + vUserInf.ORG_ID + "',KEYNAME,DEFVAL ");
            sql.Append(" FROM  S_CONSTANT T ");
            sql.Append(" WHERE NOT EXISTS(SELECT 1 FROM XT_ORG_CONFIG  X WHERE T.KEYNAME= X.KEYNAME) ");
            DbManager.ExecSQL(sql.ToString());
            dtRows = DbManager.QueryData("SELECT A.*,B.DEFVAL,KEYDESC,TYPE_ID FROM XT_ORG_CONFIG A,S_CONSTANT B WHERE A.KEYNAME=B.KEYNAME AND A.ORG_ID='" + vUserInf.ORG_ID + "'");
        }
    }
}
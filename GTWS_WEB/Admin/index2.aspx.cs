using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls; 
using TLKJ.Utils;
using TLKJ.DB;
using TLKJ.DAO;
using System.Data;
using TLKJ.Core;

public partial class Admin_index2 : PageEx
{
    public S_NODES_Dao dao = new S_NODES_Dao();
    public DataTable dtRows;
    public DataRow[] drHeader;
    public String cOrg_Full_Name;
    public String cUser_Name;
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogin();
        LoginUserInfo vUserInf = getLoginUserInfo();
        String cUserCode = vUserInf.USER_CODE;
        if (Session["TOKEN"] == null)
        {
            this.usercode.Value = cUserCode;
            long cDayTime = DateUtils.getDayTimeNum();
            String cToken = MDUtil.Get32MD5(cUserCode + cDayTime);
            List<String> sqls = new List<string>();
            sqls.Add("DELETE FROM S_TOKEN WHERE USER_ID='" + cUserCode + "'");
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            sql.Append("INSERT INTO S_TOKEN(user_id,token,create_date) ");
            sql.Append(" VALUES('" + cUserCode + "','" + cToken + "','" + cDayTime + "')");
            sqls.Add(sql.ToString());
            DbManager.ExecSQL(sqls);
            Session["TOKEN"] = cToken;
        }
        this.usercode.Value = cUserCode;
        this.token.Value = StringEx.getString(Session["TOKEN"]);
        this.orgid.Value = vUserInf.ORG_ID;

        drHeader = dao.QueryMenuItem("0");
        PageEx vPage = (PageEx)Page;
        cOrg_Full_Name = vUserInf.ORG_FULL_NAME;
        cUser_Name = vUserInf.USER_NAME;

        log4net.WriteLogFile("TOKEN的值是" + Session["TOKEN"]);
    }
}
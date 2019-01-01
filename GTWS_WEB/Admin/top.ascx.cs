using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TLKJ.Utils;
using System.Text;
using TLKJ.DB;
using TLKJ.DAO;
using TLKJ.WEB;

public partial class Admin_top : System.Web.UI.UserControl
{
    public S_NODES_Dao dao = new S_NODES_Dao();
    public DataTable dtRows;
    public String cOrg_Full_Name;
    public String cDEF_ID;
    public String cUser_Name;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageEx vPage = (PageEx)Page;
        vPage.CheckLogin();

        LoginUserInfo vUserInf = vPage.getLoginUserInfo();
        cOrg_Full_Name = vUserInf.ORG_FULL_NAME;
        cUser_Name = vUserInf.USER_NAME;
        cDEF_ID = vPage.getClientID(); 
        String cRole_ID = vUserInf.ROLE_ID;
        String cTypeID = "0";
        dtRows = dao.QueryList(cRole_ID, cTypeID);

    }
}
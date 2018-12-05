using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLKJ.DAO;
using System.Data;
using TLKJ.WebSys;
using TLKJ.Utils;
using System.Text;
using TLKJ.DB;

public partial class Admin_top : System.Web.UI.UserControl
{
    public S_NODES_Dao dao = new S_NODES_Dao();
    public DataTable dtRows;
    public String cOrg_Full_Name;
    public String cUser_Name;
    protected void Page_Load(object sender, EventArgs e)
    {
        String cTypeID = "0";
        dtRows = dao.QueryList(cTypeID);
        PageEx vPage = (PageEx)Page;
        LoginUserInfo vUserInf = vPage.getLoginUserInfo();
        cOrg_Full_Name = vUserInf.ORG_FULL_NAME;
        cUser_Name = vUserInf.USER_NAME;
       
    }    
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TLKJ.DB;
using TLKJ.WEB;
using TLKJ.Utils;
using TLKJ.DAO;

public partial class webgis : PageEx
{
    public Double X;
    public Double Y;
    public String ORG_ID;
    public DataTable dtRows = null;
    public String TYPE_ID;
    public LoginUserInfo UserInfo = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogin();
        UserInfo = getLoginUserInfo();
        ORG_ID = UserInfo.ORG_ID;

        String ID = StringEx.getString(Request.QueryString["ID"]);
        if (ID.Length > 0)
        {
            UrlMessage vMSG = null;
            try
            {
                vMSG = Newtonsoft.Json.JsonConvert.DeserializeObject<UrlMessage>(ID);
            }
            catch
            {

            }
            if (vMSG != null)
            {
                XT_CAMERA_Dao dao = new XT_CAMERA_Dao();
                String cCameraID = vMSG.Code;
                XT_CAMERA vo = dao.FindOne(cCameraID);
                double cX = vo.x;
                double cY = vo.y;
                if ((cX > 0) && (cY > 0))
                {
                    X = cX;
                    Y = cY;
                }
            }
        }

        //LoginUserInfo vUserInf = this.getLoginUserInfo();
        //X = vUserInf.X;
        //Y = vUserInf.Y;
        TYPE_ID = StringEx.getString(Request.QueryString["TYPE_ID"]);
        if (TYPE_ID.Length > 0)
        {
            String cX = StringEx.getString(Request.QueryString["X"]);
            String cY = StringEx.getString(Request.QueryString["Y"]);
            if ((cX.Length > 0) && (cY.Length > 0))
            {
                X = StringEx.GetDouble(cX);
                Y = StringEx.GetDouble(cY);
            }
        }
    }
}
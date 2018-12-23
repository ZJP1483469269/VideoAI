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

public partial class VideoAgent : PageEx
{
    public DataTable dtRows = null;
    public String ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogin();
        ID = StringEx.getString(Request.QueryString["ID"]);
        LoginUserInfo vUserInfo = getLoginUserInfo();
        String cUSER_ID = vUserInfo.USER_ID;
        String cORG_ID = vUserInfo.ORG_ID;
        String cDayTime = StringEx.getString(DateUtils.getDayTimeNum());
        String cKeyID = cUSER_ID + "@" + cORG_ID + "@" + cDayTime + "@" + ID;
        String cKeyMD = Base64.StrToBase64(cKeyID);
        ActiveMQ_Message vMessage = new ActiveMQ_Message();
        vMessage.MessageType = ActiveMQ_MessageType.VIDEO_LIVE;
        Dictionary<string, string> voConf = new Dictionary<string, string>();
        voConf.Add("ID", ID);
        voConf.Add("ORG_ID", cORG_ID);
        voConf.Add("USER_ID", cUSER_ID);
        voConf.Add("DAYTIME", cDayTime);
        ID = cKeyMD;
        vMessage.MESSAGE = JsonLib.ToJSON(voConf);
        //ActiveMQ_Producer.SendMessage(cORG_ID, cUSER_ID, vMessage);
    }
}
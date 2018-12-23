using Gecko;
using Gecko.JQuery;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using TLKJ.Utils;
using TLKJ_IVS;

namespace GTWS_TASK.UI
{
    [ComVisibleAttribute(true)]
    public partial class frmMain : frmWeb
    {
        private bool LOAD_LAST_FLAG = false;
        private bool LOAD_TOKEN_FLAG = false;
        private bool IS_HOME = false;
        private readonly string xulrunnerPath = Application.StartupPath + "/xulrunner";
        public frmMain()
        {
            InitializeComponent();
            Xpcom.Initialize(xulrunnerPath);
        }
        public void OpenWinUrl(String vUrl)
        {
            OpenUrl(vUrl);
        }
        private void frmWeb_Load(object sender, EventArgs e)
        {
            geckoWebBrowser1.Dock = DockStyle.Fill;

            geckoWebBrowser1.AddMessageEventListener("OpenWinUrl", OpenWinUrl);

            String cWeb = Config.GetAppSettings("WEB_URL");
            String cUrl = cWeb + "Admin/login.aspx";
            String cORG_ID = INIConfig.ReadString("Config", "ORG_ID");
            if (cUrl.IndexOf("?") > -1)
            {
                cUrl = cUrl + "&ORG_ID=" + cORG_ID;
            }
            else
            {
                cUrl = cUrl + "?ORG_ID=" + cORG_ID;
            }
            this.geckoWebBrowser1.Navigate(cUrl);

            JQueryExecutor ctx = new Gecko.JQuery.JQueryExecutor(geckoWebBrowser1.Window);  //先获取到jquery对象
        }

        private void geckoWebBrowser1_DocumentCompleted(object sender, Gecko.Events.GeckoDocumentCompletedEventArgs e)
        {
            String cUrl = geckoWebBrowser1.Url.AbsoluteUri;
            if (!LOAD_LAST_FLAG)
            {
                if (cUrl.IndexOf("login") > 0)
                {
                    WebJS js = new WebJS(geckoWebBrowser1.Document);
                    js.setFieldValue("user_id", INIConfig.ReadString("System", "UserID"));
                    js.setFieldValue("user_pass", INIConfig.ReadString("System", "UserPass"));
                    LOAD_LAST_FLAG = true;
                }
            }

            if ((LOAD_LAST_FLAG) && (!LOAD_TOKEN_FLAG))
            {
                if (cUrl.IndexOf("index") > 0)
                {
                    WebJS js = new WebJS(geckoWebBrowser1.Document);
                    string cUserCode = js.getFieldValue("usercode");
                    ApplicationEvent.setUserCode(cUserCode);
                    // ApplicationEvent.UserInfo.USER_CODE = cUserCode;

                    string cOrgID = js.getFieldValue("orgid");
                    //ApplicationEvent.UserInfo.ORG_ID = cOrgID;

                    string cToken = js.getFieldValue("token");
                    ApplicationEvent.Token = cToken;
                    LOAD_TOKEN_FLAG = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmTake vDialog = new frmTake();
            try
            {
                if (vDialog.ShowDialog() == DialogResult.OK)
                {

                }
            }
            finally
            {
                vDialog.Close();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TLKJ.Utils;
using TLKJ_IVS;

namespace GTWS_TASK.UI
{
    public partial class frmBase : Form
    {
        public frmBase()
        {
            InitializeComponent();
        }
        public void OpenWinUrl(String vUrl)
        {
            String cWeb = Config.GetAppSettings("WEB_URL");
            String cUrl = cWeb + vUrl;
            if (cUrl.IndexOf("?") == -1)
            {
                cUrl = cUrl + "?TOKEN=" + ApplicationEvent.Token;
            }
            else
            {
                cUrl = cUrl + "&TOKEN=" + ApplicationEvent.Token;
            }
            NameValueCollection QueryString = AppManager.ParseUrl(cUrl);
            int iTYPEID = StringEx.getInt(QueryString.Get("TYPEID"));
            String cCameraCode = StringEx.getString(QueryString.Get("CAMERACODE"));
            Form vDialog = null;
            if (iTYPEID == 1)
            {
                frmMain vItem = new frmMain();
                vItem.geckoWebBrowser1.Navigate(cUrl);
                vDialog = vItem;
            }
            else if (iTYPEID == 2)
            {
                //frmMain vItem = new frmMain();
                //vDialog = vItem;
            }
            else if (iTYPEID == 3)
            {
                frmViewer vItem = new frmViewer();
                vItem.ActiveCameraCode = cCameraCode;
                vDialog = vItem;
            }
            else if (iTYPEID == 11)
            {
                frmIEWeb vItem = new frmIEWeb();
                vItem.webBrowser1.Navigate(cUrl);
                vDialog = vItem;
            }

            if (vDialog != null)
            {
                log4net.WriteLogFile(cUrl);
                vDialog.ShowInTaskbar = false;

                vDialog.ShowDialog();
            }
        }
        private void frmBase_Load(object sender, EventArgs e)
        {

        }
    }
}

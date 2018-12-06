using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web; 
using TLKJ.Utils; 
using System.IO;
using TLKJ.DB;
using GTWS_TASK.UI;
using TLKJ.WebSys;

namespace TLKJ_IVS.UI
{
    public partial class frmXS : frmBase
    {
        public string CameraID;
        public string PHOTO_FILENAME;
        public frmXS()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            String cUrl = Config.GetAppSettings(AppConfig.WEB_URL) + "api/rest.ashx";
            Dictionary<String, String> vParmList = new Dictionary<string, string>();
            vParmList.Add("action_type", "WFJB");
            vParmList.Add("action_method", "GTWS");
            vParmList.Add("open_id", CameraID);
            //vParmList.Add("org_id", ApplicationEvent.UserInfo.ORG_ID);
            vParmList.Add("address", txtAddress.Text);
            vParmList.Add("content", txtContent.Text);
            String[] FileList = new string[1];
            FileList[0] = PHOTO_FILENAME;

            HttpUtil vTool = new HttpUtil();
            string cValue = vTool.HttpPost(cUrl, vParmList, FileList);
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            if (!String.IsNullOrEmpty(cValue))
            {
                vret = Newtonsoft.Json.JsonConvert.DeserializeObject<ActiveResult>(cValue);
            }

            if (vret.result == AppConfig.SUCCESS)
            {
                MessageBox.Show("数据保存成功！");
                this.Close();
            }
            else
            {
                MessageBox.Show("数据保存失败，稍后请重试！");
            }
        }
    }
}

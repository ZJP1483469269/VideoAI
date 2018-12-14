using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using TLKJ.DB;
using TLKJ.Utils;
using TLKJ.DAO;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Web.UI;

namespace TLKJ.WebSys
{
    /// <summary>
    /// PageEx 的摘要说明
    /// </summary>
    public class PageEx : System.Web.UI.Page
    {
        String cPageSize;
        String cPageNo;
        public int iPageSize;
        public int iPageNo;
        public int getOrgType()
        {
            LoginUserInfo vUserInf = getLoginUserInfo();
            String cGXQID = vUserInf.ORG_ID;
            return getOrgID(cGXQID).Length;
        }

        public string getOrgID(String cGXQ_ID)
        {
            return cGXQ_ID.Replace("00", "");
        }

        public string getOrgID()
        {
            LoginUserInfo vUserInf = getLoginUserInfo();
            return vUserInf.ORG_ID;
        }

        public LoginUserInfo getLoginUserInfo()
        {
            String cUserCode = null;
            if (Config.GetAppSettings("IS_DEBUG").Equals("1"))
            {
                cUserCode = "admin";
            }
            HttpCookie vkUser = Request.Cookies[AppConfig.SESSION_USER_ID];
            if (vkUser != null)
            {
                cUserCode = StringEx.getString(vkUser.Value);
            }
            if (String.IsNullOrEmpty(cUserCode))
            {
                cUserCode = StringEx.getString(Session[AppConfig.SESSION_USER_ID]);
            }
            if (String.IsNullOrEmpty(cUserCode))
            {
                String cToken = StringEx.getString(Request["TOKEN"]);
                if (!String.IsNullOrEmpty(cToken))
                {
                    cUserCode = DbManager.GetStrValue("SELECT USER_ID FROM S_TOKEN WHERE TOKEN='" + cToken + "'");
                }
            }
            if (!String.IsNullOrEmpty(cUserCode))
            {
                try
                {
                    DataTable dtRows = DbManager.QueryData("SELECT * FROM XT_USER WHERE USERCODE='" + cUserCode + "'");
                    LoginUserInfo vUser = new LoginUserInfo();
                    vUser.USER_CODE = cUserCode;
                    vUser.ORG_ID = StringEx.getString(dtRows, 0, "ORG_ID");
                    vUser.USER_NAME = StringEx.getString(dtRows, 0, "USERNAME");
                    dtRows = DbManager.QueryData("SELECT ORG_NAME,ORG_FULL_NAME,X,Y FROM XT_ORG WHERE ORG_ID='" + vUser.ORG_ID + "'");
                    vUser.ORG_NAME = StringEx.getString(dtRows, 0, "ORG_NAME");
                    vUser.ORG_FULL_NAME = StringEx.getString(dtRows, 0, "ORG_FULL_NAME");
                    vUser.X = StringEx.GetDouble(dtRows, 0, "X");
                    vUser.Y = StringEx.GetDouble(dtRows, 0, "Y");
                    vUser.APPID = DbManager.GetStrValue("SELECT WX_APPID FROM XT_ORG_WX_CONFIG WHERE ORG_ID='" + vUser.ORG_ID + "'");
                    vUser.WX_ID = DbManager.GetStrValue("SELECT WX_ID FROM XT_ORG_WX_CONFIG WHERE ORG_ID='" + vUser.ORG_ID + "'");
                    return vUser;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        //public LoginUserInfo getLoginUserInfo()
        //{
        //    HttpCookieCollection cks = Request.Cookies;
        //    HttpCookie ckUser = cks.Get(AppConfig.SESSION_USER_INF);
        //    if (ckUser != null)
        //    {
        //        String cLoginVal = ckUser.Value;
        //        try
        //        {
        //            cLoginVal = HttpUtility.UrlDecode(cLoginVal);
        //            LoginUserInfo vUser = (LoginUserInfo)JsonConvert.DeserializeObject<LoginUserInfo>(cLoginVal);
        //            return vUser;
        //        }
        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public void AlterThenGo(String cMessage, String cHref)
        {
            ClientScriptManager cs = this.ClientScript;
            cs.RegisterStartupScript(this.GetType(), "ALTER_MESSAGE", "<script>alert('" + cMessage + "');self.location='" + cHref + "';</script>");
        }

        public void AlterThenScript(String cMessage, String cScript)
        {
            ClientScriptManager cs = this.ClientScript;
            cs.RegisterStartupScript(this.GetType(), "ALTER_MESSAGE", "<script>alert('" + cMessage + "');" + cScript + ";</script>");
        }

        public void Alter(String cMessage)
        {
            ClientScriptManager cs = this.ClientScript;
            cs.RegisterStartupScript(this.GetType(), "ALTER_MESSAGE", "<script>alert('" + cMessage + "');</script>");
        }

        public void readQueryString()
        {
            cPageSize = StringEx.getString(Request.QueryString["__PAGE_SIZE"]);
            cPageNo = StringEx.getString(Request.QueryString["__PAGE_NO"]);
            iPageSize = StringEx.getInt(cPageSize, 15);
            iPageNo = StringEx.getInt(cPageNo, 1);
        }
        public void CheckLogin()
        {
            Boolean isLogin = false;
            if (Request != null)
            {
                HttpCookie ckUser = Request.Cookies.Get(AppConfig.SESSION_USER_INF);
                String cUserCode = "";
                if (ckUser != null)
                {
                    cUserCode = StringEx.getString(ckUser.Value);
                }

                if (String.IsNullOrEmpty(cUserCode))
                {
                    cUserCode = StringEx.getString(Session[AppConfig.SESSION_USER_ID]);
                }

                if (String.IsNullOrEmpty(cUserCode))
                {
                    String cToken = StringEx.getString(Request["TOKEN"]);
                    if (!String.IsNullOrEmpty(cToken))
                    {
                        cUserCode = DbManager.GetStrValue("SELECT USER_ID FROM S_TOKEN WHERE TOKEN='" + cToken + "'");
                    }
                }
                isLogin = !string.IsNullOrEmpty(cUserCode);
            }

            if (!isLogin)
            {
                Response.Redirect("/Admin/Logoff.aspx");
            }
        }
        public List<String> getUploadSQL(List<UploadFileInfo> FileList)
        {
            StringBuilder sql = new StringBuilder();
            List<String> sqls = new List<string>();
            for (int i = 0; i < FileList.Count; i++)
            {
                UploadFileInfo vUpload = FileList[i];
                sql.Clear();
                sql.Append(" INSERT INTO S_UPLOAD(ID,TEXT,URL,ORG_ID,RES_ID,ISUPLOAD) ");
                sql.Append(" VALUES('" + vUpload.ID + "','" + vUpload.FileName + "','" + vUpload.Url + "','" + vUpload.ORG_ID + "','" + vUpload.ResID + "',0)");
                sqls.Add(sql.ToString());
            }
            return sqls;
        }

        public List<UploadFileInfo> getUploadList(String cOrgID, String cDir)
        {
            List<UploadFileInfo> KeyList = new List<UploadFileInfo>();
            if (String.IsNullOrEmpty(cDir))
            {
                cDir = "Upload";
            }
            List<String> sqls = new List<string>();
            String cFileName;
            String cFileUrl;
            String cKeyID = AutoID.getAutoID();

            String cAppDir = Server.MapPath("~/");
            String cFileDir = cAppDir + cDir + "\\";
            if (!Directory.Exists(cFileDir))
            {
                Directory.CreateDirectory(cFileDir);
            }
            cFileDir = cFileDir + cKeyID.Substring(0, 8) + "\\";
            if (!Directory.Exists(cFileDir))
            {
                Directory.CreateDirectory(cFileDir);
            }
            for (int i = 0; i < Request.Files.Count; i++)
            {
                UploadFileInfo vUpload = new UploadFileInfo();
                HttpPostedFile vf = Request.Files[i];
                cFileName = Path.GetFileName(vf.FileName);
                String cFileExt = Path.GetExtension(vf.FileName);

                vf.SaveAs(cFileDir + cKeyID + cFileExt);
                cFileUrl = "/" + cDir + "/" + cKeyID.Substring(0, 8) + "/" + cKeyID + cFileExt;
                vUpload.FileName = cFileName;
                vUpload.ID = cKeyID;
                vUpload.ORG_ID = cOrgID;
                vUpload.ResID = cKeyID;
                vUpload.Url = cFileUrl;
                KeyList.Add(vUpload);

            }
            return KeyList;
        }
    }
}
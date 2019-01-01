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
using System.Web.UI.WebControls;

namespace TLKJ.WEB
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

        public void FillDropList(ListControl org_id, DataTable dtRows, String cKeyField, String cValField)
        {

        }
        public String getClientID()
        {
            string DEF_ID;
            String cORG_ID = StringEx.getString(Request.QueryString["ORG_ID"]);
            if (String.IsNullOrWhiteSpace(cORG_ID))
            {
                LoginUserInfo vUserInf = getLoginUserInfo();
                if (vUserInf != null)
                {
                    cORG_ID = vUserInf.ORG_ID;
                }
            }

            if (!String.IsNullOrWhiteSpace(cORG_ID))
            {
                if (cORG_ID.Length > 0)
                {
                    DEF_ID = cORG_ID.Substring(0, 6);
                }
                else
                {
                    DEF_ID = cORG_ID;
                }
            }
            else
            {
                DEF_ID = "8641";
            }
            return DEF_ID;
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
            String cUSER_ID = getUserID();
            if (!String.IsNullOrEmpty(cUSER_ID))
            {
                try
                {
                    DataTable dtRows = DbManager.QueryData("SELECT * FROM S_USER_INF WHERE USER_ID='" + cUSER_ID + "'");
                    LoginUserInfo vUser = new LoginUserInfo();
                    vUser.USER_CODE = StringEx.getString(dtRows, 0, "USER_CODE");
                    vUser.ORG_ID = StringEx.getString(dtRows, 0, "ORG_ID");
                    vUser.USER_NAME = StringEx.getString(dtRows, 0, "USER_NAME");
                    vUser.ROLE_ID = StringEx.getString(dtRows, 0, "ROLE_ID");
                    dtRows = DbManager.QueryData("SELECT ORG_NAME,ORG_FULL_NAME,X,Y FROM S_ORG_INF WHERE ORG_ID='" + vUser.ORG_ID + "'");
                    vUser.ORG_NAME = StringEx.getString(dtRows, 0, "ORG_NAME");
                    vUser.ORG_FULL_NAME = StringEx.getString(dtRows, 0, "ORG_FULL_NAME");
                    vUser.X = StringEx.GetDouble(dtRows, 0, "X");
                    vUser.Y = StringEx.GetDouble(dtRows, 0, "Y");
                    DBConfig vConf = new DBConfig();
                    vUser.APPID = vConf.getOrgKey(vUser.ORG_ID, "WX_APPID");
                    vUser.WX_ID = vConf.getOrgKey(vUser.ORG_ID, "WX_ID");
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

        public String getUserID()
        {
            String cUSER_ID = null;
            if (Request != null)
            {
                HttpCookie ckUser = Request.Cookies.Get(AppConfig.SESSION_USER_INF);

                if (ckUser != null)
                {
                    cUSER_ID = StringEx.getString(ckUser.Value);
                }

                if (String.IsNullOrEmpty(cUSER_ID))
                {
                    cUSER_ID = StringEx.getString(Session[AppConfig.SESSION_USER_ID]);
                }
                else
                {
                    return cUSER_ID;
                }

                if (String.IsNullOrEmpty(cUSER_ID))
                {
                    String cToken = StringEx.getString(Request["TOKEN"]);
                    if (!String.IsNullOrEmpty(cToken))
                    {
                        cUSER_ID = DbManager.GetStrValue("SELECT USER_ID FROM S_TOKEN WHERE TOKEN='" + cToken + "'");
                    }
                }
            }
            return cUSER_ID;
        }

        public void CheckLogin()
        {
            String cUSER_ID = getUserID();
            Boolean isLogin = !string.IsNullOrEmpty(cUSER_ID);
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
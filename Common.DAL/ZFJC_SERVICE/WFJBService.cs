using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TLKJ.Utils;
using TLKJ.DB;
using System.Data;
using System.IO;
using System.Web.UI;

namespace TLKJ.DAO
{
    public class WFJBService : IService
    {
        XT_JB_Dao dao = new XT_JB_Dao();
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
            readQueryString();
        }
        public void saves()
        {
            ActiveResult vret = new ActiveResult();
            XT_JB vo = new XT_JB();
            String cKeyID = StringEx.getString(request[AppConfig.__DBKEY]);
            vo = (XT_JB)RequestUtil.readFromRequest(request, vo);
            int iCode = dao.save(vo, cKeyID);
            vret = ActiveResult.Valid(iCode);
            response.Write(vret.toJSONString());
        }

        public void query()
        {
            String cORG_ID = Config.GetAppSettings("ORG_ID");
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cWhereParm = " (ORG_ID ='" + cORG_ID + "') ";
            String cSTART_DAY = StringEx.getString(request["START_DAY"]);
            String cFINISH_DAY = StringEx.getString(request["FINISH_DAY"]);

            if (!String.IsNullOrEmpty(cSTART_DAY))
            {
                cWhereParm = cWhereParm + " AND TIME>='" + cSTART_DAY + "'";
            }

            if (!String.IsNullOrEmpty(cFINISH_DAY))
            {
                cWhereParm = cWhereParm + " AND TIME<='" + cFINISH_DAY + "'";
            }
            String cOrderBy = "ORDER BY ID ASC";
            DBResult dbret = dao.QueryResult(cWhereParm, cOrderBy, iPageNo, iPageSize);
            DataTable dtRows = dbret.dtrows;
            int iRowsCount = dbret.ROW_COUNT;
            vret = ActiveResult.Query(dtRows);
            vret.total = iRowsCount;

            response.Write(vret.toJSONString());
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
            String cAppDir = System.Web.HttpContext.Current.Server.MapPath("~/");
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
            for (int i = 0; i < System.Web.HttpContext.Current.Request.Files.Count; i++)
            {
                cKeyID = AutoID.getAutoID();
                UploadFileInfo vUpload = new UploadFileInfo();
                HttpPostedFile vf = System.Web.HttpContext.Current.Request.Files[i];
                cFileName = Path.GetFileName(vf.FileName);
                String cFileExt = Path.GetExtension(vf.FileName);

                vf.SaveAs(cFileDir + cKeyID + cFileExt);
                cFileUrl = "/" + cDir + "/" + cKeyID.Substring(0, 8) + "/" + cKeyID + cFileExt;
                vUpload.FileName = cFileName;
                vUpload.ID = cKeyID;
                vUpload.ORG_ID = cOrgID;
                vUpload.ResID = cKeyID;
                vUpload.Url = cFileUrl;
                log4net.WriteLogFile("getUploadList:" + cOrgID + ":" + cKeyID);
                KeyList.Add(vUpload);

            }
            return KeyList;
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

        public void GTWS()
        {
            ActiveResult vert = new ActiveResult();
            XT_JB vo = new XT_JB();
            vo.jbtype = "GTWS";
            vo.time = DateTime.Now.ToString("yyyy-MM-dd");
            vo.sd_result = 0;
            String cOrgID = StringEx.getString(request["org_id"]);
            vo.org_id = cOrgID;
            vo.adress = StringEx.getString(request["Address"]);
            vo.neirong = StringEx.getString(request["Content"]);
            vo.open_id = StringEx.getString(request["open_id"]);

            vo.danwei = DbManager.GetStrValue("SELECT ORG_ID FROM XT_CAMERA WHERE DEVICE_ID='" + vo.open_id + "'");
            String cAppDir = System.Web.HttpContext.Current.Server.MapPath("~/");
            String cFileDir = cAppDir + "GTWSUpload\\";
            if (!Directory.Exists(cFileDir))
            {
                Directory.CreateDirectory(cFileDir);
            }
            String cKeyID = AutoID.getAutoID();
            vo.id = cKeyID;
            cFileDir = cFileDir + cKeyID.Substring(0, 8) + "\\";
            if (!Directory.Exists(cFileDir))
            {
                Directory.CreateDirectory(cFileDir);
            }

            List<string> sqls = new List<string>();

            List<UploadFileInfo> FileList = this.getUploadList(cOrgID, "GTWSUpload");
            String cFile_ID = null;
            List<String> sqlList = getUploadSQL(FileList);
            for (int i = 0; (sqlList != null) && (i < sqlList.Count); i++)
            {
                sqls.Add(sqlList[i]);
                if (i == 0)
                {
                    cFile_ID = FileList[i].ID;
                }
                else
                {
                    cFile_ID = cFile_ID + "," + FileList[i].ID;
                }
            }
            vo.files_id = cFile_ID;

            string vMaster = dao.Insert(vo);
            sqls.Add(vMaster);

            int iCode = DbManager.ExecSQL(sqls);
            ActiveResult vret = ActiveResult.Valid(iCode);
            response.Write(vret.toJSONString());
        }
    }
}

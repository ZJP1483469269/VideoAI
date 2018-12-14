using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TLKJ.Utils;
using TLKJ.DB;
using System.Data;
using TLKJ.DAO;

namespace TLKJ.DAO
{
    public class XT_CAMERA_Dao : BaseDao<XT_CAMERA>
    {
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
        }

        public DBResult search(string cORG_ID, string cMaxID, string cCOUNTY, string cADDR, string C_VILLAGE, int iPAGE_SIZE)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" (1=1) ");
            String cAreaID = cORG_ID.Replace("00", "");
            if (cORG_ID.Length > 0)
            {
                sql.Append(" AND (ORG_ID LIKE '" + cAreaID + "%') ");
            }
            if (cCOUNTY.Length > 0)
            {
                sql.Append(" AND (ORG_ID='" + cCOUNTY + "') ");
            }
            if (cADDR.Length > 0)
            {
                sql.Append(" AND (ADDR LIKE '%" + cADDR + "%') ");
            }
            if (C_VILLAGE.Length > 0)
            {
                sql.Append(" AND (VILLAGE LIKE '%" + C_VILLAGE + "%') ");
            }
            if (cMaxID.Length > 0)
            {
                sql.Append(" AND (ID> '" + cMaxID + "') ");
            }
            return DbManager.Query("*", "XT_CAMERA", sql.ToString(), " ORDER BY ID ASC ", 1, iPAGE_SIZE);
        }


        public XT_CAMERA FindOne(string cKeyID)
        {
            XT_CAMERA vo = null;
            DataTable dtRows = DbManager.QueryData("SELECT * FROM xt_camera WHERE ID='" + cKeyID + "'");
            if ((dtRows != null) && (dtRows.Rows.Count > 0))
            {
                vo = new XT_CAMERA();
                ReadDB(vo, dtRows);
            }
            return vo;
        }

        public DataTable QueryData(String cFieldList, string cWhereParm, string cOrderBy)
        {
            return QueryData("xt_camera", cFieldList, cWhereParm, cOrderBy);
        }

        public int UpdateFromVideo(string cORG_ID, string cIDS, string cNAMES)
        {
            string[] ID_List = cIDS.Split(',');
            string[] NAME_List = cNAMES.Split(',');
            int iCode = 0;
            for (int i = 0; i < ID_List.Length; i++)
            {
                String cDEVICE_ID = ID_List[i];
                String cDEVICE_NAME = NAME_List[i];
                List<string> sqls = new List<string>();
                sqls.Add("DELETE FROM XT_CAMERA_LIST WHERE ORG_ID='" + cORG_ID + "' AND DEVICE_ID='" + cDEVICE_ID + "'");
                sqls.Add("INSERT INTO XT_CAMERA_LIST(ORG_ID,DEVICE_ID,DEVICE_NAME,UPDATE_TIME) VALUES('" + cORG_ID + "','" + cDEVICE_ID + "','" + cDEVICE_NAME + "','" + DateUtils.getDayTimeNum() + "')");
                iCode = iCode + DbManager.ExecSQL(sqls);
            }
            return iCode;
        }
    }
}

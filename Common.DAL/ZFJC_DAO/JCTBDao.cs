using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TLKJ.DB;
using System.Data;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class JCTBDao : BaseDao<xt_jctb>
    {

        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
        }
        public int save(xt_jctb vo, String cKeyID)
        {
            ActiveResult vret = new ActiveResult();
            List<String> sqls = new List<string>();
            string sql = null;
            if (cKeyID.Length == 0)
            {
                sql = Insert(vo);
            }
            else
            {
                sql = Update(vo, "ogr_fid='" + cKeyID + "'");
            }
            sqls.Add(sql);
            int iCode= DbManager.ExecSQL(sqls);
            return iCode;

        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="cWhereParm"></param>
        /// <param name="cOrderBy"></param>
        /// <param name="iPageNo"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public DBResult Query(String cFieldList, String cWhereParm, String cOrderBy, int iPageNo, int iPageSize)
        {
            if (cFieldList == null)
            {
                cFieldList = "*";
            }
            return DbManager.Query(cFieldList, "xt_jctb", cWhereParm, cOrderBy, iPageNo, iPageSize);
        }
        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="cKeyID"></param>
        /// <returns></returns>
        public int del_item(String cKeyID)
        {
            ActiveResult vret = new ActiveResult();
            string sql = "DELETE FROM xt_jctb WHERE ogr_fid='" + cKeyID + "'";
            return DbManager.ExecSQL(sql);
        }

        /// <summary>
        /// 删除列表
        /// </summary>
        /// <param name="cKeyList"></param>
        /// <returns></returns>
        public int del_list(String cKeyList)
        {
            String[] KeyList = cKeyList.Split(',');
            ActiveResult vret = new ActiveResult();
            StringBuilder sql = new StringBuilder();
            sql.Append(" DELETE FROM xt_jctb ");
            sql.Append(" WHERE ogr_fid IN (");
            for (int i = 0; i < KeyList.Length; i++)
            {
                String cUserID = KeyList[i].Trim();
                if (i == 0)
                {
                    sql.Append("'" + cUserID + "'");
                }
                else
                {
                    sql.Append(",'" + cUserID + "'");
                }
            }
            sql.Append(" )");
            return DbManager.ExecSQL(sql.ToString());
        }



        /// <summary>
        /// 查找对象
        /// </summary>
        /// <param name="cDBKey"></param>
        /// <returns></returns>
        public xt_jctb FindOne(string cDBKey)
        {
            xt_jctb vo = null;
            DataTable dtRows = DbManager.QueryData("SELECT * FROM xt_jctb WHERE ogr_fid='" + cDBKey + "'");
            if ((dtRows != null) && (dtRows.Rows.Count > 0))
            {
                vo = new xt_jctb();
                ReadDB(vo, dtRows);
            }
            return vo;
        }

        public DataTable QueryData(string cWhereParm)
        {
            if (String.IsNullOrEmpty(cWhereParm))
            {
                return null;
            }

            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT * FROM xt_jctb ");
            //sql.Append(" WHERE " + cWhereParm);

            return DbManager.QueryData(sql.ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TLKJ.Utils;
using TLKJ.DB;
using System.Data;

namespace TLKJ.DAO
{
    public class S_DICTS_Dao : BaseDao<S_DICTS>
    { 
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="cKeyID"></param>
        /// <returns></returns>
        public int save(S_DICTS vo, String cKeyID)
        {
            List<String> sqls = new List<string>();
            string sql = null;
            if (cKeyID.Length == 0)
            {
                sql = Insert(vo);
            }
            else
            {
                sql = Update(vo, "DX_ID='" + cKeyID + "'");
            }
            sqls.Add(sql);
            return DbManager.ExecSQL(sqls);
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="cDBKey"></param>
        /// <returns></returns>
        public int del_item(String cDBKey)
        {
            string sql = "";
            sql = "DELETE FROM xt_dicts WHERE DX_ID='" + cDBKey + "'";
            return DbManager.ExecSQL(sql);
        }

        /// <summary>
        /// 删除列表
        /// </summary>
        /// <param name="cDBKey"></param>
        /// <returns></returns>
        public int del_list(String cKeyList)
        {
            String[] KeyList = cKeyList.Split(',');
            ActiveResult vret = new ActiveResult();
            StringBuilder sql = new StringBuilder();
            sql.Append(" DELETE FROM xt_dicts ");
            sql.Append(" WHERE GXQ_ID IN (");
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
        /// 查询数据
        /// </summary>
        /// <param name="cWhereParm"></param>
        /// <param name="cOrderBy"></param>
        /// <param name="iPageNo"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public DBResult Query(String cWhereParm, String cOrderBy, int iPageNo, int iPageSize)
        {
            return DbManager.Query("*", "xt_dicts", cWhereParm, cOrderBy, iPageNo, iPageSize);
        }

        /// <summary>
        /// 查找对象
        /// </summary>
        /// <param name="cDBKey"></param>
        /// <returns></returns>
        public S_DICTS FindOne(string cDBKey)
        {
            S_DICTS vo = null;
            DataTable dtRows = DbManager.QueryData("SELECT * FROM xt_dicts WHERE role_id='" + cDBKey + "'");
            if ((dtRows != null) && (dtRows.Rows.Count > 0))
            {
                vo = new S_DICTS();
                ReadDB(vo, dtRows);
            }
            return vo;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.IO;
using System.Configuration;
using System.Data;
using TLKJ.Utils;
using TLKJ.DB;
using System.Collections;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace TLKJ.WebSys
{
    /// <summary>
    /// AppManager 的摘要说明
    /// </summary>
    public class AppManager
    {
        public static string mConnecting = "";
        public static object objLock = new object();
        public AppManager()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            mConnecting = Config.GetAppSettings("ConnStr");
        }
        public static NameValueCollection ParseUrl(string cUrl)
        {
            NameValueCollection nvc = new NameValueCollection();
            if (cUrl == null)
            {
                return nvc;
            }

            int questionMarkIndex = cUrl.IndexOf('?');
            if (questionMarkIndex == -1)
            {
                return nvc;
            }
            String baseUrl = cUrl.Substring(0, questionMarkIndex);
            if (questionMarkIndex == cUrl.Length - 1)
            {
                return nvc;
            }
            string ps = cUrl.Substring(questionMarkIndex + 1);
            // 开始分析参数对  
            Regex re = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);
            MatchCollection mc = re.Matches(ps);
            foreach (Match m in mc)
            {
                nvc.Add(m.Result("$2").ToLower(), m.Result("$3"));
            }

            return nvc;
        }
        public static String getCookieValue(HttpRequest request, String cKeyName)
        {
            String cStr = "";
            if (request != null)
            {
                HttpCookie vCookie = request.Cookies.Get(cKeyName);
                if (vCookie != null)
                {
                    cStr = vCookie.Value;
                }
            }
            return StringEx.getString(cStr);

        }

        public static String getAreaName(String cAreaID)
        {
            String cOrgID = cAreaID.Trim();
            if (cOrgID.Length == 4)
            {
                cOrgID = cOrgID + "00";
            }
            if (cOrgID.Length == 2)
            {
                cOrgID = cOrgID + "0000";
            }
            String sql = "SELECT AREA_NAME FROM S_AREA_INF WHERE AREA_ID = '" + cOrgID + "'";
            return DbManager.GetStrValue(sql);
        }

        public static int getAreaTag(String cAreaID)
        {
            if (cAreaID.EndsWith("0000"))
            {
                return 0;
            }
            else if (cAreaID.EndsWith("00"))
            {
                return 0;
            }
            else
            {
                String sql = "SELECT TAG FROM S_AREA_INF WHERE AREA_ID = '" + cAreaID + "'";
                String cTag = DbManager.GetStrValue(sql);
                return StringEx.getInt(cTag);
            }
        }
        public static String getAreaHeader(String cAreaID)
        {
            String cOrgID = cAreaID.Trim();
            if (cOrgID.EndsWith("0000"))
            {
                return cOrgID.Substring(0, 2);
            }
            else if (cOrgID.EndsWith("00"))
            {
                return cOrgID.Substring(0, 4);
            }
            else
            {
                return cOrgID;
            }
        }
        /// <summary>
        /// 将数据集合转成列表
        /// </summary>
        /// <param name="dtRows"></param>
        /// <returns></returns>
        public static ArrayList getTableHash(DataTable dtRows)
        {
            ArrayList arKeys = new ArrayList();
            for (int k = 0; (dtRows != null) && (k < dtRows.Rows.Count); k++)
            {
                Hashtable vo = new Hashtable();
                for (int i = 0; i < dtRows.Columns.Count; i++)
                {
                    String cFieldName = dtRows.Columns[i].ColumnName;
                    String cFieldValue = StringEx.getString(dtRows, k, cFieldName);
                    vo.Add(cFieldName.ToLower(), cFieldValue);
                }
                arKeys.Add(vo);
            }
            return arKeys;
        }

        /// <summary>
        /// 将数据集的一行转成HashTable
        /// </summary>
        /// <param name="dtRows">数据集</param>
        /// <param name="iRowID">行号</param>
        /// <returns></returns>
        public static Hashtable getTableRow(DataTable dtRows, int iRowID)
        {
            Hashtable vo = new Hashtable();
            if (dtRows != null && dtRows.Rows.Count > 0)
            {
                for (int i = 0; i < dtRows.Columns.Count; i++)
                {
                    String cFieldName = dtRows.Columns[i].ColumnName;
                    String cFieldValue = StringEx.getString(dtRows, iRowID, cFieldName);
                    vo.Add(cFieldName.ToLower(), cFieldValue);
                }
            }
            return vo;
        }

        /// <summary>
        /// 输出
        /// </summary>
        /// <param name="strMessage"></param>
        public static void Alert(string strMessage)
        {
            System.Web.HttpContext.Current.Response.Write("<script>alert('" + strMessage + "');</script>");
        }

        /// <summary>
        /// 存入session 时候方便
        /// </summary>
        /// <param name="tbl"></param>
        /// <param name="iRowIdx"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public static string GetString(DataTable tbl, int iRowIdx, string colName)
        {
            return tbl.Rows[iRowIdx][colName].ToString();
        }

        public static void FillDropList(ListControl vList, DataTable dtRows, string cKeyID, string cKeyText)
        {
            for (int i = 0; (dtRows != null) && (i < dtRows.Rows.Count); i++)
            {
                String cItemID = StringEx.getString(dtRows, i, cKeyID);
                String cItemText = StringEx.getString(dtRows, i, cKeyText);
                vList.Items.Add(new ListItem(cItemText, cItemID));
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.IO;
using System.Configuration;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace TLKJ.Utils
{
    /// <summary>
    /// AppManager 的摘要说明
    /// </summary>
    public class AppManager
    {
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

        public static int getAreaType(String cOrgID)
        {
            String cAreaID = getAreaHeader(cOrgID);
            return cAreaID.Length;
        }

        public static String getAreaHeader(String cOrgID)
        {
            String cAreaID = cOrgID.Trim();
            if (cAreaID.EndsWith("0000"))
            {
                return cAreaID.Substring(0, 2);
            }
            else if (cAreaID.EndsWith("00"))
            {
                return cAreaID.Substring(0, 4);
            }
            else
            {
                return cAreaID;
            }
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
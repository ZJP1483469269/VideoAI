using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.IO;
using System.Configuration;
using System.Data;
namespace TLKJ.Utils
{
    /// <summary>
    /// AppManager 的摘要说明
    /// </summary>
    public class StringEx
    {
        /// <summary>
        /// 存入session 时候方便
        /// </summary>
        /// <param name="tbl"></param>
        /// <param name="iRowIdx"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public static string getString(DataTable dtRows, int iRowIdx, string cFieldName)
        {
            try
            {
                Object objValue = dtRows.Rows[iRowIdx][cFieldName];
                return getString(objValue);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string getString(DataTable dtRows, int iRowIdx, int iColIdx)
        {
            try
            {
                Object objValue = dtRows.Rows[iRowIdx][iColIdx];
                return getString(objValue);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static float getFloat(String cStr)
        {
            try
            {
                return Convert.ToSingle(cStr);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static string getDecimalStr(decimal dVal)
        {
            if (dVal == 0)
            {
                return "0";
            }
            String cStr = dVal.ToString();
            if (cStr.IndexOf(".") == -1)
            {
                return cStr;
            }
            else
            {
                int iStart = cStr.IndexOf(".");
                while (cStr.EndsWith("0"))
                {
                    cStr = cStr.Substring(0, cStr.Length - 1);
                }
            }
            return cStr;
        }
        /// <summary>
        /// 截取字符串部分字符
        /// </summary>
        /// <param name="cStr">字符串</param>
        /// <param name="iLength">截取长度</param>
        /// <returns></returns>
        public static string getCutStr(String cStr, int iLength)
        {

            if (String.IsNullOrEmpty(cStr))
            {
                return cStr;
            }
            else
            {
                if (cStr.Length > iLength)
                {
                    return cStr.Substring(0, iLength) + "...";
                }
                else
                {
                    return cStr;
                }
            }
        }

        public static string getEnableValue(DataTable dtRows, int iRowIdx, string cFieldName)
        {
            String cFieldValue = getString(dtRows, iRowIdx, cFieldName);
            cFieldValue = cFieldValue.ToUpper();
            if ((cFieldValue.Equals("1")) || (cFieldValue.Equals("Y")))
            {
                return "有效";
            }
            else
            {
                return "无效";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtRows"></param>
        /// <param name="iRowIdx"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public static int getInt(DataTable dtRows, int iRowIdx, string colName)
        {
            try
            {
                Object objValue = dtRows.Rows[iRowIdx][colName];
                return getInt32(objValue);
            }
            catch (Exception ex)
            {
                return AppConfig.FAILURE;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static string getSafeQueryString(Object objValue)
        {
            String cStr = getString(objValue);
            cStr = cStr.Replace(">", "");
            cStr = cStr.Replace("<", "");
            cStr = cStr.Replace(";", "");
            cStr = cStr.Replace(" ", "");
            return cStr;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static string getString(Object objValue)
        {
            String cStr = "";
            if (objValue == null)
            {
                return cStr;
            }
            cStr = objValue.ToString().Trim();
            if (cStr.Length == 0)
            {
                return "";
            }
            else
            {
                return cStr;
            }
        }
        public static int getInt(Object objValue, int iDefVal)
        {
            if (objValue == null)
            {
                return iDefVal;
            }
            String cStr = objValue.ToString().Trim();
            if (cStr.Length == 0)
            {
                return iDefVal;
            }
            else
            {
                return StringEx.getInt32(cStr);
            }
        }
        public static int getInt(Object objValue)
        {
            int iVal = -1;
            if (objValue == null)
            {
                return iVal;
            }
            try
            {
                iVal = Convert.ToInt32(objValue.ToString());
                return iVal;
            }
            catch (Exception ex)
            {
                return iVal;
            }
        }

        public static int getInt32(Object objValue)
        {
            int iVal = -1;
            if (objValue == null)
            {
                return iVal;
            }
            try
            {
                iVal = Convert.ToInt32(objValue.ToString());
                return iVal;
            }
            catch (Exception ex)
            {
                return iVal;
            }
        }
        public static Int64 getInt64(Object objValue)
        {
            Int64 iVal = -1;
            if (objValue == null)
            {
                return iVal;
            }
            try
            {
                iVal = Convert.ToInt64(objValue.ToString());
                return iVal;
            }
            catch (Exception ex)
            {
                return iVal;
            }
        }
        public static Double GetDouble(DataTable tbl, int iRowIdx, string colName)
        {
            double dVal = 0;


            if ((tbl != null) && (tbl.Rows.Count > 0))
            {
                Object obj = tbl.Rows[iRowIdx][colName];
                if (obj != null)
                {
                    try
                    {
                        dVal = double.Parse(obj.ToString());
                    }
                    catch (Exception)
                    {
                        ;
                    }
                }
            }
            return dVal;
        }

        public static Double GetDouble(string cStr)
        {
            double dVal = 0;
            try
            {
                dVal = double.Parse(cStr);
            }
            catch (Exception)
            {
                ;
            }

            return dVal;
        }
        public static string getString(String objValue)
        {
            String cStr = "";
            if (objValue == null)
            {
                return cStr;
            }
            cStr = objValue.ToString().Trim();
            if (cStr.Length == 0)
            {
                return "";
            }
            else
            {
                return cStr;
            }
        }

        public static string StringCut(string str, int count)
        {
            string result = str;
            int iLength = count;
            if ((str != null) && (str != string.Empty))
            {
                if (str.Length > iLength)
                {
                    while (str.Length > iLength)
                    {
                        result = str.Substring(0, iLength);
                        byte[] bs = System.Text.Encoding.GetEncoding("GBK").GetBytes(result);
                        if (bs.Length < count * 2 - 1)
                        {
                            iLength++;
                        }
                        else
                        {
                            return result + "…";
                        }
                    }
                    result += "…";
                }
                return result;
            }
            return "";
        }
    }
}
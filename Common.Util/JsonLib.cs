using System;
using System.Data;
using System.Configuration;
using System.Collections;
using Newtonsoft.Json;
using System.Text;
using System.IO;
namespace TLKJ.Utils
{
    /// <summary>
    /// JsonLib 的摘要说明
    /// </summary>
    public class JsonLib
    {
        public JsonLib()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public static UploadResult ToUploadResult(string cStr)
        {
            UploadResult vo = Newtonsoft.Json.JsonConvert.DeserializeObject<UploadResult>(cStr);
            return vo;
        }

        public static String ToJSON(Object obj)
        {
            String cRetStr = "";
            if (obj != null)
            {
                cRetStr = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }
            return cRetStr;
        }

        public static T ToObject<T>(String cStr)
        {
            T obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(cStr);
            return obj;
        }

        /// <summary>
        /// 将数据集合转成JSON对象
        /// </summary>
        /// <param name="dtRows"></param>
        /// <returns></returns>
        public static ArrayList ToList(DataTable dtRows)
        {
            int iLength = (dtRows == null) ? 0 : dtRows.Rows.Count;
            ArrayList arRows = new ArrayList();
            for (int i = 0; i < iLength; i++)
            {
                Hashtable aKey = new Hashtable();
                for (int k = 0; k < dtRows.Columns.Count; k++)
                {
                    String cFieldName = dtRows.Columns[k].ColumnName;
                    Object obj = dtRows.Rows[i][k];
                    if ((obj == null) || (obj == DBNull.Value))
                        aKey.Add(cFieldName, "");
                    else
                        aKey.Add(cFieldName, obj);
                }
                arRows.Add(aKey);
            }
            return arRows;
        }

        public static ArrayList ConvertDataTableToList(DataTable dtRows)
        {
            int iLength = (dtRows == null) ? 0 : dtRows.Rows.Count;
            ArrayList arKeys = new ArrayList();
            for (int i = 0; i < iLength; i++)
            {
                Hashtable aKey = new Hashtable();
                for (int j = 0; j < dtRows.Columns.Count; j++)
                {
                    String cFieldName = dtRows.Columns[j].ColumnName;
                    Object objFieldValue = dtRows.Rows[i][j];
                    if ((objFieldValue == null) || (objFieldValue == DBNull.Value))
                    {
                        objFieldValue = "";
                    }
                    aKey.Add(cFieldName.ToLower(), objFieldValue.ToString().TrimEnd());
                }
                arKeys.Add(aKey);
            }
            return arKeys;
        }

        public static Hashtable ConvertToObject(DataTable dtRows)
        {
            int iLength = (dtRows == null) ? 0 : dtRows.Rows.Count;
            Hashtable aKey = null;
            if (iLength > 0)
            {
                aKey = new Hashtable();
                for (int j = 0; j < dtRows.Columns.Count; j++)
                {
                    String cFieldName = dtRows.Columns[j].ColumnName;
                    Object objFieldValue = dtRows.Rows[0][j];
                    if ((objFieldValue == null) || (objFieldValue == DBNull.Value))
                    {
                        objFieldValue = "";
                    }
                    if (dtRows.Columns[j].DataType == typeof(byte[]))
                    {
                        byte[] b = (byte[])objFieldValue;
                        objFieldValue = System.Text.Encoding.Unicode.GetString(b);
                    }
                    aKey.Add(cFieldName.ToLower(), objFieldValue.ToString().TrimEnd());
                }
            }
            return aKey;
        }

        public static String ConvertListJSON(ArrayList arKeys)
        {
            String cRetStr = "";
            if (arKeys != null)
            {
                cRetStr = Newtonsoft.Json.JsonConvert.SerializeObject(arKeys);
            }
            return cRetStr;
        }
    }
}
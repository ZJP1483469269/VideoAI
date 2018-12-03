using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Text;
using System;
using System.Data.Common;
using TLKJ.Utils;

namespace TLKJ.DB
{
    /// <summary>
    /// JActiveTable 的摘要说明
    /// </summary>
    public class JActiveTable
    {
        public String TableName;
        public List<JActiveField> FieldList = new List<JActiveField>();
        public int AddField(JActiveField vField)
        {
            FieldList.Add(vField);
            return FieldList.Count - 1;
        }

        public int AddField(String cFieldName, String cFieldValue)
        {
            JActiveField vField = new JActiveField(cFieldName, cFieldValue);
            FieldList.Add(vField);
            return FieldList.Count - 1;
        }

        public int AddField(String cFieldName, int iVal)
        {
            JActiveField vField = new JActiveField(cFieldName, iVal);
            FieldList.Add(vField);
            return FieldList.Count - 1;
        }

        public int AddField(String cFieldName, double dVal)
        {
            JActiveField vField = new JActiveField(cFieldName, dVal);
            FieldList.Add(vField);
            return FieldList.Count - 1;
        }

        public void ClearField()
        {
            FieldList.Clear();
        }

        public String getInsertSQL()
        {
            StringBuilder sbFieldList = new StringBuilder();
            StringBuilder sbValueList = new StringBuilder(); 
            for (int i = 0; i < FieldList.Count; i++)
            {
                JActiveField aField = FieldList[i];
                String cFieldName = aField.FieldName;
                String cFieldValue = aField.FieldValue;
                if (String.IsNullOrWhiteSpace(cFieldValue))
                {
                    continue;
                }
                if (sbFieldList.Length == 0)
                {
                    sbFieldList.Append(cFieldName);
                    sbValueList.Append("'" + cFieldValue + "'");
                }
                else
                {
                    sbFieldList.Append("," + cFieldName);
                    sbValueList.Append(",'" + cFieldValue + "'");
                }
            }
            return "INSERT INTO " + TableName + "(" + sbFieldList + ") VALUES(" + sbValueList + ")";
        }

        public String getUpdateSQL(String cWhereParm)
        {
            StringBuilder ValueList = new StringBuilder();
            for (int i = 0; i < FieldList.Count; i++)
            {
                JActiveField aField = FieldList[i];
                String cFieldName = aField.FieldName;
                String cFieldValue = aField.FieldValue;
                if (String.IsNullOrWhiteSpace(cFieldValue))
                    cFieldValue = null;
                else
                    cFieldValue = "'" + cFieldValue + "'";

                if (i == 0)
                {
                    ValueList.Append(cFieldName + "=" + cFieldValue);
                }
                else
                {
                    ValueList.Append("," + cFieldName + "=" + cFieldValue);
                }
            }
            return "UPDATE " + TableName + " SET " + ValueList + " WHERE " + cWhereParm;
        }
    }
}
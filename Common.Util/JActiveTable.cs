using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Text;
using System;
using System.Data.Common; 

namespace TLKJ.Utils
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
            int idx = Find(vField.FieldName);
            if (idx == -1)
            {
                FieldList.Add(vField);
                return FieldList.Count - 1;
            }
            else
            {
                FieldList[idx] = vField;
                return idx;
            }
        }

        public int AddField(String cFieldName, String cFieldValue)
        {
            int idx = Find(cFieldName);
            if (idx == -1)
            {
                JActiveField vField = new JActiveField(cFieldName, cFieldValue);
                FieldList.Add(vField);
                return FieldList.Count - 1;
            }
            else
            {
                FieldList[idx].FieldValue = cFieldValue;
                return idx;
            }
        }

        public int AddField(String cFieldName, int iVal)
        {
            int idx = Find(cFieldName);
            if (idx == -1)
            {
                JActiveField vField = new JActiveField(cFieldName, iVal);
                FieldList.Add(vField);
                return FieldList.Count - 1;
            }
            else
            {
                FieldList[idx].FieldValue = StringEx.getString(iVal);
                return idx;
            }
        }


        public int AddField(String cFieldName, double dVal)
        {
            int idx = Find(cFieldName);
            if (idx == -1)
            {
                JActiveField vField = new JActiveField(cFieldName, dVal);
                FieldList.Add(vField);
                return FieldList.Count - 1;
            }
            else
            {
                return idx;
            }
        }

        public void ClearField()
        {
            FieldList.Clear();
        }
        public int Find(String cFieldName)
        {
            int idx = -1;
            for (int i = 0; i < FieldList.Count; i++)
            {
                JActiveField aField = FieldList[i];
                if (cFieldName.ToLower().Equals(aField.FieldName.ToLower()))
                {
                    idx = i;
                    break;
                }
            }
            return idx;
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
        public Object[] getParmList()
        {
            return ParmList.ToArray();
        }
        List<Object> ParmList = new List<object>();
        public String getInsertParmSQL()
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
                    sbValueList.Append(" @" + cFieldName + "");
                    ParmList.Add(cFieldValue);
                }
                else
                {
                    sbFieldList.Append("," + cFieldName + "");
                    sbValueList.Append(" @" + cFieldName + "");
                    ParmList.Add(cFieldValue);
                }
            }
            return "INSERT INTO " + TableName + "(" + sbFieldList + ") VALUES(" + sbValueList + ")";
        }

        public String getUpdateParmSQL(String cWhereParm)
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
                    ValueList.Append(cFieldName + "=@" + cFieldName.ToLower());
                    ParmList.Add(cFieldValue);
                }
                else
                {
                    ValueList.Append("," + cFieldName + "=@" + cFieldName.ToLower());
                    ParmList.Add(cFieldValue);
                }
            }
            return "UPDATE " + TableName + " SET " + ValueList + " WHERE " + cWhereParm;
        }
    }
}
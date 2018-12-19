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
    /// JActiveField 的摘要说明
    /// </summary>
    public class JActiveField
    {
        public int DBType;
        public String FieldName;
        public String FieldValue;
        public String FieldDesc;

        public JActiveField(string cFieldName, string cFieldValue)
        {
            this.FieldName = cFieldName;
            this.FieldValue = cFieldValue;
        }

        public JActiveField(string cFieldName, int iVal)
        {
            this.FieldName = cFieldName;
            this.FieldValue = iVal.ToString();
        }

        public JActiveField(string cFieldName, double dVal)
        {
            this.FieldName = cFieldName;
            this.FieldValue = dVal.ToString();
        }

        public JActiveField(String cFieldName, String cFieldDesc, int iDBType)
        {
            DBType = iDBType;
            this.FieldName = cFieldName;
            FieldDesc = cFieldDesc;
        }
    }
}
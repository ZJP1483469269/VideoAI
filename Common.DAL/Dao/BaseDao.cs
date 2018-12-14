using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using TLKJ.Utils;
using System.Data;
using System.Web;
using TLKJ.DB;

namespace TLKJ.DAO
{
    public class BaseDao<T>
    {
        public HttpRequest request = null;
        public HttpResponse response = null;
        public String[] InsertList(List<T> objList)
        {
            string[] sqls = new string[objList.Count];
            for (int i = 0; i < objList.Count; i++)
            {
                T obj = objList[i];
                sqls[i] = Insert(obj);
            }
            return sqls;
        }

        public DataTable QueryData(String cTableName, String cFieldList, String cWhereParm, String cOrderBy)
        {
            String sql = "SELECT " + cFieldList + " FROM " + cTableName;
            if (cWhereParm.Length > 0)
            {
                sql = sql + " WHERE " + cWhereParm;
            }
            if (cOrderBy.Length > 0)
            {
                sql = sql + cOrderBy;
            }
            return DbManager.QueryData(sql);
        }

        public String Insert(T obj)
        {
            List<String> FieldList = new List<string>();
            List<Object> ValueList = new List<Object>();

            Type vType = obj.GetType();
            //获取类名
            String className = vType.Name;
            //获取所有公有属性
            PropertyInfo[] info = vType.GetProperties();

            foreach (PropertyInfo var in info)
            {
                //取得属性的特性标签，false表示不获取因为继承而得到的标签
                Object[] attr = var.GetCustomAttributes(false);
                if (attr.Length > 0)
                {
                    //从注解数组中取第一个注解(一个属性可以包含多个注解)
                    FieldAttr myattr = attr[0] as FieldAttr;
                    if (myattr.isAuto == true)
                    {
                        continue;
                    }
                }
                FieldList.Add(var.Name);
                object objValue = var.GetValue(obj, null);
                ValueList.Add(objValue);
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO " + className);
            sql.Append(" ( ");
            for (int i = 0; i < FieldList.Count; i++)
            {
                String cFieldName = FieldList[i];
                if (i == 0)
                    sql.Append(cFieldName);
                else
                    sql.Append("," + cFieldName);
            } sql.Append(" )");


            sql.Append(" VALUES ( ");

            for (int i = 0; i < ValueList.Count; i++)
            {
                Object objValue = ValueList[i];
                String cFieldValue = (objValue == null) ? "null" : "'" + objValue.ToString() + "'";
                if (i == 0)
                    sql.Append(cFieldValue);
                else
                    sql.Append("," + cFieldValue);
            }
            sql.Append(" )");
            return sql.ToString();
        }

        public String getFieldValue(String cTableName, String cFieldName, String cWhereName, String cWhereValue)
        {
            return DbManager.GetStrValue("SELECT " + cFieldName + " FROM " + cTableName + " WHERE " + cWhereName + "='" + cWhereValue + "'");
        }

        public String Update(T obj, String cWhereParm)
        {
            List<String> FieldList = new List<string>();
            List<Object> ValueList = new List<Object>();

            Type vType = obj.GetType();
            //获取类名
            String className = vType.Name;
            //获取所有公有属性
            PropertyInfo[] info = vType.GetProperties();

            foreach (PropertyInfo var in info)
            {
                //取得属性的特性标签，false表示不获取因为继承而得到的标签
                Object[] attr = var.GetCustomAttributes(false);

                if (attr.Length > 0)
                {
                    //从注解数组中取第一个注解(一个属性可以包含多个注解)
                    FieldAttr myattr = attr[0] as FieldAttr;

                    if (myattr.isAuto == true)
                    {
                        continue;
                    }
                }
                FieldList.Add(var.Name);
                Object cFieldValue = var.GetValue(obj, null);
                ValueList.Add(cFieldValue);
            }

            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE " + className);
            sql.Append(" SET ");
            for (int i = 0; i < FieldList.Count; i++)
            {
                String cFieldName = FieldList[i];
                Object cFieldValue = ValueList[i];
                if (cFieldValue == null)
                {
                    cFieldValue = "null";
                }
                else
                {
                    cFieldValue = "'" + cFieldValue + "'";
                }
                if (i == 0)
                {
                    sql.Append(" " + cFieldName + "=" + cFieldValue + "");
                }
                else
                {
                    sql.Append("," + cFieldName + "=" + cFieldValue + "");
                }
            }

            sql.Append(" WHERE " + cWhereParm);
            return sql.ToString();
            Console.WriteLine(sql);
        }

        public ActiveResult CheckData(T obj)
        {
            ActiveResult vret = null;
            Type vType = obj.GetType();
            //获取所有公有属性
            PropertyInfo[] info = vType.GetProperties();
            foreach (PropertyInfo var in info)
            {
                //取得属性的特性标签，false表示不获取因为继承而得到的标签
                Object[] attrList = var.GetCustomAttributes(false);
                if (attrList.Length > 0)
                {
                    //从注解数组中取第一个注解(一个属性可以包含多个注解)
                    FieldAttr vAttr = attrList[0] as FieldAttr;
                    String cFieldDesc = vAttr.FieldDesc;
                    String cFieldName = vAttr.FieldName;
                    if (cFieldName == null)
                    {
                        cFieldName = var.Name;
                    }
                    Object objValue = var.GetValue(obj, null);

                    if (vAttr.isKey && (!vAttr.isAuto))
                    {
                        if (objValue == null)
                        {
                            vret = ActiveResult.Valid(cFieldName, cFieldDesc + "不能为空！");
                            break;
                        }
                    }

                    if ((vAttr.DBType == ActionField.ftString) && (objValue != null))
                    {
                        if (StringEx.getString(objValue).Length > vAttr.DBLength)
                        {
                            vret = ActiveResult.Valid(cFieldName, cFieldDesc + "长度不能超过" + vAttr.DBLength + "！");
                            break;
                        }
                    }
                    if ((vAttr.DBType == ActionField.ftBoolean) && (objValue != null))
                    {
                        String cValue = StringEx.getString(objValue).ToUpper();
                        if ((cValue.Equals("0") || cValue.Equals("1") || cValue.Equals("Y") || cValue.Equals("N")))
                        {
                            objValue = "1";
                        }
                        else
                        {
                            vret = ActiveResult.Valid(cFieldName, cFieldDesc + "必须是布尔类型！");
                            break;
                        }
                    }
                    if ((vAttr.DBType == ActionField.ftInteger) && (objValue != null))
                    {
                        try
                        {
                            int.Parse(objValue.ToString());
                        }
                        catch (Exception ex)
                        {
                            vret = ActiveResult.Valid(cFieldName, cFieldDesc + "必须是整数！");
                            break;
                        }
                    }
                    if ((vAttr.DBType == ActionField.ftLong) && (objValue != null))
                    {
                        try
                        {
                            Int64.Parse(objValue.ToString());
                        }
                        catch (Exception ex)
                        {
                            vret = ActiveResult.Valid(cFieldName, cFieldDesc + "必须是长整型！");
                            break;
                        }
                    }

                    if ((vAttr.DBType == ActionField.ftFloat) && (objValue != null))
                    {
                        try
                        {
                            float.Parse(objValue.ToString());
                        }
                        catch (Exception ex)
                        {
                            vret = ActiveResult.Valid(cFieldName, cFieldDesc + "必须是浮点数！");
                            break;
                        }
                    }
                    if ((vAttr.DBType == ActionField.ftDouble) && (objValue != null))
                    {
                        try
                        {
                            double.Parse(objValue.ToString());
                        }
                        catch (Exception ex)
                        {
                            vret = ActiveResult.Valid(cFieldName, cFieldDesc + "必须是双精度浮点数！");
                            break;
                        }
                    }
                }
            }
            return vret;
        }
        public object ReadDB(Object obj, DataRow dr)
        {
            ActiveResult vret = null;
            Type vType = obj.GetType();
            //获取所有公有属性
            PropertyInfo[] info = vType.GetProperties();
            foreach (PropertyInfo var in info)
            {
                //取得属性的特性标签，false表示不获取因为继承而得到的标签
                Object[] attrList = var.GetCustomAttributes(false);
                if (attrList.Length > 0)
                {
                    //从注解数组中取第一个注解(一个属性可以包含多个注解)
                    FieldAttr vAttr = attrList[0] as FieldAttr;
                    String cFieldDesc = vAttr.FieldDesc;
                    String cFieldName = vAttr.FieldName;
                    if (cFieldName == null)
                    {
                        cFieldName = var.Name;
                    }

                    String cFieldValue = StringEx.getString(dr[cFieldName]);
                    String cFieldType = var.PropertyType.ToString();
                    if (cFieldType.IndexOf("String") > -1)
                    {
                        var.SetValue(obj, cFieldValue, null);
                    }
                    else if (cFieldType.IndexOf("Int32") > -1)
                    {
                        int iFieldValue = StringEx.getInt(cFieldValue);
                        var.SetValue(obj, iFieldValue, null);
                    }
                    else if (cFieldType.IndexOf("DateTime") > -1)
                    {
                        try
                        {
                            DateTime vDate = DateTime.Parse(cFieldValue);
                            var.SetValue(obj, vDate, null);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else if (cFieldType.IndexOf("Single") > -1)
                    {
                        try
                        {
                            Single vDate = Single.Parse(cFieldValue);
                            var.SetValue(obj, vDate, null);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else if (cFieldType.IndexOf("Double") > -1)
                    {
                        try
                        {
                            Double vDate = Double.Parse(cFieldValue);
                            var.SetValue(obj, vDate, null);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else if (cFieldType.IndexOf("Int64") > -1)
                    {
                        int iFieldValue = StringEx.getInt(cFieldValue);
                        var.SetValue(obj, iFieldValue, null);
                    }
                }
            }

            return obj;
        }

        public object ReadDB(Object obj, DataTable dtRows)
        {
            if (dtRows == null)
                return obj;
            if (dtRows.Rows.Count > 0)
            {
                return ReadDB(obj, dtRows.Rows[0]);
            }
            else
            {
                return obj;
            }
        }

        public List<Object> ReadList(Object obj, DataTable dtRows)
        {
            List<object> vKeyList = new List<object>();
            for (int i = 0; (dtRows != null) && (i < dtRows.Rows.Count); i++)
            {
                obj = ReadDB(obj, dtRows.Rows[i]);
                vKeyList.Add(obj);
            }
            return vKeyList;
        }
    }
}
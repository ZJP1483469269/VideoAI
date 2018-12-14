using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web;
using TLKJ.DAO;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class RequestUtil
    {
        public static Object readFromRequest(HttpRequest request, Object obj)
        {
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
                    FieldAttr vAttr = attr[0] as FieldAttr;

                    String cFieldName = var.Name;
                    String cFieldValue = StringEx.getString(request[cFieldName]);

                    if (vAttr.DBType == ActionField.ftBoolean)
                    {
                        if (cFieldValue.Length > 0)
                        {
                            if ((cFieldValue != "1") && (cFieldValue != "0"))
                            {
                                if (cFieldValue.Equals("on"))
                                {
                                    cFieldValue = "1";
                                }
                                else
                                {
                                    cFieldValue = "0";
                                }
                            }
                        }
                        else
                        {
                            cFieldValue = "0";
                        }
                    }
                    if (cFieldValue.Length > 0)
                    {
                        String cFieldType = var.PropertyType.ToString();
                        if (cFieldType.IndexOf("String") > -1)
                        {
                            var.SetValue(obj, cFieldValue, null);
                        }
                        else if (cFieldType.IndexOf("Int32") > -1)
                        {
                            Int32 iFieldValue = StringEx.getInt32(cFieldValue);
                            var.SetValue(obj, iFieldValue, null);
                        }
                        else if (cFieldType.IndexOf("Int64") > -1)
                        {
                            Int64 iFieldValue = StringEx.getInt64(cFieldValue);
                            var.SetValue(obj, iFieldValue, null);
                        }
                        else if (cFieldType.IndexOf("Single") > -1)
                        {
                            try
                            {
                                Single iFieldValue = Single.Parse(cFieldValue);
                                var.SetValue(obj, iFieldValue, null);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        else if (cFieldType.IndexOf("Double") > -1)
                        {
                            try
                            {
                                Double iFieldValue = Double.Parse(cFieldValue);
                                var.SetValue(obj, iFieldValue, null);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        else if (cFieldType.IndexOf("DateTime") > -1)
                        {
                            try
                            {
                                DateTime iFieldValue = DateTime.Parse(cFieldValue);
                                var.SetValue(obj, iFieldValue, null);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
            }
            return obj;
        }
    }
}
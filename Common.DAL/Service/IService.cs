using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TLKJ.Utils;
using System.Reflection;

namespace TLKJ.DAO
{
    public class IService
    {
        public readonly String DEL_ITEM_KEY = "dbkey";
        public readonly String DEL_LIST_KEY = "dbkeys";
        public int iPageSize = 20;
        public int iPageNo = 1;
        public HttpRequest request = null;
        public HttpResponse response = null;
        public String formName = ""; //要查询的表明
        public String orderByIndex = "";//排序字段
        public String backValue = "";//查询的值
        public String whereTerm = "";//查询条件 where 后面条件  

        public void readQueryString()
        {
            String cPageSize = StringEx.getString(request[AppConfig.__PAGE_SIZE]);
            String cPageNo = StringEx.getString(request[AppConfig.__PAGE_NO]);

            String cLimit = StringEx.getString(request["limit"]);
            String cOffset = StringEx.getString(request["offset"]);


            if ((cLimit.Length > 0) && (cOffset.Length > 0))
            {
                iPageSize = StringEx.getInt(cLimit, 15);
                int iOffset = StringEx.getInt(cOffset);
                iPageNo = (iOffset / iPageSize) + 1;
                if (iPageNo <= 0)
                {
                    iPageNo = 1;
                }
            }
        }

        public ActiveResult Valid(Object obj)
        {
            ActiveResult vret = null;
            Type vType = obj.GetType();
            //获取类名
            String className = vType.Name;
            //获取所有公有属性
            PropertyInfo[] info = vType.GetProperties();

            foreach (PropertyInfo var in info)
            {
                //取得属性的特性标签，false表示不获取因为继承而得到的标签
                Object[] arAttr = var.GetCustomAttributes(false);
                if (arAttr.Length > 0)
                {
                    //从注解数组中取第一个注解(一个属性可以包含多个注解)
                    FieldAttr voAttr = arAttr[0] as FieldAttr;
                    if (voAttr.isAuto == true)
                    {
                        continue;
                    }

                    String FieldName = var.Name;
                    String cFileDesc = voAttr.FieldDesc;
                    object objValue = var.GetValue(obj, null);
                    String cFieldValue = StringEx.getString(objValue);
                    if (voAttr.isRequire)
                    {
                        if (String.IsNullOrEmpty(cFieldValue))
                        {
                            return ActiveResult.Valid(FieldName, cFileDesc + "不能为空!");
                        }
                    }

                    if (voAttr.DBLength > 0)
                    {
                        if (voAttr.DBLength < cFieldValue.Length)
                        {
                            return ActiveResult.Valid(FieldName, cFileDesc + "超过" + cFieldValue.Length + "字符!");
                        }
                    }
                }
            }

            return vret;
        }
    }
}

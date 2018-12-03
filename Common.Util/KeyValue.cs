using System;
using System.Collections.Generic;
using System.Web;
namespace TLKJ.Utils
{
    /// <summary>
    /// KeyValue 的摘要说明
    /// </summary>
    public class KeyValue
    {
        public String Text;
        public String Val;
        public KeyValue()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public KeyValue(String cKeyValue, String cKeyText)
        {
            Val = cKeyValue;
            Text = cKeyText;
        }
    }
}
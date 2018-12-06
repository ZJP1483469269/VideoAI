using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;
using System.Data; 
using Gecko;
using System.Windows.Forms;
using Gecko.JQuery;

namespace TLKJ_IVS
{
    public class WebJS
    {
        private GeckoDocument vGecko;
        private HtmlDocument vHtml;
        public WebJS(GeckoDocument vDoc)
        {
            vGecko = vDoc;
            vHtml = null;
        }

        public WebJS(HtmlDocument vDoc)
        {
            vHtml = vDoc;
            vGecko = null;
        }

        private GeckoElement getGeckoElement(String cKeyID)
        {
            if (vGecko != null)
            {
                return vGecko.GetElementById(cKeyID);
            }
            else
            {
                return null;
            }
        }

        private HtmlElement getHtmlElement(String cKeyID)
        {
            if (vHtml != null)
            {
                return vHtml.GetElementById(cKeyID);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据ID获取数据信息。
        /// </summary>
        /// <param name="cKeyID"></param>
        /// <returns></returns>
        public String getFieldValue(String cField)
        {
            GeckoElement voGecko = getGeckoElement(cField);
            if (voGecko != null)
            {
                if (voGecko.Attributes["value"] != null)
                {
                    return StringEx.getString(voGecko.Attributes["value"].TextContent);
                }
            }

            HtmlElement voHtml = getHtmlElement(cField);
            if (voHtml != null)
            {
                return StringEx.getString(voHtml.GetAttribute("value"));
            }

            return null;
        }

        public bool setFieldValue(String cField, String cKeyValue)
        {
            GeckoElement voGecko = getGeckoElement(cField);
            if (voGecko != null)
            {
                voGecko.SetAttribute("value", cKeyValue);
                return true;
            }

            HtmlElement voHtml = getHtmlElement(cField);
            if (voHtml != null)
            {
                voHtml.SetAttribute("value", cKeyValue);
                return true;
            }

            return false;
        }

        public void runJS(String cScript)
        {
            if (vGecko != null)
            {
                JQueryExecutor js = new Gecko.JQuery.JQueryExecutor(vGecko.DefaultView);
                js.ExecuteJQuery(cScript);
            }

            if (vHtml != null)
            {
                vHtml.InvokeScript(cScript);
            }
        }

        public String getJSValue(String cScript)
        {
            if (vGecko != null)
            {
                JQueryExecutor js = new Gecko.JQuery.JQueryExecutor(vGecko.DefaultView);
                JsVal jsRet = js.ExecuteJQuery(cScript);
                if (!jsRet.IsNull)
                {
                    return StringEx.getString(jsRet);
                }
            }

            if (vGecko != null)
            {
                Object objRet = vHtml.InvokeScript(cScript);
                if (objRet != null)
                {
                    return StringEx.getString(objRet);
                }
            }

            return null;
        }
    }
}

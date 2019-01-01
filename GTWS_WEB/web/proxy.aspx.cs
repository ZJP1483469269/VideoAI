using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLKJ.DB;
using TLKJ.Utils;
using TLKJ.WEB;
using System.IO;

public partial class proxy : PageEx
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String cPageID = StringEx.getSafeQueryString(Request.QueryString["PAGE_ID"]);
        if (String.IsNullOrWhiteSpace(cPageID))
        {
            cPageID = "NAV_LIST";
        }
        if (String.IsNullOrWhiteSpace(cPageID))
        {
            Response.Write("");
            Response.End();
        }
        else
        {
            DataTable dtConfig = DbManager.QueryData("SELECT * FROM s_page WHERE PAGE_ID='" + cPageID + "'");
            String cPageHtml = StringEx.getString(dtConfig, 0, "PAGE_HTML");

            while (cPageHtml.IndexOf("{template}") > -1)
            {
                int iStart = cPageHtml.LastIndexOf("{template}");
                int iFinish = cPageHtml.LastIndexOf("{/template}");
                String cRowTemplate = "";
                if ((iStart > -1) && (iFinish > -1))
                {
                    cRowTemplate = cPageHtml.Substring(iStart, iFinish - iStart + "{/template}".Length);
                    String cRepeatPage = cRowTemplate.Replace("{template}", "").Replace("{/template}", "");
                    String cTemplate = getTemplate(cRepeatPage);
                    cPageHtml = cPageHtml.Replace(cRowTemplate, cTemplate);
                }
            }
            Response.Clear();
            Response.Write(cPageHtml);
            Response.End();
        }
    }

    protected String getTemplate(String cPageID)
    {
        if (String.IsNullOrWhiteSpace(cPageID))
        {
            return "";
        }
        else
        {
            DataTable dtConfig = DbManager.QueryData("SELECT * FROM s_template WHERE TEMPLATE_ID='" + cPageID + "'");
            String cPageHtml = StringEx.getString(dtConfig, 0, "PAGE_HTML");
            String cSQLText = StringEx.getString(dtConfig, 0, "SQL_TEXT");
            if (!String.IsNullOrWhiteSpace(cSQLText))
            {
                DataTable dtRows = DbManager.QueryData(cSQLText);
                int iStart = cPageHtml.IndexOf("{for}");
                int iFinish = cPageHtml.IndexOf("{/for}");
                String cRowTemplate = null;
                if ((iStart > -1) && (iFinish > -1))
                {
                    cRowTemplate = cPageHtml.Substring(iStart, iFinish - iStart + "{/for}".Length);
                }
                else
                {
                    cRowTemplate = cPageHtml;
                }

                String cRepeatPage = cRowTemplate.Replace("{for}", "").Replace("{/for}", "");

                StringBuilder sb = new StringBuilder();
                for (int i = 0; (dtRows != null) && (i < dtRows.Rows.Count); i++)
                {
                    String cRowText = cRepeatPage;
                    cRowText = cRowText.Replace("{row_no}", StringEx.getString(i + 1));
                    for (int j = 0; j < dtRows.Columns.Count; j++)
                    {
                        String cFieldName = dtRows.Columns[j].ColumnName;
                        String cFieldValue = StringEx.getString(dtRows, i, j);
                        cRowText = cRowText.Replace("{" + cFieldName.ToLower() + "}", StringEx.getString(cFieldValue));
                    }
                    sb.Append(cRowText);
                }
                cPageHtml = cPageHtml.Replace(cRowTemplate, sb.ToString());
            }
            return cPageHtml;
        }
    }
}
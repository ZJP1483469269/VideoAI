using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using Xceed.Words.NET;

namespace TLKJ.WebSys
{
    /// <summary>
    /// WordDocx 的摘要说明
    /// </summary>
    public class WordDocx
    {
        public WordDocx()
        {


        }
        public void ExportReport(String cTemplate, String cExportFileName, DataTable dtRows)
        {
            String cFileUrl = @"C:\template.docx";
            String cFile2Url = @"C:\2017.docx";
            DocX doc = DocX.Load(cFileUrl);
            doc.ReplaceText("{VAR_WF_GDMJ}", "违法耕地面积12345");
            doc.SaveAs(cFile2Url);
        }
    }
}
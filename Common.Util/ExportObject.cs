using System;
using System.Collections.Generic;
using System.Web;
namespace TLKJ.Utils
{
    /// <ExportObject>
    /// ExportObject 的摘要说明
    /// </summary>
    public class ExportObject
    {
        public string TABLE_ID;
        public string SYS_ID;
        public ExportRow rows = new ExportRow();

        public void AddFieldValue(String cFieldName, String cFieldValue)
        {
            ExportField vo = new ExportField();
            vo.FieldName = cFieldName;
            vo.FieldValue = cFieldValue;
            rows.FieldList.Add(vo);
        }

        public void AddFileValue(String cID, String cText, String cUrl, String cData)
        {
            ExportFile vo = new ExportFile();
            vo.ID = cID;
            vo.Text = cText;
            vo.Url = cUrl;
            vo.Data = cData;
            rows.FileList.Add(vo);
        }
    }
}
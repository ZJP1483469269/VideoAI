using System;
using System.Collections.Generic;
using System.Web;
namespace TLKJ.Utils
{
    /// <ExportRow>
    /// ExportRow 的摘要说明
    /// </summary>
    public class ExportRow
    {
        public List<ExportField> FieldList = new List<ExportField>();
        public List<ExportFile> FileList = new List<ExportFile>();
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Data;

namespace TLKJ.Utils
{
    /// <summary>
    /// Summary description for DBResult
    /// </summary>
    public class DBResult
    {
        public int Code = -1;

        public int PAGE_COUNT = 0;
        public int ROW_COUNT = 0;
        public int PAGE_SIZE = 10;
        public int PAGE_NO = 1;
        public string Message = "";
        public DataTable dtrows;
        public DBResult()
        {

        }
    }
}
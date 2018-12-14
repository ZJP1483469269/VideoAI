using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class XT_JB_RESULT
    {
        private String _id;
        [FieldAttr(FieldDesc = "ID", DBLength = 26)]
        public String id
        {
            get { return _id; }
            set { _id = value; }
        }
        private String _result_date;
        [FieldAttr(FieldDesc = "result_date", DBLength = 8)]
        public String result_date
        {
            get { return _result_date; }
            set { _result_date = value; }
        }
        private String _result;
        [FieldAttr(FieldDesc = "举报结果", DBLength = 3000)]
        public String result
        {
            get { return _result; }
            set { _result = value; }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class S_PAGE
    {
        private String _page_id;
        [FieldAttr(isKey = true, FieldDesc = "页面编码", DBLength = 60)]
        public String page_id
        {
            get { return _page_id; }
            set { _page_id = value; }
        }
        private String _page_name;
        [FieldAttr(FieldDesc = "页面名称", DBLength = 200)]
        public String page_name
        {
            get { return _page_name; }
            set { _page_name = value; }
        }
        private String _page_html;
        [FieldAttr(FieldDesc = "页面内容", DBLength = 60)]
        public String page_html
        {
            get { return _page_html; }
            set { _page_html = value; }
        }

        private String _org_id;
        [FieldAttr(FieldDesc = "单位编码", DBLength = 60)]
        public String org_id
        {
            get { return _org_id; }
            set { _org_id = value; }
        }

        private int _isactive;
        [FieldAttr(FieldDesc = "状态", DBType = ActionField.ftBoolean)]
        public int isactive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public S_PAGE()
        {

        }
    }
}

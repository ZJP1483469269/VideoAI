using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class S_TEMPLATE
    {
        private String _template_id;
        [FieldAttr(isKey = true, FieldDesc = "模板编码", DBLength = 60)]
        public String template_id
        {
            get { return _template_id; }
            set { _template_id = value; }
        }
        private String _template_name;
        [FieldAttr(FieldDesc = "模板名称", DBLength = 200)]
        public String template_name
        {
            get { return _template_name; }
            set { _template_name = value; }
        }

        private String _sql_text;
        [FieldAttr(FieldDesc = "SQL语句", DBLength = 200)]
        public String sql_text
        {
            get { return _sql_text; }
            set { _sql_text = value; }
        }

        private String _org_id;
        [FieldAttr(FieldDesc = "单位编码", DBLength = 60)]
        public String org_id
        {
            get { return _org_id; }
            set { _org_id = value; }
        }

        private String _page_html;
        [FieldAttr(FieldDesc = "页面内容", DBLength = 60)]
        public String page_html
        {
            get { return _page_html; }
            set { _page_html = value; }
        }


        public S_TEMPLATE()
        {

        }
    }
}

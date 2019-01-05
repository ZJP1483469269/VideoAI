using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class XT_IMG_REC
    {
        private String _id;
        [FieldAttr(FieldDesc = "新闻编码", DBLength = 60)]
        public String id
        {
            get { return _id; }
            set { _id = value; }
        }
        private String _org_id;
        [FieldAttr(FieldDesc = "单位编码", DBLength = 30)]
        public String org_id
        {
            get { return _org_id; }
            set { _org_id = value; }
        }

        private String _cls_id;
        [FieldAttr(FieldDesc = "类型", DBLength = 30)]
        public String cls_id
        {
            get { return _cls_id; }
            set { _cls_id = value; }
        }

        private String _page_title;
        [FieldAttr(FieldDesc = "标题", DBLength = 200)]
        public String page_title
        {
            get { return _page_title; }
            set { _page_title = value; }
        }

        private String _page_html;
        [FieldAttr(FieldDesc = "内容", DBLength = 3000)]
        public String page_html
        {
            get { return _page_html; }
            set { _page_html = value; }
        }

        private String _publish_org;
        [FieldAttr(FieldDesc = "发布单位", DBLength = 120)]
        public String publish_org
        {
            get { return _publish_org; }
            set { _publish_org = value; }
        }

        private String _publish_date;
        [FieldAttr(FieldDesc = "发布日期", DBLength = 30)]
        public String publish_date
        {
            get { return _publish_date; }
            set { _publish_date = value; }
        }
        private String _update_time;
        [FieldAttr(FieldDesc = "填报时间", DBLength = 30)]
        public String update_time
        {
            get { return _update_time; }
            set { _update_time = value; }
        }

        private int _is_active;
        [FieldAttr(FieldDesc = "是否有效", DBType = ActionField.ftBoolean)]
        public int is_active
        {
            get { return _is_active; }
            set { _is_active = value; }
        }
        
         private int _ALARM_FLAG;
        [FieldAttr(FieldDesc = "是否有效", DBType = ActionField.ftBoolean)]
        public int ALARM_FLAG
        {
            get { return _ALARM_FLAG; }
            set { _ALARM_FLAG = value; }
        }
       
        private String _REC_ID;
        [FieldAttr(FieldDesc = "填报时间", DBLength = 30)]
        public String REC_ID
        {
            get { return _REC_ID; }
            set { _REC_ID = value; }
        }


        public XT_IMG_REC()
        {

        }
    }
}

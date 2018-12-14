using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class XT_JB
    {
        private String _id;
        [FieldAttr(FieldDesc = "ID", DBLength = 26)]
        public String id
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _xianqu;
        [FieldAttr(FieldDesc = "县区", DBLength = 60)]
        public String xianqu
        {
            get { return _xianqu; }
            set { _xianqu = value; }
        }

        private String _adress;
        [FieldAttr(FieldDesc = "发生地", DBLength = 60)]
        public String adress
        {
            get { return _adress; }
            set { _adress = value; }
        }

        private String _time;
        [FieldAttr(FieldDesc = "时间", DBLength = 14)]
        public String time
        {
            get { return _time; }
            set { _time = value; }
        }

        private String _appid;
        [FieldAttr(FieldDesc = "微信ID", DBLength = 60)]
        public String appid
        {
            get { return _appid; }
            set { _appid = value; }
        }

        private String _danwei;
        [FieldAttr(FieldDesc = "单位", DBLength = 60)]
        public String danwei
        {
            get { return _danwei; }
            set { _danwei = value; }
        }

        private String _org_id;
        [FieldAttr(isRequire = true, FieldDesc = "单位编码", DBLength = 6)]
        public String org_id
        {
            get { return _org_id; }
            set { _org_id = value; }
        }

        private String _open_id;
        [FieldAttr(FieldDesc = "举报用户OPEN_ID", DBLength = 60)]
        public String open_id
        {
            get { return _open_id; }
            set { _open_id = value; }
        }

        private String _jbtype;
        [FieldAttr(FieldDesc = "举报类型", DBLength = 10)]
        public String jbtype
        {
            get { return _jbtype; }
            set { _jbtype = value; }
        }

        private int _sd_result;
        [FieldAttr(FieldDesc = "审核结果")]
        public int sd_result
        {
            get { return _sd_result; }
            set { _sd_result = value; }
        }

        private String _neirong;
        [FieldAttr(FieldDesc = "举报内容", DBLength = 30)]
        public String neirong
        {
            get { return _neirong; }
            set { _neirong = value; }
        }


        private String _files_id;
        [FieldAttr(FieldDesc = "举报文件ID列表", DBLength = 160)]
        public String files_id
        {
            get { return _files_id; }
            set { _files_id = value; }
        }
    }
}

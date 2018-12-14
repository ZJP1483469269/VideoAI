using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class XT_CAMERA
    {
        private String _id;
        [FieldAttr(isKey = true, FieldDesc = "设备编号", DBLength = 30)]
        public String id
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _org_id;
        [FieldAttr(FieldDesc = "所属县区编码", DBLength = 6)]
        public String org_id
        {
            get { return _org_id; }
            set { _org_id = value; }
        }

        private String _county;
        [FieldAttr(FieldDesc = "所属县区", DBLength = 60)]
        public String county
        {
            get { return _county; }
            set { _county = value; }
        }
        private String _village;
        [FieldAttr(FieldDesc = "所属乡镇", DBLength = 60)]
        public String village
        {
            get { return _village; }
            set { _village = value; }
        }

        private String _unit;
        [FieldAttr(FieldDesc = "安装位置所属单位", DBLength = 60)]
        public String unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        private Double _x;
        [FieldAttr(FieldDesc = "X")]
        public Double x
        {
            get { return _x; }
            set { _x = value; }
        }
        private Double _y;
        [FieldAttr(FieldDesc = "Y")]
        public Double y
        {
            get { return _y; }
            set { _y = value; }
        }
        private String _view_range;
        [FieldAttr(FieldDesc = "监控范围", DBLength = 60)]
        public String view_range
        {
            get { return _view_range; }
            set { _view_range = value; }
        }
        private String _addr;
        [FieldAttr(FieldDesc = "详细地址", DBLength = 60)]
        public String addr
        {
            get { return _addr; }
            set { _addr = value; }
        }

        private String _device_id;
        [FieldAttr(FieldDesc = "设备编号", DBLength = 100)]
        public String device_id
        {
            get { return _device_id; }
            set { _device_id = value; }
        }

        private int _is_complete;
        [FieldAttr(FieldDesc = "是否建设完成", DBType = ActionField.ftBoolean)]
        public int is_complete
        {
            get { return _is_complete; }
            set { _is_complete = value; }
        }

        public XT_CAMERA()
        {

        }
    }
}

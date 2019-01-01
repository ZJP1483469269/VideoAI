using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class XT_CAMERA_CONFIG
    {
        private String _device_id;
        [FieldAttr(isKey = true, FieldDesc = "字典编码", DBLength = 60)]
        public String device_id
        {
            get { return _device_id; }
            set { _device_id = value; }
        }
        private Double _offset_p;
        [FieldAttr(FieldDesc = "偏移量P", DBType = ActionField.ftDouble)]
        public Double offset_p
        {
            get { return _offset_p; }
            set { _offset_p = value; }
        }
        private Double _offset_t;
        [FieldAttr(FieldDesc = "偏移量T", DBType = ActionField.ftDouble)]
        public Double offset_t
        {
            get { return _offset_t; }
            set { _offset_t = value; }
        }

        private Double _offset_x;
        [FieldAttr(FieldDesc = "偏移量X", DBType = ActionField.ftDouble)]
        public Double offset_x
        {
            get { return _offset_x; }
            set { _offset_x = value; }
        }

        private Double _offset_h;
        [FieldAttr(FieldDesc = "偏移量H", DBType = ActionField.ftDouble)]
        public Double offset_h
        {
            get { return _offset_h; }
            set { _offset_h = value; }
        }
        private Double _x;
        [FieldAttr(FieldDesc = "GPS.X坐标", DBType = ActionField.ftDouble)]
        public Double x
        {
            get { return _x; }
            set { _x = value; }
        }
        private Double _y;
        [FieldAttr(FieldDesc = "GPS.Y坐标", DBType = ActionField.ftDouble)]
        public Double y
        {
            get { return _y; }
            set { _y = value; }
        }
        public XT_CAMERA_CONFIG()
        {

        }
    }
}

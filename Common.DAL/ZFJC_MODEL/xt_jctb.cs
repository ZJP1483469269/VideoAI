using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class xt_jctb
    {
        private int _objectid_1;
        [FieldAttr(FieldDesc = "序号", DBLength = 20)]
        public int objectid_1
        {
            get { return _objectid_1; }
            set { _objectid_1 = value; }
        }

        private string _ogr_geometry;
        [FieldAttr(FieldDesc = "图斑坐标", DBLength = Int16.MaxValue)]
        public string ogr_geometry
        {
            get { return _ogr_geometry; }
            set { _ogr_geometry = value; }
        }

        private string _XZQDM;
        [FieldAttr(FieldDesc = "行政区代码", DBLength = 30)]
        public string XZQDM
        {
            get { return _XZQDM; }
            set { _XZQDM = value; }
        }
        private string _XMC;
        [FieldAttr(FieldDesc = "行政区名称", DBLength = 30)]
        public string XMC
        {
            get { return _XMC; }
            set { _XMC = value; }
        }
        private int _JCBH;
        [FieldAttr(FieldDesc = "监测编号", DBLength = 30)]
        public int JCBH
        {
            get { return _JCBH; }
            set { _JCBH = value; }
        }
        private string _TBLX;
        [FieldAttr(FieldDesc = "图斑类型", DBLength = 30)]
        public string TBLX
        {
            get { return _TBLX; }
            set { _TBLX = value; }
        }
        private string _JCMJ;
        [FieldAttr(FieldDesc = "监测面积", DBLength = 30)]
        public string JCMJ
        {
            get { return _JCMJ; }
            set { _JCMJ = value; }
        }


    }
}

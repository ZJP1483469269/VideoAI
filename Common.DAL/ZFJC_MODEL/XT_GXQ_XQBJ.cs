using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class XT_GXQ_XQBJ
    {
        private int _OBJECTID;
        [FieldAttr(isKey = true, FieldDesc = "编码", DBLength = 30)]
        public int OBJECTID
        {
            get { return _OBJECTID; }
            set { _OBJECTID = value; }
        }

        private String _GXQDM;
        [FieldAttr(FieldDesc = "管辖区代码", DBLength = 60)]
        public String GXQDM
        {
            get { return _GXQDM; }
            set { _GXQDM = value; }
        }

        private String _GXQMC;
        [FieldAttr(FieldDesc = "管辖区名称", DBLength = 60)]
        public String GXQMC
        {
            get { return _GXQMC; }
            set { _GXQMC = value; }
        }

        private Double _CITY;
        [FieldAttr(FieldDesc = "城市", DBLength = 20)]
        public Double CITY
        {
            get { return _CITY; }
            set { _CITY = value; }
        }

        private int _DH;
        [FieldAttr(FieldDesc = "带号")]
        public int DH
        {
            get { return _DH; }
            set { _DH = value; }
        }

        private String _Shape;
        [FieldAttr(FieldDesc = "坐标", DBType = ActionField.ftBoolean)]
        public String Shape
        {
            get { return _Shape; }
            set { _Shape = value; }
        }
       
    }
}

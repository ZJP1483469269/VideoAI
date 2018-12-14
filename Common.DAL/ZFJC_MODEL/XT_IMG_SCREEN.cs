using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.DAO
{
    public class XT_IMG_SCREEN
    {
        private String _id;
        [FieldAttr(FieldDesc = "ID", DBLength = 26)]
        public String id
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _CAMERA_ID;
        [FieldAttr(FieldDesc = "摄像机ID", DBLength = 60)]
        public String CAMERA_ID
        {
            get { return _CAMERA_ID; }
            set { _CAMERA_ID = value; }
        }

        private String _CAMERA_NAME;
        [FieldAttr(FieldDesc = "摄像机名称", DBLength = 60)]
        public String CAMERA_NAME
        {
            get { return _CAMERA_NAME; }
            set { _CAMERA_NAME = value; }
        }

        private String _SCREEN_URL;
        [FieldAttr(FieldDesc = "截图地址", DBLength = 100)]
        public String SCREEN_URL
        {
            get { return _SCREEN_URL; }
            set { _SCREEN_URL = value; }
        }

        private String _CREATE_TIME;
        [FieldAttr(FieldDesc = "截图时间", DBLength = 60)]
        public String CREATE_TIME
        {
            get { return _CREATE_TIME; }
            set { _CREATE_TIME = value; }
        }

        private int _IS_CUTOUT;
        [FieldAttr(FieldDesc = "是否抠图")]
        public int IS_CUTOUT
        {
            get { return _IS_CUTOUT; }
            set { _IS_CUTOUT = value; }
        }

        private String _CUTOUT_URL;
        [FieldAttr(FieldDesc = "抠图路径", DBLength = 100)]
        public String CUTOUT_URL
        {
            get { return _CUTOUT_URL; }
            set { _CUTOUT_URL = value; }
        }

        private int _CUTOUT_COUNT;
        [FieldAttr(isRequire = true, FieldDesc = "抠图数量")]
        public int CUTOUT_COUNT
        {
            get { return _CUTOUT_COUNT; }
            set { _CUTOUT_COUNT = value; }
        }

        private String _CUTOUT_TXT;
        [FieldAttr(FieldDesc = "抠图描述")]
        public String CUTOUT_TXT
        {
            get { return _CUTOUT_TXT; }
            set { _CUTOUT_TXT = value; }
        }

        private int _IS_DONE;
        [FieldAttr(FieldDesc = "是否处理")]
        public int IS_DONE
        {
            get { return _IS_DONE; }
            set { _IS_DONE = value; }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Web;
using System.IO;

namespace TLKJ.Utils
{
    /// <summary>
    /// Summary description for RoleClass
    /// </summary>
    public class AppConfig
    {
        public static int FAILURE = -1;
        public static int SUCCESS = 1;
        public static int VALIDATE = 2;
        //登录时候的session 
        public readonly static string SESSION_USER_INF = "SESSION_USER_INF";
        public readonly static string SESSION_USER_ID = "SESSION_USER_ID";

        public readonly static string __DBKEY = "__dbkey";
        public readonly static string __PAGE_SIZE = "__PAGE_SIZE";
        public readonly static string __PAGE_NO = "__PAGE_NO";

        public readonly static string ACTIVE_MQ_HOST = "ACTIVE_MQ_HOST";
        public readonly static string ACTIVE_MQ_PORT = "ACTIVE_MQ_PORT";
        public readonly static string WEB_URL = "WEB_URL";

        public readonly static string CACHE_USER = "CACHE_USER";


        public static readonly string VALID = "VALID";
        public static readonly string EMPTY = "EMPTY";

        public static readonly string IMAGE_MAX = "IMAGE_MAX";
        public static readonly string IMAGE_MIN = "IMAGE_MIN"; 

        public static readonly string GRAY_MIN = "GRAY_MIN";
        public static readonly string GRAY_MAX = "GRAY_MAX";
        public static readonly string RECT_TOP = "RECT_TOP";
        public static readonly string RECT_LEFT = "RECT_LEFT";
        public static readonly string RECT_WIDTH = "RECT_WIDTH";
        public static readonly string RECT_HEIGHT = "RECT_HEIGHT";

        public static readonly string BINARY_MIN = "BINARY_MIN";
        public static readonly string BINARY_MAX = "BINARY_MAX";

        public static readonly string DAY_AM = "DAY_AM";
        public static readonly string DAY_PM = "DAY_PM";
        public static readonly string EXPORT_IMAGE = "EXPORT_IMAGE";
        public static readonly string ORG_ID = "ORG_ID"; 

        //public readonly static string COOKIE_COOKIEUSERNAME = "COOKIE_COOKIEUSERNAME";

        public static void CheckDir(string cFileDir)
        {
            if (!Directory.Exists(cFileDir))
            {
                Directory.CreateDirectory(cFileDir);
            }
        }
    }
}
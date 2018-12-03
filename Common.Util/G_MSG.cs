using System;
using System.Collections.Generic;
using System.Text;
using TLKJ.Utils;

namespace TLKJ.Utils
{
    public class G_MSG
    {
        public static int Code;
        public static int iVal;
        public static float fVal;
        public static double dVal;
        public static String cVal;
        public static void Clear()
        {
            Code = AppConfig.FAILURE;
            iVal = 0;
            fVal = 0;
            dVal = 0;
            cVal = "";
        }

        public static void setVal(int iCode)
        {
            G_MSG.Code = AppConfig.SUCCESS;
            G_MSG.iVal = iCode;
        }

        public static void setVal(float iCode)
        {
            G_MSG.Code = AppConfig.SUCCESS;
            G_MSG.fVal = iCode;
        }

        public static void setVal(double iCode)
        {
            G_MSG.Code = AppConfig.SUCCESS;
            G_MSG.dVal = iCode;
        }

        public static void setVal(String cVal)
        {
            G_MSG.Code = AppConfig.SUCCESS;
            G_MSG.cVal = cVal;
        }
    }
}

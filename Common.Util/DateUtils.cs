using System;
using System.Collections.Generic;
using System.Data; 
using System.Text;
using System.Web;
namespace TLKJ.Utils
{
    public class DateUtils
    {
        public static String getDayTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static String getDay()
        {
            return getDayTime().Substring(0, 10);
        }

        public static String getDayNum()
        {
            return getDay().Replace("-", "");
        }

        public static String getDayNum(String cDay)
        {
            return cDay.Replace("-", "");
        }

        public static Int64 getDayTimeNum()
        {
            String cStr = DateTime.Now.ToString("yyyyMMddHHmmss");
            return Int64.Parse(cStr);
        }

        public static String getDayMin(String cDay)
        {
            String cStr = cDay.Replace(":", "").Replace("-", "").Replace(" ", "");
            return cStr.Substring(0, 8) + "000000";
        }

        public static String getDayMax(String cDay)
        {
            String cStr = cDay.Replace(":", "").Replace("-", "").Replace(" ", "");
            return cStr.Substring(0, 8) + "999999";
        }

        public static String getDayAdd(String cDay, int iDay)
        {
            DateTime vd = DateTime.Parse(cDay);
            vd.AddDays(iDay);
            return vd.ToString("yyyy-MM-dd");
        }

        public static String getDateStr(DateTime vDate, String cFormat)
        {
            if (vDate == null)
            {
                return "";
            }
            String cStr = "";
            try
            {
                cStr = vDate.ToString(cFormat);
            }
            catch (Exception e)
            {
                return "";
            }
            return cStr;
        }

        public static String getDateVal(String lTime)
        {
            return null;
        }

        public static String getDateTimeVal(String lTime)
        {
            return null;
        }

        public static DateTime getDateTimeVal(Int64 lTime)
        {
            return DateTime.Now;
        }

        public static DateTime getDateValue(Int64 lTime)
        {
            return getDateValue(lTime.ToString(), "yyyyMMddHHmmss");
        }

        public static DateTime getDateValue(String cDate, String cFormat)
        {
            DateTime vDate = DateTime.Now;
            try
            {
                vDate = DateTime.Parse(cDate);
            }
            catch (Exception e)
            {
                ;
            }
            return vDate;
        }

    }
}
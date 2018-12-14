using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections;

namespace TLKJ.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class ActiveResult
    {
        public int result;
        public int total = 0;
        public ArrayList rows = new ArrayList();
        public String validfield = "";
        public String validmessage = "";
        public Object info = new Object();
        public int isLock = 0;
        //public int tbtype = -1; //0违法，1合法，2非新增的
        public ActiveResult()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 执行返回结果
        /// </summary>
        /// <param name="iEffect"></param>
        /// <returns></returns>
        public static ActiveResult Valid(int iEffect)
        {
            ActiveResult ret = new ActiveResult();
            if (iEffect > 0)
                ret.result = AppConfig.SUCCESS;
            else
                ret.result = AppConfig.FAILURE;
            return ret;
        }

        public static ActiveResult Valid(bool TypeOfBool)
        {
            ActiveResult ret = new ActiveResult();
            if (TypeOfBool)
                ret.result = AppConfig.SUCCESS;
            else
                ret.result = AppConfig.FAILURE;
            return ret;
        }
        /// <summary>
        /// 执行返回结果
        /// </summary>
        /// <param name="iEffect"></param>
        /// <returns></returns>
        public static ActiveResult Valid(int iEffect, string str)
        {
            ActiveResult ret = new ActiveResult();
            if (iEffect > 0)
            {
                ret.result = 0;
            }
            else
            {
                ret.isLock = iEffect;
            }

            return ret;

        }

        /// <summary>
        /// 字段数据验证失败
        /// </summary>
        /// <param name="cFieldName"></param>
        /// <param name="cValidMessage"></param>
        /// <returns></returns>
        public static ActiveResult Valid(String cFieldName, String cValidMessage)
        {
            ActiveResult ret = new ActiveResult();
            ret.result = AppConfig.FAILURE;
            ret.validfield = cFieldName;
            ret.validmessage = cValidMessage;
            return ret;
        }

        /// <summary>
        /// 错误的信息

        /// </summary>
        /// <param name="cMessage"></param>
        /// <returns></returns>
        public static ActiveResult Valid(String cMessage)
        {
            ActiveResult ret = new ActiveResult();
            ret.result = AppConfig.FAILURE;
            ret.validfield = "";
            ret.validmessage = cMessage;
            return ret;
        }

        /// <summary>
        /// 返回数据集对象

        /// </summary>
        /// <param name="dtRows"></param>
        /// <returns></returns>
        public static ActiveResult Query(DataTable dtRows)
        {
            ActiveResult ret = new ActiveResult();
            if (dtRows == null)
            {
                ret.result = AppConfig.FAILURE;
            }
            else
            {
                ret.result = AppConfig.SUCCESS;
                ret.total = dtRows.Rows.Count;
                ret.rows = JsonLib.ConvertDataTableToList(dtRows);
            }

            return ret;
        }

        /// <summary>
        /// 返回单个数据对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static ActiveResult returnObject(Object obj)
        {
            ActiveResult ret = new ActiveResult();
            if (obj == null)
            {
                ret.result = AppConfig.FAILURE;
            }
            else
            {
                ret.result = AppConfig.SUCCESS;
                ret.info = obj;
            }
            return ret;
        }

        /// <summary>
        /// 转成JSON对象字符串

        /// </summary>
        /// <returns></returns>
        public String toJSONString()
        {
            String cStr = JsonLib.ToJSON(this);
            log4net.WriteLogFile("ActiveResult：" + cStr);
            return JsonLib.ToJSON(this);
        }
    }
}
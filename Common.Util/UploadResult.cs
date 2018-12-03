﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
namespace TLKJ.Utils
{
    /// <summary>
    /// UploadResult 的摘要说明
    /// </summary>
    public class UploadResult
    {
        public int Code = 0;
        public String ID = "";
        public String Url = "";
        public String Text = "";
        public UploadResult()
        {
            Code = AppConfig.SUCCESS;
        }

        public static UploadResult Valid(int iCode)
        {
            UploadResult ret = new UploadResult();
            if (iCode > 0)
                ret.Code = AppConfig.SUCCESS;
            else
                ret.Code = AppConfig.FAILURE;
            return ret;
        }

        public String ToJSON()
        {
            return JsonLib.ToJSON(this);
        }
    }
}
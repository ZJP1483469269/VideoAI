<%@ WebHandler Language="C#" Class="DefaultAspx" %>

using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Configuration;
using TLKJ.Utils;
using TLKJ.DB;
using TLKJ.WebSys;
using System.Data;
using System.Text;
using System.Collections.Generic;
public class DefaultAspx : IHttpHandler
{
    public HttpRequest request;
    public HttpResponse response;
    public String Url_ActionTypeID = "";
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        this.response = context.Response;
        this.request = context.Request;
        log4net.WriteLogFile("参数传递错误，action_method为空！");
        Url_ActionTypeID = StringEx.getString(context.Request["action_method"]);
        if (Url_ActionTypeID.Length == 0)
        {
            log4net.WriteLogFile("参数传递错误，action_method为空！");
            response.Write(ActiveResult.Valid("action_method", "不能为空").toJSONString());
            response.End();
            return;
        }
        Type t = this.GetType();
        System.Reflection.MethodInfo mv = t.GetMethod(Url_ActionTypeID);
        if (mv == null)
        {
            log4net.WriteLogFile("未发现方法" + Url_ActionTypeID + "！");
            response.Write(ActiveResult.Valid("action_method", Url_ActionTypeID + "未发现").toJSONString());
            response.End();
            return;
        }
        mv.Invoke(this, null);
    }

    /// <summary>
    /// 用户登录
    /// </summary>
    public void UserLogin()
    {
        try
        {
            String cUserCount = StringEx.getSafeQueryString(request["user_name"]);//密码
            String cUserPassw = StringEx.getSafeQueryString(request["user_pass"]);//账号
            String cCurVersion = StringEx.getString(request["app_ver"]); //客户端版本
            String cAndroidVer = StringEx.getString(request["androidver"]); //android版本
            String cDeviceId = StringEx.getString(request["deviceid"]); //设备ID

            string sql = "select count(1) from XT_MOBILE_USER where USER_COUNT='" + cUserCount + "' and PASS_WORD='" + cUserPassw + "' AND IS_ACTIVE='1'";
            int iCode = StringEx.getInt(DbManager.GetStrValue(sql));
            ActiveResult vert = ActiveResult.Valid(iCode);
            if (iCode == 1)
            {//登录成功
                String currentStr = DateTime.Now.ToLocalTime().ToString();
                sql = "update XT_MOBILE_USER set APPVER='" + cCurVersion + "',DEVICEID='" + cDeviceId + "',ANDRIOD_ID='" + cAndroidVer + "',UPDATETIME='" + currentStr + "' where PHONE_NUM = '" + cUserCount + "'";
                iCode = DbManager.ExecSQL(sql);
                if (iCode == 0)
                {
                    log4net.WriteLogFile("手机版本号更新错误");
                    response.Write(ActiveResult.Valid("app_ver", "客户端版本更新失败").toJSONString());
                    return;
                }
                sql = "select ORG_ID from XT_MOBILE_USER where USER_COUNT='" + cUserCount + "'";
                DataTable dt_table_userInfo = DbManager.QueryData(sql);
                Hashtable ht = new Hashtable();
                if (dt_table_userInfo != null && dt_table_userInfo.Rows.Count > 0)
                {
                    ht.Add("usre_org", StringEx.getString(dt_table_userInfo, 0, "ORG_ID"));
                    vert.info = ht;
                }
                DataTable dt_table_userOrgInfo = DbManager.QueryData("select ORG_NAME from xt_org where ORG_ID in(select ORG_ID from XT_MOBILE_USER where USER_COUNT ='" + cUserCount + "')");
                if (dt_table_userOrgInfo != null && dt_table_userOrgInfo.Rows.Count > 0)
                {
                    ht.Add("org_name", StringEx.getString(dt_table_userOrgInfo, 0, "ORG_NAME"));
                    vert.info = ht;
                }
                response.Write(vert.toJSONString());
            }
            else
            {
                sql = "select * from XT_MOBILE_USER where USER_COUNT =" + cUserCount;
                DataTable dt_table = null;
                dt_table = DbManager.QueryData(sql);
                if (dt_table.Rows.Count <= 0)
                {
                    response.Write(ActiveResult.Valid("USER_COUNT", "账号不存在").toJSONString());
                }
                else
                {
                    response.Write(ActiveResult.Valid("USER_COUNT", "账号密码不正确").toJSONString());
                }
            }
            response.End();
        }
        catch (Exception ex)
        {
            log4net.WriteLogFile("手机端登陆时候：" + ex.Message);
        }
    }


    /// <summary>
    /// 返回详情
    /// </summary>
    public void back_list_info()
    {
        try
        {
            String cUserCount = StringEx.getSafeQueryString(request["user_name"]);
            int cRefreshSain = StringEx.getInt(request["sign"]);
            String sql = "";
            int starSign = cRefreshSain + 1;
            int endSign = cRefreshSain + 10;
            sql = "select ORG_ID from XT_MOBILE_USER where USER_COUNT=" + cUserCount;
            String userOrg = DbManager.GetStrValue(sql);
            //sql = "select  * from(select  *, ROW_NUMBER() over(order by time desc) as rows from XT_JB)aa where rows BETWEEN '" + starSign + "' and '" + endSign + "' AND ISNULL(IS_HC,'0') = '0' and  XIANQU  = " + userOrg;
            sql = "select  *,(SELECT STUFF((select ',' + URL  from S_UPLOAD a, XT_JB b where charindex( Rtrim(a.ID) ,b.FILES_ID )>0 and aa.ID =b.id FOR XML PATH('')),1,1,'')AS T) as urls_path from(select  *, ROW_NUMBER() over(order by time desc) as rows from XT_JB)aa where rows BETWEEN '" + starSign + "' and '" + endSign + "'  AND ISNULL(IS_HC,'0') = '0' and  XIANQU  ='" + userOrg + "'";

            DataTable dt_tbale = DbManager.QueryData(sql);
            ActiveResult acts = new ActiveResult();
            Hashtable hts = new Hashtable();
            hts.Add("base", getTableHash(dt_tbale));
            acts.info = hts;
            object obj = acts.toJSONString();
            response.Write(obj);

        }
        catch (Exception ex)
        {
            log4net.WriteLogFile("返回列表数据时候：" + ex.Message);
        }
    }


    #region
    /// <summary>
    /// 返回数据列表
    /// </summary>
    //public void back_list_info()
    //{
    //    try
    //    {
    //        String cUserCount = StringEx.getSafeQueryString(request["user_name"]);
    //        int cRefreshSain = StringEx.getInt(request["sign"]);
    //        String sql = "";
    //        int starSign = cRefreshSain + 1;
    //        int endSign = cRefreshSain + 10;
    //        sql = "select ORG_ID from XT_MOBILE_USER where USER_COUNT=" + cUserCount;
    //        String userOrg = DbManager.GetStrValue(sql);
    //        sql = "select  * from(select  *, ROW_NUMBER() over(order by time desc) as rows from XT_JB)aa where rows BETWEEN '" + starSign + "' and '" + endSign + "' AND ISNULL(IS_HC,'0') = '0' and  XIANQU  = " + userOrg;
    //        DataTable dt_Tagble = DbManager.QueryData(sql);
    //        response.Write(ActiveResult.Query(dt_Tagble).toJSONString());
    //    }
    //    catch (Exception ex)
    //    {
    //        log4net.WriteLogFile("返回列表数据时候：" + ex.Message);
    //    }
    //}

    /// <summary>
    /// 根据ID返回具体详情数据
    /// </summary>
    //public void back_jb_info()
    //{
    //    String cJb_id = StringEx.getString(request["jb_id"]);
    //    String sql = "";
    //    sql = "select * from xt_jb where id ='" + cJb_id + "'";
    //    DataTable dt_tbale = null;
    //    dt_tbale = DbManager.QueryData(sql);

    //    string[] ars;
    //    ActiveResult acts = new ActiveResult();
    //    Hashtable ht = new Hashtable();
    //    Hashtable hts = new Hashtable();
    //    for (int i = 0; i < (dt_tbale == null ? 0 : dt_tbale.Rows.Count); i++)
    //    {
    //        ars = dt_tbale.Rows[i]["FILES_ID"].ToString().Split(',');
    //        for (int j = 0; j < ars.Length - 1; j++)
    //        {
    //            sql = "select url from S_UPLOAD where id ='" + ars[j] + "'";

    //            string strTemps = DbManager.GetStrValue(sql);
    //            ht.Add("filse" + j, strTemps);
    //        }

    //    }
    //    hts.Add("base", getTableHashs(dt_tbale));
    //    hts.Add("urls", ht);
    //    acts.info = hts;
    //    object obj = acts.toJSONString();
    //    response.Write(obj);
    //}
    #endregion

    /// <summary>
    /// 现场核查数据
    /// </summary>
    public void hands_verify_info()
    {
        String cID = StringEx.getString(request["hc_id"]);
        String cResult = StringEx.getString(request["hc_result"]);//是否属实 0不属实 1属实 2 部分属实
        String cNr = StringEx.getString(request["hc_nr"]);//现场状况
        String cAdressDiscript = StringEx.getString(request["hc_adress"]);// 现场位置
        String cRange = StringEx.getString(request["zb_area"]);// 坐标范围

        String cYdDw = StringEx.getString(request["yddw"]);// 用地单位
        String cZdYT = StringEx.getString(request["zdyt"]);// 战地用途
        String cZdlx = StringEx.getString(request["zdlx"]);//占地类型
        String cBz = StringEx.getString(request["bz"]);// 备注
        String cXmRiqi = StringEx.getString(request["xmrq"]);//项目日期

        String strCurDate = DateTime.Now.ToString("yyyy-MM-dd");
        List<String> arr = new List<string>();
        String sql = "";
        HttpFileCollection files = request.Files;
        log4net.WriteLogFile("上传文件个数：" + files.Count.ToString());
        if (files != null)
        {
            int iFileLen = files.Count;
            for (int i = 0; i < iFileLen; i++)
            {
                HttpPostedFile hPf = files[i];
                String cFileName = System.IO.Path.GetFileName(hPf.FileName);
                String cFileType = System.IO.Path.GetExtension(hPf.FileName);
                String cFilePath = System.Web.HttpContext.Current.Server.MapPath("~/") + "images/" + strCurDate + "/";
                String current_Url = HttpContext.Current.Server.MapPath("/");
                string final_Path = cFilePath;
                if (!System.IO.Directory.Exists(final_Path))
                {
                    System.IO.Directory.CreateDirectory(final_Path);
                }
                String cChange_FilesName = AutoID.getAutoID();
                string path = final_Path + cChange_FilesName + cFileType;
                hPf.SaveAs(path);
                string photo_path = "/" + cFilePath + cChange_FilesName + cFileType;
                string video_path = "/" + cFilePath + cChange_FilesName + cFileType;

                String[] ars;
                string photo_angleName = "";
                string photo_xyPos = "";
                ars = cFileName.Split('&');
                for (int k = 0; k < ars.Length; k++)
                {
                    if (k == 1)
                    {
                        photo_angleName = ars[k].ToString();
                    }
                    else if (k == 2)
                    {
                        photo_xyPos = ars[k].ToString();
                    }
                }
                if (cFileType == ".jpg")
                {
                    video_path = "";
                }
                else
                {
                    photo_path = "";
                }
                if (photo_xyPos != "")
                {
                    photo_xyPos = photo_xyPos.Substring(0, photo_xyPos.Length - 4);
                    if (photo_xyPos.Contains(","))
                    {
                        photo_xyPos = photo_xyPos.Replace(",", "-");
                    }
                }
                sql = "insert into XT_MOBILE_PATH(jb_id,photo_path,video_path,phonto_angle,photo_xypos,load_time)values('" + cID + "','" + photo_path + "','" + video_path + "','" + photo_angleName + "','" + photo_xyPos + "','" + strCurDate + "')";
                arr.Add(sql);
            }
            sql = "insert into XT_MOBILE_HC_RESLUT(HC_ID,HC_TIME,HC_ADRESS,HC_JL,HC_JG,HC_AREA,YDDW,ZDYT,BZ,ZDLX)values('" + cID + "','" + cXmRiqi + "','" + cAdressDiscript + "','" + cResult + "','" + cNr + "','" + cRange + "','" + cYdDw + "','" + cZdYT + "','" + cBz + "','" + cZdlx + "')";
            arr.Add(sql);

            sql = "UPDATE XT_JB SET IS_HC ='1' WHERE ID = '" + cID + "'";
            arr.Add(sql);
            //sql = "select * from ";
        }
        int iCode = DbManager.ExecSQL(arr);
        ActiveResult vert = ActiveResult.Valid(iCode);
        response.Write(vert.toJSONString());
    }

    /// <summary>
    /// 移动端30秒定为
    /// </summary>
    public void update_mobile_pos()
    {
        String sql = "";
        try
        {
            String cUserCount = StringEx.getSafeQueryString(request["user_name"]);//账号
            String cMbilePosi_x = StringEx.getString(request["pos_x"]);
            String cMbilePosi_y = StringEx.getString(request["pos_y"]);
            String currentTimeStr = DateTime.Now.ToLocalTime().ToString();
            sql = "select * from XT_MOBILE_POS where PHONE_NUM = " + cUserCount;
            DataTable dt_tbale = DbManager.QueryData(sql);
            if (dt_tbale.Rows.Count > 0)
            {
                sql = "update XT_MOBILE_POS set POS_X='" + cMbilePosi_x + "',POS_Y='" + cMbilePosi_y + "' ,UPDATETIME='" + currentTimeStr + "' where PHONE_NUM =" + cUserCount;
            }
            else
            {
                sql = "insert into XT_MOBILE_POS(POS_X,POS_Y,UPDATETIME,PHONE_NUM) values('" + cMbilePosi_x + "','" + cMbilePosi_y + "','" + currentTimeStr + "','" + cUserCount + "') ";
            }

            int iCode = DbManager.ExecSQL(sql);
            ActiveResult vert = ActiveResult.Valid(iCode);
            response.Write(vert.toJSONString());
        }
        catch (Exception ex)
        {
            log4net.WriteLogFile("更新移动端坐时： sql语句" + sql + "错误内容" + ex);
        }
    }







    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    public ArrayList getTableHash(DataTable dtRows)
    {
        ArrayList arKeys = new ArrayList();
        for (int k = 0; (dtRows != null) && (k < dtRows.Rows.Count); k++)
        {
            Hashtable vo = new Hashtable();
            for (int i = 0; i < dtRows.Columns.Count; i++)
            {
                String cFieldName = dtRows.Columns[i].ColumnName;
                String cFieldValue = StringEx.getString(dtRows, k, cFieldName);
                vo.Add(cFieldName.ToLower(), cFieldValue);
            }
            arKeys.Add(vo);
        }
        return arKeys;
    }

    public Hashtable getTableHashs(DataTable dtRows)
    {
        //ArrayList arKeys = new ArrayList();
        Hashtable vos = new Hashtable();
        Hashtable vo = new Hashtable();
        for (int k = 0; (dtRows != null) && (k < dtRows.Rows.Count); k++)
        {

            for (int i = 0; i < dtRows.Columns.Count; i++)
            {
                String cFieldName = dtRows.Columns[i].ColumnName;
                String cFieldValue = StringEx.getString(dtRows, k, cFieldName);
                vo.Add(cFieldName.ToLower(), cFieldValue);
            }

        }
        return vo;
    }


    public Hashtable getTableRow(DataTable dtRows, int iRowID)
    {
        Hashtable vo = new Hashtable();
        if (dtRows != null && dtRows.Rows.Count > 0)
        {
            for (int i = 0; i < dtRows.Columns.Count; i++)
            {
                String cFieldName = dtRows.Columns[i].ColumnName;
                String cFieldValue = StringEx.getString(dtRows, iRowID, cFieldName);
                vo.Add(cFieldName.ToLower(), cFieldValue);
            }
        }
        return vo;
    }

    private class kvInfo
    {
        public String Name;
        public String Value;
        public kvInfo()
        {
        }
        public kvInfo(String cKeyName, String cKeyValue)
        {
            Name = cKeyName;
            Value = cKeyValue;
        }
    }

}
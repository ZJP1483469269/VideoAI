using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using TLKJ.Utils;
using TLKJ_IVS;

namespace TLKJ.SYS
{
    /// <summary>
    /// ChartTool 的摘要说明
    /// </summary>
    public class ChartTool
    {
        public int CHART_ID;
        public int CHART_TYPE;
        public String CHART_NAME;
        public String WHERE_PARM;
        public String GROUP_PARM;
        public String TABLENAME;
        public String ORDERBY_1;
        public String ORDERBY_2;
        public DataTable dtConfig;
        public static ChartTool LoadConfig(int iTypeID)
        {
            ChartTool vo = new ChartTool();
            vo.dtConfig = WebSQL.QueryData("select * from XT_CHART where CHART_ID='" + iTypeID + "'");
            DataTable dtRows = vo.dtConfig;
            if ((dtRows != null) && (dtRows.Rows.Count > 0))
            {
                vo.CHART_ID = StringEx.getInt(dtRows, 0, "CHART_ID");
                vo.CHART_NAME = StringEx.getString(dtRows, 0, "CHART_NAME");
                vo.TABLENAME = StringEx.getString(dtRows, 0, "CHART_TABLENAME");
                vo.WHERE_PARM = StringEx.getString(dtRows, 0, "WHERE_PARM");
                vo.CHART_TYPE = StringEx.getInt(dtRows, 0, "CHART_TYPE");
                vo.GROUP_PARM = StringEx.getString(dtRows, 0, "GROUP_PARM");

                vo.ORDERBY_1 = StringEx.getString(dtRows, 0, "ORDERBY_1");
                vo.ORDERBY_2 = StringEx.getString(dtRows, 0, "ORDERBY_2");
            }

            vo.dtConfig = WebSQL.QueryData("select * from XT_CHART_CONFIG where CHART_ID='" + iTypeID + "'");
            return vo;
        }
    }
}
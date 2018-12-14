using System; 

namespace TLKJ.Utils
{
    /// <summary>
    /// Eval 的摘要说明。
    /// </summary>
    public class Eval
    {
        public static object EvalString(string expression)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            object oret = null;
            try
            {
                System.Data.DataColumn Col = new System.Data.DataColumn("col1", typeof(string), expression);
                table.Columns.Add(Col);
                table.Rows.Add(new object[] { "" });
                oret = table.Rows[0][0];

            }
            catch (Exception)
            {
                oret = null;
                throw new EvalExcption();
            }
            return oret;
        }
    }

    class EvalExcption : System.Exception
    {
        public EvalExcption(string s, Exception e)
            : base(s, e)
        {
        }

        public EvalExcption()
            : base("EvalExcption:表达式计算有错误发生")
        {

        }
    }
}
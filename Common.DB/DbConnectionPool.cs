using System;
using System.Data;
using System.Threading;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Collections;
using System.Configuration;
using TLKJ.Utils;

/// <summary>
/// DbConnectionPool 的摘要说明 
/// </summary>

namespace TLKJ.DB
{
    public class DbConnectionPool
    {

        private DbConnectionPool()
        {

        }

        private static ArrayList AllConnects;

        //定义存放数据库链接的队列
        private static Queue Connects;

        //数据库参数  
        private static string cConnString = "";  //链接字符串 

        //链接池参数 
        private static int InitSize = 3;        //初始化链接池大小
        private static int iDB_MAX = 10;       //最大链接数
        private static int ActiveCount = 0;         //最大链接数

        //定义数据库类型，1表示为SqlServer数据库，2表示其它的OLE DB；默认为SqlServer
        private static int iDB_TYPE = 1;

        private static void InitConfig()
        {
            try
            {
                if ((AllConnects == null) || (AllConnects.Count == 0))
                {
                    AllConnects = new ArrayList();
                }
                AllConnects.Clear();
                ActiveCount = 0;

                cConnString = Config.GetAppSettings(AppConfig.DB_URL).ToString();
                iDB_MAX = int.Parse(Config.GetAppSettings(AppConfig.DB_MAX).ToString());
                iDB_TYPE = int.Parse(Config.GetAppSettings(AppConfig.DB_TYPE).ToString());
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile(ex.Message, 3);
            }
        }

        /// <summary>
        /// 初始化链接池
        /// </summary>
        private static void initPool()
        {
            InitConfig();
            log4net.WriteLogFile("初始化数据库连接", 1);
            int i;
            for (i = 1; i <= InitSize; i++)
            {
                if (iDB_TYPE == 1)
                {
                    DBSqlServer Connect = new DBSqlServer(cConnString);
                    if (Connect != null)
                        Connects.Enqueue(Connect);
                }
                else if (iDB_TYPE == 2)
                {
                    DBSqlServer Connect = new DBSqlServer(cConnString);
                    if (Connect != null)
                        Connects.Enqueue(Connect);
                }
                else if (iDB_TYPE == 3)
                {
                    DBSqlServer Connect = new DBSqlServer(cConnString);
                    if (Connect != null)
                        Connects.Enqueue(Connect);
                }
            }
        }

        /// <summary>
        /// 释放一个活运链接

        /// </summary>
        /// <param name="AConnect"></param>
        public static void ReturnConnect(JDBBASE AConnect)
        {
            try
            {
                if (AConnect != null)
                {
                    ActiveCount--;
                    if (ActiveCount > iDB_MAX)
                    {
                        AConnect.CloseConnect();
                    }
                    else
                    {
                        if (AConnect.Connectd)
                        {
                            Connects.Enqueue(AConnect);
                        }
                        else
                        {
                            AConnect = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile("数据库连接归还失败:" + ex.Message, 3);
            }
        }

        /// <summary>
        /// 释放一个活运链接

        /// </summary>
        /// <returns></returns>
        public static JDBBASE Instance()
        {
            if ((Connects == null) || (Connects.Count == 0))
            {
                Connects = new Queue();
                initPool();
            }

            //没有连接生成一个新的链接

            try
            {
                object Connect = null;
                if (Connects.Count == 0)
                {
                    if (iDB_TYPE == 1)
                    {
                        Connect = new DBSqlServer(cConnString);
                    }
                    else if (iDB_TYPE == 2)
                    {
                        Connect = new DBSqlServer(cConnString);
                    }
                    else if (iDB_TYPE == 3)
                    {
                        Connect = new DBSqlServer(cConnString);
                    }

                    if (Connect != null)
                    {
                        Connects.Enqueue(Connect);
                    }

                    //如果只有一个连接时
                    JDBBASE mydb = (JDBBASE)Connects.Dequeue();
                    if ((mydb == null) || (!mydb.Connectd))
                    {
                        ClearPool();
                        return null;
                    }
                    else
                    {
                        return mydb;
                    }
                }
                else
                {
                    //获得一个连接,如果当前或缺的链接无效，则重新申请一个

                    JDBBASE mydb = (JDBBASE)Connects.Dequeue();
                    if ((mydb == null) || (!mydb.Connectd))
                    {
                        mydb = DbConnectionPool.Instance();
                        return mydb;
                    }
                    else
                    {
                        ActiveCount++;
                        return mydb;
                    }
                }
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile(ex.Message, 3);
                return null;
            }
        }

        /// <summary>
        /// 销毁链接池
        /// </summary>
        public static void ClearPool()
        {
            while (Connects.Count > 0)
            {
                JDBBASE row = (JDBBASE)Connects.Dequeue();
                row.CloseConnect();
            }
            ActiveCount = 0;
        }
    }
}


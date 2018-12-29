using System;
using System.Collections.Generic;
using System.Text;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using TLKJ.Utils;

namespace TLKJ.SYS
{
    public class ActiveMQ_Consumer
    {
        private IConnectionFactory factory;

        private static ActiveMQ_Consumer instance;

        public static ActiveMQ_Consumer getInstance()
        {
            if (instance == null)
            {
                instance = new ActiveMQ_Consumer();
            }
            return instance;
        }

        private ActiveMQ_Consumer()
        {
            try
            {
                string cHost = INIConfig.ReadString("ACTIVE_MQ", AppConfig.ACTIVE_MQ_HOST);
                string cPort = INIConfig.ReadString("ACTIVE_MQ", AppConfig.ACTIVE_MQ_PORT);

                //初始化工厂，这里默认的URL是不需要修改的
                factory = new ConnectionFactory("tcp://" + cHost + ":" + cPort);
            }
            catch
            {
                log4net.WriteLogFile("ActiveMQ:初始化失败!!");
            }
        }

        public IConnectionFactory getFactory()
        {
            return factory;
        }

    }
}

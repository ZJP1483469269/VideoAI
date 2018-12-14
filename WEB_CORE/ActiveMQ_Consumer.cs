using System;
using System.Collections.Generic;
using System.Text;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using TLKJ.Utils;

namespace TLKJ.WebSys
{
    public class ActiveMQ_Consumer
    {
        private IConnectionFactory factory;
        public ActiveMQ_Consumer()
        {
            InitConsumer();
        }

        public void InitConsumer()
        {
            string cHost = Config.GetAppSettings(AppConfig.ACTIVE_MQ_HOST);
            string cPort = Config.GetAppSettings(AppConfig.ACTIVE_MQ_PORT);
            //创建连接工厂
            factory = new ConnectionFactory("tcp://" + cHost + ":" + cPort);
            //通过工厂构建连接
            IConnection connection = factory.CreateConnection();
            //这个是连接的客户端名称标识
            connection.ClientId = "firstQueueListener";
            //启动连接，监听的话要主动启动连接
            connection.Start();
            //通过连接创建一个会话
            ISession session = connection.CreateSession();
            //通过会话创建一个消费者，这里就是Queue这种会话类型的监听参数设置
            IMessageConsumer consumer = session.CreateConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue("firstQueue"), "filter='demo'");
            //注册监听事件
            consumer.Listener += new MessageListener(consumer_Listener);
            //connection.Stop();
            //connection.Close();  
        }

        void consumer_Listener(IMessage message)
        {
            ITextMessage msg = (ITextMessage)message;
            //异步调用下，否则无法回归主线程
             log4net.WriteLogFile(msg.Text);
            //ActiveMQ_Consumer.Invoke(new DelegateRevMessage(RevMessage), msg);
        }

        public delegate void DelegateRevMessage(ITextMessage message);

        public void RevMessage(ITextMessage message)
        {
             log4net.WriteLogFile(string.Format(@"接收到:{0}{1}", message.Text, Environment.NewLine));
        }
    }
}

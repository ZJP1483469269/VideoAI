using System;
using System.Collections.Generic;
using System.Text;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using TLKJ.Utils;

namespace TLKJ.WEB
{
    public class ActiveMQ_Producer
    {
        private static IConnectionFactory factory;

        public ActiveMQ_Producer()
        {
            InitProducer();
        }

        public void InitProducer()
        {
            try
            {
                string cHost = Config.GetAppSettings(AppConfig.ACTIVE_MQ_HOST);
                string cPort = Config.GetAppSettings(AppConfig.ACTIVE_MQ_PORT);

                //初始化工厂，这里默认的URL是不需要修改的
                //factory = new ConnectionFactory("tcp://localhost:61616");
                factory = new ConnectionFactory("tcp://" + cHost + ":" + cPort);
            }
            catch
            {
                 log4net.WriteLogFile("ActiveMQ:初始化失败!!");
            }
        }

        public static void SendMessage(String cUSR_ID, String cORG_ID, ActiveMQ_Message vMessage)
        {
            try
            {
                //通过工厂建立连接
                using (IConnection vActiveMQ = factory.CreateConnection())
                {
                    //通过连接创建Session会话
                    using (ISession session = vActiveMQ.CreateSession())
                    {
                        //通过会话创建生产者，方法里面new出来的是MQ中的Queue
                        IMessageProducer prod = session.CreateProducer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue("M" + cORG_ID + "X" + cUSR_ID));
                        //创建一个发送的消息对象
                        ITextMessage message = prod.CreateTextMessage();
                        //给这个对象赋实际的消息
                        message.Text = JsonLib.ToJSON(vMessage);
                        //设置消息对象的属性，这个很重要哦，是Queue的过滤条件，也是P2P消息的唯一指定属性
                        message.Properties.SetString("filter", "demo");
                        //生产者把消息发送出去，几个枚举参数MsgDeliveryMode是否长链，MsgPriority消息优先级别，发送最小单位，当然还有其他重载
                        prod.Send(message, MsgDeliveryMode.NonPersistent, MsgPriority.Normal, TimeSpan.MinValue);

                    }
                }
            }
            catch (Exception ex)
            {
                 log4net.WriteLogFile(ex.Message);
            }
        }

        public static void SendMessage(String cStr)
        {
            try
            {
                //通过工厂建立连接
                using (IConnection vActiveMQ = factory.CreateConnection())
                {
                    //通过连接创建Session会话
                    using (ISession session = vActiveMQ.CreateSession())
                    {
                        //通过会话创建生产者，方法里面new出来的是MQ中的Queue
                        IMessageProducer prod = session.CreateProducer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue("firstQueue"));
                        //创建一个发送的消息对象
                        ITextMessage message = prod.CreateTextMessage();
                        //给这个对象赋实际的消息
                        message.Text = cStr;
                        //设置消息对象的属性，这个很重要哦，是Queue的过滤条件，也是P2P消息的唯一指定属性
                        message.Properties.SetString("filter", "demo");
                        //生产者把消息发送出去，几个枚举参数MsgDeliveryMode是否长链，MsgPriority消息优先级别，发送最小单位，当然还有其他重载
                        prod.Send(message, MsgDeliveryMode.NonPersistent, MsgPriority.Normal, TimeSpan.MinValue);

                    }
                }
            }
            catch (Exception ex)
            {
                 log4net.WriteLogFile(ex.Message);
            }
        }
    }
}

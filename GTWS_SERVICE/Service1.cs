using Apache.NMS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;
using TLKJ.SYS;
using TLKJ.Util;
using TLKJ.Utils;

namespace GTWS_SERVICE
{
    public partial class Service1 : ServiceBase
    {
        private void Change(ActiveMQ_Message vMessage)
        {
            if (vMessage.CMD_ID == 11)
            {
                String cFormTitle = "国土智能监控取证系统";
                IntPtr hWnd = WinAPI.FindWindow(null, cFormTitle);
                if (hWnd != IntPtr.Zero)
                {
                    COPYDATASTRUCT cds = new COPYDATASTRUCT();
                    cds.lpData = vMessage.MESSAGE;
                    WinAPI.PostMessage(hWnd, WinAPI.WM_USER + 1001, 0, ref cds);
                }
                else
                {
                    try
                    {
                        String cFileName = Application.StartupPath + "\\GTWS_AI.exe";
                        if (File.Exists(cFileName))
                        {
                            System.Diagnostics.Process.Start(cFileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        log4net.WriteLogFile(ex.Message, LogType.ERROR);
                    }
                }
            }
            else if (vMessage.CMD_ID == 12)
            {
                String cFormTitle = "国土智能监控取证系统";
                IntPtr hWnd = WinAPI.FindWindow(null, cFormTitle);
                if (hWnd != IntPtr.Zero)
                {
                    WinAPI.PostMessage(hWnd, WinAPI.WM_CLOSE, 0, 0);
                }
            }
            else
            {
                log4net.WriteLogFile(string.Format(@"接收到:{0}{1}", vMessage.FROM_ID + "," + vMessage.USER_ID + "," + vMessage.MESSAGE, Environment.NewLine), LogType.ERROR);
            }
        }

        public void InitMQClient()
        {
            String cUserID = AppManager.getIPAddr();
            ActiveMQ_Consumer vMQ = ActiveMQ_Consumer.getInstance();
            IConnectionFactory vActiveMQ = vMQ.getFactory();
            IConnection vConnect = vActiveMQ.CreateConnection();
            ISession vSession = vConnect.CreateSession();

            //这个是连接的客户端名称标识 
            //启动连接，监听的话要主动启动连接
            vConnect.Start();
            //通过连接创建一个会话 
            //通过会话创建一个消费者，这里就是Queue这种会话类型的监听参数设置
            IMessageConsumer vConsumer = vSession.CreateConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue("MQ" + cUserID + "C"));
            //注册监听事件
            vConsumer.Listener += new MessageListener(onConsumerListener);
        }

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            InitMQClient();
        }

        protected override void OnStop()
        {

        }

        void onConsumerListener(IMessage message)
        {
            ITextMessage msg = (ITextMessage)message;
            //异步调用下，否则无法回归主线程
            log4net.WriteLogFile(msg.Text);
            String cStr = msg.Text;
            ActiveMQ_Message vMessage = JsonLib.ToObject<ActiveMQ_Message>(cStr);
            Change(vMessage);
        }
    }
}

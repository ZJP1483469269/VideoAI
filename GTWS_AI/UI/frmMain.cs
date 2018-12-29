using Apache.NMS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TLKJ.SYS;
using TLKJ.Utils;

namespace GTWS_AI.UI
{
    public partial class frmMain : Form
    {
        delegate void Change(ActiveMQ_Message text);
        public frmMain()
        {
            InitializeComponent();
        }
        private void Settext(ActiveMQ_Message vMessage)
        {
            if (vMessage.CMD_ID == 11)
            {
                this.Show();
                this.BringToFront();
            }
            else if (vMessage.CMD_ID == 12)
            {
                this.Hide(); 
            }
            else
            {
                log4net.WriteLogFile(string.Format(@"接收到:{0}{1}", vMessage.FROM_ID + "," + vMessage.USER_ID + "," + vMessage.MESSAGE, Environment.NewLine));
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

        private void frmMain_Load(object sender, EventArgs e)
        {
            InitMQClient();
            this.Hide();
        }
        void onConsumerListener(IMessage message)
        {
            ITextMessage msg = (ITextMessage)message;
            //异步调用下，否则无法回归主线程
            log4net.WriteLogFile(msg.Text);
            String cStr = msg.Text;
            ActiveMQ_Message vMessage = JsonLib.ToObject<ActiveMQ_Message>(cStr);
            this.BeginInvoke(new Change(Settext), vMessage);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

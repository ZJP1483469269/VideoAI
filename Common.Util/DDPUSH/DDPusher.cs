using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace TLKJ.Push
{
    /// <summary>
    /// DDPush C#推送实现，仅实现了自定义推送消息
    /// 2014-10-29
    /// </summary>
    class DDPusher
    {
        private int version = 1;
        private int appId = 1;
        private int timeout;

        private string host;
        private int port;
        private TcpClient client;

        public DDPusher(String host, int port)
            : this(host, port, 5000)
        {
            this.host = host;
            this.port = port;
        }
        public DDPusher(String host, int port, int timeoutMillis)
            : this(host, port, timeoutMillis, 1, 1)
        {
        }
        public DDPusher(String host, int port, int timeoutMillis, int version, int appId)
        {
            this.setVersion(version);
            this.setAppId(appId);
            this.host = host;
            this.port = port;
            this.timeout = timeoutMillis;
            initSocket();
        }
        private void initSocket()
        {
            client = new TcpClient();
            client.SendTimeout = this.timeout;
            client.Connect(host, port);
        }
        public void close()
        {
            if (client == null || client.Connected == false)
            {
                return;
            }
            client.Close();
        }
        public void setVersion(int version)
        {
            if (version < 1 || version > 255)
            {
                throw new Exception("version must be 1 to 255");
            }
            this.version = version;
        }
        public void setAppId(int appId)
        {
            if (appId < 1 || appId > 255)
            {
                throw new Exception("appId must be 1 to 255");
            }
            this.appId = appId;
        }
        private bool checkUuidArray(byte[] uuid)
        {
            if (uuid == null || uuid.Length != 16)
            {
                throw new Exception("uuid byte array must be not null and length of 16");
            }
            return true;
        }
        private bool checkLongArray(byte[] longArray)
        {
            if (longArray == null || longArray.Length != 8)
            {
                throw new Exception("array must be not null and length of 8");
            }
            return true;
        }
        /// <summary>
        /// 通用推送
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public bool push0x10Message(byte[] uuid)
        {
            checkUuidArray(uuid);
            if (client == null || client.Connected == false)
            {
                throw new ArgumentException("参数socket为null，或者未连接到远程计算机");
            }

            byte[] data = Encoding.UTF8.GetBytes("");
            Console.WriteLine("connected:" + client.Connected);
            NetworkStream ns = client.GetStream();
            ns.WriteByte((byte)version);
            ns.WriteByte((byte)appId);
            ns.WriteByte(16);
            ns.Write(uuid, 0, uuid.Length);
            ns.Write(new byte[] { 0, 0 }, 0, 2);
            ns.Write(data, 0, data.Length);
            ns.Flush();
            ns.Close();
            close();
            return true;
        }
        /// <summary>
        /// 分类推送
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool push0x11Message(byte[] uuid, int data)
        {
            checkUuidArray(uuid);
            if (client == null || client.Connected == false)
            {
                throw new ArgumentException("参数socket为null，或者未连接到远程计算机");
            }

            if (data > 255)
            {
                throw new ArgumentException("data array length illegal, min 1, max 255");
            }
            byte[] bytes = new byte[8];
            bytes[7] = (byte)data;
            Console.WriteLine("connected:" + client.Connected);
            NetworkStream ns = client.GetStream();
            ns.WriteByte((byte)version);
            ns.WriteByte((byte)appId);
            ns.WriteByte(17);
            ns.Write(uuid, 0, uuid.Length);
            ns.Write(new byte[] { 0, 8 }, 0, 2);
            ns.Write(bytes, 0, bytes.Length);
            ns.Flush();
            ns.Close();
            close();
            return true;
        }
        /// <summary>
        /// 发送自定义推送信息 未加推送结果校验
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool push0x20Message(byte[] uuid, byte[] data)
        {
            checkUuidArray(uuid);
            if (client == null || client.Connected == false)
            {
                throw new ArgumentException("参数socket为null，或者未连接到远程计算机");
            }

            if (data == null)
            {
                throw new ArgumentException("data array is null");
            }
            if (data.Length == 0 || data.Length > 500)
            {
                throw new ArgumentException("data array length illegal, min 1, max 500");
            }

            Console.WriteLine("connected:" + client.Connected);
            NetworkStream ns = client.GetStream();
            ns.WriteByte((byte)version);
            ns.WriteByte((byte)appId);
            ns.WriteByte(32);
            ns.Write(uuid, 0, uuid.Length);
            ByteBuffer tempBuffer = new ByteBuffer();
            tempBuffer.PushUInt16((UInt16)data.Length);
            byte[] dataLen = tempBuffer.ToByteArray();
            ns.Write(dataLen, 0, dataLen.Length);
            ns.Write(data, 0, data.Length);
            ns.Flush();
            ns.Close();
            close();
            return true;
        }

        static void Main(string[] args)
        {
            DDPusher p = new DDPusher("192.168.0.87", 9999, 5000);
            DDPusher p1 = new DDPusher("192.168.0.87", 9999, 5000);
            DDPusher p2 = new DDPusher("192.168.0.87", 9999, 5000);

            MD5CryptoServiceProvider md5csp = new MD5CryptoServiceProvider();
            string user = "uuuu";
            //string user = "012682002848514";
            byte[] buuid = System.Text.Encoding.UTF8.GetBytes(user);
            byte[] buser = md5csp.ComputeHash(buuid);
            string sdata = "321.hello,c#";
            byte[] bdata = System.Text.Encoding.UTF8.GetBytes(sdata);
            Console.WriteLine("send to user:" + user);

            p.push0x10Message(buser);
            p1.push0x11Message(buser, 243);
            p2.push0x20Message(buser, bdata);

            Console.ReadLine();
        }
    }
}

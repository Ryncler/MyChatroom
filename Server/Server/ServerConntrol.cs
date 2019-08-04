using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;

namespace Server
{
    public class ServerConntrol
    {
        private Socket serverSocket;
        public  ServerConntrol()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void Start()
        {
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, 61100));
            serverSocket.Listen(5);
            Console.WriteLine("服务器启动成功！");

            Thread threadAccept = new Thread(Accept);
            threadAccept.IsBackground = true;
            threadAccept.Start();
        }
        private void Accept()
        {
            Socket client = serverSocket.Accept();
            IPEndPoint point = client.RemoteEndPoint as IPEndPoint;
            Console.WriteLine("【"+point.Address+":"+point.Port+"】客户连接成功！");

            Thread threadReceive = new Thread(Receive);
            threadReceive.IsBackground = true;
            threadReceive.Start(client);
            Accept();
        }
        private void Receive(object obj)
        {
            Socket client = obj as Socket;
            IPEndPoint point = client.RemoteEndPoint as IPEndPoint;
            try
            {
                byte[] msg = new byte[1024];
                int msgLeng = client.Receive(msg);
                string inft=Encoding.UTF8.GetString(msg, 0, msgLeng);
                if (inft == "天气")
                {
                    Console.WriteLine("今天柳州天气：多云");
                }
                else
                {
                    Console.WriteLine("【" + point.Address + ":" + point.Port + "】：" + inft);
                }
                Receive(client);
            }
            catch
            {
                Console.WriteLine("【" + point.Address + ":" + point.Port + "】退出聊天");
            }
            
        }
    }
}

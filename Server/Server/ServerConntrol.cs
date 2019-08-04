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
        private List<Socket> clientList; 
        public  ServerConntrol()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientList = new List<Socket>();
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

            clientList.Add(client);
           
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
                string msgStr = "【" + point.Address + ":" + point.Port + "】"+ Encoding.UTF8.GetString(msg, 0, msgLeng);
                Console.WriteLine(msgStr);
                Broadcast(client, msgStr);
                Receive(client);
            }
            catch
            {
                Console.WriteLine("【" + point.Address + ":" + point.Port + "】退出聊天");
                clientList.Remove(client);
            }
            
        }
        private void Broadcast(Socket client,string msg)
        {
            foreach (var item in clientList)
            {
                if (item == client)
                {
                   //client.Send(Encoding.UTF8.GetBytes(msg));
                }
                else
                {
                    client.Send(Encoding.UTF8.GetBytes(msg));
                    //Console.WriteLine(msg);
                }
            } 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    public class ClientConntrol
    {
        private Socket clientSocket;
        public ClientConntrol()
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void Connent(string ip,int port)
        {
            clientSocket.Connect(ip, port);
            Console.WriteLine("连接服务器成功！");

            Thread threadReceive = new Thread(Receive);
            threadReceive.IsBackground = true;
            threadReceive.Start(); 
        }
        private void Receive()
        {
            while (true)
            {
                try
                {
                    byte[] msg = new byte[1024];
                    int msgLeng = clientSocket.Receive(msg);
                    if (msg==null)
                    {
                        break;
                    }
                    Console.WriteLine("服务器说：" + Encoding.UTF8.GetString(msg, 0, msgLeng));
                }
                catch
                {
                    Console.WriteLine("服务器炸了，已断开连接");
                    break;
                }
            }
        }
        public void Send()
        {
            Thread threadSend = new Thread(ReadSend);
            //threadSend.IsBackground = true;
            threadSend.Start();
            
        }
        private void ReadSend()
        {
            Console.WriteLine("请输入要发送的内容（输入：退出 即可退出聊天）：");
            string msg = Console.ReadLine();
            while (msg != "退出")
            {
                clientSocket.Send(Encoding.UTF8.GetBytes(msg));
                msg = Console.ReadLine();
            }
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
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
        }
        private void Receive()
        {
            try
            {
                byte[] msg = new byte[1024];
                int msgLeng = clientSocket.Receive(msg);
                Console.WriteLine("服务器说：" + Encoding.UTF8.GetString(msg, 0, msgLeng));

                Receive();
            }
            catch
            {
                Console.WriteLine("服务器炸了，已断开连接");
            }
        }
        public void Send(string msg)
        {
            clientSocket.Send(Encoding.UTF8.GetBytes(msg));
        }
    }
}

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
    }
}

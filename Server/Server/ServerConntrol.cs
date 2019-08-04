using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;

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
            serverSocket.Bind(new IPEndPoint(IPAddress.Any,61100));
            serverSocket.Listen(5);
            Console.WriteLine("服务器启动成功！");
        }
    }
}

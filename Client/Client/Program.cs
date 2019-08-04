using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientConntrol client = new ClientConntrol();
            client.Connent("127.0.0.1", 61100);

            Console.WriteLine("请输入要发送的内容（输入：退出 即可退出聊天）：");
            string msg = Console.ReadLine();
            while (msg != "退出")
            {
                client.Send(msg);
                msg = Console.ReadLine();
            }
            Console.ReadLine();
        }
    }
}

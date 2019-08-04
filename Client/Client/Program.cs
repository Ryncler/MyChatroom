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
            Console.ReadLine();
        }
    }
}

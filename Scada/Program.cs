using Master.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Net;
using System.IO.Ports;
using System.Net.Security;
using Master.TcpCommunication;

namespace Master
{
    public class Program
    {
        static void Main(string[] args)
        {
            Scada scada = new Scada();
            scada.Start();
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Common;
using Slave.Communication;

namespace Slave
{
    internal class Program
    {
        static void Main(string[] args)
        {

            CommunicationStream stream = new CommunicationStream();

            Console.WriteLine(Encoding.UTF8.GetString(stream.RecvBytes(11)));
            Console.ReadKey();
        }
    }
}

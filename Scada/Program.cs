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
using System.Reflection;
using Common.Message;
using Common.Utilities;
using Master.Communication;
using Common.Message.Modbus;
using System.Threading;

namespace Master
{

    public class Program
    {
        static void Main(string[] args)
        {
            Scada scada = new Scada();
            scada.Start();

            while (true)
            {
            }
        }
    }
}

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
                //it's looks bad but now we using console.Read inside a thread and if i stop
                //the app with console.readline then that will be triggered and not the one inside 
                //the task
            }
        }
    }
}

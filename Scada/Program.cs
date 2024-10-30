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

namespace Master
{

    public class Program
    {
        static void Main(string[] args)
        {
            SerializationHandler serializationHandler = new SerializationHandler();

            ModbusReadInputRegistersResponse mrirr = new ModbusReadInputRegistersResponse(4, new short[] { 12, 34, 45, 67 });
            ModbusPDU modbusPDU = new ModbusPDU(FunctionCode.ReadInputRegisters, mrirr);
            byte[] serial = serializationHandler.SerializeToBytes(modbusPDU);
            byte[] respoonse = serializationHandler.SerializeToBytes(mrirr);    //SerializeToBytes working

            (object temp, int offset1) = serializationHandler.DeserializeFromBytes(respoonse, 0, typeof(ModbusPDU));
            (object mrirDeSeri,int offset2) = serializationHandler.DeserializeFromBytes(respoonse, 0, typeof(ModbusReadInputRegistersResponse));
            ModbusPDU modbusPDU1 = temp as ModbusPDU;

            ModbusReadInputRegistersResponse modbusPD1 = mrirDeSeri as ModbusReadInputRegistersResponse;

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

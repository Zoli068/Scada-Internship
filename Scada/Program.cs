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

            ModbusRequestData modbusRequestData = new ModbusRequestData(1234,6789);
            byte[] serializedObject = serializationHandler.SerializeToBytes(modbusRequestData);
            ModbusRequestData requestWithSerializaiton= serializationHandler.DeserializeToBytes(serializedObject, typeof(ModbusRequestData)) as ModbusRequestData;

            ModbusPDU modbusPDU = new ModbusPDU(FunctionCode.WriteMultipleCoils, modbusRequestData);
            byte[] serial = serializationHandler.SerializeToBytes(modbusPDU);

            ModbusPDU desserilaized= serializationHandler.DeserializeToBytes(serial, typeof(ModbusPDU)) as ModbusPDU;


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

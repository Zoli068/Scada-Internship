using Common.Message;
using Common.Utilities;
using Master.CommandHandler.MessageInitiateHandler;
using System;
using System.Threading;

namespace Master.UI
{
    /// <summary>
    /// A basic UI class, with a basic terminal based menu
    /// </summary>
    public class ConsoleUI
    {
        private Action<FunctionCode, IMessageDTO> initateMessage;

        public ConsoleUI(Action<FunctionCode, IMessageDTO> initateMessage)
        {
            this.initateMessage = initateMessage;
        }

        public void Start()
        {
            int selectedOption;
            bool running = true;

            while (running)
            {

                Console.WriteLine("Choose your option");
                Console.WriteLine("0 - Exit");
                Console.WriteLine("1 - Read Coils");
                Console.WriteLine("2 - Read Discrete Inputs");
                Console.WriteLine("3 - Read Holding Registers");
                Console.WriteLine("4 - Read Input Registers");
                Console.WriteLine("5 - Write Single Coil");
                Console.WriteLine("6 - Write Single Register");
                Console.WriteLine("7 - Write Multiple Coils");
                Console.WriteLine("8 - Write Multiple Registers");

                if (!int.TryParse(Console.ReadLine(), out selectedOption))
                {
                    continue;
                }

                switch (selectedOption)
                {
                    case 0:
                        running = false;
                        break;
                    case 1:
                        ReadCoils();
                        break;
                    case 2:
                        ReadDiscreteInputsRequest();
                        break;
                    case 3:
                        ReadHoldingRegisters();
                        break;
                    case 4:
                        ReadInputRegisters();
                        break;
                    case 5:
                        WriteSingleCoil();
                        break;
                    case 6:
                        WriteSingleRegister();
                        break;
                    case 7:
                        WriteMultipleCoilsRequest();
                        break;
                    case 8:
                        WriteMultipleRegisters();
                        break;
                }
                Thread.Sleep(200);
            }
        }

        private void ReadCoils()
        {
            ushort address;
            ushort quantity;

            Console.Clear();
            Console.WriteLine("Read Coils");
            Console.WriteLine("Enter the address:");
            if (!ushort.TryParse(Console.ReadLine(), out address)) { return; }

            Console.WriteLine("Enter the quantity:");
            if (!ushort.TryParse(Console.ReadLine(), out quantity)) { return; }

            InitiateReadModbusDTO initiateReadModbusDTO = new InitiateReadModbusDTO();
            initiateReadModbusDTO.Address = address;
            initiateReadModbusDTO.Quantity = quantity;

            initateMessage(FunctionCode.ReadCoils, initiateReadModbusDTO);
        }

        private void ReadDiscreteInputsRequest()
        {
            ushort address;
            ushort quantity;

            Console.Clear();
            Console.WriteLine("Read Discrete Inputs");
            Console.WriteLine("Enter the address:");
            if (!ushort.TryParse(Console.ReadLine(), out address)) { return; }

            Console.WriteLine("Enter the quantity:");
            if (!ushort.TryParse(Console.ReadLine(), out quantity)) { return; }

            InitiateReadModbusDTO initiateReadModbusDTO = new InitiateReadModbusDTO();
            initiateReadModbusDTO.Address = address;
            initiateReadModbusDTO.Quantity = quantity;

            initateMessage(FunctionCode.ReadDiscreteInputs, initiateReadModbusDTO);
        }

        private void ReadHoldingRegisters()
        {
            ushort address;
            ushort quantity;

            Console.Clear();
            Console.WriteLine("Read Holding Registers");
            Console.WriteLine("Enter the address:");
            if (!ushort.TryParse(Console.ReadLine(), out address)) { return; }

            Console.WriteLine("Enter the quantity:");
            if (!ushort.TryParse(Console.ReadLine(), out quantity)) { return; }

            InitiateReadModbusDTO initiateReadModbusDTO = new InitiateReadModbusDTO();
            initiateReadModbusDTO.Address = address;
            initiateReadModbusDTO.Quantity = quantity;

            initateMessage(FunctionCode.ReadHoldingRegisters, initiateReadModbusDTO);
        }

        private void ReadInputRegisters()
        {
            ushort address;
            ushort quantity;

            Console.Clear();
            Console.WriteLine("Read Input Registers");
            Console.WriteLine("Enter the address:");
            if (!ushort.TryParse(Console.ReadLine(), out address)) { return; }

            Console.WriteLine("Enter the quantity:");
            if (!ushort.TryParse(Console.ReadLine(), out quantity)) { return; }

            InitiateReadModbusDTO initiateReadModbusDTO = new InitiateReadModbusDTO();
            initiateReadModbusDTO.Address = address;
            initiateReadModbusDTO.Quantity = quantity;

            initateMessage(FunctionCode.ReadInputRegisters, initiateReadModbusDTO);
        }

        private void WriteSingleCoil()
        {
            ushort address;
            short value;

            Console.Clear();
            Console.WriteLine("Write Single Coil");
            Console.WriteLine("Enter the address:");
            if (!ushort.TryParse(Console.ReadLine(), out address)) { return; }

            Console.WriteLine("Enter the value:");
            if (!short.TryParse(Console.ReadLine(), out value)) { return; }

            InitiateWriteSingleModbusDTO initiateWriteSingleModbusDTO = new InitiateWriteSingleModbusDTO();
            initiateWriteSingleModbusDTO.Address = address;
            initiateWriteSingleModbusDTO.Value = value;

            initateMessage(FunctionCode.WriteSingleCoil, initiateWriteSingleModbusDTO);
        }

        private void WriteSingleRegister()
        {
            ushort address;
            short value;

            Console.Clear();
            Console.WriteLine("Write Single Register");
            Console.WriteLine("Enter the address:");
            if (!ushort.TryParse(Console.ReadLine(), out address)) { return; }

            Console.WriteLine("Enter the value:");
            if (!short.TryParse(Console.ReadLine(), out value)) { return; }

            InitiateWriteSingleModbusDTO initiateWriteSingleModbusDTO = new InitiateWriteSingleModbusDTO();
            initiateWriteSingleModbusDTO.Address = address;
            initiateWriteSingleModbusDTO.Value = value;

            initateMessage(FunctionCode.WriteSingleRegister, initiateWriteSingleModbusDTO);
        }

        private void WriteMultipleRegisters()
        {
            ushort address;
            ushort quantity;

            Console.Clear();
            Console.WriteLine("Write Multiple Register");
            Console.WriteLine("Enter the address:");
            if (!ushort.TryParse(Console.ReadLine(), out address)) { return; }

            Console.WriteLine("Enter the quantity:");
            if (!ushort.TryParse(Console.ReadLine(), out quantity)) { return; }

            short[] values = new short[quantity];

            for (int i = 0; i < values.Length; i++)
            {
                Console.WriteLine("Enter the " + (i + 1) + " value:");
                if (!short.TryParse(Console.ReadLine(), out values[i])) { return; }
            }

            InitiateWriteMultipleModbusDTO initiateWriteMultipleModbusDTO = new InitiateWriteMultipleModbusDTO();

            initiateWriteMultipleModbusDTO.Address = address;
            initiateWriteMultipleModbusDTO.Quantity = quantity;
            initiateWriteMultipleModbusDTO.Values = values;

            initateMessage(FunctionCode.WriteMultipleRegisters, initiateWriteMultipleModbusDTO);
        }

        private void WriteMultipleCoilsRequest()
        {
            ushort address;
            ushort quantity;

            Console.Clear();
            Console.WriteLine("Write Multiple Coils");
            Console.WriteLine("Enter the address:");
            if (!ushort.TryParse(Console.ReadLine(), out address)) { return; }

            Console.WriteLine("Enter the quantity:");
            if (!ushort.TryParse(Console.ReadLine(), out quantity)) { return; }

            short[] values = new short[quantity];

            for (int i = 0; i < values.Length; i++)
            {
                Console.WriteLine("Enter the " + (i + 1) + " value:");
                if (!short.TryParse(Console.ReadLine(), out values[i])) { return; }
            }

            InitiateWriteMultipleModbusDTO initiateWriteMultipleModbusDTO = new InitiateWriteMultipleModbusDTO();

            initiateWriteMultipleModbusDTO.Address = address;
            initiateWriteMultipleModbusDTO.Quantity = quantity;
            initiateWriteMultipleModbusDTO.Values = values;

            initateMessage(FunctionCode.WriteMultipleCoils, initiateWriteMultipleModbusDTO);
        }
    }
}

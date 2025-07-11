using Demo_VisionMaster.Controls;
using Demo_VisionMaster.Views;
using DotNetEnv;
using IoTClient.Clients;
using IoTClient.Clients.Modbus;
using IoTClient.Enums;
using IoTClient.Models;
using MVD.Internal.Comm;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static Demo_VisionMaster.Enums.DataTypeEnum;
using static Demo_VisionMaster.Events.PlcEvent;

namespace Demo_VisionMaster.Services
{
    public class PlcService
    {
        public PlcService() { }
        ModbusTcpClient _client;
        System.Timers.Timer _timerMainCircle = new System.Timers.Timer();
        System.Timers.Timer _timerCheckConnect = new System.Timers.Timer();
        public event TriggerEvent _handleTriggerEvent;

        #region Sample code
        //public void sample() 
        //{
        //    byte stationNumber = 1;
        //    //1、Instantiate the client-enter the correct IP and port
        //    ModbusTcpClient client = new ModbusTcpClient("127.0.0.1", 502);

        //    //2、Write operation-parameters are: address, value, station number, function code
        //    client.Write("4", (short)33, 2, 16);

        //    //2.1、[Note] When writing data, you need to clarify the data type
        //    client.Write("0", (short)33, 2, 16);    //Write short type value
        //    client.Write("4", (ushort)33, 2, 16);   //Write ushort type value
        //    client.Write("8", (int)33, 2, 16);      //Write int type value
        //    client.Write("12", (uint)33, 2, 16);    //Write uint type value
        //    client.Write("16", (long)33, 2, 16);    //Write long type value
        //    client.Write("20", (ulong)33, 2, 16);   //Write ulong type value
        //    client.Write("24", (float)33, 2, 16);   //Write float type value
        //    client.Write("28", (double)33, 2, 16);  //Write double type value
        //    client.Write("32", true, 2, 5);         //Write Coil type value
        //    client.Write("100", "orderCode", stationNumber);  //Write string

        //    //3、Read operation-the parameters are: address, station number, function code
        //    var value = client.ReadInt16("4", 2, 3).Value;

        //    //3.1、Other types of data reading
        //    client.ReadInt16("0", stationNumber, 3);    //short type data read
        //    client.ReadUInt16("4", stationNumber, 3);   //ushort type data read
        //    client.ReadInt32("8", stationNumber, 3);    //int type data read
        //    client.ReadUInt32("12", stationNumber, 3);  //uint type data read
        //    client.ReadInt64("16", stationNumber, 3);   //long type data read
        //    client.ReadUInt64("20", stationNumber, 3);  //ulong type data read
        //    client.ReadFloat("24", stationNumber, 3);   //float type data read
        //    client.ReadDouble("28", stationNumber, 3);  //double type data read
        //    client.ReadCoil("32", stationNumber, 1);    //Coil type data read
        //    client.ReadDiscrete("32", stationNumber, 2);//Discrete type data read
        //    client.ReadString("100", stationNumber, readLength: 10); //Read string

        //    //4、If there is no active Open, it will automatically open and close the connection every time you read and write operations, which will greatly reduce the efficiency of reading and writing. So it is recommended to open and close manually.
        //    client.Open();

        //    //5、Read and write operations will return the operation result object Result
        //    var result = client.ReadInt16("4", 2, 3);
        //    //5.1 Whether the reading is successful (true or false)
        //    var isSucceed = result.IsSucceed;
        //    //5.2 Exception information for failed reading
        //    var errMsg = result.Err;
        //    //5.3 Read the request message actually sent by the operation
        //    var requst = result.Requst;
        //    //5.4 Read the response message from the server
        //    var response = result.Response;
        //    //5.5 Read value
        //    var value3 = result.Value;


        //    //6、Batch read
        //    var list = new List<ModbusInput>();
        //    list.Add(new ModbusInput()
        //    {
        //        Address = "2",
        //        DataType = DataTypeEnum.Int16,
        //        FunctionCode = 3,
        //        StationNumber = 1
        //    });
        //    list.Add(new ModbusInput()
        //    {
        //        Address = "2",
        //        DataType = DataTypeEnum.Int16,
        //        FunctionCode = 4,
        //        StationNumber = 1
        //    });
        //    list.Add(new ModbusInput()
        //    {
        //        Address = "199",
        //        DataType = DataTypeEnum.Int16,
        //        FunctionCode = 3,
        //        StationNumber = 1
        //    });
        //    var result1 = client.BatchRead(list);

        //    //7、Other parameters of the constructor
        //    //IP, port, timeout time, big and small end settings
        //    //ModbusTcpClient client1 = new ModbusTcpClient("127.0.0.1", 502, 1500, EndianFormat.ABCD);

        //}
        #endregion
        List<ModbusInput> inputs = new List<ModbusInput>();
        public bool Connect(string IP, int PORT) 
        {
            this._client = new ModbusTcpClient(IP, PORT);
            this._client.Open();
            return this._client.Connected;
        } 
        public void InitPLC() 
        {
            string IPAddress = System.Environment.GetEnvironmentVariable("PLC_IP");
            int Port = int.Parse(System.Environment.GetEnvironmentVariable("PLC_PORT"));
            inputs.Add(new ModbusInput()
            {
                Address = "0",
                DataType = DataTypeEnum.Bool,
                FunctionCode = 1,
                StationNumber = 1
            });
            inputs.Add(new ModbusInput()
            {
                Address = "1",
                DataType = DataTypeEnum.Bool,
                FunctionCode = 1,
                StationNumber = 1
            });
            inputs.Add(new ModbusInput()
            {
                Address = "2",
                DataType = DataTypeEnum.Bool,
                FunctionCode = 1,
                StationNumber = 1
            });
            inputs.Add(new ModbusInput()
            {
                Address = "3",
                DataType = DataTypeEnum.Bool,
                FunctionCode = 1,
                StationNumber = 1
            });
            if (Connect(IPAddress, Port) == true) 
            {
                ViewHome.Instance.UpdateAppStatus($"Connect With PLC Success: {DateTime.Now}");
            }
        }
        public void RunProcess()
        {
            if (_client.Connected)
            {
                _timerMainCircle.Interval = 100;
                _timerMainCircle.Elapsed += _timerMainCircle_Elapsed; ;
                _timerMainCircle.Start();
                _timerCheckConnect.Interval = 1000;
                _timerCheckConnect.Elapsed += _timerCheckConnect_Elapsed;
                _timerCheckConnect.Start();
                _handleTriggerEvent += PlcService__handleTriggerEvent;
            }
        }

        private void PlcService__handleTriggerEvent()
        {
            var data = AppCoreBackend.Ins.handlHardTrigger();
            WriteRequestToPLC(data);
        }

        private void _timerMainCircle_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _timerMainCircle.Stop();
            try
            {
                if (_client.Connected == false)
                {
                    return;
                }
                //if (_client.ReadCoil("0", 1, 1).Value)
                //{
                //    _handleTriggerEvent.Invoke();
                //}
                var output = _client.BatchRead(inputs).Value;
                for (int i = 0; i < output.Count; i++) 
                {
                    if ((bool)output[i].Value == true) 
                    {
                       var Line = AppCoreBackend.Ins.handlHardTrigger(i);
                       WriteRequestToPLC(i,Line);
                    }
                    else 
                    {
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewHome.Instance.UpdateAppStatus($"Exception at: {ex.StackTrace}");
            }
            finally 
            {
                _timerMainCircle.Start();
            }
        }
        public void WriteRequestToPLC(double Value) 
        {
            if (_client.Connected != true) return; 
            var t = _client.Write("1", (double)Value);
            var r = _client.ReadInt32("1", 1, 3);

            if (Value == r.Value)
            {
                Console.WriteLine($"Send to PLC: {Value}");
                ViewHome.Instance.UpdateAppStatus($"Send to PLC: {Value}  {DateTime.Now}");
            }
            else
            {
                Console.WriteLine($"Fail to send: {Value}");
                ViewHome.Instance.UpdateAppStatus($"Value not match with: {Value}  {DateTime.Now}");
            }
        }
        public void WriteRequestToPLC(int index,double Value)
        {
            if (_client.Connected != true) return;
            var t = _client.Write($"{index}", (double)Value);
            var r = _client.ReadInt32("1", 1, 3);

            if (Value == r.Value)
            { 
                ViewHome.Instance.UpdateAppStatus($"Send to PLC buffer {index}: {Value}  {DateTime.Now}");
            }
            else
            {
                ViewHome.Instance.UpdateAppStatus($"Value buffer {index} not match with: {Value} {DateTime.Now}");
            }
        }
        private void _timerCheckConnect_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _timerCheckConnect.Stop();
            if (_client.Connected != true) 
            {
                ViewHome.Instance.UpdateAppStatus($"PLC disconnected at: {DateTime.Now}");

                _timerMainCircle.Stop();
                InitPLC();
                RunProcess(); // Reconnect and start the process again
            }
            else 
            {
                _timerCheckConnect.Start();
            }
        }
        public void Deinit() 
        {
            //_client.Close();
            _timerMainCircle.Stop();
            _timerCheckConnect.Stop();
            ViewHome.Instance.UpdateAppStatus($"Stop app complited: {DateTime.Now}");
        }
    }
}

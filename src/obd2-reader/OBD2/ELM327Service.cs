using System;
using System.Threading;
using System.Threading.Tasks;
using OBD.NET.Common.Devices;
using OBD.NET.Common.Extensions;
using OBD.NET.Common.Logging;
using OBD.NET.Common.OBDData;

namespace obd2_reader.OBD2
{
    public class ELM327Service{
        private const string _comPort = "COM4";
        private const int _baudRate = 9600;

        public void Poll(CancellationToken cancellationToken = default){
            Console.WriteLine("ELM327 Service - Poll Started");
            using (var connection = new SerialConnection(_comPort, _baudRate))
            using (var device = new ELM327(connection))
            {
                device.SubscribeDataReceived<EngineRPM>((sender, data) => 
                    Console.WriteLine("EngineRPM: " + data.Data.Rpm));
                device.SubscribeDataReceived<VehicleSpeed>((sender, data) => 
                    Console.WriteLine("VehicleSpeed: " + data.Data.Speed));
                device.SubscribeDataReceived<EngineCoolantTemperatureSensor>((sender, data) =>
                    Console.WriteLine($"Engine Coolant Temp: {data.Data.Sensor1.Value}"));

                device.SubscribeDataReceived<IOBDData>((sender, data) => 
                    Console.WriteLine($"PID {data.Data.PID.ToHexString()}: {data.Data}"));

                device.Initialize();
                device.RequestData<FuelType>();
                while (!cancellationToken.IsCancellationRequested){
                    device.RequestData<EngineRPM>();
                    device.RequestData<VehicleSpeed>();
                    device.RequestData<EngineCoolantTemperatureSensor>();
                    
                    Thread.Sleep(200);
                }
            }
            Console.WriteLine("ELM327 Service - Poll Stopped");
        }
    }
}
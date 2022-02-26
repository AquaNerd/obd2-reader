#nullable disable

using System.Threading;
using System;
using System.IO.Ports;
using OBD.NET.Common.Communication;
using OBD.NET.Common.Communication.EventArgs;
using System.Threading.Tasks;

namespace obd2_reader.OBD2{

    public class SerialConnection : ISerialConnection {
        private readonly SerialPort _serialPort;
        private readonly byte[] _readBuffer = new byte[1024];

        public SerialConnection(string portName, int baudRate){
            _serialPort = new SerialPort(portName, baudRate);
            _serialPort.DataReceived += SerialPortOnDataReceived;
        }

        public event EventHandler<DataReceivedEventArgs> DataReceived;

        public bool IsOpen => _serialPort?.IsOpen ?? false;
        public bool IsAsync => false;

        public void Connect() => _serialPort.Open();
        public async Task ConnectAsync() => await Task.Run(() => _serialPort.Open());
        
        public void Disconnect() => _serialPort.Close();

        private void SerialPortOnDataReceived(object sender, SerialDataReceivedEventArgs args) {
            int count = _serialPort.Read(_readBuffer, 0, _serialPort.BytesToRead);
            DataReceived?.Invoke(this, new DataReceivedEventArgs(count, _readBuffer));
        }

        public void Write(byte[] data) => _serialPort.Write(data, 0, data.Length);

        public async Task WriteAsync(byte[] data) => await Task.Run(() => _serialPort.Write(data, 0, data.Length));

        public void Dispose() => _serialPort?.Dispose();
    }
}
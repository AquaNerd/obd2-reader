using System;
using System.Threading;
using System.Xml.Linq;
// See https://aka.ms/new-console-template for more information
using obd2_reader.OBD2;

var service = new ELM327Service();

var _continue = true;
var message = string.Empty;

while(_continue){
    service.Poll();
    Thread.Sleep(1000);
}
Console.WriteLine("Stopping service now");
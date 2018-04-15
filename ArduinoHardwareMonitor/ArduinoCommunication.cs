using System;
using CommandMessenger;
using CommandMessenger.Serialport;
using CommandMessenger.TransportLayer;
using System.Threading;
using OpenHardwareMonitor.Hardware;
using OpenHardwareMonitor.Helpers;
using System.Globalization;

namespace ArduinoHardwareMonitor
{
    //entries that start with p means commands sent from PC. Those with a are sent from Arduino
    enum Command
    {
        pCheckArduino,
        aCheckResponse,
        pSendValues,
        pSendEmptyValues,
        aGoNextSensor
    }

    class ArduinoCommunication
    {
        SerialTransport _serialTransport;
        CmdMessenger _cmdMessenger;
        bool _arduinoFound = false;
        public EventHandler NextSensorEvent = null;

        public bool SearchAndConnectToArduino()
        {
            for (var i = 0; i <= 20; i++)
            {
                _serialTransport = new SerialTransport
                {
                    CurrentSerialSettings = { PortName = "COM" + i.ToString(), BaudRate = 115200 }
                };
                _cmdMessenger = new CmdMessenger(_serialTransport)
                {
                    BoardType = BoardType.Bit16
                };
                AttachCommandCallBacks();
                _cmdMessenger.Connect();

                var commandCheck = new SendCommand((int)Command.pCheckArduino);
                _cmdMessenger.SendCommand(commandCheck);
                Thread.Sleep(100);//wait 100 ms for the response
                if (_arduinoFound)
                    return true;
                //else
                _cmdMessenger.Disconnect();
                _cmdMessenger.Dispose();
                _serialTransport.Dispose();
            }
            return false;
        }

        public void SendSensorValue(ISensor sensor, int sensorIndex, int sensorAllWatchedCount)
        {
            string firstLine, secondLine;

            var sensorIndexString = string.Format("{0}/{1}", sensorIndex, sensorAllWatchedCount);
            firstLine = string.Format("{0}-{1}", sensor.Hardware.HardwareType.ShortName(), sensor.SensorType.ShortName());
            firstLine = firstLine.PadRight(16);
            firstLine = firstLine.Substring(0, 16 - sensorIndexString.Length - 1);//our LCD has only 16 chars and we want the sensorIndexString to be at the end of the line
            firstLine += " " + sensorIndexString;

            var value = GetSensorFormatedValue(sensor);
            var sensorName = sensor.Name;
            if (sensor.Hardware.HardwareType == HardwareType.CPU && sensorName.StartsWith("CPU"))
                sensorName = sensorName.Substring(4);

            secondLine = sensorName.PadRight(16);
            secondLine = secondLine.Substring(0, 16 - value.Length - 1);
            secondLine += " " + value;

            var commandSend = new SendCommand((int)Command.pSendValues);
            commandSend.AddArgument(firstLine);
            commandSend.AddArgument(secondLine);
            _cmdMessenger.SendCommand(commandSend);
        }

        private string GetSensorFormatedValue(ISensor sensor)
        {
            string format = "";
            switch (sensor.SensorType)
            {
                case SensorType.Voltage: format = "{0:F3}V"; break;
                case SensorType.Clock: format = "{0:F0}MHz"; break;
                case SensorType.Load: format = "{0:F1}%"; break;
                case SensorType.Temperature: format = "{0:F1}C"; break;
                case SensorType.Fan: format = "{0:F0}RPM"; break;
                case SensorType.Flow: format = "{0:F0}L/h"; break;
                case SensorType.Control: format = "{0:F1}%"; break;
                case SensorType.Level: format = "{0:F1}%"; break;
                case SensorType.Power: format = "{0:F1}W"; break;
                case SensorType.Data: format = "{0:F1}GB"; break;
                case SensorType.Factor: format = "{0:F3}"; break;
            }
            if (sensor.Value.HasValue)
                return string.Format(format, sensor.Value.Value.ToString("F", CultureInfo.CreateSpecificCulture("en-US")));
            else
                return string.Empty;
        }

        public void SendNothing()
        {
            var commandSend = new SendCommand((int)Command.pSendEmptyValues);
            _cmdMessenger.SendCommand(commandSend);
        }


        private void AttachCommandCallBacks()
        {
            _cmdMessenger.Attach((int)Command.aCheckResponse, OnReceiveCheckResponse);
            _cmdMessenger.Attach((int)Command.aGoNextSensor, OnGoNextSensor);
        }

        DateTime _lastNextCommandTime = DateTime.Now;
        private void OnGoNextSensor(ReceivedCommand receivedCommand)
        {
            if ((DateTime.Now - _lastNextCommandTime).Milliseconds > 500)
            {
                NextSensorEvent(null, null);
                _lastNextCommandTime = DateTime.Now;
            }
        }

        private void OnReceiveCheckResponse(ReceivedCommand receivedCommand)
        {
            var response = receivedCommand.ReadStringArg().Trim();
            if (response == "OK")
                _arduinoFound = true;
        }

        public void Exit()
        {
            _cmdMessenger.Disconnect();
            _cmdMessenger.Dispose();
            _serialTransport.Dispose();
        }
    }
}

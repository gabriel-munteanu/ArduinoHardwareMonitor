using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArduinoHardwareMonitor
{
    //class used to discover all sensors from the tree sensor structure
    class SensorVisitor : IVisitor
    {
        List<ISensor> _sensors = null;

        public SensorVisitor(List<ISensor> sensors)
        {
            if (sensors == null)
                throw new ArgumentNullException("sensors list");
            _sensors = sensors;
        }

        public void VisitSensor(ISensor sensor)
        {
            if(sensor == null)
                throw new ArgumentNullException("sensor");
            _sensors.Add(sensor);
        }


        public void VisitComputer(IComputer computer)
        {
            if (computer == null)
                throw new ArgumentNullException("computer");
            computer.Traverse(this);
        }

        public void VisitHardware(IHardware hardware)
        {
            if (hardware == null)
                throw new ArgumentNullException("hardware");
            hardware.Traverse(this);
        }    

        public void VisitParameter(IParameter parameter)
        {
            
        }
    }
}

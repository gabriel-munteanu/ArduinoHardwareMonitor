using System;
using System.Collections.Generic;
using System.Text;

namespace OpenHardwareMonitor.Helpers
{
    class ShortNameAttribute : Attribute
    {
        public string ShortName { get; set; }

        public ShortNameAttribute(string shortName)
        {
            ShortName = shortName;
        }
    }
}

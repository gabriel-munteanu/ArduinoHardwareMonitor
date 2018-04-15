using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OpenHardwareMonitor.Helpers
{
    public static class ExtensionMethods
    {
        public static string ShortName(this HardwareType hardwareType)
        {
            return hardwareType.GetAttributeValueOrDefault<ShortNameAttribute>().ShortName;
        }

        public static string ShortName(this SensorType hardwareType)
        {
            return hardwareType.GetAttributeValueOrDefault<ShortNameAttribute>().ShortName;
        }

        private static FieldInfo GetFieldInfo(Enum value)
        {
            return value.GetType().GetField(value.ToString());

        }
        public static T GetAttributeValueOrDefault<T>(this Enum value) where T : Attribute
        {
            var fieldInfo = GetFieldInfo(value);
            if (fieldInfo == null)
                return null;

            return fieldInfo.GetCustomAttributes(typeof(T), false)[0] as T;
        }
    }
}

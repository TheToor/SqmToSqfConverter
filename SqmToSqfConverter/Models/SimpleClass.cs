using System;
using System.Collections.Generic;

namespace SqmToSqfConverter.Models
{
    public class SimpleClass
    {
        public string ClassName { get; set; }
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, SimpleClass> SubClasses { get; set; } = new Dictionary<string, SimpleClass>();

        public string GetProperty(string propertyName)
        {
            if (Properties.ContainsKey(propertyName))
                return Properties[propertyName].Replace("\"", String.Empty);
            return null;
        }

        public float? TryGetPropertyAsFloat(string propertyName)
        {
            if (Properties.ContainsKey(propertyName))
                return Convert.ToSingle(Properties[propertyName].Replace("\"", String.Empty));
            return null;
        }

        public string GetSubProperty(string subClass, string propertyName)
        {
            if (SubClasses.ContainsKey(subClass) && SubClasses[subClass].Properties.ContainsKey(propertyName))
                return SubClasses[subClass].Properties[propertyName].Replace("\"", String.Empty);
            return null;
        }

        public int? TryGetSubPropertyAsInt(string subClass, string propertyName)
        {
            if (SubClasses.ContainsKey(subClass) && SubClasses[subClass].Properties.ContainsKey(propertyName))
            {
                return Convert.ToInt32(SubClasses[subClass].Properties[propertyName].Replace("\"", String.Empty));
            }
            return null;
        }

        public float? TryGetSubPropertyAsFloat(string subClass, string propertyName)
        {
            if (SubClasses.ContainsKey(subClass) && SubClasses[subClass].Properties.ContainsKey(propertyName))
            {
                return Convert.ToSingle(SubClasses[subClass].Properties[propertyName].Replace("\"", String.Empty));
            }
            return null;
        }
    }
}

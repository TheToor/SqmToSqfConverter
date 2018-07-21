using System;

namespace SqmToSqfConverter.Models
{
    public class ArmAWaypoint : IArmAItem
    {
        public int ID { get; set; }
        public string Side { get; set; }
        public string DataType { get; } = "Waypoint";
        public string VariableName { get; set; }

        public Vector3 Position { get; set; }
        public string ShowMP { get; set; }
        public string Type { get; set; }

        public float? ATLOffset { get; set; }

        public string CombatMode { get; set; }
        public string Formation { get; set; }
        public string Speed { get; set; }
        public string Combat { get; set; }

        public string GetSqf(out string[] sqf, ConvertOptions options, string groupName = null)
        {
            throw new NotSupportedException();
        }
    }
}

using System;
using System.Collections.Generic;

namespace SqmToSqfConverter.Models
{
    public class ArmAMarker : IArmAItem
    {
        public int ID { get; set; }
        public string Side { get; set; }
        public string DataType { get; } = "Marker";
        public string VariableName { get; set; }

        public Vector3 Position { get; set; }
        public float? ATLOffset { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public string ColorName { get; set; }
        public float? Alpha { get; set; }

        public float? SizeA { get; set; }
        public float? SizeB { get; set; }
        public float? Angle { get; set; }

        public string FillName { get; set; }
        public string MarkerType { get; set; }

        public string GetSqf(out string[] sqf, string groupName = null)
        {
            var code = new List<string>();

            var name = Name ?? $"marker{ID}";
            code.Add($"{name} = createMarker [\"{name}\", {Position}];");
            code.Add($"\"{name}\" setMarkerType \"{Type}\";");

            if(!String.IsNullOrEmpty(Text))
                code.Add($"\"{name}\" setMarkerText \"{Text}\";");
            if (Alpha.HasValue)
                code.Add($"\"{name}\" setMarkerAlpha {Alpha.Value};");
            if (!String.IsNullOrEmpty(ColorName))
                code.Add($"\"{name}\" setMarkerColor \"{ColorName}\";");
            if (SizeA.HasValue || SizeB.HasValue)
            {
                var a = SizeA.HasValue ? SizeA.Value : 1;
                var b = SizeB.HasValue ? SizeB.Value : 1;

                code.Add($"\"{name}\" setMarkerSize [{a},{b}];");
            }
            if (Angle.HasValue)
                code.Add($"\"{name}\" setMarkerDir {Angle.Value};");
            if(!String.IsNullOrEmpty(FillName))
                code.Add($"\"{name}\" setMarkerBrush \"{FillName}\";");
            if(!String.IsNullOrEmpty(MarkerType))
                code.Add($"\"{name}\" setMarkerShape \"{MarkerType}\";");

            sqf = code.ToArray();
            VariableName = name;
            return name;
        }
    }
}

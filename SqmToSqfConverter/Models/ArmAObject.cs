using System;
using System.Collections.Generic;

namespace SqmToSqfConverter.Models
{
    public class ArmAObject : IArmAItem
    {
        public int ID { get; set; }
        public string Side { get; set; }
        public string DataType { get; } = "Object";
        public string VariableName { get; set; }

        public string Type { get; set; }

        public PositionInfo PositionInfo { get; } = new PositionInfo();
        public Attributes Attributes { get; } = new Attributes();
        public List<CustomAttribute> CustomAttributes { get; } = new List<CustomAttribute>();
        // Needs research
        public ArmAFlags Flags { get; set; }
        public float? ATLOffset { get; set; }

        public bool GunnerSeatUsed { get; set; }

        public string GetSqf(out string[] sqf, ConvertOptions options, string groupName = null)
        {
            var code = new List<string>();

            var name = Attributes.Name ?? $"vehicle{ID.ToString()}";

            var position = PositionInfo.Position;
            if ((Flags & ArmAFlags.Unit) == ArmAFlags.Unit || !String.IsNullOrEmpty(groupName))
            {
                if (string.IsNullOrEmpty(groupName))
                    throw new Exception("Tried to create unit without a group");
                if (name.StartsWith("vehicle"))
                    name = name.Replace("vehicle", "unit");

                code.Add($"{name} = {groupName} createUnit [\"{Type}\", {position}, [], 0, \"CAN_COLLIDE\"];");
            }
            else
                code.Add($"{name} = createVehicle [\"{Type}\", {position}, [], 0, \"CAN_COLLIDE\"];");

            var setUpVector = false;
            if (PositionInfo.Angle != null)
            {
                var angle = PositionInfo.Angle;
                //yaw = X, pitch = Y, roll/bank = Z
                code.Add($"{name} setDir deg({angle.Z});");
                if (angle.X != 0 || angle.Y != 0)
                    code.Add($"[{name}, {angle.X}, {angle.Y}] call BIS_fnc_setPitchBank;");
                else
                    setUpVector = true;
            }
            else
                setUpVector = true;

            code.Add($"{name} setPosASL {position};");
            if((Flags & ArmAFlags.SetOnGround) == ArmAFlags.SetOnGround)
                code.Add($"{name} setPosATL [getPosATL {name} select 0, getPosATL {name} select 1, 0];");
            else if (ATLOffset.HasValue)
                code.Add($"{name} setPosATL [getPosATL {name} select 0, getPosATL {name} select 1, {ATLOffset.Value}];");

            if(setUpVector)
                code.Add($"{name} setVectorUp [0,0,1];");

            if (Type == "Land_Carrier_01_base_F")
                code.Add($"[{name}] call BIS_fnc_Carrier01PosUpdate;");

            if (Attributes.Health.HasValue)
                code.Add($"{name} setDamage {Attributes.Health.Value};");
            if (Attributes.Ammo.HasValue)
                code.Add($"{name} setVehicleAmmo {Attributes.Ammo.Value};");
            if (Attributes.Fuel.HasValue)
                code.Add($"{name} setFuel {Attributes.Fuel.Value};");

            foreach(var customAttribute in CustomAttributes)
            {
                var formattedCode = customAttribute.Expression.Replace("_this", name);
                if (customAttribute.ValueType == "STRING" || customAttribute.ValueType == "ANY")
                    formattedCode = formattedCode.Replace("_value", $"\"{customAttribute.Value}\"");
                else
                    formattedCode = formattedCode.Replace("_value", $"{customAttribute.Value}");

                code.Add(formattedCode);
            }

            if (options.AddToGlobalArray)
                code.Add($"objects pushBack {name};");

            code.Add("");

            sqf = code.ToArray();
            VariableName = name;
            return name;
            /*
            if (positionInfo.Properties.ContainsKey("angles[]"))
            {
                var angle = TranslateVector(positionInfo.Properties["angles[]"]);
                _cfgRot params["_P", "_Y", "_R"];
                _Y = deg _Y;
                _P = 360 - (deg _P);
                _R = 360 - (deg _R);         
                {
                    private _deg = call compile format [ "%1 mod 360", _x ];
                    if ( _deg < 0 ) then { _deg = linearConversion[ -0, -360, _deg, 360, 0 ]; };
                    call compile format[ "%1 = _deg", _x ];
                } forEach [ "_P", "_R", "_Y" ];        
                private _up = [sin _R, -sin _P, cos _R * cos _P];
                _obj setDir _Y;
                _obj setVectorUp _up;
            }
            */
        }
    }
}

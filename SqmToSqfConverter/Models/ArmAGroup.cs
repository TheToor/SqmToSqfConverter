using System;
using System.Collections.Generic;

namespace SqmToSqfConverter.Models
{
    public class ArmAGroup : IArmAItem
    {
        public int ID { get; set; }
        public string Side { get; set; }
        public string DataType { get; } = "Group";
        public string VariableName { get; set; }

        public float? ATLOffset { get; set; }

        public GroupAttributes Attributes { get; set; } = new GroupAttributes();

        public List<ArmAObject> Entities { get; } = new List<ArmAObject>();
        public List<Link> Links { get; } = new List<Link>();
        public List<ArmAWaypoint> Waypoints { get; set; } = new List<ArmAWaypoint>();

        public string GetSqf(out string[] sqf, string groupName = null)
        {
            var code = new List<string>();

            var name = Attributes.Name ?? $"group{ID}";

            code.Add($"{name} = createGroup {Side};");
            if (!String.IsNullOrEmpty(Attributes.CombatMode))
                code.Add($"{name} setCombatMode \"{Attributes.CombatMode}\";");
            if (!String.IsNullOrEmpty(Attributes.Behaviour))
                code.Add($"{name} setBehaviour \"{Attributes.Behaviour}\";");
            if (!String.IsNullOrEmpty(Attributes.SpeedMode))
                code.Add($"{name} setSpeedMode \"{Attributes.SpeedMode}\";");
            if (!String.IsNullOrEmpty(Attributes.Formation))
                code.Add($"{name} setFormation \"{Attributes.Formation}\";");
            if (Attributes.DynamicSimulation)
                code.Add($"{name} enableDynamicSimulation true;");

            foreach(var entity in Entities)
            {
                entity.ATLOffset = ATLOffset;
                var unitName = entity.GetSqf(out var entitySQF, name);
                code.AddRange(entitySQF);
            }

            foreach (var link in Links)
            {
                var group = Parser.Mission.Groups.Find(g => g.Entities.Find(u => u.ID == link.ObjectToLink) != null);
                var unit = group.Entities.Find(u => u.ID == link.ObjectToLink);
                var linker = Parser.Mission.Objects.Find(o => o.ID == link.ObjectToLinkTo);

                if (unit == null || linker == null)
                    throw new Exception($"Invalid link: {ID}: {link.ObjectToLink}->{link.ObjectToLinkTo}");

                if((linker.Flags & ArmAFlags.Unit) != ArmAFlags.Unit)
                {
                    // Put unit in vehicle
                    switch(link.Role)
                    {
                        // Driver
                        case 1:
                            code.Add($"{unit.VariableName} moveInDriver {linker.VariableName};");
                            break;

                        // Commander / Gunner
                        case 2:
                            {
                                var action = "moveInCommander";
                                if(!linker.GunnerSeatUsed)
                                {
                                    action = "moveInGunner";
                                    linker.GunnerSeatUsed = true;
                                }

                                code.Add($"{unit.VariableName} {action} {linker.VariableName};");
                            }
                            break;

                        // Passenger
                        case 3:
                            {
                                if (link.CargoIndex.HasValue)
                                    code.Add($"{unit.VariableName} moveInCargo [{linker.VariableName}, {link.CargoIndex.Value}];");
                                else
                                    code.Add($"{unit.VariableName} moveInCargo {linker.VariableName};");
                            }
                            break;
                    }
                }
            }

            foreach(var waypoint in Waypoints)
            {
                var waypointName = $"waypoint{waypoint.ID}";
                code.Add($"{waypointName} = {name} addWaypoint [{waypoint.Position}, 0];");
                code.Add($"{waypointName} setWaypointType \"{waypoint.Type}\";");
                if (!String.IsNullOrEmpty(waypoint.CombatMode))
                    code.Add($"{waypointName} setWaypointCombatMode \"{waypoint.CombatMode}\";");
                if (!String.IsNullOrEmpty(waypoint.Formation))
                    code.Add($"{waypointName} setWaypointFormation \"{waypoint.Formation}\";");
                if (!String.IsNullOrEmpty(waypoint.Speed))
                    code.Add($"{waypointName} setWaypointSpeed \"{waypoint.Speed}\";");
                if (!String.IsNullOrEmpty(waypoint.Combat))
                    code.Add($"{waypointName} setWaypointBehaviour \"{waypoint.Combat}\";");

                waypoint.VariableName = waypointName;
            }

            code.Add("");

            sqf = code.ToArray();
            VariableName = name;
            return name;
        }
    }
}

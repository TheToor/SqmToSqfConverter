using SqmToSqfConverter.Models;
using System;
using System.Collections.Generic;

namespace SqmToSqfConverter
{
    internal class Parser
    {
        private SimpleClass _missionFile;
        private List<string> _sqf = new List<string>();

        internal static Mission Mission;

        internal Parser(SimpleClass missionFile)
        {
            _missionFile = missionFile;

            Mission = new Mission();
        }

        internal string[] Parse()
        {
            var intel = _missionFile.SubClasses["Intel"];
            ParseIntel(intel);

            var entities = _missionFile.SubClasses["Entities"];
            ParseEntities(entities);

            return _sqf.ToArray();
        }

        private void WriteSqf(string line)
        {
            _sqf.Add(line);
        }

        private void ParseIntel(SimpleClass intel)
        {
            var year = intel.Properties["year"];
            var month = intel.Properties["month"];
            var day = intel.Properties["day"];
            var hour = intel.Properties["hour"];
            var min = intel.Properties["minute"];

            WriteSqf($"setDate [{year}, {month}, {day}, {hour}, {min}];");
            WriteSqf("");
        }

        private void ParseEntities(SimpleClass entities)
        {
            foreach(var entity in entities.SubClasses)
            {
                ParseEntity(entity);
            }

            foreach(var entity in Mission.Objects)
            {
                entity.GetSqf(out var sqf);
                _sqf.AddRange(sqf);
            }

            foreach(var group in Mission.Groups)
            {
                group.GetSqf(out var sqf);
                _sqf.AddRange(sqf);
            }

            foreach(var marker in Mission.Markers)
            {
                marker.GetSqf(out var sqf);
                _sqf.AddRange(sqf);
            }
        }

        private void ParseEntity(KeyValuePair<string, SimpleClass> entity)
        {
            var obj = entity.Value;
            var type = obj.Properties["dataType"].Replace("\"", String.Empty);

            switch(type)
            {
                case "Object":
                    var armaObject = ProcessObject(obj);
                    Mission.Objects.Add(armaObject);
                    break;

                case "Group":
                    var group = ProcessGroup(obj);
                    Mission.Groups.Add(group);
                    break;

                case "Marker":
                    var marker = ProcessMarker(obj);
                    Mission.Markers.Add(marker);
                    break;

                default:
                    throw new Exception($"Unknown type: {type}");
            }
        }

        private Vector3 TranslateVector(string position)
        {
            if (String.IsNullOrEmpty(position))
                return null;

            var split = position.Split(',');
            split[0] = split[0].Substring(1, split[0].Length - 1);
            split[2] = split[2].Substring(0, split[2].Length - 1);

            return new Vector3()
            {
                X = Convert.ToSingle(split[0]),
                Y = Convert.ToSingle(split[2]),
                Z = Convert.ToSingle(split[1])
            };
        }

        private ArmAMarker ProcessMarker(SimpleClass obj)
        {
            var marker = new ArmAMarker();
            marker.ID = Convert.ToInt32(obj.GetProperty("id"));
            marker.ATLOffset = obj.TryGetPropertyAsFloat("atlOffset");
            marker.Name = obj.GetProperty("name");
            marker.Type = obj.GetProperty("type");
            marker.Text = obj.GetProperty("text");
            marker.ColorName = obj.GetProperty("colorName");
            marker.Alpha = obj.TryGetPropertyAsFloat("alpha");
            marker.SizeA = obj.TryGetPropertyAsFloat("a");
            marker.SizeB = obj.TryGetPropertyAsFloat("b");
            marker.Angle = obj.TryGetPropertyAsFloat("angle");
            marker.FillName = obj.GetProperty("fillName");
            marker.MarkerType = obj.GetProperty("markerType");
            marker.Position = TranslateVector(obj.GetProperty("position[]"));

            return marker;
        }

        private ArmAGroup ProcessGroup(SimpleClass obj)
        {
            var group = new ArmAGroup();
            group.Side = obj.GetProperty("side");
            group.ID = Convert.ToInt32(obj.GetProperty("id"));
            group.ATLOffset = obj.TryGetPropertyAsFloat("atlOffset");

            var entities = obj.SubClasses["Entities"];
            foreach(var entity in entities.SubClasses.Values)
            {
                switch(entity.Properties["dataType"].Replace("\"", String.Empty))
                {
                    case "Waypoint":
                        {
                            var waypoint = ProcessWaypoint(entity);
                            group.Waypoints.Add(waypoint);
                        }
                        break;

                    default:
                        {
                            var armaEntity = ProcessObject(entity);
                            group.Entities.Add(armaEntity);
                        }
                        break;
                }
            }

            var attributes = obj.SubClasses["Attributes"];
            group.Attributes.Name = attributes.GetProperty("name");
            group.Attributes.CombatMode = attributes.GetProperty("combatMode");
            group.Attributes.Behaviour = attributes.GetProperty("behaviour");
            group.Attributes.SpeedMode = attributes.GetProperty("speedMode");
            group.Attributes.Formation = attributes.GetProperty("formation");
            group.Attributes.DynamicSimulation = attributes.GetProperty("dynamicSimulation") != null;

            if(obj.SubClasses.ContainsKey("CrewLinks"))
            {
                var links = obj.SubClasses["CrewLinks"].SubClasses["Links"];

                foreach(var link in links.SubClasses.Values)
                {
                    var armaLink = new Link()
                    {
                        ObjectToLink = Convert.ToInt32(link.Properties["item0"]),
                        ObjectToLinkTo = Convert.ToInt32(link.Properties["item1"]),
                        Role = Convert.ToInt32(link.SubClasses["CustomData"].Properties["role"]),
                        CargoIndex = link.TryGetSubPropertyAsInt("CustomData", "cargoIndex")
                    };

                    group.Links.Add(armaLink);
                }
            }

            return group;
        }

        private ArmAWaypoint ProcessWaypoint(SimpleClass obj)
        {
            var waypoint = new ArmAWaypoint();
            waypoint.ID = Convert.ToInt32(obj.GetProperty("id"));
            waypoint.Type = obj.GetProperty("type");
            waypoint.Position = TranslateVector(obj.GetProperty("position[]"));
            waypoint.ATLOffset = obj.TryGetPropertyAsFloat("atlOffset");
            waypoint.ShowMP = obj.GetProperty("showWP");
            waypoint.CombatMode = obj.GetProperty("combatMode");
            waypoint.Formation = obj.GetProperty("formation");
            waypoint.Speed = obj.GetProperty("speed");
            waypoint.Combat = obj.GetProperty("combat");

            return waypoint;
        }

        private ArmAObject ProcessObject(SimpleClass obj)
        {
            var vehicle = new ArmAObject();
            vehicle.Side = obj.GetProperty("side");
            vehicle.ID = Convert.ToInt32(obj.GetProperty("id"));
            vehicle.Type = obj.GetProperty("type");
            vehicle.Flags = (ArmAFlags)Convert.ToInt32(obj.GetProperty("flags"));
            vehicle.ATLOffset = obj.TryGetPropertyAsFloat("atlOffset");

            vehicle.PositionInfo.Position = TranslateVector(obj.GetSubProperty("PositionInfo", "position[]"));
            vehicle.PositionInfo.Angle = TranslateVector(obj.GetSubProperty("PositionInfo", "angles[]"));

            vehicle.Attributes.Health = obj.TryGetSubPropertyAsFloat("Attributes", "health");
            vehicle.Attributes.Ammo = obj.TryGetSubPropertyAsFloat("Attributes", "ammo");
            vehicle.Attributes.Fuel = obj.TryGetSubPropertyAsFloat("Attributes", "fuel");
            vehicle.Attributes.Skill = obj.TryGetSubPropertyAsFloat("Attributes", "skill");
            if (vehicle.Attributes.Skill.HasValue)
                vehicle.Flags |= ArmAFlags.Unit;

            vehicle.Attributes.Name = obj.GetSubProperty("Attributes", "name");
            vehicle.Attributes.Rank = obj.GetSubProperty("Attributes", "rank");
            vehicle.Attributes.Stance = obj.GetSubProperty("Attributes", "stance");

            if(obj.SubClasses.ContainsKey("CustomAttributes"))
            {
                var attributes = obj.SubClasses["CustomAttributes"];
                foreach(var attribute in attributes.SubClasses.Values)
                {
                    var customAttribute = new CustomAttribute();
                    customAttribute.Property = attribute.GetProperty("property");
                    customAttribute.Expression = attribute.GetProperty("expression");
                    customAttribute.ValueType = attribute.SubClasses["Value"].SubClasses["data"].SubClasses["type"].SubClasses["type[]"].GetProperty("type");

                    var value = attribute.SubClasses["Value"].SubClasses["data"].GetProperty("value");
                    switch (customAttribute.ValueType)
                    {
                        case "STRING":
                        case "ANY":
                            customAttribute.Value = value;
                            break;

                        case "SCALAR":
                            customAttribute.Value = Convert.ToSingle(value);
                            break;

                        default:
                            throw new Exception($"Unknown value type: {customAttribute.ValueType}");
                    }

                    vehicle.CustomAttributes.Add(customAttribute);
                }
            }
            return vehicle;
        }
    }
}

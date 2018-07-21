using SqmToSqfConverter.Models;
using System.Collections.Generic;
using System.Linq;

namespace SqmToSqfConverter.Forms
{
    public partial class Stats : System.Windows.Forms.Form
    {
        public Stats()
        {
            InitializeComponent();

            LabelCountVehicles.Text = $"Vehicles: {Parser.Mission.Objects.Count}";
            LabelCountUnits.Text = $"Units: {Parser.Mission.Groups.Sum(m => m.Entities.Count)}";
            LabelCountGroups.Text = $"Groups: {Parser.Mission.Groups.Count}";

            var text = "==== Units ====\n";
            var entities = new List<ArmAObject>();
            foreach (var missionEntities in Parser.Mission.Groups.Select(m => m.Entities))
                entities.AddRange(missionEntities);
            var groupedEntities = entities.GroupBy(e => e.Type).OrderByDescending(e => e.Count());
            foreach (var group in groupedEntities)
                text += $"[{group.Count().ToString("000")}]: {group.First().Type}\n";

            text += "==== Objects ====\n";
            var groupedObjects = Parser.Mission.Objects.GroupBy(o => o.Type).OrderByDescending(g => g.Count());
            foreach (var group in groupedObjects)
                text += $"[{group.Count().ToString("000")}]: {group.First().Type}\n";

            LabelDetailedStats.Text = text;
        }
    }
}

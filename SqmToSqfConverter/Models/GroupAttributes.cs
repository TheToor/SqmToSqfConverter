namespace SqmToSqfConverter.Models
{
    public class GroupAttributes
    {
        public string Name { get; set; }
        public string CombatMode { get; set; }
        public string Behaviour { get; set; }
        public string SpeedMode { get; set; }
        public string Formation { get; set; }
        
        public bool DynamicSimulation { get; set; }
    }
}

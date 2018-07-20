using System.Collections.Generic;

namespace SqmToSqfConverter.Models
{
    public class Mission
    {
        public List<ArmAGroup> Groups = new List<ArmAGroup>();
        public List<ArmAObject> Objects = new List<ArmAObject>();
        public List<ArmAMarker> Markers = new List<ArmAMarker>();
    }
}

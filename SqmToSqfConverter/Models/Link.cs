namespace SqmToSqfConverter.Models
{
    public class Link
    {
        public int ObjectToLink { get; set; }
        public int ObjectToLinkTo { get; set; }
        public int Role { get; set; }
        public int? CargoIndex { get; set; }
    }
}

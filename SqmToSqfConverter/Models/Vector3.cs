namespace SqmToSqfConverter.Models
{
    public class Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public override string ToString()
        {
            return $"[{X},{Y},{Z}]";
        }
    }
}

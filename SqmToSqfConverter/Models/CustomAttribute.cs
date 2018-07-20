namespace SqmToSqfConverter.Models
{
    public class CustomAttribute
    {
        public string Property { get; set; }
        public string Expression { get; set; }

        public dynamic Value { get; set; }
        public string ValueType { get; set; }
    }
}

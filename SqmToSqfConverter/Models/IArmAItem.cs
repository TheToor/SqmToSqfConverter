namespace SqmToSqfConverter.Models
{
    public interface IArmAItem
    {
        int ID { get; set; }
        string Side { get; set; }
        string DataType { get; }

        string VariableName { get; set; }

        string GetSqf(out string[] sqf, string groupName = null);
    }
}

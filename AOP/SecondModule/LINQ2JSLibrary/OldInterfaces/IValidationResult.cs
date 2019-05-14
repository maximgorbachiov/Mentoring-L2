namespace LINQ2JSLibrary.OldInterfaces
{
    public interface IValidationResult
    {
        bool IsValid { get; set; }
        string ValidationFunction { get; set; }
    }
}

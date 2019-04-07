namespace LINQ2JSLibrary
{
    public interface IValidationResult
    {
        bool IsValid { get; set; }
        string ValidationFunction { get; set; }
    }
}

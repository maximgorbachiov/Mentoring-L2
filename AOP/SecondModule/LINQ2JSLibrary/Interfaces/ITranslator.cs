namespace LINQ2JSLibrary.Interfaces
{
    public interface ITranslator<TModel, TResult>
    {
        TResult Translate(IValidationsStorage<TModel> validationsStorage);
    }
}

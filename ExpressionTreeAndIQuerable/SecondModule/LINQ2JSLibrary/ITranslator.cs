using System.Linq.Expressions;

namespace LINQ2JSLibrary
{
    public interface ITranslator<TModel, TResult>
    {
        TResult Translate(Expression expression);
    }
}

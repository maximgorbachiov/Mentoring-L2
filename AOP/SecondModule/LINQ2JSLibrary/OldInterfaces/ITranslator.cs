using System.Linq.Expressions;

namespace LINQ2JSLibrary.OldInterfaces
{
    public interface ITranslator<TModel, TResult>
    {
        TResult Translate(Expression expression);
    }
}

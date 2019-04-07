using System.Linq.Expressions;

namespace LINQ2JSLibrary
{
    public interface IValidatationProvider<TModel, TResult>
    {
        TResult Execute(Expression expression);
    }
}

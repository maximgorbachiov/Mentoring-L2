using System.Linq.Expressions;

namespace LINQ2JSLibrary.OldInterfaces
{
    public interface IValidatationProvider<TModel, TResult>
    {
        TResult Execute(Expression expression);
    }
}

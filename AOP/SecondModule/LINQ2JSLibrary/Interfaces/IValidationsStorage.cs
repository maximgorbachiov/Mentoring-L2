using System;
using System.Linq.Expressions;

namespace LINQ2JSLibrary.Interfaces
{
    public interface IValidationsStorage<TModel>
    {
        Expression Expression { get; }
        void AddValidation<TMember>(Expression<Func<TModel, TMember>> member, Expression<Func<TMember, bool>> predicate);
    }
}

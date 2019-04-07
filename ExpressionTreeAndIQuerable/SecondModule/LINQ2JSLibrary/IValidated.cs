using System.Collections.Generic;
using System.Linq.Expressions;

namespace LINQ2JSLibrary
{
    public interface IValidated<TModel, TResult>
    {
        List<Expression> Expressions { get; }
        IValidatationProvider<TModel, TResult> ValidatationProvider { get; }
        TResult BuildValidation();
    }
}

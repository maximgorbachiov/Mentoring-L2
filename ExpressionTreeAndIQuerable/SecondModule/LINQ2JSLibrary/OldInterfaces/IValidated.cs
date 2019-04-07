using System.Collections.Generic;
using System.Linq.Expressions;

namespace LINQ2JSLibrary.OldInterfaces
{
    public interface IValidated<TModel, TResult>
    {
        List<Expression> Expressions { get; }
        IValidatationProvider<TModel, TResult> ValidatationProvider { get; }
        TResult BuildValidation();
    }
}

using System;
using System.Linq.Expressions;
using LINQ2JSLibrary.OldInterfaces;

namespace LINQ2JSLibrary.OldImplementations
{
    public class ValidationProvider<TModel> : IValidatationProvider<TModel, Func<TModel, bool>>
    {
        public Func<TModel, bool> Execute(Expression expression)
        {
            var translator = new ActionTranslator<TModel>();
            return translator.Translate(expression);
        }
    }
}

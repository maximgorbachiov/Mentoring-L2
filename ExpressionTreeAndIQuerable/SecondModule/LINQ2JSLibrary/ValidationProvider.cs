using System;
using System.Linq.Expressions;

namespace LINQ2JSLibrary
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

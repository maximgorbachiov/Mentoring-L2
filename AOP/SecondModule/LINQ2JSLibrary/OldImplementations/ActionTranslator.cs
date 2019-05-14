using System;
using System.Linq.Expressions;

namespace LINQ2JSLibrary.OldImplementations
{
    public class ActionTranslator<TModel> : Translator<TModel, Func<TModel, bool>>
    {
        public override Func<TModel, bool> Translate(Expression expression)
        {
            throw new NotImplementedException();
        }
    }
}

using System.Linq.Expressions;
using LINQ2JSLibrary.OldInterfaces;

namespace LINQ2JSLibrary.OldImplementations
{
    public abstract class Translator<TModel, TResult> : ExpressionVisitor, ITranslator<TModel, TResult>
    {
        public abstract TResult Translate(Expression expression);
    }
}

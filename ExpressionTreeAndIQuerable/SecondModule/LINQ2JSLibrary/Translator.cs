using System.Linq.Expressions;

namespace LINQ2JSLibrary
{
    public abstract class Translator<TModel, TResult> : ExpressionVisitor, ITranslator<TModel, TResult>
    {
        public abstract TResult Translate(Expression expression);
    }
}

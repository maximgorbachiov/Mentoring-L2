using System.Linq.Expressions;
using LINQ2JSLibrary.Interfaces;

namespace LINQ2JSLibrary.Implementations
{
    public abstract class Translator<TModel, TResult> : ExpressionVisitor, ITranslator<TModel, TResult>
    {
        public abstract TResult Translate(IValidationsStorage<TModel> validationsStorage);
    }
}

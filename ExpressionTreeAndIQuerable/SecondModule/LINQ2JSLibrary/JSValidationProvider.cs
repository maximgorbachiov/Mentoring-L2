using System.Linq.Expressions;

namespace LINQ2JSLibrary
{
    public class JSValidationProvider<TModel> : IValidatationProvider<TModel, string>
    {
        public string Execute(Expression expression)
        {
            var jsTranslator = new JSTranslator<TModel>();
            return jsTranslator.Translate(expression);
        }
    }
}

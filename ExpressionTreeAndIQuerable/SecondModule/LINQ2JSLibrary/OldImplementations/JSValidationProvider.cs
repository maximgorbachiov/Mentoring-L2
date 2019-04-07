using System.Linq.Expressions;
using LINQ2JSLibrary.OldInterfaces;

namespace LINQ2JSLibrary.OldImplementations
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

using System.Linq.Expressions;
using System.Text;

namespace LINQ2JSLibrary.OldImplementations
{
    public class JSTranslator<TModel> : Translator<TModel, string>
    {
        StringBuilder resultString;

        public override string Translate(Expression exp)
        {
            resultString = new StringBuilder();
            Visit(exp);
            return resultString.ToString();
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(ValidatedModelExtension))
            {
                var operand = (node.Arguments[1] as UnaryExpression).Operand;
                var member = (operand as LambdaExpression).Body as MemberExpression;
                var value = node.Arguments[2] as ConstantExpression;

                switch (node.Method.Name)
                {
                    case "StartWith":
                        this.resultString.Append($"if (!(model.{member.Member.Name}.startsWith({"\"" + value.Value + "\""}))) {{");
                        this.resultString.Append("return false;");
                        this.resultString.Append("}");
                        break;
                    case "GreaterThan":
                        this.resultString.Append($"if (model.{member.Member.Name} <= {value.Value}) {{");
                        this.resultString.Append("return false;");
                        this.resultString.Append("}");
                        break;
                }
                return null;
            }
            return base.VisitMethodCall(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            this.resultString.Append($"return {node.Value.ToString().ToLower()};");
            return null;
        }
    }
}

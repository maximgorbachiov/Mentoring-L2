using System.Linq.Expressions;
using System.Text;
using LINQ2JSLibrary.Interfaces;
using LINQ2JSLibrary.Loggers;

namespace LINQ2JSLibrary.Implementations
{
    public class JSValidationsTranslator<TModel> : Translator<TModel, string>
    {
        StringBuilder resultString;

        [CodeRewritingLogger]
        public override string Translate(IValidationsStorage<TModel> validationsStorage)
        {
            resultString = new StringBuilder();
            resultString.AppendLine("function validate(model) {");
            Visit(validationsStorage.Expression);
            resultString.AppendLine("return true;");
            resultString.AppendLine("}");
            return resultString.ToString();
        }

        protected override Expression VisitInvocation(InvocationExpression node)
        {
            var member = node.Arguments[0] as MemberExpression;
            this.resultString.AppendLine($"if (!(model.{member.Member.Name}invocationPattern)) {{");
            this.resultString.AppendLine("return false;");
            this.resultString.AppendLine("}");

            var result = base.VisitInvocation(node);
            return result;
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            Expression nodeBody = node.Body;
            if (nodeBody.NodeType == ExpressionType.Call)
            {
                this.HandleCallExpression();
            }
            else
            {
                this.HandleBinaryExpression(nodeBody);
            }

            return base.VisitLambda<T>(node);
        }

        private void HandleCallExpression()
        {
            this.resultString.Replace("invocationPattern", ".methodPattern");
        }

        private void HandleBinaryExpression(Expression node)
        {
            var expression = node as BinaryExpression;
            var rightOperand = expression.Right as ConstantExpression;

            switch (node.NodeType)
            {
                case ExpressionType.GreaterThan:
                    this.resultString.Replace("invocationPattern", $" > {rightOperand.Value}");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    this.resultString.Replace("invocationPattern", $" >= {rightOperand.Value}");
                    break;
                case ExpressionType.LessThan:
                    this.resultString.Replace("invocationPattern", $" < {rightOperand.Value}");
                    break;
                case ExpressionType.LessThanOrEqual:
                    this.resultString.Replace("invocationPattern", $" <= {rightOperand.Value}");
                    break;
                case ExpressionType.Equal:
                    this.resultString.Replace("invocationPattern", $" == {rightOperand.Value}");
                    break;
                case ExpressionType.NotEqual:
                    this.resultString.Replace("invocationPattern", $" != {rightOperand.Value}");
                    break;
            }
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var pattern = node.Arguments[0] as ConstantExpression;
            switch (node.Method.Name)
            {
                case "StartsWith":
                    var startWithMethod = $"startsWith({"\"" + pattern.Value + "\""})";
                    this.resultString.Replace("methodPattern", startWithMethod);
                    break;
                case "Contains":
                    var containsMethod = $"includes({"\"" + pattern.Value + "\""})";
                    this.resultString.Replace("methodPattern", containsMethod);
                    break;
            }
            var result = base.VisitMethodCall(node);
            return result;
        }
    }
}

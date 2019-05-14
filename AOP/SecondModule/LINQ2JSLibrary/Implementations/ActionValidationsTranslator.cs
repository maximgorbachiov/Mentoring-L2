using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LINQ2JSLibrary.Interfaces;
using LINQ2JSLibrary.Loggers;

namespace LINQ2JSLibrary.Implementations
{
    public class ActionValidationsTranslator<TModel> : Translator<TModel, Func<TModel, bool>>
    {
        private List<Expression> functionBody = new List<Expression>();
        private ParameterExpression resultVariable = Expression.Parameter(typeof(bool));
        private ParameterExpression modelParam = Expression.Parameter(typeof(TModel));

        [CodeRewritingLogger]
        public override Func<TModel, bool> Translate(IValidationsStorage<TModel> validationsStorage)
        {
            var blockExpression = Visit(validationsStorage.Expression);
            var lambda = Expression.Lambda<Func<TModel, bool>>(blockExpression, modelParam);
            var validationFunc = lambda.Compile();
            return validationFunc;
        }

        protected override Expression VisitBlock(BlockExpression node)
        {
            this.functionBody.Add(Expression.Assign(resultVariable, Expression.Constant(true)));
            base.VisitBlock(node);
            this.functionBody.Add(resultVariable);
            var blockExpression = Expression.Block(new ParameterExpression[] { resultVariable }, this.functionBody);
            return blockExpression;
        }

        protected override Expression VisitInvocation(InvocationExpression node)
        {
            var member = node.Arguments[0] as MemberExpression;
            var memberExpression = Expression.MakeMemberAccess(modelParam, member.Member);
            var newInvocation = Expression.Invoke(node.Expression, memberExpression);
            var assignFalse = Expression.Assign(this.resultVariable, Expression.Constant(false));
            var notExpression = Expression.Not(newInvocation);
            var ifStatement = Expression.IfThen(notExpression, assignFalse);
            this.functionBody.Add(ifStatement);
            return base.VisitInvocation(node);
        }
    }
}

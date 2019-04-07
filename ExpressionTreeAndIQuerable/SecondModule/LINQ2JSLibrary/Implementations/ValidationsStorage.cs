using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LINQ2JSLibrary.Interfaces;

namespace LINQ2JSLibrary.Implementations
{
    public class ValidationsStorage<TModel> : IValidationsStorage<TModel>
    {
        private List<Expression> expressions = new List<Expression>();

        public Expression Expression { get; protected set; }

        public void AddValidation<TMember>(Expression<Func<TModel, TMember>> member, Expression<Func<TMember, bool>> predicate)
        {
            var memberGet = member.Body as MemberExpression;
            if (memberGet != null)
            {
                var invokePredicate = Expression.Invoke(predicate, memberGet);
                this.expressions.Add(invokePredicate);
                this.Expression = Expression.Block(expressions);
            }
        }
    }
}

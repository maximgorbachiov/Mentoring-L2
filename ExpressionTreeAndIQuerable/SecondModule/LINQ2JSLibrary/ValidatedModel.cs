using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace LINQ2JSLibrary
{
    public class ValidatedModel<TModel, TResult> : IValidated<TModel, TResult>
    {
        public List<Expression> Expressions { get; }

        public IValidatationProvider<TModel, TResult> ValidatationProvider { get; protected set; }

        public ValidatedModel(IValidatationProvider<TModel, TResult> validatationProvider)
        {
            this.Expressions = new List<Expression>();
            this.ValidatationProvider = validatationProvider;
        }

        public TResult BuildValidation()
        {
            this.Expressions.Add(Expression.Constant(true));
            BlockExpression blockExpression = Expression.Block(this.Expressions);
            return this.ValidatationProvider.Execute(blockExpression);
        }
    }

    public static class ValidatedModelExtension
    {
        public static IValidated<TModel, TResult> StartWith<TModel, TResult>(this IValidated<TModel, TResult> validatedModel, Expression<Func<TModel, string>> member, string pattern)
        {
            MethodInfo startWith = typeof(ValidatedModelExtension).GetMethod("StartWith");
            startWith = startWith.MakeGenericMethod(new Type[] { typeof(TModel), typeof(TResult) });
            var source = Expression.Parameter(typeof(IValidated<TModel, TResult>), "source");
            var startWithExpression = Expression.Call(startWith, source, member, Expression.Constant(pattern));
            validatedModel.Expressions.Add(startWithExpression);
            return validatedModel;
        }

        public static IValidated<TModel, TResult> GreaterThan<TModel, TResult>(this IValidated<TModel, TResult> validatedModel, Expression<Func<TModel, int>> member, int value)
        {
            MethodInfo greaterThan = typeof(ValidatedModelExtension).GetMethod("GreaterThan");
            greaterThan = greaterThan.MakeGenericMethod(new Type[] { typeof(TModel), typeof(TResult) });
            var source = Expression.Parameter(typeof(IValidated<TModel, TResult>), "source");
            var greaterThanExpression = Expression.Call(greaterThan, source, member, Expression.Constant(value));
            validatedModel.Expressions.Add(greaterThanExpression);
            return validatedModel;
        }
    }
}

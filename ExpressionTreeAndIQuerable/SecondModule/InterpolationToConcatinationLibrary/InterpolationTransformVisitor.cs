using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace InterpolationToConcatinationLibrary
{
    public class InterpolationTransformVisitor : ExpressionVisitor
    {
        private string interpolationBraceTemplate = @"\w*\s*\{\s*\w+\s*\}";
        private MethodInfo concatMethod = typeof(string).GetMethod("Concat", new[] { typeof(string), typeof(string) });
        private MethodInfo toString = typeof(object).GetMethod("ToString");

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            // Interpolation of string works as string.Format method with arguments
            if (node.Method.DeclaringType == typeof(string) && node.Method.Name.Equals("Format"))
            {
                var arguments = node.Arguments;
                string template = "";

                // first argument of string.Format method is a template, where it should set strings-parameters
                if (arguments[0].NodeType == ExpressionType.Constant)
                {
                    var firstArgumentOfFormat = (ConstantExpression)arguments[0];
                    template = (string)firstArgumentOfFormat.Value;
                }

                // if we have strings to be setted in template
                if (arguments.Count > 1)
                {
                    // List of expressions of objects which should be placed with .ToString() method (e.g. $"{obj}" will be obj.ToString())
                    List<MethodCallExpression> toStringExpressions = new List<MethodCallExpression>();

                    IReadOnlyCollection<Expression> argumentsCollection = null;
                    // if string.Format gets new object[] { some params } we should get this params
                    if (arguments[1].NodeType == ExpressionType.NewArrayInit)
                    {
                        argumentsCollection = ((NewArrayExpression)arguments[1]).Expressions;
                    }
                    else
                    {
                        argumentsCollection = new ReadOnlyCollection<Expression>(arguments.Skip(1).ToList());
                    }

                    // create expression for each argument, except template, of Format method
                    foreach (var arrayArgumentExpression in argumentsCollection)
                    {
                        Expression argumentExpression = arrayArgumentExpression;
                        // when structure is parameter for Format method it boxes, so this is Convert operation
                        if (argumentExpression.NodeType == ExpressionType.Convert)
                        {
                            var unaryExpression = (UnaryExpression)arrayArgumentExpression;
                            argumentExpression = unaryExpression.Operand;
                        }
                        // Make argument.ToString instead of {argument}
                        toStringExpressions.Add(Expression.Call(argumentExpression, toString));
                    }
                    
                    // get all constant strings in tempate (e.g. $" some {argument} method" will getting into " some " and " method")
                    string[] expressionParts = Regex.Split(template, interpolationBraceTemplate);
                    Expression lastExpression = null;

                    int j = 0;
                    for (int i = 0; i < expressionParts.Length - 1; i++)
                    {
                        j = i - 1;
                        // Create first constant expression
                        if (lastExpression == null)
                        {
                            lastExpression = Expression.Constant(expressionParts[i]);
                        }
                        else
                        {
                            // Concat constant string and getted before expression and one more constant string
                            lastExpression = Expression.Add(lastExpression, toStringExpressions[j], concatMethod);
                            lastExpression = Expression.Add(lastExpression, Expression.Constant(expressionParts[i]), concatMethod);
                        }
                    }
                    while (++j < toStringExpressions.Count)
                    {
                        lastExpression = Expression.Add(lastExpression, toStringExpressions[j], concatMethod);
                    }

                    return lastExpression;
                }
            }

            return base.VisitMethodCall(node);
        }
    }
}

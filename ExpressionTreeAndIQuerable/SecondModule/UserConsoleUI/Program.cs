using System;
using System.Linq.Expressions;
using InterpolationToConcatinationLibrary;

namespace UserConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Expression<Func<User, string>> expression = (user) => $"object value is {user} {user.Name} {user.Surname} {user.Age} {1}";
            var compiledExpression = expression.Compile();
            Console.WriteLine(expression);
            Console.WriteLine(compiledExpression);
            Console.WriteLine(compiledExpression(new User { Name = "Maksim", Surname = "Harbachou", Age = 23 }));
            Console.ReadLine();*/

            /*Expression<Func<User, string>>  expression = (user) => "object value is " + user.ToString() +" " + user.Name.ToString() + " " + user.Surname.ToString() + " " + user.Age.ToString() + " " + 1.ToString();
            var compiledExpression = expression.Compile();
            Console.WriteLine(expression);
            Console.WriteLine(compiledExpression);
            Console.WriteLine(compiledExpression(new User { Name = "Maksim", Surname = "Harbachou", Age = 23 }));
            Console.ReadLine();*/

            Expression<Func<User, string>> expression = (user) => $"object value: name - {user.Name}, surname - {user.Surname}, age - {user.Age}, number - {1}";
            //Expression<Func<User, string>> expression = (user) => $"object value: name - {user.Name}, surname - {user.Surname}, age - {user.Age}";
            var transofmedExpression = (new InterpolationTransformVisitor().VisitAndConvert(expression, ""));
            Console.WriteLine(expression + " | " + expression.Compile().Invoke(new User { Name = "Maksim", Surname = "Harbachou", Age = 23 }));
            Console.WriteLine(transofmedExpression + " | " + transofmedExpression.Compile().Invoke(new User { Name = "Maksim", Surname = "Harbachou", Age = 23 }));
            Console.ReadLine();
        }

        private class User
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public int Age { get; set; }
        }
    }
}

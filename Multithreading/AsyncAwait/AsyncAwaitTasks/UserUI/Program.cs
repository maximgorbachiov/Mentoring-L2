using System;
using AsyncCalculator;

namespace UserUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region First task
            Console.WriteLine("Task1 started");
            AsyncCalculatorClass.UserProcessStart();
            Console.WriteLine("Task1 ended");
            Console.ReadLine();
            #endregion
        }
    }
}

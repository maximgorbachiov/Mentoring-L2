using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadTasks.Task1;
using ThreadTasks.Task2;
using ThreadTasks.Task3;

namespace UserConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*#region First Task
            Console.WriteLine("First Task");
            Task1.RunTasks();
            Console.ReadKey();
            #endregion

            Console.WriteLine();

            #region Second Task
            Console.WriteLine("Second Task");
            Task2.RunChainOfTasks();
            Console.ReadKey();
            #endregion

            Console.WriteLine();*/

            #region Third Task
            Console.WriteLine("Third Task");
            Task3.RunMatrixMultiplication();
            Console.ReadKey();
            #endregion

            Console.WriteLine();
        }
    }
}

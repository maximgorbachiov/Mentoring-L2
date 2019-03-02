using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadTasks.Task1;
using ThreadTasks.Task2;

namespace UserConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*Console.WriteLine("First Task");
            Task1.RunTasks();
            Console.ReadKey();*/

            Console.WriteLine();

            Console.WriteLine("Second Task");
            Task2.RunChainOfTasks();
            Console.ReadKey();
        }
    }
}

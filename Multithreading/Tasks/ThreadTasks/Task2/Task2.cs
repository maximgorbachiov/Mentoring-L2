using System;
using System.Linq;
using System.Threading.Tasks;

namespace ThreadTasks.Task2
{
    public static class Task2
    {
        private static Random random = new Random();

        public static void RunChainOfTasks()
        {
            Task.Factory
                .StartNew(CreateRandomNumbers)
                .ContinueWith((firstTask) => MultiplyWithRandomNumbers(firstTask.Result))
                .ContinueWith((secondTask) => SortByAscending(secondTask.Result))
                .ContinueWith((thirdTask) => PrintAverageValue(thirdTask.Result));
        }

        private static int[] CreateRandomNumbers()
        {
            int[] randomIntegers = new int[10];

            Console.WriteLine("Created random numbers:");
            for (int i = 0; i < randomIntegers.Length; i++)
            {
                randomIntegers[i] = random.Next(0, 1000);
                Console.Write($"{randomIntegers[i]} ");
            }
            Console.WriteLine();

            return randomIntegers;
        }

        private static int[] MultiplyWithRandomNumbers(int[] array)
        {
            int[] result = new int[array.Length];

            Console.WriteLine("Multiplication of random numbers:");
            for (int i = 0; i < array.Length; i++)
            {
                int randomNumber = random.Next(0, 1000);
                result[i] = array[i] * randomNumber;
                Console.WriteLine($"{array[i]} * {randomNumber} = {result[i]}");
            }
            Console.WriteLine();

            return result;
        }

        private static int[] SortByAscending(int[] array)
        {
            array = array.OrderBy(element => element).ToArray();

            Console.WriteLine("Sort by ascending:");
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine();

            return array;
        }

        private static void PrintAverageValue(int[] array)
        {
            Console.WriteLine($"Average value: {array.Average()}");
        }
    }
}

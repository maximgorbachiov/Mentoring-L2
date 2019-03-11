using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCalculator
{
    public static class AsyncCalculatorClass
    {
        public static void UserProcessStart()
        {
            int userNumber;
            CancellationTokenSource cts = null;

            Console.WriteLine("Enter value more than 0 to start calculation or equal or less than 0 to finish calculations at all");
            do
            {
                string n = Console.ReadLine();

                while (!int.TryParse(n, out userNumber))
                {
                    Console.WriteLine("Value should be a number type!");
                    n = Console.ReadLine();
                }

                if (cts != null)
                {
                    cts.Cancel();
                }

                if (userNumber > 0)
                {
                    cts = new CancellationTokenSource();
                    CalculateSum(userNumber, cts.Token);
                    Console.WriteLine("Calculation started. Enter another value more than 0 to finish current calculation and start new one");
                }
            }
            while (userNumber > 0);

            Console.WriteLine("Calculation finished");
        }

        private static async Task CalculateSum(int userNumber, CancellationToken ct)
        {
            await Task.Factory.StartNew(() =>
            {
                int temp = 0;

                for (int i = 0; i < userNumber; i++)
                {
                    if (ct.IsCancellationRequested)
                    {
                        return;
                    }
                    Thread.Sleep(1);
                    temp += i;
                }
                Console.WriteLine($"Sum of all elements from 0 to {userNumber} = {temp}");
            }, ct);
        }
    }
}

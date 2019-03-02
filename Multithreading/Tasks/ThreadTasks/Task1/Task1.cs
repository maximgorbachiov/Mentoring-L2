using System;
using System.Threading.Tasks;

namespace ThreadTasks.Task1
{
    public static class Task1
    {
        public static void RunTasks()
        {
            Task[] tasks = new Task[100];

            for (int i = 0; i < tasks.Length; i++)
            {
                int number = i + 1;

                tasks[i] = Task.Factory.StartNew(() =>
                {
                    int iterationsCount = 1000;

                    for (int j = 1; j <= iterationsCount; j++)
                    {
                        Console.WriteLine($"Task #{number} - {{{j}}}");
                    }
                });
            }

            Task.Factory.ContinueWhenAll(tasks, (completedTasks) => { Console.WriteLine("All tasks finished their work"); });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTasks.Task7
{
    public static class Task7
    {
        public static void RunScenarios()
        {
            RunTasks();
        }

        private static async void RunTasks()
        {
            CancellationTokenSource cts1 = new CancellationTokenSource();
            CancellationTokenSource cts2 = new CancellationTokenSource();
            CancellationTokenSource cts3 = new CancellationTokenSource();
            CancellationToken ct1 = cts1.Token;
            CancellationToken ct2 = cts2.Token;
            CancellationToken ct3 = cts3.Token;

            List<Task> tasks = RunTasks(1, ct1);
            Task.WaitAll(tasks.ToArray());
            Console.ReadKey();

            tasks = RunTasks(-1, ct2);
            Task.WaitAll(tasks.ToArray());
            Console.ReadKey();

            tasks = RunTasks(1, ct3);
            cts3.Cancel();
            Task.WaitAll(tasks.ToArray());
        }

        private static List<Task> RunTasks(int value, CancellationToken ct)
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Factory.StartNew(() => RunParentTask(value, ct), ct));
            tasks.Add(tasks[0].ContinueWith((parentTask) =>
            {
                Console.WriteLine("This task executed regardless of the result of the parent task");
                Console.WriteLine($"Runs on the thread: {Thread.CurrentThread.ManagedThreadId}");
            }));
            tasks.Add(tasks[0].ContinueWith((parentTask) =>
            {
                if (parentTask.IsFaulted)
                {
                    Console.WriteLine("This task executed when parent task failed");
                    Console.WriteLine($"Error: {parentTask.Exception.Message}");
                    Console.WriteLine($"Runs on the thread: {Thread.CurrentThread.ManagedThreadId}");
                }
                else
                {
                    Console.WriteLine("This task was cancelled");
                    Console.WriteLine($"Runs on the thread: {Thread.CurrentThread.ManagedThreadId}");
                }
            }, TaskContinuationOptions.NotOnRanToCompletion));
            tasks.Add(tasks[0].ContinueWith((parentTask) =>
            {
                Console.WriteLine("This task executed synchronously when parent task failed");
                Console.WriteLine($"Error: {parentTask.Exception.Message}");
                Console.WriteLine($"Runs on the thread: {Thread.CurrentThread.ManagedThreadId}");
            }, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously));
            tasks.Add(tasks[0].ContinueWith((parentTask) =>
            {
                Console.WriteLine("This task should execute not in a thread pool");
                Console.WriteLine($"Runs on the thread: {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"Is threadpool thread: {Thread.CurrentThread.IsThreadPoolThread}");
            }, ct, TaskContinuationOptions.OnlyOnCanceled, new CustomTaskScheduler()));

            return tasks;
        }

        private static TaskResults RunParentTask(int value, CancellationToken cts)
        {
            Console.WriteLine($"Parent task for all other tasks");
            Console.WriteLine($"Runs on the thread: {Thread.CurrentThread.ManagedThreadId}");
            if (cts.IsCancellationRequested)
            {
                return TaskResults.Cancelled;
            }
            if (value < 0)
            {
                throw new ArgumentException("value should be more than 0 to be executed with success or equal to 0 to be executed without success and without failure");
            }
            return TaskResults.Success;
        }
    }

    public enum TaskResults
    {
        Success,
        Cancelled
    }
}

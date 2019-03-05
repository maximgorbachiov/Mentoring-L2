using System;
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
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;
            await RunTasks(1, ct);
            await RunTasks(-1, ct);
            RunTasks(1, ct);
            cts.Cancel();
        }

        private static Task<TaskResults> RunTasks(int value, CancellationToken ct)
        {
            Task<TaskResults> task = Task.Factory.StartNew(() => RunParentTask(value, ct), ct);
            task.ContinueWith((parentTask) =>
            {
                Console.WriteLine("This task executed regardless of the result of the parent task");
                Console.WriteLine($"Runs on the thread: {Thread.CurrentThread.ManagedThreadId}");
            });
            task.ContinueWith((parentTask) =>
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
            }, TaskContinuationOptions.NotOnRanToCompletion);
            task.ContinueWith((parentTask) =>
            {
                Console.WriteLine("This task executed synchronously when parent task failed");
                Console.WriteLine($"Error: {parentTask.Exception.Message}");
                Console.WriteLine($"Runs on the thread: {Thread.CurrentThread.ManagedThreadId}");
            }, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
            task.ContinueWith((parentTask) =>
            {
                Console.WriteLine("This task should execute not in a thread pool");
                Console.WriteLine($"Runs on the thread: {Thread.CurrentThread.ManagedThreadId}");
            }, ct, TaskContinuationOptions.OnlyOnCanceled, new CustomTaskScheduler());

            return task;
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

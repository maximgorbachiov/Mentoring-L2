using System;
using System.Threading;

namespace ThreadTasks.Task5
{
    public static class Task5
    {
        private static Semaphore semaphore = new Semaphore(1, 1);

        public static void RunThreadsWithState(object state)
        {
            semaphore.WaitOne();
            int value = (int)state;

            Console.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId} and state value: {value}");
            value--;
            state = value;

            if (value != 0)
            {
                ThreadPool.QueueUserWorkItem(RunThreadsWithState, state);
                Console.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId} released");
            }
            semaphore.Release();
        }
    }
}

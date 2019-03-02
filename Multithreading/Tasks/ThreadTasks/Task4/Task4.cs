using System;
using System.Threading;

namespace ThreadTasks.Task4
{
    public static class Task4
    {
        public static void RunThreadsWithState(object state)
        {
            int value = (int)state;

            Console.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId} and state value: {value}");
            value--;
            state = value;

            if (value != 0)
            {
                ParameterizedThreadStart threadFunction = new ParameterizedThreadStart(RunThreadsWithState);
                Thread thread = new Thread(threadFunction);
                thread.Start(state);

                thread.Join();
            }
            Console.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId} released");
        }
    }
}

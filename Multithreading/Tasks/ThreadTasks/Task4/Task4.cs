using System;
using System.Threading;

namespace ThreadTasks.Task4
{
    public static class Task4
    {
        public static void RunThreadsWithState(object state)
        {
            int value = (int)state;

            if (value != 0)
            {
                value--;
                Console.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId} and state value: {value}");
                state = value;

                ParameterizedThreadStart threadFunction = new ParameterizedThreadStart(RunThreadsWithState);
                Thread thread = new Thread(threadFunction);
                thread.Start(state);

                thread.Join();
            }
            else
            {
                Console.WriteLine("Last thread");
            }
        }
    }
}

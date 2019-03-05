using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTasks.Task7
{
    public class CustomTaskScheduler : TaskScheduler
    {
        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return new List<Task>();
        }

        protected override void QueueTask(Task task)
        {
            Thread thread = new Thread(() =>
            {
                Console.WriteLine("Custom task scheduler run new thread not from pull");
                Console.WriteLine($"Thread number: {Thread.CurrentThread.ManagedThreadId}");
                base.TryExecuteTask(task);
            });
            thread.Start();
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            return base.TryExecuteTask(task);
        }
    }
}

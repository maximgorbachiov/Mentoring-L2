using System.Threading;

namespace ThreadTasks.Task6
{
    public static class Task6
    {
        private static ConcurrentCollection collection = new ConcurrentCollection();
        private static int cycleCount = 10;

        public static void RunThreads()
        {
            Thread thread = new Thread(Log);
            thread.Start();

            for (int i = 0; i < cycleCount; i++)
            {
                collection.AddElement(i);
            }
        }

        private static void Log()
        {
            for (int i = 0; i < cycleCount; i++)
            {
                collection.LogElements();
            }
        }
    }
}

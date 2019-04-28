using System;

namespace LoggerService
{
    public static class Logger
    {
        public static void Log(object logData)
        {
            Console.WriteLine(logData);
        }
    }
}

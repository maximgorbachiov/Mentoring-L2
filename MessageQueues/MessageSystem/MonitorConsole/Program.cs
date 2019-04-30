using LoggerService;
using MonitorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonitorConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Monitor service starts work");

            IMonitorService monitorService = new MonitorService();
            ThreadPool.QueueUserWorkItem(monitorService.Listen);

            string userInput = string.Empty;
            while (userInput != "exit")
            {
                // user should enter command like stop serviceName role, continue serviceName role, changeServiceTime serviceName role newTime
                userInput = Console.ReadLine();
                userInput = userInput.ToLower();
                var commandParts = userInput.Split(' ');
                if (commandParts.Length > 2)
                {
                    string commandName = commandParts[0];
                    string serviceName = commandParts[1];
                    string role = commandParts[2];

                    switch (commandName)
                    {
                        case "stop":
                            if (!monitorService.StopService(serviceName, role))
                            {
                                Logger.Log("You entered inccorect command format. Please enter commad in right format. E.g. stop service1 client");
                            }
                            break;
                        case "continue":
                            if (!monitorService.StopService(serviceName, role))
                            {
                                Logger.Log("You entered inccorect command format. Please enter commad in right format. E.g. continue service1 client");
                            }
                            break;
                        case "changeServiceTime":
                            if (commandParts.Length > 3)
                            {
                                int newWorkTime = int.Parse(commandParts[3]);
                                if (!monitorService.ChangeServiceProcessTime(serviceName, role, newWorkTime))
                                {
                                    Logger.Log("You entered inccorect command format. Please enter commad in right format. E.g. changeServiceTime service1 client 1000");
                                }
                            }
                            break;
                        default:
                            Logger.Log("Please enter existed commands");
                            break;
                    }
                }
            }
        }
    }
}

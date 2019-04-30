using System;
using System.Threading;
using ConfigurationReader.Interfaces.Services;
using ConfigurationReader.Services;
using ModelsDescriptionLibrary.Interfaces;
using ReceiverLibrary;

namespace ReceiverUI
{
    public class Program
    {
        private const string ConfigPath = "ServiceConfig.json";

        public static void Main(string[] args)
        {
            IConfigurationService configurationService = new JsonConfigurationService();
            IConfiguration configuration = configurationService.GetConfiguration(ConfigPath);
            IReceiverService receiverService = new ReceiverService(configuration);

            if (receiverService.CreateConnectionWithMonitor())
            {
                ThreadPool.QueueUserWorkItem(receiverService.StartMessagingWithClients);
                ThreadPool.QueueUserWorkItem(receiverService.StartMessagingWithMonitor);
                Console.ReadLine();
            }
            else
            {
                Console.ReadLine();
            }
        }
    }
}

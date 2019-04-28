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
            IReceiverService clientService = new ReceiverService(configuration);

            if (clientService.CreateConnectionWithMonitor())
            {
                ThreadPool.QueueUserWorkItem(clientService.StartMessagingWithClients);
                ThreadPool.QueueUserWorkItem(clientService.StartMessagingWithMonitor);
                Console.ReadLine();
            }
            else
            {
                Console.ReadLine();
            }
        }
    }
}

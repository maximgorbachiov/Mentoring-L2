using System;
using System.Threading;
using ClientLibrary;
using ConfigurationReader.Interfaces.Services;
using ConfigurationReader.Services;
using ModelsDescriptionLibrary.Interfaces;

namespace ClientBot
{
    public class Program
    {
        private const string ConfigPath = "ServiceConfig.json";

        public static void Main(string[] args)
        {
            IConfigurationService configurationService = new JsonConfigurationService();
            IConfiguration configuration = configurationService.GetConfiguration(ConfigPath);
            IClientService clientService = new ClientService(configuration);

            if (clientService.CreateConnectionWithMonitor())
            {
                ThreadPool.QueueUserWorkItem(clientService.StartMessagingWithReceiver);
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

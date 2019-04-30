using System.IO;
using ConfigurationReader.Interfaces.Services;
using ModelsDescriptionLibrary.Interfaces;
using ModelsDescriptionLibrary.Models;
using Newtonsoft.Json;

namespace ConfigurationReader.Services
{
    public class JsonConfigurationService : IConfigurationService
    {
        public IConfiguration GetConfiguration(string configPath)
        {
            IConfiguration configuration = null;

            using(StreamReader stream = new StreamReader(configPath))
            {
                string jsonSettings = stream.ReadToEnd();
                configuration = JsonConvert.DeserializeObject<Configuration>(jsonSettings, new JsonSerializerSettings
                {
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                });
            }

            return configuration;
        }
    }
}

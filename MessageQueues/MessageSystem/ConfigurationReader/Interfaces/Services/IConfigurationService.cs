using ModelsDescriptionLibrary.Interfaces;

namespace ConfigurationReader.Interfaces.Services
{
    public interface IConfigurationService
    {
        IConfiguration GetConfiguration(string configPath);
    }
}

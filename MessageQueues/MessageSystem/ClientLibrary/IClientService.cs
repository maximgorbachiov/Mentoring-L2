using ModelsDescriptionLibrary.Models;

namespace ClientLibrary
{
    public interface IClientService
    {
        bool CreateConnectionWithMonitor();
        void StartMessagingWithHandler(object data);
        void StartMessagingWithMonitor(object data);
    }
}

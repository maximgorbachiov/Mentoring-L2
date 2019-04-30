using ServiceLibrary;

namespace ClientLibrary
{
    public interface IClientService : IMessageSystemUnit
    {
        void StartMessagingWithReceiver(object data);
    }
}

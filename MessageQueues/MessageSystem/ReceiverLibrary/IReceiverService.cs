using ServiceLibrary;

namespace ReceiverLibrary
{
    public interface IReceiverService : IMessageSystemUnit
    {
        void StartMessagingWithClients(object data);
    }
}

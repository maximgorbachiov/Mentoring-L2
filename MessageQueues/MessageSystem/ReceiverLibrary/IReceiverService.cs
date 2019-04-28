namespace ReceiverLibrary
{
    public interface IReceiverService
    {
        bool CreateConnectionWithMonitor();
        void StartMessagingWithClients(object data);
        void StartMessagingWithMonitor(object data);
    }
}

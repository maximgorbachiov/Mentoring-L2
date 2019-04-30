namespace ServiceLibrary
{
    public interface IMessageSystemUnit
    {
        bool CreateConnectionWithMonitor();
        void StartMessagingWithMonitor(object data);
    }
}

using System.Messaging;

namespace MessageQueueLibrary
{
    public interface IMessageQueueFactory
    {
        MessageQueue GetMessageQueue(string queueName);
    }
}

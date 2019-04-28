using System.Messaging;

namespace MessageQueueLibrary
{
    public class MessageQueueFactory : IMessageQueueFactory
    {
        public MessageQueue GetMessageQueue(string queueName)
        {
            MessageQueue messageQueue = null;

            if (MessageQueue.Exists(queueName))
            {
                messageQueue = new MessageQueue(queueName);
            }
            else
            {
                messageQueue = MessageQueue.Create(queueName);
            }

            return messageQueue;
        }
    }
}

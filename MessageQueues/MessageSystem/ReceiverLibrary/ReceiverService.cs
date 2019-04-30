using System;
using System.Messaging;
using System.Threading;
using LoggerService;
using ModelsDescriptionLibrary.Interfaces;
using ModelsDescriptionLibrary.Models;
using ServiceLibrary;

namespace ReceiverLibrary
{
    public class ReceiverService : MessageSystemUnit, IReceiverService
    {
        private const string ReceiverQueueName = @".\Private$\ReceiverQueue";
        private MessageQueue receiverQueue = null;

        public ReceiverService(IConfiguration configuration) : base(configuration)
        {
            this.receiverQueue = this.messageQueueFactory.GetMessageQueue(ReceiverQueueName);
            this.receiverQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(UnifiedMessage) });
        }

        public void StartMessagingWithClients(object data)
        {
            while (true)
            {
                lock (this.SynchObj)
                {
                    if (this.workingState)
                    {
                        Message message = this.receiverQueue.Receive();
                        Data clientData = message.Body as Data;
                        Logger.Log($"{clientData} was received");
                    }
                }
                Thread.Sleep(this.workTime);
            }
        }
    }
}

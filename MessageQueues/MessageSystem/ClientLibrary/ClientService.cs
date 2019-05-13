using System;
using System.Messaging;
using System.Threading;
using LoggerService;
using ModelsDescriptionLibrary.Interfaces;
using ModelsDescriptionLibrary.Models;
using ModelsDescriptionLibrary.Models.Enums;
using ServiceLibrary;

namespace ClientLibrary
{
    public class ClientService : MessageSystemUnit, IClientService
    {
        private const string ReceiverQueueName = @".\Private$\ReceiverQueue";
        private MessageQueue receiverQueue = null;

        public ClientService(IConfiguration configuration) : base(configuration)
        {
            this.receiverQueue = this.messageQueueFactory.GetMessageQueue(ReceiverQueueName);
            this.receiverQueue.Formatter = new BinaryMessageFormatter();
        }

        public void StartMessagingWithReceiver(object data)
        {
            while (true)
            {
                lock (this.SynchObj)
                {
                    if (this.workingState)
                    {
                        Data sentData = new Data
                        {
                            ServiceId = this.configuration.ServiceId.ToString(),
                            Info = this.random.Next().ToString(),
                            DateTime = DateTime.Now
                        };
                        UnifiedMessage message = new UnifiedMessage { MessageRole = MessageRole.ClientData, Data = sentData };
                        this.receiverQueue.Send(message);
                        Logger.Log(sentData);
                    }
                }
                Thread.Sleep(this.workTime);
            }
        }
    }
}

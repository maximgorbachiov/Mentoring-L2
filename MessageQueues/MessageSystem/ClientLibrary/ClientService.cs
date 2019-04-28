using System;
using System.Messaging;
using System.Threading;
using LoggerService;
using MessageQueueLibrary;
using ModelsDescriptionLibrary.Interfaces;
using ModelsDescriptionLibrary.Models;
using ModelsDescriptionLibrary.Models.Enums;

namespace ClientLibrary
{
    public class ClientService : IClientService
    {
        private const string HandlerQueueName = @".\Private$\HandlerQueue";
        private const string ControlQueueName = @".\Private$\ControlQueue";

        private IMessageQueueFactory messageQueueFactory = new MessageQueueFactory();

        private MessageQueue handlerQueue = null;
        private MessageQueue replyQueue = null;
        private MessageQueue controlQueue = null;

        private IConfiguration configuration = null;

        private object synchObj = new object();
        private bool workingState = true;
        private Random random = new Random();
        private int workTime;

        public ClientService(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.workTime = configuration.WorkTime;
            this.handlerQueue = this.messageQueueFactory.GetMessageQueue(HandlerQueueName);
            this.replyQueue = this.messageQueueFactory.GetMessageQueue(configuration.ReplyQueue);
            this.controlQueue = this.messageQueueFactory.GetMessageQueue(ControlQueueName);
        }

        public bool CreateConnectionWithMonitor()
        {
            Logger.Log("Trying to set connection with Monitor");

            bool result = false;

            ServiceInitMessage initMessage = new ServiceInitMessage(this.configuration, DateTime.Now);
            this.SendInitMessage(initMessage);

            Message message = this.replyQueue.Peek();
            InitReplyMessage messageBody = message.Body as InitReplyMessage;

            if (messageBody != null)
            {
                this.replyQueue.Receive();

                if (messageBody.Status == InitStatus.Error)
                {
                    Logger.Log($"Service with ID = {this.configuration.ServiceId} has already exist in the system. Change your Service Id");
                }
                else
                {
                    Logger.Log("Service successfully connected to the Monitor");
                    result = true;
                }
            }

            return result;
        }

        public void StartMessagingWithHandler(object data)
        {
            while (true)
            {
                lock (synchObj)
                {
                    if (this.workingState)
                    {
                        Data sentData = new Data
                        {
                            ServiceId = this.configuration.ServiceId.ToString(),
                            Info = this.random.Next().ToString(),
                            DateTime = DateTime.Now
                        };
                        this.handlerQueue.Send(sentData);
                        Logger.Log(sentData);

                        Thread.Sleep(this.workTime);
                    }
                }
            }
        }

        public void StartMessagingWithMonitor(object data)
        {
            while (true)
            {
                lock (synchObj)
                {
                    Message message = this.replyQueue.Peek();
                    ICommand command = message.Body as ICommand;

                    if (command != null)
                    {
                        switch (command.CommandType)
                        {
                            case CommandType.STOP:
                                this.workingState = false;
                                Logger.Log("Service temporarily stopped");
                                break;
                            case CommandType.CONTINUE:
                                this.workingState = true;
                                Logger.Log("Service continued to work");
                                break;
                            case CommandType.CHANGE_WORKTIME:
                                int? newValue = command.Data as int?;
                                if (newValue.HasValue)
                                {
                                    this.workTime = newValue.Value;
                                }
                                Logger.Log($"Service work time was changed to {this.workTime}");
                                break;
                        }
                    }
                }
            }
        }

        protected void SendInitMessage(ServiceInitMessage initMessage)
        {
            Message message = new Message(initMessage);
            message.ResponseQueue = new MessageQueue(initMessage.ServiceConfiguration.ReplyQueue);
            this.controlQueue.Send(message);
        }
    }
}

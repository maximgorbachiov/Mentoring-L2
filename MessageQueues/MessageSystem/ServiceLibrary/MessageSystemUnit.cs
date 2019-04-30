using System;
using System.Messaging;
using LoggerService;
using MessageQueueLibrary;
using ModelsDescriptionLibrary.Interfaces;
using ModelsDescriptionLibrary.Models;
using ModelsDescriptionLibrary.Models.Enums;

namespace ServiceLibrary
{
    public abstract class MessageSystemUnit
    {
        private const string ControlQueueName = @".\Private$\ControlQueue";

        protected IMessageQueueFactory messageQueueFactory = new MessageQueueFactory();

        protected MessageQueue replyQueue = null;
        protected MessageQueue controlQueue = null;

        protected bool workingState = true;
        protected Random random = new Random();
        protected int workTime;

        protected IConfiguration configuration = null;

        protected object SynchObj { get; private set; }

        public MessageSystemUnit(IConfiguration configuration)
        {
            this.Init(configuration);
        }

        protected void Init(IConfiguration configuration)
        {
            this.SynchObj = new object();
            this.configuration = configuration;
            this.workTime = configuration.WorkTime;
            this.replyQueue = this.messageQueueFactory.GetMessageQueue(configuration.ReplyQueue);
            this.replyQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(UnifiedMessage), typeof(ServiceInitMessage) });
            this.controlQueue = this.messageQueueFactory.GetMessageQueue(ControlQueueName);
        }

        public bool CreateConnectionWithMonitor()
        {
            Logger.Log("Trying to set connection with Monitor");

            bool result = false;

            ServiceInitMessage initMessage = new ServiceInitMessage
            {
                ServiceConfiguration = this.configuration,
                InitStart = DateTime.Now
            };
            this.SendInitMessage(initMessage);

            Message message = this.replyQueue.Peek();
            UnifiedMessage messageBody = message.Body as UnifiedMessage;
            var monitorReply = messageBody.Data as InitReplyMessage;

            if (monitorReply != null)
            {
                this.replyQueue.Receive();

                if (monitorReply.Status == Status.Error)
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

        public void StartMessagingWithMonitor(object data)
        {
            while (true)
            {
                Message message = this.replyQueue.Peek();
                UnifiedMessage unifiedMessage = message.Body as UnifiedMessage;
                ICommand command = unifiedMessage.Data as ICommand;

                if (command != null)
                {
                    switch (command.CommandType)
                    {
                        case CommandType.STOP:
                            lock (this.SynchObj)
                            {
                                this.workingState = false;
                            }
                            Logger.Log("Service temporarily stopped");
                            break;
                        case CommandType.CONTINUE:
                            lock (this.SynchObj)
                            {
                                this.workingState = true;
                            }
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
                    this.controlQueue.Send(new UnifiedMessage
                    {
                        MessageRole = MessageRole.CommandReply,
                        Data = new CommandReplyMessage
                        {
                            ServiceId = this.configuration.ServiceId,
                            ServiceRole = this.configuration.ServiceRole,
                            CommandType = command.CommandType,
                            Status = Status.OK
                        }
                    });
                }
            }
        }

        protected virtual void SendInitMessage(ServiceInitMessage initMessage)
        {
            UnifiedMessage unifiedMessage = new UnifiedMessage
            {
                MessageRole = MessageRole.ServiceInit,
                Data = initMessage
            };
            Message message = new Message(unifiedMessage);
            message.ResponseQueue = this.replyQueue;
            this.controlQueue.Send(unifiedMessage);
        }
    }
}

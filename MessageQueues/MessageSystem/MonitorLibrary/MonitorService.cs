using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using LoggerService;
using MessageQueueLibrary;
using ModelsDescriptionLibrary.Interfaces;
using ModelsDescriptionLibrary.Models;
using ModelsDescriptionLibrary.Models.Enums;

namespace MonitorLibrary
{
    public class MonitorService : IMonitorService
    {
        private const string ControlQueueName = @".\Private$\ControlQueue";
        private MessageQueue controlQueue;
        private IMessageQueueFactory messageQueueFactory = new MessageQueueFactory();

        private List<StoredCommand> storedCommands = new List<StoredCommand>();

        public List<ServiceInstance> ClientServices { get; set; }
        public List<ServiceInstance> ReceiverServices { get; set; }

        private object synchObj = new object();

        public MonitorService()
        {
            this.controlQueue = messageQueueFactory.GetMessageQueue(ControlQueueName);
        }

        public void Listen(object data)
        {
            while (true)
            {
                Message message = this.controlQueue.Receive();
                UnifiedMessage unifiedMessage = message.Body as UnifiedMessage;

                switch (unifiedMessage.MessageRole)
                {
                    case MessageRole.ServiceInit:
                        this.ProcessInitMessage(unifiedMessage, message.ResponseQueue);
                        break;
                    case MessageRole.CommandReply:
                        this.ProcessCommandReply(unifiedMessage);
                        break;
                }
            }
        }

        public bool StopService(string serviceName, string role)
        {
            bool result = false;
            var serviceInstance = this.FindServiceByNameAndRole(serviceName, role);

            if (serviceInstance != null)
            {
                this.StopService(serviceInstance);
                result = true;
            }
            return result;
        }

        public bool ContinueService(string serviceName, string role)
        {
            bool result = false;
            var serviceInstance = this.FindServiceByNameAndRole(serviceName, role);

            if (serviceInstance != null)
            {
                this.ContinueService(serviceInstance);
                result = true;
            }
            return result;
        }

        public bool ChangeServiceProcessTime(string serviceName, string role, int newWorkTime)
        {
            bool result = false;
            var serviceInstance = this.FindServiceByNameAndRole(serviceName, role);

            if (serviceInstance != null)
            {
                this.ChangeServiceProcessTime(serviceInstance, newWorkTime);
                result = true;
            }
            return result;
        }

        public void StopService(ServiceInstance serviceInstance)
        {
            this.CreateStoredCommand(serviceInstance, CommandType.STOP);
            serviceInstance.SendCommand(CommandType.STOP);
        }

        public void ContinueService(ServiceInstance serviceInstance)
        {
            this.CreateStoredCommand(serviceInstance, CommandType.CONTINUE);
            serviceInstance.SendCommand(CommandType.CONTINUE);
        }

        public void ChangeServiceProcessTime(ServiceInstance serviceInstance, int newWorkTime)
        {
            this.CreateStoredCommand(serviceInstance, CommandType.CHANGE_WORKTIME);
            serviceInstance.SendCommand(CommandType.CHANGE_WORKTIME, newWorkTime);
        }

        private void ProcessInitMessage(UnifiedMessage message, MessageQueue responseQueue)
        {
            ServiceInitMessage initMessage = message.Data as ServiceInitMessage;
            var initReplyMessage = new InitReplyMessage { MonitorReplyTime = DateTime.Now };

            bool isClientExist = this.CheckService(initMessage.ServiceConfiguration, this.ClientServices);
            bool isReceiverExist = this.CheckService(initMessage.ServiceConfiguration, this.ReceiverServices);

            if (isClientExist || isReceiverExist)
            {
                initReplyMessage.Status = Status.Error;
            }
            else
            {
                initReplyMessage.Status = Status.OK;
                ServiceInstance serviceInstance = new ServiceInstance
                {
                    Configuration = initMessage.ServiceConfiguration,
                    MessageQueue = responseQueue
                };

                var servicesGroup = serviceInstance.Configuration.ServiceRole == ServiceRole.Client
                    ? this.ClientServices
                    : this.ReceiverServices;

                servicesGroup.Add(serviceInstance);
            }
            responseQueue.Send(initReplyMessage);
        }

        private void ProcessCommandReply(UnifiedMessage message)
        {
            CommandReplyMessage commandReply = message.Data as CommandReplyMessage;

            var storedCommand = this.storedCommands
                .FirstOrDefault(command => command.ServiceId == commandReply.ServiceId && command.ServiceRole == commandReply.ServiceRole);
            if (storedCommand != null)
            {
                lock (this.synchObj)
                {
                    this.storedCommands.Remove(storedCommand);
                }
                if (commandReply.Status == Status.OK)
                {
                    this.LogCommandCompletion(commandReply, storedCommand.Data);
                }
                else
                {
                    Logger.Log($"Service {commandReply.ServiceId} with role {commandReply.ServiceRole} respond with error.");
                }
            }
        }

        private bool CheckService(IConfiguration configuration, List<ServiceInstance> services)
        {
            bool result = services.Any(service => service.Configuration.ServiceId == configuration.ServiceId
                && service.Configuration.ServiceRole == configuration.ServiceRole);
            return result;
        }

        private void CreateStoredCommand(ServiceInstance serviceInstance, CommandType commandType)
        {
            lock (this.synchObj)
            {
                this.storedCommands.Add(new StoredCommand
                {
                    ServiceId = serviceInstance.Configuration.ServiceId,
                    ServiceRole = serviceInstance.Configuration.ServiceRole,
                    CommandType = commandType
                });
            }
        }

        private ServiceInstance FindServiceByNameAndRole(string name, string role)
        {
            var serviceInstance = this.ClientServices.FirstOrDefault(service => service.Configuration.ServiceId == name
                && service.Configuration.ServiceRole.ToString() == role);

            if (serviceInstance == null)
            {
                serviceInstance = this.ReceiverServices.FirstOrDefault(service => service.Configuration.ServiceId == name
                    && service.Configuration.ServiceRole.ToString() == role);
            }

            return serviceInstance;
        }

        private void LogCommandCompletion(CommandReplyMessage commandReply, object data)
        {
            switch (commandReply.CommandType)
            {
                case CommandType.STOP:
                    Logger.Log($"Service {commandReply.ServiceId} with role {commandReply.ServiceRole} successfully stopped");
                    break;
                case CommandType.CONTINUE:
                    Logger.Log($"Service {commandReply.ServiceId} with role {commandReply.ServiceRole} successfully continued");
                    break;
                case CommandType.CHANGE_WORKTIME:
                    Logger.Log($"Service {commandReply.ServiceId} with role {commandReply.ServiceRole} successfully change work time to {data}");
                    break;
            }
        }
    }
}

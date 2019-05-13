using ModelsDescriptionLibrary.Interfaces;
using ModelsDescriptionLibrary.Models.Enums;
using System;

namespace ModelsDescriptionLibrary.Models
{
    [Serializable]
    public class Configuration : IConfiguration
    {
        public ServiceRole ServiceRole { get; }
        public string ServiceId { get; }
        public int WorkTime { get; }
        public string ReplyQueue { get; }

        public Configuration(ServiceRole serviceRole, string serviceId, int workTime, string replyQueue)
        {
            this.ReplyQueue = replyQueue.Replace("{#ID}", serviceId.ToString());
            this.ServiceRole = serviceRole;
            this.ServiceId = serviceId;
            this.WorkTime = workTime;
        }
    }
}

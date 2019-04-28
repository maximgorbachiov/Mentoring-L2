using System;
using ModelsDescriptionLibrary.Models.Enums;

namespace ModelsDescriptionLibrary.Models
{
    public class InitReplyMessage
    {
        public int ServiceId { get; }
        public DateTime MonitorReplyTime { get; }
        public InitStatus Status { get; }

        public InitReplyMessage(int serviceId, DateTime replyTime, InitStatus status)
        {
            this.ServiceId = serviceId;
            this.MonitorReplyTime = replyTime;
            this.Status = status;
        }
    }
}

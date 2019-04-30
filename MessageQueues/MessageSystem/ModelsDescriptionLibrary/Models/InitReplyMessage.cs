using System;
using ModelsDescriptionLibrary.Models.Enums;

namespace ModelsDescriptionLibrary.Models
{
    public class InitReplyMessage
    {
        public DateTime MonitorReplyTime { get; set; }
        public Status Status { get; set; }
    }
}

using System;
using ModelsDescriptionLibrary.Models.Enums;

namespace ModelsDescriptionLibrary.Models
{
    [Serializable]
    public class InitReplyMessage
    {
        public DateTime MonitorReplyTime { get; set; }
        public Status Status { get; set; }
    }
}

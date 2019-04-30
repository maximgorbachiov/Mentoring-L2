using ModelsDescriptionLibrary.Models.Enums;

namespace ModelsDescriptionLibrary.Models
{
    public class CommandReplyMessage
    {
        public string ServiceId { get; set; }
        public ServiceRole ServiceRole { get; set; }
        public CommandType CommandType { get; set; }
        public Status Status { get; set; }
    }
}

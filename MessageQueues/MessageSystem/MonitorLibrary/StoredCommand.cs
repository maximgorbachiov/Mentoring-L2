using ModelsDescriptionLibrary;
using ModelsDescriptionLibrary.Models.Enums;

namespace MonitorLibrary
{
    public class StoredCommand : Command
    {
        public string ServiceId { get; set; }
        public ServiceRole ServiceRole { get; set; }
    }
}

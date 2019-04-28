using ModelsDescriptionLibrary.Interfaces;
using ModelsDescriptionLibrary.Models.Enums;

namespace ModelsDescriptionLibrary.Models
{
    public class Configuration : IConfiguration
    {
        public ServiceRole ServiceRole { get; }
        public int ServiceId { get; }
        public int WorkTime { get; }
        public string ReplyQueue { get; }

        public Configuration(ServiceRole serviceRole, int id, int workTime, string replyQueue)
        {
            this.ReplyQueue = replyQueue.Replace("{#ID}", id.ToString());
            this.ServiceRole = serviceRole;
            this.ServiceId = id;
            this.WorkTime = workTime;
        }
    }
}

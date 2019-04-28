using ModelsDescriptionLibrary.Models.Enums;

namespace ModelsDescriptionLibrary.Interfaces
{
    public interface IConfiguration
    {
        /// <summary>
        /// A Queue which uses to reply to the service
        /// </summary>
        string ReplyQueue { get; }

        /// <summary>
        /// Show what the role of current service (Client or Receiver)
        /// </summary>
        ServiceRole ServiceRole { get; }

        /// <summary>
        /// Service id. If two services have different roles they can have the same ServiceId
        /// </summary>
        int ServiceId { get; }

        /// <summary>
        /// Time which service spend to do something in milliseconds
        /// </summary>
        int WorkTime { get; }
    }
}

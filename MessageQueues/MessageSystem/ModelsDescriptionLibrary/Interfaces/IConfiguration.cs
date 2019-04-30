using ModelsDescriptionLibrary.Models.Enums;
using Newtonsoft.Json;

namespace ModelsDescriptionLibrary.Interfaces
{
    public interface IConfiguration
    {
        /// <summary>
        /// A Queue which uses to reply to the service
        /// </summary>
        [JsonProperty("ReplyQueue")]
        string ReplyQueue { get; }

        /// <summary>
        /// Show what the role of current service (Client or Receiver)
        /// </summary>
        [JsonProperty("ServiceRole")]
        ServiceRole ServiceRole { get; }

        /// <summary>
        /// Service id. If two services have different roles they can have the same ServiceId
        /// </summary>
        [JsonProperty("ServiceId")]
        string ServiceId { get; }

        /// <summary>
        /// Time which service spend to do something in milliseconds
        /// </summary>
        [JsonProperty("WorkTime")]
        int WorkTime { get; }
    }
}

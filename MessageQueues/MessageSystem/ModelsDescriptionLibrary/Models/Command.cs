using ModelsDescriptionLibrary.Interfaces;
using ModelsDescriptionLibrary.Models.Enums;

namespace ModelsDescriptionLibrary
{
    public class Command : ICommand
    {
        public int ServiceId { get; }
        public object Data { get; }
        public CommandType CommandType { get; }

        public Command(CommandType commandType, int serviceId, object data)
        {
            this.CommandType = commandType;
            this.ServiceId = serviceId;
            this.Data = data;
        }
    }
}

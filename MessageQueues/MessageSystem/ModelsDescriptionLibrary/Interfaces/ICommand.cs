using ModelsDescriptionLibrary.Models.Enums;

namespace ModelsDescriptionLibrary.Interfaces
{
    public interface ICommand
    {
        CommandType CommandType { get; }
        int ServiceId { get; }
        object Data { get; }
    }
}

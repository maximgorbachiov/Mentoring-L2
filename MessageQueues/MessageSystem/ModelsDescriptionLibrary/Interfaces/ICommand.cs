using ModelsDescriptionLibrary.Models.Enums;

namespace ModelsDescriptionLibrary.Interfaces
{
    public interface ICommand
    {
        CommandType CommandType { get; set; }
        object Data { get; set; }
    }
}

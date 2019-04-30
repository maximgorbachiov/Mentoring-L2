using ModelsDescriptionLibrary.Interfaces;
using ModelsDescriptionLibrary.Models.Enums;

namespace ModelsDescriptionLibrary
{
    public class Command : ICommand
    {
        public object Data { get; set; }
        public CommandType CommandType { get; set; }
    }
}

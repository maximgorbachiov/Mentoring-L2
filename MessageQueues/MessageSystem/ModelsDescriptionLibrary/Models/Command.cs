using ModelsDescriptionLibrary.Interfaces;
using ModelsDescriptionLibrary.Models.Enums;
using System;

namespace ModelsDescriptionLibrary
{
    [Serializable]
    public class Command : ICommand
    {
        public object Data { get; set; }
        public CommandType CommandType { get; set; }
    }
}

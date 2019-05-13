using ModelsDescriptionLibrary.Interfaces;
using System;

namespace ModelsDescriptionLibrary.Models
{
    [Serializable]
    public class ServiceInitMessage
    {
        public IConfiguration ServiceConfiguration { get; set; }
        public DateTime InitStart { get; set; }
    }
}

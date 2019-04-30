using System;
using ModelsDescriptionLibrary.Interfaces;

namespace ModelsDescriptionLibrary.Models
{
    public class ServiceInitMessage
    {
        public IConfiguration ServiceConfiguration { get; set; }
        public DateTime InitStart { get; set; }
    }
}

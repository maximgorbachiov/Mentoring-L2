using System;
using ModelsDescriptionLibrary.Interfaces;

namespace ModelsDescriptionLibrary.Models
{
    public class ServiceInitMessage
    {
        public IConfiguration ServiceConfiguration { get; }
        public DateTime InitStart { get; }

        public ServiceInitMessage(IConfiguration configuration, DateTime initStart)
        {
            this.ServiceConfiguration = configuration;
            this.InitStart = initStart;
        }
    }
}

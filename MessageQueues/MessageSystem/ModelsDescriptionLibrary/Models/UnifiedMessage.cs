using ModelsDescriptionLibrary.Models.Enums;
using System;
using System.Xml.Serialization;

namespace ModelsDescriptionLibrary.Models
{
    //[XmlInclude(typeof(ServiceInitMessage))]
    [Serializable]
    public class UnifiedMessage
    {
        public MessageRole MessageRole { get; set; }
        public object Data { get; set; }
    }
}

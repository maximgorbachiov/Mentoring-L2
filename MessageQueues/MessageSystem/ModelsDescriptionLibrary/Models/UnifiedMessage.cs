using ModelsDescriptionLibrary.Models.Enums;

namespace ModelsDescriptionLibrary.Models
{
    public class UnifiedMessage
    {
        public MessageRole MessageRole { get; set; }
        public object Data { get; set; }
    }
}

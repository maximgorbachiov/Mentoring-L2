using System;

namespace ModelsDescriptionLibrary.Models
{
    [Serializable]
    public class Data
    {
        public string ServiceId { get; set; }
        public string Info { get; set; }
        public DateTime DateTime { get; set; }

        public override string ToString()
        {
            return $"Service ID = {ServiceId} provide such info {Info}. Data was sent at {DateTime}";
        }
    }
}

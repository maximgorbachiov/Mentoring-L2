using ModelsDescriptionLibrary;
using ModelsDescriptionLibrary.Interfaces;
using ModelsDescriptionLibrary.Models;
using ModelsDescriptionLibrary.Models.Enums;
using System.Messaging;

namespace MonitorLibrary
{
    public class ServiceInstance
    {
        public IConfiguration Configuration { get; set; }
        public MessageQueue MessageQueue { get; set; }

        public void SendCommand(CommandType commandType, object data = null)
        {
            MessageQueue.Send(new UnifiedMessage
            {
                MessageRole = MessageRole.MonitorCommand,
                Data = new Command { CommandType = commandType, Data = data }
            });
        }
    }
}

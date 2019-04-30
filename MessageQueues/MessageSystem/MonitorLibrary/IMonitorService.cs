using System.Collections.Generic;

namespace MonitorLibrary
{
    public interface IMonitorService
    {
        List<ServiceInstance> ClientServices { get; set; }
        List<ServiceInstance> ReceiverServices { get; set; }
        void Listen(object data);
        void StopService(ServiceInstance serviceInstance);
        bool StopService(string serviceName, string role);
        void ContinueService(ServiceInstance serviceInstance);
        bool ContinueService(string serviceName, string role);
        void ChangeServiceProcessTime(ServiceInstance serviceInstance, int newWorkTime);
        bool ChangeServiceProcessTime(string serviceName, string role, int newWorkTime);
    }
}

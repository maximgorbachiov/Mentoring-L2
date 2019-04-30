using System.Collections.ObjectModel;

namespace MonitorUI
{
    public class ViewModel
    {
        public ObservableCollection<ServiceViewModel> Services { get; } = new ObservableCollection<ServiceViewModel>();
    }
}

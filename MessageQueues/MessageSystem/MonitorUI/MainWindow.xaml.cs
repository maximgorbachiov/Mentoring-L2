using ModelsDescriptionLibrary.Models.Enums;
using MonitorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MonitorUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IMonitorService monitorService = new MonitorService();

        private string clientIconLocation = @"..\..\client.jpg";
        private string receiverIconLocation = @"..\..\receiver.jpg";

        private ViewModel ClientsViewModel = new ViewModel();
        private ViewModel ReceiversViewModel = new ViewModel();

        public MainWindow()
        {
            InitializeComponent();
            //ThreadPool.QueueUserWorkItem(this.monitorService.Listen);
            //this.ClientsListView.ItemsSource = this.clientServices;
            //this.ReceiversListView.ItemsSource = this.receiverServices;
            /*this.CreateServiceItem(new ServiceViewModel
            {
                ServiceName = "client1",
                Image = new BitmapImage(new Uri(clientIconLocation, UriKind.Relative))
            }, true);
            this.CreateServiceItem(new ServiceViewModel
            {
                ServiceName = "client2",
                Image = new BitmapImage(new Uri(clientIconLocation, UriKind.Relative))
            }, true);

            this.CreateServiceItem(new ServiceViewModel
            {
                ServiceName = "receiver1",
                Image = new BitmapImage(new Uri(receiverIconLocation, UriKind.Relative))
            }, false);
            this.CreateServiceItem(new ServiceViewModel
            {
                ServiceName = "receiver2",
                Image = new BitmapImage(new Uri(receiverIconLocation, UriKind.Relative))
            }, false);*/
        }

        private void StopServiceButton_Click(object sender, RoutedEventArgs e)
        {
            //ServiceInstance serviceInstance = this.FindServiceInstance();
            //this.monitorService.StopService(serviceInstance);
        }

        private void ContinueServiceButton_Click(object sender, RoutedEventArgs e)
        {
            //ServiceInstance serviceInstance = this.FindServiceInstance();
            //this.monitorService.ContinueService(serviceInstance);
        }

        private void ChangeWorkTimeButton_Click(object sender, RoutedEventArgs e)
        {
            /*ServiceInstance serviceInstance = this.FindServiceInstance();
            if (int.TryParse(this.NewTimeTextBox.Text, out int newWorkTime))
            {
                this.monitorService.ChangeServiceProcessTime(serviceInstance, newWorkTime);
            }
            else
            {
                MessageBox.Show("To change time you should enter only integer value. Time is set in milliseconds");
            }*/
        }

        /*private ServiceInstance FindServiceInstance()
        {
            var serviceLabel = this.ClientsBox.SelectedItem as Label;

            ServiceInstance serviceInstance = this.FindServiceInstanceInList(this.monitorService.ClientServices, serviceLabel.Content.ToString());
            if (serviceInstance == null)
            {
                ServiceInstance serviceInstance = this.FindServiceInstanceInList(this.monitorService.ClientServices, serviceLabel.Content.ToString());
            }
            else
            {
                service = this.ReceiversBox.SelectedItem as ServiceViewModel;
                serviceInstance = this.monitorService.ReceiverServices
                    .FirstOrDefault(receiver => receiver.Configuration.ServiceId == serviceLabel.ServiceName);
            }
            return serviceInstance;
        }

        private ServiceInstance FindServiceInstanceInList(List<ServiceInstance> serviceInstances, string serviceName)
        {
            return serviceInstances.FirstOrDefault(receiver => receiver.Configuration.ServiceId == serviceName);
        }

        private void CreateServiceItem(ServiceInstance serviceInstance)
        {
            StackPanel stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
            string iconLocation = serviceInstance.Configuration.ServiceRole == ServiceRole.Client
                ? this.clientIconLocation
                : this.receiverIconLocation;
            Image image = new Image
            {
                Source = new BitmapImage { UriSource = new Uri(iconLocation, UriKind.Relative) }
            };
            Label label = new Label { Content = serviceInstance.Configuration.ServiceId };
            stackPanel.Children.Add(image);
            stackPanel.Children.Add(label);
            ListBoxItem listBoxItem = new ListBoxItem { Content = stackPanel, Background = Brushes.Green };
            var listView = serviceInstance.Configuration.ServiceRole == ServiceRole.Client
                ? this.ClientsBox
                : this.ReceiversBox;
            listView.Items.Add(listBoxItem);
        }

        private void CreateServiceItem(ServiceViewModel serviceViewModel, bool isClient)
        {
            Label label = new Label { Content = serviceViewModel.ServiceName, Bi };
            ListBoxItem listBoxItem = new ListBoxItem { Content = label, Background = Brushes.Green };
            var listView = isClient
                ? this.ClientsBox
                : this.ReceiversBox;
            listView.Items.Add(listBoxItem);
        }*/
    }
}

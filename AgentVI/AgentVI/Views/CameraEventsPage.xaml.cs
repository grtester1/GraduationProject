#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AgentVI.ViewModels;
using AgentVI.Services;
using AgentVI.Interfaces;
using AgentVI.Utils;

namespace AgentVI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraEventsPage : ContentPage, INotifyContentViewChanged
    {
        private SensorEventsListViewModel SensorEventsListVM = null;
        public event EventHandler<UpdatedContentEventArgs> RaiseContentViewUpdateEvent;

        public CameraEventsPage()
        {
            InitializeComponent();
        }

        public CameraEventsPage(Sensor i_Sensor) : this()
        {
            SensorEventsListVM = new SensorEventsListViewModel(i_Sensor);
            SensorEventsListVM.UpdateSensorEvents();
            cameraEventsListView.BindingContext = SensorEventsListVM;
            sensorNameLabel.Text = SensorEventsListVM.SensorSource.Name;
            IsEmptyFiller.IsVisible = SensorEventsListVM.IsEmptyFolder;
            IsEmptyText.IsVisible = SensorEventsListVM.IsEmptyFolder;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SensorEventsListVM.UpdateSensorEvents();
        }

        private async void OnRefresh(object sender, EventArgs e)
        {
            try
            {
                await Task.Factory.StartNew(() => SensorEventsListVM.UpdateSensorEvents());
                ((ListView)sender).IsRefreshing = false;
            }catch(AggregateException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void onCameraEventTapped(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void onCameraEventBackButtonTapped(object sender, EventArgs e)
        {
            RaiseContentViewUpdateEvent?.Invoke(this, new UpdatedContentEventArgs(null , true));
        }
    }
}
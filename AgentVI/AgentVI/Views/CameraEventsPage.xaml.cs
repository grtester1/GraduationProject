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

namespace AgentVI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraEventsPage : ContentPage
    {
        private SensorEventsListViewModel SensorEventsLVVM = null;

        public CameraEventsPage()
        {
            InitializeComponent();
        }

        public CameraEventsPage(InnoviObjectCollection<SensorEvent> i_SensorEventCollection) : this()
        {
            SensorEventsLVVM = new SensorEventsListViewModel(i_SensorEventCollection);
            cameraEventsListView.ItemsSource = SensorEventsLVVM.SensorEventList;
            cameraEventsListView.BindingContext = SensorEventsLVVM.SensorEventList;
            initOnFilterStateUpdatedEventHandler();
        }

        public CameraEventsPage(Sensor i_Sensor) : this(i_Sensor.SensorEvents)
        {
        }

        private void initOnFilterStateUpdatedEventHandler()
        {
            ServiceManager.Instance.FilterService.FilterStateUpdated += SensorEventsLVVM.OnFilterStateUpdated;
        }

        private void onCameraEventTapped(object sender, EventArgs e)
        {

        }
    }
}
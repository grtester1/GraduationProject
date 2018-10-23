using AgentVI.Interfaces;
using AgentVI.Models;
using AgentVI.Utils;
using AgentVI.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.DeviceOrientation;
using Plugin.DeviceOrientation.Abstractions;
using System.Net.Sockets;

namespace AgentVI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventDetailsPage : ContentPage, INotifyContentViewChanged, IFocusable
    {
        private EventDetailsViewModel eventDetailsViewModel = null;
        public event EventHandler<UpdatedContentEventArgs> RaiseContentViewUpdateEvent;
        private LandscapeEventDetailsPage landscapeEventDetailsPage = null;


        public EventDetailsPage()
        {
            InitializeComponent();
        }


        public EventDetailsPage(EventModel i_EventModel) : this()
        {
            Task.Factory.StartNew(() =>
            {
                landscapeEventDetailsPage = new LandscapeEventDetailsPage(i_EventModel);
                landscapeEventDetailsPage.RaiseContentViewUpdateEvent += eventsRouter;
            });
            eventDetailsViewModel = new EventDetailsViewModel(i_EventModel);
            CrossDeviceOrientation.Current.UnlockOrientation();
            CrossDeviceOrientation.Current.OrientationChanged += handleOrientationChanged;
            bindUnbindableUIFields();
        }

        private void handleOrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            if(e.Orientation == DeviceOrientations.Landscape)
            {
                quitClipLoading();
                RaiseContentViewUpdateEvent?.Invoke(this, new UpdatedContentEventArgs(UpdatedContentEventArgs.EContentUpdateType.PushAsync, landscapeEventDetailsPage));
            }
        }

        private void quitClipLoading()
        {
            //SensorEventClipVideoPlayer.IsVisible = SensorEventClipVideoPlayer.IsEnabled = false;
        }

        private void restartClipLoading()
        {
            //SensorEventClipVideoPlayer.IsVisible = SensorEventClipVideoPlayer.IsEnabled = true;
        }

        private void onEventDetailsBackButtonTapped(object sender, EventArgs e)
        {
            RaiseContentViewUpdateEvent?.Invoke(this, new UpdatedContentEventArgs(UpdatedContentEventArgs.EContentUpdateType.Pop));
            CrossDeviceOrientation.Current.LockOrientation(DeviceOrientations.Portrait);
        }

        private async void bindUnbindableUIFields()
        {
            await Task.Factory.StartNew(() =>
            {
                //SensorEventClipVideoPlayer.Source = new Converters.tmpVidPlaceholderGeneratorConverter()
                //                                    .Convert(eventDetailsViewModel.SensorEventClipPath, typeof(string), null, null)
                //                                    .ToString();
                SensorEventClipVideoPlayer.Source = eventDetailsViewModel.SensorEventClipPath;
                sensorNameLabel.Text = eventDetailsViewModel.SensorName;
                sensorEventRuleNameLabel.Text = eventDetailsViewModel.SensorEventRuleName;
                SensorEventDateTimeLabel.Text = eventDetailsViewModel.SensorEventDateTime;
                SensorEventRuleNameImage.Source = eventDetailsViewModel.SensorEventObjectType;
                SensorEventBehaviorLabel.Text = eventDetailsViewModel.SensorEventBehavior;
            });
            OnPropertyChanged();
        }

        private void eventsRouter(object sender, UpdatedContentEventArgs e)
        {
            RaiseContentViewUpdateEvent?.Invoke(this, e);
        }

        public void Refocus()
        {
            restartClipLoading();
        }

        private void SensorEventClipVideoPlayer_Navigating(object sender, WebNavigatingEventArgs e)
        {

        }

        private void SensorEventClipVideoPlayer_Navigated(object sender, WebNavigatedEventArgs e)
        {

        }
    }
}
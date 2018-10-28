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

namespace AgentVI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventDetailsPage : ContentPage, INotifyContentViewChanged, IFocusable, IBindable
    {
        public event EventHandler<UpdatedContentEventArgs> RaiseContentViewUpdateEvent;
        public IBindableVM BindableViewModel => eventDetailsViewModel;
        public ContentPage ContentPage => this;
        private EventDetailsViewModel eventDetailsViewModel = null;
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
            checkLockOrientation();
        }

        private void handleOrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            if(e.Orientation == DeviceOrientations.Landscape)
            {
                quitClipLoading();
                RaiseContentViewUpdateEvent?.Invoke(
                    this, new UpdatedContentEventArgs(
                    UpdatedContentEventArgs.EContentUpdateType.PushAsync, landscapeEventDetailsPage, landscapeEventDetailsPage.BindableViewModel
                    ));
            }
        }

        private void quitClipLoading()
        {
            SensorEventClipVideoPlayer.IsVisible = SensorEventClipVideoPlayer.IsEnabled = false;
        }

        private void restartClipLoading()
        {
            SensorEventClipVideoPlayer.IsVisible = SensorEventClipVideoPlayer.IsEnabled = true;
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
                //sensorNameLabel.Text = eventDetailsViewModel.SensorName;
                sensorEventRuleNameLabel.Text = eventDetailsViewModel.SensorEventRuleName;
                SensorEventDateTimeLabel.Text = eventDetailsViewModel.SensorEventDateTime;
                SensorEventRuleNameImage.Source = eventDetailsViewModel.SensorEventObjectType;
                SensorEventBehaviorLabel.Text = eventDetailsViewModel.SensorEventBehavior;
                bool isEventClipAvailable = checkLockOrientation();
                Device.BeginInvokeOnMainThread(() =>
                {
                    SensorEventClipVideoPlayer.IsVisible = isEventClipAvailable;
                }
                );
            });
            OnPropertyChanged();
        }

        private bool checkLockOrientation()
        {
            bool res = true;
            if (!eventDetailsViewModel.IsClipAvailable)
            {
                res = false;
                CrossDeviceOrientation.Current.LockOrientation(DeviceOrientations.Portrait);
            }
            return res;
        }

        private void eventsRouter(object sender, UpdatedContentEventArgs e)
        {
            RaiseContentViewUpdateEvent?.Invoke(this, e);
        }

        public void Refocus()
        {
            restartClipLoading();
        }
    }
}
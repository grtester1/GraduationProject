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

        public EventDetailsPage(EventModel i_EventModel, bool i_IsLive = false) : this()
        {
            Task.Factory.StartNew(() =>
            {
                landscapeEventDetailsPage = new LandscapeEventDetailsPage(i_EventModel, i_IsLive);
                landscapeEventDetailsPage.RaiseContentViewUpdateEvent += eventsRouter;
            });
            eventDetailsViewModel = new EventDetailsViewModel(i_EventModel, i_IsLive);
            eventDetailsViewModel.EventRouter += eventsRouter;
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
            eventDetailsViewModel.IsPlayerVisible = false;
        }

        private void restartClipLoading()
        {
            eventDetailsViewModel.IsPlayerVisible = true;
        }

        private async void onEventDetailsBackButtonTapped(object sender, EventArgs e)
        {
            await Task.Factory.StartNew(() => RaiseContentViewUpdateEvent?.Invoke(this, new UpdatedContentEventArgs(UpdatedContentEventArgs.EContentUpdateType.Buffering)));
            await Task.Factory.StartNew(()=> RaiseContentViewUpdateEvent?.Invoke(this, new UpdatedContentEventArgs(UpdatedContentEventArgs.EContentUpdateType.Pop)));
            await Task.Factory.StartNew(()=> RaiseContentViewUpdateEvent?.Invoke(this, new UpdatedContentEventArgs(UpdatedContentEventArgs.EContentUpdateType.Pop)));
            CrossDeviceOrientation.Current.LockOrientation(DeviceOrientations.Portrait);
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
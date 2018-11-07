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
            Console.WriteLine("###Logger###   -   in EventDetailsPage.EventDetailsPage main thread @ begin ctr");
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("###Logger###   -   in EventDetailsPage.EventDetailsPage thread 1@ begin thread");
                Console.WriteLine("###Logger###   -   in EventDetailsPage.EventDetailsPage thread 1@ before LandscapeEventDetailsPage ctr");
                landscapeEventDetailsPage = new LandscapeEventDetailsPage(i_EventModel, i_IsLive);
                Console.WriteLine("###Logger###   -   in EventDetailsPage.EventDetailsPage thread 1@ after LandscapeEventDetailsPage ctr");
                landscapeEventDetailsPage.RaiseContentViewUpdateEvent += eventsRouter;
                Console.WriteLine("###Logger###   -   in EventDetailsPage.EventDetailsPage thread 1@ end thread");
            });
            Console.WriteLine("###Logger###   -   in EventDetailsPage.EventDetailsPage main thread @ before EventDetailsViewModel");
            eventDetailsViewModel = new EventDetailsViewModel(i_EventModel, i_IsLive);
            Console.WriteLine("###Logger###   -   in EventDetailsPage.EventDetailsPage main thread @ after EventDetailsViewModel");
            eventDetailsViewModel.EventRouter += eventsRouter;
            Console.WriteLine("###Logger###   -   in EventDetailsPage.EventDetailsPage main thread @ before UnlockOrientation");
            CrossDeviceOrientation.Current.UnlockOrientation();
            Console.WriteLine("###Logger###   -   in EventDetailsPage.EventDetailsPage main thread @ after UnlockOrientation");
            CrossDeviceOrientation.Current.OrientationChanged += handleOrientationChanged;
            Console.WriteLine("###Logger###   -   in EventDetailsPage.EventDetailsPage main thread @ before checklockorientation");
            checkLockOrientation();
            Console.WriteLine("###Logger###   -   in EventDetailsPage.EventDetailsPage main thread @ after checklockorientation");
            Console.WriteLine("###Logger###   -   in EventDetailsPage.EventDetailsPage main thread @ after ctr");
        }

        private void handleOrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            if (e.Orientation == DeviceOrientations.Landscape)
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
            Console.WriteLine("###Logger###   -   in EventDetailsPage.checkLockOrientation main thread @ entering");
            bool res = true;
            if (!eventDetailsViewModel.IsClipAvailable)
            {
                res = false;
                CrossDeviceOrientation.Current.LockOrientation(DeviceOrientations.Portrait);
            }
            Console.WriteLine("###Logger###   -   in EventDetailsPage.checkLockOrientation main thread @ exiting");
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
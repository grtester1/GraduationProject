using AgentVI.Interfaces;
using AgentVI.Models;
using AgentVI.Utils;
using AgentVI.ViewModels;
using Plugin.DeviceOrientation;
using Plugin.DeviceOrientation.Abstractions;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgentVI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LandscapeEventDetailsPage : ContentPage, INotifyContentViewChanged, IFocusable
    {
        public event EventHandler<UpdatedContentEventArgs> RaiseContentViewUpdateEvent;
        private LandscapeEventDetailsPageVM landscapeEventDetailsPageVM = null;

        public LandscapeEventDetailsPage ()
		{
			InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public LandscapeEventDetailsPage(EventModel i_EventModel):this()
        {
            CrossDeviceOrientation.Current.UnlockOrientation();
            CrossDeviceOrientation.Current.OrientationChanged += handleOrientationChanged;
            landscapeEventDetailsPageVM = new LandscapeEventDetailsPageVM(i_EventModel);
            BindingContext = landscapeEventDetailsPageVM;
        }

        private void handleOrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            if (e.Orientation == DeviceOrientations.Portrait)
            {
                quitClipLoading();
                RaiseContentViewUpdateEvent?.Invoke(this, new UpdatedContentEventArgs(UpdatedContentEventArgs.EContentUpdateType.PopAsync));
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

        public void Refocus()
        {
            restartClipLoading();
        }
    }
}
using AgentVI.Models;
using AgentVI.ViewModels;
using System;
using Xamarin.Forms;
using AgentVI.Interfaces;
using Plugin.DeviceOrientation;
using Plugin.DeviceOrientation.Abstractions;
using EAppTab = AgentVI.ViewModels.MainPageViewModel.EAppTab;
using EContentUpdateType = AgentVI.Utils.UpdatedContentEventArgs.EContentUpdateType;
using AgentVI.Utils;
using FFImageLoading.Svg.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgentVI.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPageViewModel mainPageVM { get; private set; } = null;
        private Dictionary<EAppTab, SvgCachedImage> tabsCollection;

        private MainPage()
        {
            InitializeComponent();
            CrossDeviceOrientation.Current.LockOrientation(DeviceOrientations.Portrait);
            NavigationPage.SetHasNavigationBar(this, false);
            tabsCollection = new Dictionary<EAppTab, SvgCachedImage>(){
                { EAppTab.EventsPage, FooterBarEventsIcon },
                { EAppTab.SensorsPage, FooterBarCamerasIcon},
                { EAppTab.HealthPage, FooterBarHealthsIcon},
                { EAppTab.SettingsPage, FooterBarSettingsIcon }
            };
        }

        public MainPage(IProgress<ProgressReportModel> i_ProgressReporter) : this()
        {
            mainPageVM = new MainPageViewModel(i_ProgressReporter, tabsCollection);
            mainPageVM.RaiseContentViewUpdateEvent += OnMainNavigationPushPopRequest;
            FooterBarEvents_Clicked(null, null);
            bindPageControllers();
        }

        private void OnMainNavigationPushPopRequest(object sender, UpdatedContentEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ArgumentException exceptionIns = new ArgumentException("MainPage.OnMainNavigationPushPopRequest was used incorrectly");

                if (e == null || e.ContentUpdateType == EContentUpdateType.None)
                    throw exceptionIns;
                switch (e.ContentUpdateType)
                {
                    case EContentUpdateType.PushAsync:
                        Navigation.PushAsync(e.UpdatedContent);
                        break;
                    case EContentUpdateType.PopAsync:
                        Navigation.PopAsync();
                        break;
                    default:
                        PlaceHolder.Content = e.UpdatedContent.Content;
                        break;
                }
            });
        }

        private void bindPageControllers()
        {
            BindingContext = mainPageVM;
            FilterStateIndicatorListView.BindingContext = mainPageVM.FilterIndicator;
        }

        void FilterStateIndicator_Tapped(object i_Sender, EventArgs i_EventArgs)
        {
            try
            {
                Navigation.PushModalAsync(mainPageVM.FilterPage);
            }catch(InvalidOperationException ex)
            {
                Console.WriteLine("Tapped twice the button before it was opened. No action needed");
            }
        }

        void FooterBarEvents_Clicked(object i_Sender, EventArgs i_EventArgs)
        {
            Task.Factory.StartNew(() =>
            mainPageVM.updateContentView(EAppTab.EventsPage, mainPageVM.PagesCollection[EAppTab.EventsPage].Item1));
        }

        void FooterBarCameras_Clicked(object i_Sender, EventArgs i_EventArgs)
        {
            Task.Factory.StartNew(() => 
            mainPageVM.updateContentView(EAppTab.SensorsPage, mainPageVM.PagesCollection[EAppTab.SensorsPage].Item1));
        }

        void FooterBarHealth_Clicked(object i_Sender, EventArgs i_EventArgs)
        {
            Task.Factory.StartNew(() =>
            mainPageVM.updateContentView(EAppTab.HealthPage, mainPageVM.PagesCollection[EAppTab.HealthPage].Item1));
        }

        void FooterBarSettings_Clicked(object i_Sender, EventArgs i_EventArgs)
        {
            Task.Factory.StartNew(() =>
            mainPageVM.updateContentView(EAppTab.SettingsPage, mainPageVM.PagesCollection[EAppTab.SettingsPage].Item1));
        }

        protected override bool OnBackButtonPressed()
        {
            DependencyService.Get<IBackButtonPressed>().NativeOnBackButtonPressed();
            return true;
        }

    }
}
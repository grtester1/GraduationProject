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
                { EAppTab.SettingsPage, FooterBarSettingsIcon },
                {EAppTab.HealthPage, FooterBarHealthIcon }
            };
        }

        public MainPage(IProgress<ProgressReportModel> i_ProgressReporter) : this()
        {
            mainPageVM = new MainPageViewModel(i_ProgressReporter, tabsCollection);
            mainPageVM.RaiseContentViewUpdateEvent += OnMainNavigationPushPopRequest;
            FooterBarEvents_Clicked(null, null);
            BindingContext = mainPageVM;
        }

        protected override void OnAppearing()
        {
            mainPageVM.FiltrationPath = null;
            base.OnAppearing();
        }

        private void OnMainNavigationPushPopRequest(object sender, UpdatedContentEventArgs e)
        {
            Console.WriteLine("###Logger###   -   in MainPage.OnMainNavigationPushPopRequest main thread @ before execution on UI thread");
            Device.BeginInvokeOnMainThread(() =>
            {
                Console.WriteLine("###Logger###   -   in MainPage.OnMainNavigationPushPopRequest UI thread @ begin execution on UI thread");
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
                        Console.WriteLine("###Logger###   -   in MainPage.OnMainNavigationPushPopRequest UI thread @ begin setting ContentView");
                        PlaceHolder.Content = e.UpdatedContent.Content;
                        Console.WriteLine("###Logger###   -   in MainPage.OnMainNavigationPushPopRequest UI thread @ end setting ContentView");
                        if (e.UpdatedVM != null)
                        {
                            Console.WriteLine("###Logger###   -   in MainPage.OnMainNavigationPushPopRequest UI thread @ begin setting Binding");
                            BindingContext = e.UpdatedVM;
                            Console.WriteLine("###Logger###   -   in MainPage.OnMainNavigationPushPopRequest UI thread @ end setting Binding");
                        }
                        break;
                }
                Console.WriteLine("###Logger###   -   in MainPage.OnMainNavigationPushPopRequest main thread @ end execution on UI thread");
            });
            Console.WriteLine("###Logger###   -   in MainPage.OnMainNavigationPushPopRequest main thread @ after execution on UI thread");
        }

        private async void OnFilterStateIndicatorClicked(object i_Sender, EventArgs i_EventArgs)
        {
            try
            {
                BindingContext = mainPageVM;
                await Navigation.PushModalAsync(mainPageVM.FilterPage);
            }catch(InvalidOperationException ex)
            {
                Console.WriteLine("Tapped twice the button before it was opened. No action needed");
            }
        }

        private void FooterBarEvents_Clicked(object i_Sender, EventArgs i_EventArgs)
        {
            Task.Factory.StartNew(() =>
            mainPageVM.updateContentView(
                EAppTab.EventsPage,
                mainPageVM.PagesCollection[EAppTab.EventsPage].Item1.ContentPage,
                mainPageVM.PagesCollection[EAppTab.EventsPage].Item1.BindableViewModel
            )
            );
        }

        private void FooterBarCameras_Clicked(object i_Sender, EventArgs i_EventArgs)
        {
            Task.Factory.StartNew(() => 
            mainPageVM.updateContentView(
                EAppTab.SensorsPage,
                mainPageVM.PagesCollection[EAppTab.SensorsPage].Item1.ContentPage,
                mainPageVM.PagesCollection[EAppTab.SensorsPage].Item1.BindableViewModel
                )
                );
        }

        private void FooterBarHealth_Clicked(object i_Sender, EventArgs i_EventArgs)
        {
            Task.Factory.StartNew(() =>
            mainPageVM.updateContentView
            (
                EAppTab.HealthPage,
                mainPageVM.PagesCollection[EAppTab.HealthPage].Item1.ContentPage,
                mainPageVM.PagesCollection[EAppTab.HealthPage].Item1.BindableViewModel
                )
                );
        }

        private void FooterBarSettings_Clicked(object i_Sender, EventArgs i_EventArgs)
        {
            Task.Factory.StartNew(() =>
            mainPageVM.updateContentView
            (
                EAppTab.SettingsPage,
                mainPageVM.PagesCollection[EAppTab.SettingsPage].Item1.ContentPage,
                mainPageVM.PagesCollection[EAppTab.SettingsPage].Item1.BindableViewModel
                )
                );
        }

        protected override bool OnBackButtonPressed()
        {
            DependencyService.Get<IBackButtonPressed>().NativeOnBackButtonPressed();
            return true;
        }

        private void ResetHierarchyButtonClicked(object sender, EventArgs e)
        {
            mainPageVM.ResetHierarchyToRootLevel();
        }
    }
}
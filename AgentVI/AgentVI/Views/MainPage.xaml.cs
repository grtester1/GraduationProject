using AgentVI.Models;
using AgentVI.Services;
using AgentVI.Utils;
using AgentVI.ViewModels;
using FFImageLoading.Svg.Forms;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using AgentVI.Interfaces;
using Plugin.DeviceOrientation;
using Plugin.DeviceOrientation.Abstractions;
using EContentUpdateType = AgentVI.Utils.UpdatedContentEventArgs.EContentUpdateType;
using EAppTab = AgentVI.ViewModels.MainPageViewModel.EAppTab;
using ESelection = AgentVI.ViewModels.MainPageViewModel.ESelection;

namespace AgentVI.Views
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel mainPageVM { get; set; } = null;
        
        public MainPage()
        {
            InitializeComponent();
            CrossDeviceOrientation.Current.LockOrientation(DeviceOrientations.Portrait);
        }

        public MainPage(IProgress<ProgressReportModel> i_ProgressReporter):this()
        {
            mainPageVM = new MainPageViewModel(i_ProgressReporter);
            NavigationPage.SetHasNavigationBar(this, false);
            bindPageControllers();
            FooterBarEvents_Clicked(null, null);
        }

        private void bindPageControllers()
        {
            FilterStateIndicatorListView.BindingContext = mainPageVM.FilterIndicator;
            BindingContext = mainPageVM;
        }

        void FilterStateIndicator_Tapped(object i_Sender, EventArgs i_EventArgs)
        {
            Navigation.PushModalAsync(mainPageVM.FilterPage);
        }

        void FooterBarEvents_Clicked(object i_Sender, EventArgs i_EventArgs)
        {
            mainPageVM.currentPageInContentView = pageCollection[EAppTab.EventsPage].Item1;
            PlaceHolder.Content = currentPageInContentView.Content;
            
            markSelectedTab(EAppTab.EventsPage);
            resetContentViewStack();
        }

        void FooterBarCameras_Clicked(object i_Sender, EventArgs i_EventArgs)
        {
            currentPageInContentView = pageCollection[AppTab.SensorsPage].Item1;
            PlaceHolder.Content = currentPageInContentView.Content;
            markSelectedTab(EAppTab.SensorsPage);
            resetContentViewStack();
        }

        void FooterBarSettings_Clicked(object i_Sender, EventArgs i_EventArgs)
        {
            currentPageInContentView = pageCollection[AppTab.SettingsPage].Item1;
            PlaceHolder.Content = currentPageInContentView.Content;
            markSelectedTab(EAppTab.SettingsPage);
            resetContentViewStack();
        }

        protected override bool OnBackButtonPressed()
        {
            DependencyService.Get<IBackButtonPressed>().NativeOnBackButtonPressed();
            return true;
        }

        
    }
}
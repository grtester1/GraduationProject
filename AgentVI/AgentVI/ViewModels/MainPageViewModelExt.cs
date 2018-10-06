using AgentVI.Models;
using FFImageLoading.Svg.Forms;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using AgentVI.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AgentVI.ViewModels
{
    public partial class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        internal Dictionary<EAppTab, Tuple<ContentPage, SvgCachedImage>> PagesCollection { get; private set; }
        internal Page FilterPage { get; private set; }
        internal ContentPage currentPageInContentView { get; set; }
        private WaitingPage waitingPage { get; set; }

        private IProgress<ProgressReportModel> progressReporter;
        private Stack<ContentPage> contentViewStack;

        private readonly object contentViewUpdateLock = new object();
        private const short k_NumberOfInitializations = 8;
        internal enum EAppTab { EventsPage, SensorsPage, SettingsPage };
        internal enum ESelection { Active, Passive };

        internal LoginPageViewModel LoginContext { get; private set; }
        private SvgCachedImage _footerBarEventsIcon;
        internal SvgCachedImage FooterBarEventsIcon
        {
            get => _footerBarEventsIcon;
            private set
            {
                _footerBarEventsIcon = value;
                OnPropertyChanged(nameof(FooterBarEventsIcon));
            }
        }
        private SvgCachedImage _footerBarCamerasIcon;
        internal SvgCachedImage FooterBarCamerasIcon
        {
            get => _footerBarCamerasIcon;
            private set
            {
                _footerBarCamerasIcon = value;
                OnPropertyChanged(nameof(FooterBarCamerasIcon));
            }
        }
        private SvgCachedImage _footerBarHealthIcon;
        internal SvgCachedImage FooterBarHealthIcon
        {
            get => _footerBarHealthIcon;
            private set
            {
                _footerBarHealthIcon = value;
                OnPropertyChanged(nameof(FooterBarHealthIcon));
            }
        }
        private SvgCachedImage _footerBarSettingsIcon;
        internal SvgCachedImage FooterBarSettingsIcon
        {
            get => _footerBarSettingsIcon;
            private set
            {
                _footerBarSettingsIcon = value;
                OnPropertyChanged(nameof(FooterBarSettingsIcon));
            }
        }
        private FilterIndicatorViewModel _filterIndicator;
        internal FilterIndicatorViewModel FilterIndicator
        {
            get => _filterIndicator;
            private set
            {
                _filterIndicator = value;
                OnPropertyChanged(nameof(FilterIndicator));
            }
        }
        private View _contentView;
        internal View ContentView
        {
            get => _contentView;
            set
            {
                _contentView = value;
                OnPropertyChanged(nameof(ContentView));
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

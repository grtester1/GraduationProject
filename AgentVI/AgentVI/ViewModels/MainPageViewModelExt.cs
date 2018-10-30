using AgentVI.Models;
using FFImageLoading.Svg.Forms;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using AgentVI.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AgentVI.Utils;
using AgentVI.Services;
using AgentVI.Interfaces;

namespace AgentVI.ViewModels
{
    public partial class MainPageViewModel : IBindableVM ,INotifyPropertyChanged
    {
        public event EventHandler<UpdatedContentEventArgs> RaiseContentViewUpdateEvent;
        public event PropertyChangedEventHandler PropertyChanged;
        internal Dictionary<EAppTab, Tuple<IBindable, SvgCachedImage>> PagesCollection { get; private set; }
        internal Page FilterPage { get; private set; }
        internal ContentPage currentPageInContentView { get; set; }
        internal IBindableVM currentPageVMInContentView { get; set; }
        private WaitingPage waitingPage { get; set; }

        private IProgress<ProgressReportModel> progressReporter;
        private Stack<System.Tuple<ContentPage, IBindableVM>> contentViewStack;

        private readonly object contentViewUpdateLock = new object();
        private const short k_NumberOfInitializations = 8;
        internal enum EAppTab { EventsPage, SensorsPage, HealthPage, SettingsPage, None };
        internal enum ESelection { Active, Passive };


        public string AccountName
        {
            get
            {
                if (ServiceManager.Instance.FilterService != null)
                    return ServiceManager.Instance.FilterService.CurrentAccount.Name;
                else if(LoginContext != null)
                    return LoginContext.Username;
                else
                    return String.Empty;
            }
        }
        private LoginPageViewModel _loginContext;
        internal LoginPageViewModel LoginContext
        {
            get => _loginContext;
            set
            {
                _loginContext = value;
                OnPropertyChanged(nameof(LoginContext));
            }
        }
        private FilterIndicatorViewModel _filterIndicator;
        public FilterIndicatorViewModel FilterIndicator
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
            private set
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

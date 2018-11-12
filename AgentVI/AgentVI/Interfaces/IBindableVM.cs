using AgentVI.Utils;
using AgentVI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace AgentVI.Interfaces
{
    public abstract class IBindableVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<UpdatedContentEventArgs> EventsRouter;

        private string _filtrationPath;
        public string FiltrationPath
        {
            get => getCurrentFiltrationStringRep();
            set
            {
                _filtrationPath = getCurrentFiltrationStringRep();
                IsRootSelectedAndResetClickable = _filtrationPath != null
                                                    && _filtrationPath.CompareTo(string.Empty) != 0
                                                    && _filtrationPath.Length != 0;
                OnPropertyChanged(nameof(FiltrationPath));
            }
        }

        private bool _IsRootSelectedAndResetClickable;
        public bool IsRootSelectedAndResetClickable
        {
            get => _IsRootSelectedAndResetClickable;
            private set
            {
                _IsRootSelectedAndResetClickable = value;
                OnPropertyChanged(nameof(IsRootSelectedAndResetClickable));
            }
        }

        private bool _IsStillLoading = false;
        public virtual bool IsStillLoading
        {
            get => _IsStillLoading;
            set => _IsStillLoading = false;
        }

        private bool _IsEmptyView = false;
        public virtual bool IsEmptyView
        {
            get => false;
            set => _IsEmptyView = false;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected static void HandleExceptionVisibility(Exception ex)
        {
            Device.BeginInvokeOnMainThread(() =>
            App.Current.MainPage.DisplayAlert(Settings.ErrorTitleAlertText, ex.Message, Settings.ErrorButtonAlertText)
            );
        }

        protected void eventsRouter(object sender, UpdatedContentEventArgs e)
        {
            EventsRouter?.Invoke(this, e);
        }

        private string getCurrentFiltrationStringRep()
        {
            string res = null;
            if (Services.ServiceManager.Instance.LoginService.LoggedInUser != null &&
                Services.ServiceManager.Instance.FilterService != null)
            {
                res = Services.ServiceManager.Instance.FilterService.CurrentStringPath;
            }
            else
            {
                res = string.Empty;
            }

            return res;
        }
    }
}

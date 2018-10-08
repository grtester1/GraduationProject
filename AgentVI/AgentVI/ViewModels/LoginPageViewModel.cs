#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using AgentVI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using AgentVI.Services;

namespace AgentVI.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string AccessToken { get; private set; }
        public string UserEmail { get; private set; }
        public string Username { get; private set; }

        public LoginPageViewModel()
        {
            AccessToken = InnoviApiService.AccessToken;
            UserEmail = ServiceManager.Instance.LoginService.LoggedInUser.UserEmail;
            Username = ServiceManager.Instance.LoginService.LoggedInUser.Username;
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

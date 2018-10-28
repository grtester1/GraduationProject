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
using AgentVI.Utils;
using Xamarin.Forms;
using AgentVI.Services;
using AgentVI.Interfaces;

namespace AgentVI.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged, IBindableVM
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string AccessToken { get; private set; }
        public string UserEmail { get; private set; }
        public string Username { get; private set; }
        public bool _isBusyLoading;
        public bool IsBusyLoading
        {
            get => _isBusyLoading;
            set
            {
                _isBusyLoading = value;
                OnPropertyChanged(nameof(IsBusyLoading));
            }
        }
        private string _connectingUsername;
        public string ConnectingUsername
        {
            get => _connectingUsername;
            set
            {
                _connectingUsername = value;
                OnPropertyChanged(nameof(ConnectingUsername));
            }
        }
        private string _connectingPassword;
        public string ConnectingPassword
        {
            get => _connectingPassword;
            set
            {
                _connectingPassword = value;
                OnPropertyChanged(nameof(ConnectingPassword));
            }
        }
        private LoginResult loginResult { get; set; }

        public LoginPageViewModel()
        {
            IsBusyLoading = false;
        }

        private bool areCredentialsValid()
        {
            bool isUsernameEmpty = string.IsNullOrEmpty(ConnectingUsername) || string.IsNullOrWhiteSpace(ConnectingUsername);
            bool isPasswordEmpty = string.IsNullOrEmpty(ConnectingPassword) || string.IsNullOrWhiteSpace(ConnectingPassword);

            return !(isUsernameEmpty || isPasswordEmpty);
        }

        public void TryLogin()
        {
            if (!areCredentialsValid())
            {
                throw new ArgumentException("Please enter your username and password.");
            }
            else
            {
                loginResult = InnoviApiService.Login(ConnectingUsername, ConnectingPassword);
                if (loginResult.ErrorMessage != LoginResult.eErrorMessage.Empty)
                {
                    string errorMsg = loginResult.ErrorMessage.convertEnumToString();
                    loginResult = null;
                    throw new ArgumentException(errorMsg);
                }
                else
                {
                    ServiceManager.Instance.LoginService.InitServiceModule(loginResult.User);
                    initLocals();
                    ServiceManager.Instance.LoginService.SaveCredentials(AccessToken);
                }
            }
        }

        private void initLocals()
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

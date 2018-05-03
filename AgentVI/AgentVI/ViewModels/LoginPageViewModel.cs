using AgentVI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using InnoviApiProxy;
using AgentVI.Services;

namespace AgentVI.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string AccessToken { get; private set; }
        public string UserEmail { get; private set; }
        public string Username { get; private set; }

        public void InitializeFields(User i_loggedInUser)
        {
            if (i_loggedInUser != null)
            {
                ServiceManager.Instance.LoginService.setLoggedInUser(i_loggedInUser);
                AccessToken = InnoviApiProxy.Settings.AccessToken;
                UserEmail = i_loggedInUser.UserEmail;
                Username = i_loggedInUser.Username;
            }
            else
            {
                throw new Exception("Method InitializeFields/LoginPageVM was called with null param");
            }
        }


        public LoginPageViewModel()
        {

        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

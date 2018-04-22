using AgentVI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace AgentVI.ViewModels
{
    public class TemporaryShit : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static TemporaryShit Instance = null;

        public InnoviApiProxy.User LoggedInUser { get; set; }
        public string AccessToken { get; set; }
        public string UserEmail { get; set; }
        public List<InnoviApiProxy.Account> Accounts { get; set; }

        private string _Username;
        public string Username
        {
            get
            {
                return _Username;
            }
            set
            {
                _Username = value;
                OnPropertyChanged();
            }
        }

        public void InitializeFields()
        {
            if (LoggedInUser == null)
                throw new Exception("Shit");
            else
            {
                AccessToken = LoggedInUser.AccessToken;
                UserEmail = LoggedInUser.UserEmail;
                Accounts = LoggedInUser.Accounts;
            }
        }


        private TemporaryShit() { }

        public static TemporaryShit getInstance()
        {
            if(Instance == null)
            {
                Instance = new TemporaryShit();
            }
            return Instance;
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

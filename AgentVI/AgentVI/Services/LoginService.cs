using System;
using System.Collections.Generic;
using System.Text;
using InnoviApiProxy;
using System.Linq;
using Xamarin.Auth;

namespace AgentVI.Services
{
    public class LoginService : ILoginService
    {
        public User LoggedInUser { get; private set; }

        public void setLoggedInUser(User i_loggedInUser)
        {
            LoggedInUser = i_loggedInUser;
        }

        /*public string UserName
        {
            get
            {
                var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
                return (account != null) ? account.Username : null;
            }
        }*/

        public string AccessToken
        {
            get
            {
                var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
                return (account != null) ? account.Username : null;
            }
        }

        public void SaveCredentials(string accessToken)
        {
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                Xamarin.Auth.Account account = new Xamarin.Auth.Account
                {
                    Username = accessToken
                };
                AccountStore.Create().Save(account, App.AppName);
            }

        }

        public void DeleteCredentials()
        {
            var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
            if (account != null)
            {
                AccountStore.Create().Delete(account, App.AppName);
            }
        }

        public bool DoCredentialsExist()
        {
            return AccountStore.Create().FindAccountsForService(App.AppName).Any() ? true : false;
        }
    }
}
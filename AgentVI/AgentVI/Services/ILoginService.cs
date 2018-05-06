using InnoviApiProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgentVI.Services
{
    public interface ILoginService
    {
        User LoggedInUser { get; }

        void setLoggedInUser(User i_loggedInUser);

        string AccessToken { get; }

        void SaveCredentials(string accessToken);

        void DeleteCredentials();

        bool DoCredentialsExist();
    }
}

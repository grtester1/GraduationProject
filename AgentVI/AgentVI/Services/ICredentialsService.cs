using System;

using Xamarin.Forms;

namespace AgentVI.Services
{
    public interface ICredentialsService
    {
        string UserName { get; }

        string Password { get; }

        //string AccessToken { get; }

        void SaveCredentials (string userName, string password);

        //void SaveCredentials(string accessToken);

        void DeleteCredentials ();

        bool DoCredentialsExist ();
    }
}


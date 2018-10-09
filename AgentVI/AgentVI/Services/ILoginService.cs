#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Collections.Generic;
using System.Text;

namespace AgentVI.Services
{
    public interface ILoginService : IServiceModule
    {
        User                    LoggedInUser { get; }
        bool                    ArmCamersSettings { get; set; }
        bool                    PushNotificationsSettings { get; set; }
        string                  AccessToken { get; }
        void                    SaveCredentials(string accessToken);
        void                    DeleteCredentials();
        bool                    DoCredentialsExist();
    }
}

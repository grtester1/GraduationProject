#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using AgentVI.Utils;

namespace AgentVI.Services
{
    public partial class ServiceManager
    {
        private class LoginServiceS : ILoginService
        {
            public User LoggedInUser { get; private set; }

            public LoginServiceS() { }

            public bool InitServiceModule(User i_User = null)
            {
                bool res = false;

                if (i_User != null)
                {
                    LoggedInUser = i_User;
                    res = true;
                }

                return res;
            }

            private ISettings AppSettings
            {
                get
                {
                    return CrossSettings.Current;
                }
            }

            public bool ArmCamersSettings
            {
                get
                {
                    return AppSettings.GetValueOrDefault("ArmCamersSettings", false);
                }
                set
                {
                    AppSettings.AddOrUpdateValue("ArmCamersSettings", value);
                }
            }

            public bool PushNotificationsSettings
            {
                get
                {
                    return AppSettings.GetValueOrDefault("PushNotificationsSettings", false);
                }
                set
                {
                    AppSettings.AddOrUpdateValue("PushNotificationsSettings", value);
                }
            }

            public string AccessToken
            {
                get
                {
                    return AppSettings.GetValueOrDefault("AccessToken", "");
                }
                private set
                {
                    AppSettings.AddOrUpdateValue("AccessToken", value);
                }
            }

            public void SaveCredentials(string i_accessToken)
            {
                if (!string.IsNullOrWhiteSpace(i_accessToken))
                {
                    AccessToken = i_accessToken;
                }
            }

            public void DeleteCredentials()
            {
                AppSettings.Remove("AccessToken");
            }

            public bool DoCredentialsExist()
            {
                return string.IsNullOrWhiteSpace(AccessToken) ? false : true;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using InnoviApiProxy;

namespace AgentVI.Services
{
    public class LoginService
    {
        private static LoginService _Instance = null;
        public static LoginService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new LoginService();
                }
                return _Instance;
            }
        }
        public User LoggedInUser { get; private set; }


        public void setLoggedInUser(User i_loggedInUser)
        {
            LoggedInUser = i_loggedInUser;
        }


        private LoginService() { }
    }
}

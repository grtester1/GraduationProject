using System;
using System.Collections.Generic;
using System.Text;
using InnoviApiProxy;

namespace AgentVI.Services
{
    public class LoginService : ILoginService
    {
        public User LoggedInUser { get; private set; }

        public void setLoggedInUser(User i_loggedInUser)
        {
            LoggedInUser = i_loggedInUser;
        }


        public LoginService() { }
    }
}

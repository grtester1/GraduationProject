using System;
using System.Collections.Generic;
using System.Text;

namespace DummyProxy
{
    public static class InnoviApiService
    {
        public static string AccessToken { get; internal set; }

        public static LoginResult Login(string i_Email, string i_Password)
        {
            return User.Login(i_Email, i_Password);
        }

        public static LoginResult Connect(string i_AccessToken)
        {
            return User.Connect(i_AccessToken);
        }

        public static void Logout()
        {
            User user = User.Fetch();
            user.Logout();
        }
    }
}

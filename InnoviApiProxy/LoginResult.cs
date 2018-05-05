using System;
using System.Collections.Generic;
using System.Text;

namespace InnoviApiProxy
{
    public class LoginResult
    {
        public eErrorMessage ErrorMessage { get; internal set; }
        public User User { get; internal set; }

        internal LoginResult() { }

        public enum eErrorMessage
        {
            Empty,
            WrongCredentials,
            AccessTokenExpired,
            ServerError
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace InnoviApiProxy
{
    public class LoginResult
    {
        public eErrorMessage ErrorMessage { get;  set; }
        public User User { get;  set; }

        internal LoginResult() { }

        public enum eErrorMessage
        {
            Empty,
            WrongCredentials,
            ServerError
        }
    }


}

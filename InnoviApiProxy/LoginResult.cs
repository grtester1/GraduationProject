using System;
using System.Collections.Generic;
using System.Text;

namespace InnoviApiProxy
{
    public class LoginResult
    {
        public eErrorMessage ErrorMessage { get; set; }
        public User User { get; set; }

        internal LoginResult() { }

        public enum eErrorMessage
        {
            Empty,
            WrongCredentials,
            ServerError
        }


    }

    public static class LoginResultExtensionMethods
    {
        public static String toString(this LoginResult.eErrorMessage i_ErrorMessage)
        {
            switch(i_ErrorMessage)
            {
                case LoginResult.eErrorMessage.Empty:
                    return "Empty";
                case LoginResult.eErrorMessage.ServerError:
                    return "Server Error";
                case LoginResult.eErrorMessage.WrongCredentials:
                    return "Wrong Credentials";
            }

            return null;
        }
    }

}

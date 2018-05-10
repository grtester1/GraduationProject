using System;
using System.Collections.Generic;
using System.Net.Http;

namespace InnoviApiProxy
{
    public static class InnoviApiService
    {
        public static string AccessToken { get; internal set; }

        public static void SendVerificationEmail(string i_UserEmail)
        {

        }

        public static void ResetPassword(string i_VerificationCode)
        {

        }

        public static void ChangePassword(string i_OldPassword, string i_NewPassword)
        {

        }

        public static LoginResult Login(string i_Username, string i_Password)
        {
            LoginResult loginResult;

            try
            {
                loginResult = User.Login(i_Username, i_Password);
            }
            catch (InvalidOperationException)
            {
                loginResult = new LoginResult();
                loginResult.ErrorMessage = LoginResult.eErrorMessage.LoggedInUserAlreadyExists;
            }

            return loginResult;
        }

        public static LoginResult Connect(string i_AccessToken)
        {
            LoginResult loginResult;

            try
            {
                loginResult = User.Connect(i_AccessToken);
            }
            catch (InvalidOperationException)
            {
                loginResult = new LoginResult();
                loginResult.ErrorMessage = LoginResult.eErrorMessage.LoggedInUserAlreadyExists;
            }

            return loginResult;
        }

        public static void Logout()
        {
            User user = User.Fetch();

            if (user != null)
            {
                user.Logout();
            }
            else
            {
                throw new InvalidOperationException("Not logged in");
            }
        }

        internal static void RefreshAccessToken(HttpResponseMessage i_ResponseMessage)
        {
            IEnumerable<string> values;
            if (i_ResponseMessage.Headers.TryGetValues("x-access-token", out values))
            {
                IEnumerator<string> enumerator = values.GetEnumerator();
                enumerator.MoveNext();
                AccessToken = enumerator.Current;
            }
            else
            {
                throw new HttpRequestException("Failed to fetch access token from server");
            }
        }
    }
}

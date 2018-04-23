using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace InnoviApiProxy
{
    // Consider making a singleton
    public class User
    {
        public string Username { get; set; }
        public string UserEmail { get; set; }
        public string AccessToken { get; set; }
        public List<Account> Accounts { get; set; }
        //  public InnoviObjectCollection<Account> Accounts { get; set; }

        internal User() { }

        public static LoginResult Login(string i_Email, string i_Password)
        {
            HttpClient client = HttpUtils.BaseHttpClient();

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "api/user/login");
            Dictionary<string, string> jsonBuilder = new Dictionary<string, string>();
            jsonBuilder.Add("email", i_Email);
            jsonBuilder.Add("password", i_Password);
            string requestBody = JsonConvert.SerializeObject(jsonBuilder);
            httpRequest.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            Task<HttpResponseMessage> result = client.SendAsync(httpRequest);
            HttpResponseMessage response = result.Result;
            JObject responseJsonObject = HttpUtils.GetHttpResponseBody(response);

            LoginResult loginResult = new LoginResult();
            string error = responseJsonObject["error"].ToString();

            if (error == String.Empty)
            {
                User user = JsonConvert.DeserializeObject<User>(responseJsonObject["entity"].ToString());
                loginResult.User = user;
                loginResult.ErrorMessage = LoginResult.eErrorMessage.Empty;
                Settings.AccessToken = user.AccessToken;
            }
            else
            {
                // change according to error string - find out what strings are possible
                loginResult.ErrorMessage = LoginResult.eErrorMessage.WrongCredentials;
            }

           
            return loginResult;
        }

        public void Logout()
        {
            throw new Exception("Not yet implemented");
        }

        public List<Folder> GetDefaultAccountFolders()
        {
            return HttpUtils.GetFolders(0);
        }
    }
}
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
    public class User
    {
        private static User m_Instance;
        [JsonProperty]
        public string Username { get; private set; }
        [JsonProperty]
        public string UserEmail { get; private set; }
        [JsonProperty]
        public string AccessToken { get; private set; }
        [JsonProperty]
        public List<Account> Accounts { get; private set; }
        //  public InnoviObjectCollection<Account> Accounts { get; set; }

        private User() { }

        public static LoginResult Login(string i_Email, string i_Password)
        {
            if (m_Instance != null)
            {
                throw new Exception("There is already a logged in user. At first you should try to connect w/ AccessToken!");
            }

            HttpClient client = HttpUtils.BaseHttpClient();
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "api/user/login");

            Dictionary<string, string> jsonBuilder = new Dictionary<string, string>();
            jsonBuilder.Add("email", i_Email);
            jsonBuilder.Add("password", i_Password);
            string requestBody = JsonConvert.SerializeObject(jsonBuilder);
            httpRequest.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            return getLoginResult(client, httpRequest);
        }

        // response should include login data
        public static LoginResult Connect(string i_AccessToken)
        {
            throw new Exception("Not yet implemented");
            HttpClient client = HttpUtils.BaseHttpClient();
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "api/user/refresh-token");
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-ACCESS-TOKEN", i_AccessToken);

            Dictionary<string, string> jsonBuilder = new Dictionary<string, string>();
            jsonBuilder.Add("accountId", "0");
            string requestBody = JsonConvert.SerializeObject(jsonBuilder);
            httpRequest.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            return getLoginResult(client, httpRequest);
        }

        public void Logout()
        {
            throw new Exception("Not yet implemented");
        }

        public List<Folder> GetDefaultAccountFolders()
        {
            return HttpUtils.GetFolders(0);
        }

        private static LoginResult getLoginResult(HttpClient i_Client, HttpRequestMessage i_HttpRequestMessage)
        {
            Task<HttpResponseMessage> result = i_Client.SendAsync(i_HttpRequestMessage);
            HttpResponseMessage response = result.Result;
            JObject responseJsonObject = HttpUtils.GetHttpResponseBody(response);

            LoginResult loginResult = new LoginResult();
            string error = responseJsonObject["error"].ToString();

            if (error == String.Empty)
            {
                m_Instance = JsonConvert.DeserializeObject<User>(responseJsonObject["entity"].ToString());
                loginResult.User = m_Instance;
                loginResult.ErrorMessage = LoginResult.eErrorMessage.Empty;
                Settings.AccessToken = m_Instance.AccessToken;
            }
            else
            {
                if (i_HttpRequestMessage.RequestUri.ToString() == Settings.InnoviApiEndpoint + "api/user/refresh-token")
                {
                    loginResult.ErrorMessage = LoginResult.eErrorMessage.AccessTokenExpired;
                }
                else
                {
                    // change according to error string - find out what strings are possible
                    loginResult.ErrorMessage = LoginResult.eErrorMessage.WrongCredentials;
                }
            }

            return loginResult;
        }
    }
}
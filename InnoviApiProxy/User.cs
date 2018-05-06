using System;
using System.Collections.Generic;
using System.Net.Http;
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
        public List<Account> Accounts { get; private set; }

        private User() { }

        public static LoginResult Login(string i_Email, string i_Password)
        {
            if (m_Instance != null)
            {
                throw new Exception("There is already a logged in user");
            }

            HttpClient client = HttpUtils.BaseHttpClient();
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, Settings.ApiVersionEndpoint + "user/login");

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
            HttpClient client = HttpUtils.BaseHttpClient();
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, Settings.ApiVersionEndpoint +"user/refresh-token");
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-ACCESS-TOKEN", i_AccessToken);

            Dictionary<string, string> jsonBuilder = new Dictionary<string, string>();
            jsonBuilder.Add("accountId", "0");
            string requestBody = JsonConvert.SerializeObject(jsonBuilder);
            httpRequest.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            return getLoginResult(client, httpRequest);
        }

        public void Logout()
        {
            Settings.AccessToken = null;
            m_Instance = null;
        }

        public InnoviObjectCollection<Folder> GetDefaultAccountFolders()
        {
            return new InnoviObjectCollection<Folder>(HttpUtils.GetFolders, 0);
        }


        public InnoviObjectCollection<SensorEvent> GetDefaultAccountEvents()
        {
            return new InnoviObjectCollection<SensorEvent>(HttpUtils.GetFolderEvents, 0);
        }

        public InnoviObjectCollection<Sensor> GetDefaultAccountSensors()
        {
            return new InnoviObjectCollection<Sensor>(HttpUtils.GetFolderSensors, -1);
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
                //       Settings.AccessToken = m_Instance.AccessToken;
                Settings.AccessToken = responseJsonObject["entity"]["accessToken"].ToString();
            }
            else
            {
                if (i_HttpRequestMessage.RequestUri.ToString() == Settings.InnoviApiEndpoint + Settings.ApiVersionEndpoint +
                    "user/refresh-token")
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
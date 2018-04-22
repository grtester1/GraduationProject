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

        // gets http response
        // currently uses a dummy API due to ssl certificate issue in Innovi API
        private static HttpResponseMessage getLoginResponse(string i_Email, string i_Password)
        {
            HttpClient myClient = new HttpClient();
            myClient.BaseAddress = new Uri(Settings.InnoviApiEndpoint);
            myClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            myClient.DefaultRequestHeaders.TryAddWithoutValidation("X-API-KEY", Settings.ApiKey);
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "api/user/login");
            //     httpRequest.Headers.Add("X-API-KEY", Settings.ApiKey);

            Dictionary<string, string> jsonBuilder = new Dictionary<string, string>();
            jsonBuilder.Add("email", i_Email);
            jsonBuilder.Add("password", i_Password);
            HttpContent content = new FormUrlEncodedContent(jsonBuilder);

            string requestBody = JsonConvert.SerializeObject(jsonBuilder);
            httpRequest.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            Task<HttpResponseMessage> result = myClient.SendAsync(httpRequest);
            return result.Result;
        }

        public static LoginResult Login(string i_Email, string i_Password)
        {
            HttpResponseMessage response = getLoginResponse(i_Email, i_Password);
            Task<string> responseAsString = response.Content.ReadAsStringAsync();
            string str = responseAsString.Result;
            JObject responseJsonObject = JObject.Parse(str);

            LoginResult loginResult = new LoginResult();
            string error = responseJsonObject["error"].ToString();

            if (error == String.Empty)
            {
                User testUser = JsonConvert.DeserializeObject<User>(responseJsonObject["entity"].ToString());
                loginResult.User = testUser;
                loginResult.ErrorMessage = LoginResult.eErrorMessage.Empty;
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

        private static User createDummyUser()
        {
            User dummyUser = new User();

            Sensor dummySensor1 = new Sensor();
            dummySensor1.Name = "Dummy Camera 1";
            dummySensor1.SensorStatus = Sensor.eSensorStatus.Active;
            dummySensor1.SensorType = Sensor.eSensorType.Ccd;
            dummySensor1.IsEnabledByUser = true;
            dummySensor1.IsRecording = true;
            Dictionary<string, Sensor> dummySensors = new Dictionary<string, Sensor>();
            dummySensors.Add(dummySensor1.Name, dummySensor1);

            Sensor dummySensor2 = new Sensor();
            dummySensor2.Name = "Dummy Camera 2";
            dummySensor2.SensorStatus = Sensor.eSensorStatus.Inactive;
            dummySensor2.SensorType = Sensor.eSensorType.Thermal;
            dummySensors.Add(dummySensor2.Name, dummySensor2);


            SiteFolder dummySiteFolder = new SiteFolder();
            dummySiteFolder.Name = "Dummy Site";
            dummySiteFolder.Sensors = new InnoviObjectCollection<Sensor>(dummySensors);
            Dictionary<string, SiteFolder> dummySiteFolders = new Dictionary<string, SiteFolder>();
            dummySiteFolders.Add(dummySiteFolder.Name, dummySiteFolder);

            CustomerFolder dummyCustomerFolder = new CustomerFolder();
            dummyCustomerFolder.Name = "Dummy Customer";
            dummyCustomerFolder.SiteFolders = new InnoviObjectCollection<SiteFolder>(dummySiteFolders);
            Dictionary<string, CustomerFolder> dummyCustomerFolders = new Dictionary<string, CustomerFolder>();
            dummyCustomerFolders.Add(dummyCustomerFolder.Name, dummyCustomerFolder);

            Account dummyAccount = new Account();
            dummyAccount.Name = "Dummy Account";
            dummyAccount.Status = Account.eAccountStatus.Active;
            dummyAccount.CustomerFolders = new InnoviObjectCollection<CustomerFolder>(dummyCustomerFolders);
            Dictionary<string, Account> dummyAccounts = new Dictionary<string, Account>();
            dummyAccounts.Add(dummyAccount.Name, dummyAccount);

            dummyUser.Username = "Ami";
            //   dummyUser.Accounts = new InnoviObjectCollection<Account>(dummyAccounts);

            return dummyUser;
        }
    }
}
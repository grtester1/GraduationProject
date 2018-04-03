using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace InnoviApiProxy
{
    // Consider making a singleton
    public class User
    {
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public InnoviObjectCollection<Account> Accounts { get; set; }

        internal User() { }
        
        // gets http response
        // currently uses a dummy API due to ssl certificate issue in Innovi API
        private static  HttpResponseMessage Test()
        {
            HttpClient myClient = new HttpClient();
            myClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
            myClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //     myClient.DefaultRequestHeaders.TryAddWithoutValidation("X-API-KEY", Settings.ApiKey);
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "users");
            //     httpRequest.Headers.Add("X-API-KEY", Settings.ApiKey);

            Dictionary<string, string> jsonBuilder = new Dictionary<string, string>();
            jsonBuilder.Add("email", "ramot.n@gmail.com");
            jsonBuilder.Add("password", "password");
            HttpContent content = new FormUrlEncodedContent(jsonBuilder);

            string requestBody = JsonConvert.SerializeObject(jsonBuilder);
            httpRequest.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
 
            Task<HttpResponseMessage> result = myClient.SendAsync(httpRequest);
            return result.Result;
        }
    
        public static LoginResult Login(string i_Email, string i_Password)
        {
            HttpResponseMessage response = Test();
            Task<string> responseAsString = response.Content.ReadAsStringAsync();
            string str = responseAsString.Result;
            
            LoginResult loginResult = new LoginResult();

            LoginResult.eErrorMessage errorMessage = LoginResult.eErrorMessage.Empty;
            loginResult.ErrorMessage = errorMessage;

            // change to fetch real data
            User user = createDummyUser();
            loginResult.User = user;

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
            dummyUser.Accounts = new InnoviObjectCollection<Account>(dummyAccounts);

            return dummyUser;
        }
    }
}
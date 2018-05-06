using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Linq;

namespace InnoviApiProxy
{
    internal static class HttpUtils
    {
        internal static HttpClient BaseHttpClient()
        {
            HttpClient myClient = new HttpClient();
            myClient.BaseAddress = new Uri(Settings.InnoviApiEndpoint);
            myClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            myClient.DefaultRequestHeaders.TryAddWithoutValidation("X-API-KEY", Settings.ApiKey);

            return myClient;
        }

        internal static JObject GetHttpResponseBody(HttpResponseMessage i_HttpResponseMessage)
        {
            Task<string> responseAsString = i_HttpResponseMessage.Content.ReadAsStringAsync();
            string str = responseAsString.Result;
            JObject responseJsonObject = null;

            try
            {
                responseJsonObject = JObject.Parse(str);
            }
            catch(JsonReaderException ex)
            {
                responseJsonObject = null;
            }

            return responseJsonObject;
        }

        private static void AddSensorNameToEvent(SensorEvent i_SensorEvent)
        {
            Cache sensorNameCache = Cache.Fetch();

            i_SensorEvent.SensorName = sensorNameCache.GetSensorName(i_SensorEvent.sensorId);
        }

        private static void AddSensorNamesToEventsList(List<SensorEvent> i_SensorEvents)
        {
            foreach (SensorEvent sensorEvent in i_SensorEvents)
            {
                AddSensorNameToEvent(sensorEvent);
            }
        }

        private static void verifyLoggedInStatus()
        {
            if (Settings.AccessToken == null)
            {
                throw new Exception("Not logged in");
            }
        }

        private static void verifyCodeZero(JObject i_HttpResponseBody)
        {
            if (i_HttpResponseBody["code"].ToString() != "0")
            {
                throw new Exception("Error during http request");
            }
        }

        internal static string GetSensorNameById(int i_SensorId)
        {
            verifyLoggedInStatus();

            HttpClient client = BaseHttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-ACCESS-TOKEN", Settings.AccessToken); 
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, Settings.ApiVersionEndpoint + 
                "sensors/" + i_SensorId.ToString());
            Task<HttpResponseMessage> result = client.SendAsync(httpRequest);
            HttpResponseMessage response = result.Result;
            JObject responseJsonObject = GetHttpResponseBody(response);

            verifyCodeZero(responseJsonObject);
            Settings.RefreshAccessToken(response);
            Sensor sensor = JsonConvert.DeserializeObject<Sensor>(responseJsonObject["entity"].ToString());

            string sensorName = "Unavailable";
            
            if(sensor != null)
            {
                sensorName = sensor.Name;
            }

            return sensorName;
        }

        internal static void UpdateSensorNamesCache(List<int> i_SensorIds)
        {
            verifyLoggedInStatus();

            if (i_SensorIds.Count < 1)
            {
                throw new Exception("List must contain at least one value");
            }

            HttpClient client = BaseHttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-ACCESS-TOKEN", Settings.AccessToken);
            string baseUri = Settings.ApiVersionEndpoint + "sensors/list?";
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, baseUri);
            StringBuilder requestUriBuilder = new StringBuilder();
            requestUriBuilder.Append(baseUri);
            int sensorCount = 0;

            foreach (int SensorId in i_SensorIds)
            {
                if (sensorCount > 0)
                {
                    requestUriBuilder.Append("&");
                }

                requestUriBuilder.Append("id=" + SensorId.ToString());
            }

            httpRequest.RequestUri = new Uri(requestUriBuilder.ToString());

            Task<HttpResponseMessage> result = client.SendAsync(httpRequest);
            HttpResponseMessage response = result.Result;
            JObject responseJsonObject = GetHttpResponseBody(response);

            verifyCodeZero(responseJsonObject);
            Settings.RefreshAccessToken(response);
            List<Sensor> sensors = JsonConvert.DeserializeObject<List<Sensor>>(responseJsonObject["list"].ToString());

            Cache cache = Cache.Fetch();

            List<string> sensorNames = new List<string>();

            foreach (Sensor sensor in sensors)
            {
                cache.AddToSensorCache(sensor.sensorId, sensor.Name);
            }
        }

        internal static bool SwitchAccount(int i_AccountId)
        {
            bool isSuccessful = false;

            verifyLoggedInStatus();
            HttpClient client = BaseHttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-ACCESS-TOKEN", Settings.AccessToken); 
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, Settings.ApiVersionEndpoint +"user/switch-account");

            Dictionary<string, string> jsonBuilder = new Dictionary<string, string>();
            jsonBuilder.Add("accountId", i_AccountId.ToString());
            string requestBody = JsonConvert.SerializeObject(jsonBuilder);
            httpRequest.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> result = client.SendAsync(httpRequest);
            HttpResponseMessage response = result.Result;
            JObject responseJsonObject = GetHttpResponseBody(response);

            verifyCodeZero(responseJsonObject);
            Settings.RefreshAccessToken(response);
            isSuccessful = responseJsonObject["code"].ToString() == "0";

            return isSuccessful;
        }

        internal static List<SensorEvent> GetFolderEvents(int i_FolderId, int i_PageId, out int i_PagesCount)
        {
            verifyLoggedInStatus();
            HttpClient client = BaseHttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-ACCESS-TOKEN", Settings.AccessToken); 

            return getFolderEventsHepler(client, 0,  i_PageId, out i_PagesCount);
        }

        private static List<SensorEvent> getFolderEventsHepler(HttpClient i_Client, int i_FolderId, int i_PageId, out int i_PagesCount)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, Settings.ApiVersionEndpoint + 
                "events?page=" + i_PageId.ToString());
            Task<HttpResponseMessage> result = i_Client.SendAsync(httpRequest);
            HttpResponseMessage response = result.Result;
            JObject responseJsonObject = GetHttpResponseBody(response);

            i_PagesCount = int.Parse(responseJsonObject["pages"].ToString());

            List<SensorEvent> events = JsonConvert.DeserializeObject<List<SensorEvent>>(responseJsonObject["list"].ToString());

            verifyCodeZero(responseJsonObject);
            Settings.RefreshAccessToken(response);

            List<SensorEvent> sortedEvents = events.OrderByDescending(x => x.StartTime).ToList();
            sortedEvents.Reverse();

            if (sortedEvents.Count == 0)
            {
                sortedEvents = null;
            }
            else
            {
                AddSensorNamesToEventsList(sortedEvents);
            }

            return sortedEvents;
        }

        internal static List<Sensor> GetFolderSensors(int i_FolderId, int i_PageId, out int i_PagesCount)
        {
            verifyLoggedInStatus();
            HttpClient client = BaseHttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-ACCESS-TOKEN", Settings.AccessToken); 

            return getFolderSensorsHelper(client, i_FolderId, i_PageId, out i_PagesCount);
        }

        private static List<Sensor> getFolderSensorsHelper(HttpClient i_Client, int i_FolderId, int i_PageId, out int i_PagesCount)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, Settings.ApiVersionEndpoint + 
                "sensors?page=" + i_PageId.ToString() + "&folder=" + i_FolderId.ToString());

            Task<HttpResponseMessage> result = i_Client.SendAsync(httpRequest);
            HttpResponseMessage response = result.Result;
            JObject responseJsonObject = GetHttpResponseBody(response);

            i_PagesCount = int.Parse(responseJsonObject["pages"].ToString());

            List<Sensor> sensors = JsonConvert.DeserializeObject<List<Sensor>>(responseJsonObject["list"].ToString());

            verifyCodeZero(responseJsonObject);
            Settings.RefreshAccessToken(response);
            List<Sensor> SortedSensors = sensors.OrderByDescending(x => x.Name).ToList();
            SortedSensors.Reverse();

            if (sensors.Count == 0)
            {
                sensors = null;
            }

            return SortedSensors;
        }

        internal static List<Folder> GetFolders(int i_FolderId, int i_PageId, out int i_PagesCount)
        {
            verifyLoggedInStatus();
            HttpClient client = BaseHttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-ACCESS-TOKEN", Settings.AccessToken); 
          

            return GetFoldersHelper(client, i_FolderId, i_PageId, out i_PagesCount);
        }

        private static List<Folder> GetFoldersHelper(HttpClient i_Client, int i_FolderId, int i_PageId, out int i_PagesCount)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, Settings.ApiVersionEndpoint + 
                "folders/" + i_FolderId.ToString() + "/folders" + "?page=" + i_PageId.ToString());


            Task <HttpResponseMessage> result = i_Client.SendAsync(httpRequest);
            HttpResponseMessage response = result.Result;
            JObject responseJsonObject = GetHttpResponseBody(response);

            i_PagesCount = int.Parse(responseJsonObject["pages"].ToString());

            List<Folder> folders = JsonConvert.DeserializeObject<List<Folder>>(responseJsonObject["list"].ToString());

            verifyCodeZero(responseJsonObject);
            Settings.RefreshAccessToken(response);

            List<Folder> sortedFolders = folders.OrderByDescending(x => x.Name).ToList();
            sortedFolders.Reverse();

            if (folders.Count == 0)
            {
                folders = null;
            }

            return sortedFolders;
        }

        internal static List<SensorEvent> GetSensorEvents(int i_SensorId, int i_PageId, out int i_PagesCount)
        {
            verifyLoggedInStatus();
            HttpClient client = BaseHttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-ACCESS-TOKEN", Settings.AccessToken); 
          
            return GetSensorEventsHelper(client, i_SensorId, i_PageId, out i_PagesCount);
        }

        private static List<SensorEvent> GetSensorEventsHelper(HttpClient i_Client, int i_SensorId, int i_PageId, out int i_PagesCount)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, Settings.ApiVersionEndpoint + 
                "events?sensorId=" + i_SensorId.ToString() + "&page=" + i_PageId.ToString());


            Task<HttpResponseMessage> result = i_Client.SendAsync(httpRequest);
            HttpResponseMessage response = result.Result;
            JObject responseJsonObject = GetHttpResponseBody(response);

            i_PagesCount = int.Parse(responseJsonObject["pages"].ToString());
            Settings.RefreshAccessToken(response);
            List<SensorEvent> events = JsonConvert.DeserializeObject<List<SensorEvent>>(responseJsonObject["list"].ToString());

            verifyCodeZero(responseJsonObject);
            Settings.RefreshAccessToken(response);
            List<SensorEvent> sortedEvents = events.OrderByDescending(x => x.StartTime).ToList();
            sortedEvents.Reverse();

            if (events.Count == 0)
            {
                events = null;
            }
            else
            {
                AddSensorNamesToEventsList(sortedEvents);
            }

            return sortedEvents;
        }
    }
}

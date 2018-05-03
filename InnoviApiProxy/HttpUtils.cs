using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Linq;
using System.Text;

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
            JObject responseJsonObject = JObject.Parse(str);

            return responseJsonObject;
        }

        internal static List<Folder> GetFolders(int i_FolderId)
        {
            // change
            if (Settings.AccessToken == null)
            {
                throw new Exception("Not logged in");
            }

            HttpClient client = BaseHttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-ACCESS-TOKEN", Settings.AccessToken); // CHANGE THIS
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, "v1/folders/" + i_FolderId.ToString() + "/folders");
            Task<HttpResponseMessage> result = client.SendAsync(httpRequest);
            HttpResponseMessage response = result.Result;
            JObject responseJsonObject = GetHttpResponseBody(response);

            // if code == 0 => no errors
            Settings.RefreshAccessToken(response);
            List<Folder> folders = JsonConvert.DeserializeObject<List<Folder>>(responseJsonObject["list"].ToString());
            if (folders.Count == 0)
            {
                folders = null;
            }

            return folders;
        }

        internal static List<Sensor> GetSensors(int i_FolderId)
        {
            // change
            if (Settings.AccessToken == null)
            {
                throw new Exception("Not logged in");
            }

            HttpClient client = BaseHttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-ACCESS-TOKEN", Settings.AccessToken); // CHANGE THIS
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, "v1/sensors?folder=" + i_FolderId.ToString());
            Task<HttpResponseMessage> result = client.SendAsync(httpRequest);
            HttpResponseMessage response = result.Result;
            JObject responseJsonObject = GetHttpResponseBody(response);

            // if code == 0 => no errors
            Settings.RefreshAccessToken(response);
            List<Sensor> sensors = JsonConvert.DeserializeObject<List<Sensor>>(responseJsonObject["list"].ToString());
            if (sensors.Count == 0)
            {
                sensors = null;
            }

            return sensors;
        }

        internal static List<SensorEvent> GetEvents(int i_SensorId)
        {
            // change
            if (Settings.AccessToken == null)
            {
                throw new Exception("Not logged in");
            }

            HttpClient client = BaseHttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-ACCESS-TOKEN", Settings.AccessToken); // CHANGE THIS
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, "v1/events?sensorId=" + i_SensorId.ToString());
            Task<HttpResponseMessage> result = client.SendAsync(httpRequest);
            HttpResponseMessage response = result.Result;
            JObject responseJsonObject = GetHttpResponseBody(response);

            // if code == 0 => no errors
            Settings.RefreshAccessToken(response);
            List<SensorEvent> sensorEvents = JsonConvert.DeserializeObject<List<SensorEvent>>(responseJsonObject["list"].ToString());
            if (sensorEvents.Count == 0)
            {
                sensorEvents = null;
            }
            else
            {
                AddSensorNamesToEventsList(sensorEvents);
            }

            return sensorEvents;
        }

        internal static List<SensorEvent> GetFolderEvents(int i_FolderId)
        {
            // change
            if (Settings.AccessToken == null)
            {
                throw new Exception("Not logged in");
            }

            HttpClient client = BaseHttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-ACCESS-TOKEN", Settings.AccessToken); // CHANGE THIS
            

            return getFolderEventsHepler(client, 0);
        }

        private static List<SensorEvent> getFolderEventsHepler(HttpClient i_Client, int i_FolderId)
        {
         

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, "v1/events?page=1");
            Task<HttpResponseMessage> result = i_Client.SendAsync(httpRequest);
            HttpResponseMessage response = result.Result;
            JObject responseJsonObject = GetHttpResponseBody(response);

            int currentPage = int.Parse(responseJsonObject["page"].ToString());
            int totalPages = int.Parse(responseJsonObject["pages"].ToString());

            List<SensorEvent> events = JsonConvert.DeserializeObject<List<SensorEvent>>(responseJsonObject["list"].ToString());

            while (currentPage < totalPages)
            {
                currentPage++;

                var newHttpRequest = new HttpRequestMessage(HttpMethod.Get, "v1/events?page=" + currentPage.ToString());
                Task<HttpResponseMessage> newResult = i_Client.SendAsync(newHttpRequest);
                HttpResponseMessage newResponse = newResult.Result;
                JObject newResponseJsonObject = GetHttpResponseBody(newResponse);
                List<SensorEvent> currentEvents = JsonConvert.DeserializeObject<List<SensorEvent>>(newResponseJsonObject["list"].ToString());
                Settings.RefreshAccessToken(newResponse);
                events.AddRange(currentEvents);

            }
            // if code == 0 => no errors

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

        internal static List<Sensor> GetFolderSensors(int i_FolderId)
        {
            if (Settings.AccessToken == null)
            {
                throw new Exception("Not logged in");
            }

      //      List<Folder> subFolders = 
            HttpClient client = BaseHttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-ACCESS-TOKEN", Settings.AccessToken); // CHANGE THIS


            return getFolderSensorsHelper(client, i_FolderId);
        }

        private static List<Sensor> getFolderSensorsHelper(HttpClient i_Client, int i_FolderId)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, "v1/sensors?page=1&folder=" + i_FolderId.ToString());
            Task<HttpResponseMessage> result = i_Client.SendAsync(httpRequest);
            HttpResponseMessage response = result.Result;
            JObject responseJsonObject = GetHttpResponseBody(response);

            int currentPage = int.Parse(responseJsonObject["page"].ToString());
            int totalPages = int.Parse(responseJsonObject["pages"].ToString());

            List<Sensor> sensors = JsonConvert.DeserializeObject<List<Sensor>>(responseJsonObject["list"].ToString());

            while (currentPage < totalPages)
            {
                currentPage++;

                var newHttpRequest = new HttpRequestMessage(HttpMethod.Get, "v1/events?page=" + currentPage.ToString()
                        + "&folder=" + i_FolderId.ToString());
                Task<HttpResponseMessage> newResult = i_Client.SendAsync(newHttpRequest);
                HttpResponseMessage newResponse = newResult.Result;
                JObject newResponseJsonObject = GetHttpResponseBody(newResponse);
                List<Sensor> currentSensors = JsonConvert.DeserializeObject<List<Sensor>>(newResponseJsonObject["list"].ToString());
                Settings.RefreshAccessToken(newResponse);
                sensors.AddRange(currentSensors);

            }
            // if code == 0 => no errors

            List<Sensor> sortedEvents = sensors.OrderByDescending(x => x.Name).ToList();
            sortedEvents.Reverse();

            if (sensors.Count == 0)
            {
                sensors = null;
            }

            return sortedEvents;
        }

        internal static string GetSensorNameById(int i_SensorId)
        {
            if (Settings.AccessToken == null)
            {
                throw new Exception("Not logged in");
            }

            HttpClient client = BaseHttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-ACCESS-TOKEN", Settings.AccessToken); // CHANGE THIS
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, "v1/sensors/" + i_SensorId.ToString());
            Task<HttpResponseMessage> result = client.SendAsync(httpRequest);
            HttpResponseMessage response = result.Result;
            JObject responseJsonObject = GetHttpResponseBody(response);

            // if code == 0 => no errors
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
            if (Settings.AccessToken == null)
            {
                throw new Exception("Not logged in");
            }

            if (i_SensorIds.Count < 1)
            {
                throw new Exception("List must contain at least one value");
            }

            HttpClient client = BaseHttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-ACCESS-TOKEN", Settings.AccessToken); // CHANGE THIS
            string baseUri = "v1/sensors/list?";
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

            // if code == 0 => no errors
            Settings.RefreshAccessToken(response);
            List<Sensor> sensors = JsonConvert.DeserializeObject<List<Sensor>>(responseJsonObject["list"].ToString());

            Cache cache = Cache.Fetch();

            List<string> sensorNames = new List<string>();

            foreach (Sensor sensor in sensors)
            {
                cache.AddToSensorCache(sensor.sensorId, sensor.Name);
            }
        }
    }
}

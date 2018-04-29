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
            List<SensorEvent> sensors = JsonConvert.DeserializeObject<List<SensorEvent>>(responseJsonObject["list"].ToString());
            if (sensors.Count == 0)
            {
                sensors = null;
            }

            return sensors;
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


            if (events.Count == 0)
            {
                events = null;
            }

            return sortedEvents;
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

            if (sensors.Count == 0)
            {
                sensors = null;
            }

            return sortedEvents;
        }
    }
}

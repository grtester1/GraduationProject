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
    }
}

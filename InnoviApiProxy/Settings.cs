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
    // consider making a singleton
    public static class Settings
    {
        internal static string ApiKey { get; } = "eyJhbGciOiJIUzI1NiJ9.eyJqdGkiOiJtb2JpbGUtYXBwLXhtciIsImlzcyI6IjAiLCJpYXQiOjE1MTY4ODIzOTF9.S4AuQKaXOYzm_gTFUu52YAaFuLij4JSESNHvy4KuaoE";
        internal static string InnoviApiEndpoint { get; } = "https://api.innovi.io";
        public static string AccessToken { get; internal set; }

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
                throw new Exception("Failed to fetch access token");
            }
        }

    }
}
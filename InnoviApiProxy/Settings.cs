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
    internal static class Settings
    {
        internal static string ApiKey { get; } = "eyJhbGciOiJIUzI1NiJ9.eyJqdGkiOiJtb2JpbGUtYXBwLXhtciIsImlzcyI6IjAiLCJpYXQiOjE1MTY4ODIzOTF9.S4AuQKaXOYzm_gTFUu52YAaFuLij4JSESNHvy4KuaoE";
        internal static string InnoviApiEndpoint { get; } = "https://api.innovi.io/";
        internal static string ApiVersionEndpoint { get; } = "v1/";
    }
}
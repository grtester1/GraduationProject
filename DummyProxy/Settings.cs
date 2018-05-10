using System;
using System.Collections.Generic;
using System.Text;

namespace DummyProxy
{
    public class Settings
    {

        internal static string ApiKey { get; } = "eyJhbGciOiJIUzI1NiJ9.eyJqdGkiOiJtb2JpbGUtYXBwLXhtciIsImlzcyI6IjAiLCJpYXQiOjE1MTY4ODIzOTF9.S4AuQKaXOYzm_gTFUu52YAaFuLij4JSESNHvy4KuaoE";
        internal static string InnoviApiEndpoint { get; } = "https://api.innovi.io/";
        internal static string ApiVersionEndpoint { get; } = "v1/";
        public static string AccessToken { get; internal set; } 


    }
}

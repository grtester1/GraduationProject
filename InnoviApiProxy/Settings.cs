using System;
using System.Collections.Generic;
using System.Text;

namespace InnoviApiProxy
{
    // consider making a singleton
    public static class Settings
    {
        public static string ApiKey { get; } = "eyJhbGciOiJIUzI1NiJ9.eyJqdGkiOiJtb2JpbGUtYXBwLXhtciIsImlzcyI6IjAiLCJpYXQiOjE1MTY4ODIzOTF9.S4AuQKaXOYzm_gTFUu52YAaFuLij4JSESNHvy4KuaoE";
        public static string InnoviApiDomain { get; } = "https://api.innovi.agentvi.com";

    }
}

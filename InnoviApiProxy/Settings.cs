﻿using System;
using System.Collections.Generic;
using System.Text;

namespace InnoviApiProxy
{
    // consider making a singleton
    internal static class Settings
    {
        internal static string ApiKey { get; } = "eyJhbGciOiJIUzI1NiJ9.eyJqdGkiOiJtb2JpbGUtYXBwLXhtciIsImlzcyI6IjAiLCJpYXQiOjE1MTY4ODIzOTF9.S4AuQKaXOYzm_gTFUu52YAaFuLij4JSESNHvy4KuaoE";
        internal static string InnoviApiEndpoint { get; } = "https://api.innovi.io";
        internal static string AccessToken { get; set; } 

    }
}
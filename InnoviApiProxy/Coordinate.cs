using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace InnoviApiProxy
{
    public class Coordinate
    {
        [JsonProperty]
        public float Latitude { get; private set; }
        [JsonProperty]
        public float Longitude { get; private set; }
        [JsonProperty]
        public float Altitude { get; private set; }

        internal Coordinate() { }
    }
}

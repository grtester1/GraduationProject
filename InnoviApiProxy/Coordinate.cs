using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace InnoviApiProxy
{
    public class Coordinate
    {
        [JsonPropertyAttribute]
        public float Latitude { get; private set; }
        [JsonPropertyAttribute]
        public float Longitude { get; private set; }
        [JsonPropertyAttribute]
        public float Altitude { get; private set; }

        internal Coordinate() { }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace InnoviApiProxy
{
    public class SensorEvent : InnoviObject
    {
        [JsonPropertyAttribute]
        public int EventId { get; private set; }
        [JsonPropertyAttribute]
        public int SensorId { get; private set; }
        [JsonPropertyAttribute]
        public int AccountId { get; private set; }
        [JsonPropertyAttribute]
        public ulong StartTime { get; private set; }
        [JsonPropertyAttribute]
        public int ObjectType { get; private set; }
        [JsonPropertyAttribute]
        public int RuleId { get; private set; }
        [JsonPropertyAttribute]
        public string Name { get; private set; }
        [JsonPropertyAttribute]
        public string ImagePath { get; private set; }
        [JsonPropertyAttribute]
        public string ClipPath { get; private set; }
        [JsonPropertyAttribute]
        public Sensor.eSensorEventTag Tag { get; private set; }

        internal SensorEvent() { }
    }
}

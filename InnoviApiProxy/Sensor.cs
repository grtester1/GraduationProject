using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace InnoviApiProxy
{
    [JsonObjectAttribute]
    public class Sensor : InnoviObject
    {
        [JsonPropertyAttribute]
        public int Id { get; private set; }
        [JsonPropertyAttribute]
        public int AccountId { get; private set; }
        [JsonPropertyAttribute]
        public int FolderId { get; private set; }
        [JsonPropertyAttribute]
        public string Name { get; private set; }
        [JsonPropertyAttribute]
        public eSensorType SensorType { get; private set; }
        [JsonPropertyAttribute]
        public eSensorStatus Status { get; private set; }
        [JsonPropertyAttribute]
        public eStreamType StreamType { get; private set; }
        [JsonPropertyAttribute]
        public Coordinate GeoLocation { get; private set; }
        [JsonPropertyAttribute]
        public List<Coordinate> GeoArea { get; private set; }
        [JsonPropertyAttribute]
        public List<KeyValuePair<string, string>> Tags { get; private set; }
        [JsonPropertyAttribute]
        public string LoginKey { get; private set; }
        [JsonPropertyAttribute]
        public bool IsRecording { get; private set; }
        [JsonPropertyAttribute]
        public bool CreateEventImage { get; private set; }
        [JsonPropertyAttribute]
        public bool CreateEventClip { get; private set; }
        [JsonPropertyAttribute]
        public bool IsEnabledByUser { get; private set; }
        [JsonPropertyAttribute]
        public bool IsOptimizationEnabled { get; private set; }
        [JsonPropertyAttribute]
        public uint AlarmInterval { get; private set; }
        [JsonPropertyAttribute]
        public string StreamUrl { get; private set; }

        public List<SensorEvent> SensorEvents { get; set; }

        public void Arm()
        {
            throw new Exception("Not yet implemented");
        }

        public void Disarm()
        {
            throw new Exception("Not yet implemented");
        }

        public enum eSensorEventTag
        {
            None,
            False,
            True
        }

        public enum eSensorStatus
        {
            Undefined,
            Active,
            Warning,
            Error,
            Inactive
        }

        public enum eSensorType
        {
            Undefined,
            Ccd,
            Thermal
        }

        public enum eStreamType
        {
            Undefined,
            Rtsp,
            Onvif,
            Clip,
            Rtp,
            Multicast,
            Kinesis
        }

        internal Sensor() { }
    }
}

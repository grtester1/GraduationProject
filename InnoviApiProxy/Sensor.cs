using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace InnoviApiProxy
{
    public class Sensor : InnoviObject
    {
        [JsonProperty("id")]
        internal int sensorId { get; private set; }
        [JsonProperty]
        private int accountId { get; set; }
        [JsonProperty]
        private int folderId { get; set; }
        [JsonProperty]
        public string Name { get; private set; }
        [JsonProperty("type")]
        public eSensorType SensorType { get; private set; }
        [JsonProperty("status")]
        public eSensorStatus Status { get; private set; }
        [JsonProperty("streamType")]
        public eStreamType StreamType { get; private set; }
        [JsonProperty]
        public Coordinate GeoLocation { get; private set; }
        [JsonProperty]
        public List<Coordinate> GeoArea { get; private set; }
        [JsonProperty]
        public List<KeyValuePair<string, string>> Tags { get; private set; }
        [JsonProperty]
        public string LoginKey { get; private set; }
        [JsonProperty("recording")]
        public bool IsRecording { get; private set; }
        [JsonProperty("createEventImage")]
        public bool IsEventImageCreationRequired { get; private set; }
        [JsonProperty("createEventClip")]
        public bool IsEventClipCreationRequired { get; private set; }
        [JsonProperty("enabledByUser")]
        public bool IsEnabledByUser { get; private set; }
        [JsonProperty("optimizationEnabled")]
        public bool IsOptimizationEnabled { get; private set; }
        [JsonProperty]
        public uint AlarmInterval { get; private set; }
        [JsonProperty]
        public string StreamUrl { get; private set; }
        public string LiveViewStream { get; internal set; } = string.Empty;
        public int Health { get; internal set; } = 100;

        public InnoviObjectCollection<SensorEvent> SensorEvents
        {
            get
            {
                return new InnoviObjectCollection<SensorEvent>(HttpUtils.GetSensorEvents, sensorId);
            }
            private set
            {
                SensorEvents = value;
            }
        }

        public byte[] ReferenceImage
        {
            get
            {
                return HttpUtils.GetSensorReferenceImage(accountId, sensorId);
            }
        }

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

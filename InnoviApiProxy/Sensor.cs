using System;
using System.Collections.Generic;
using System.Text;

namespace InnoviApiProxy
{
    public class Sensor : InnoviObject
    {
        private int m_SensorId;
        private int m_AccountId;
        private int m_FolderId;
        public string Name { get; set; }
        public eAccountStatus Status { get; set; }
        public eSensorType SensorType { get; set; }
        public eSensorStatus SensorStatus { get; set; }
        public eStreamType StreamType { get; set; }
        public Coordinate GeoLocation { get; set; }
        public List<Coordinate> GeoArea { get; set; }
        public List<KeyValuePair<string, string>> Tags { get; set; }
        public string LoginKey { get; set; }
        public bool IsRecording { get; set; }
        public bool IsEventImageRequired { get; set; }
        public bool IsEventClipRequired { get; set; }
        public bool IsEnabledByUser { get; set; }
        public bool IsOptimizationEnabled { get; set; }
        public uint AlarmInterval { get; set; }
        public string StreamUrl { get; set; }

        public InnoviObjectCollection<SensorEvent> SensorEvents { get; set; }
    }
}

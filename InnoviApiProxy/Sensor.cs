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
        public Account.eAccountStatus Status { get; set; }
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
    }
}

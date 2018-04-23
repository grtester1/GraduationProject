using System;
using System.Collections.Generic;
using System.Text;

namespace InnoviApiProxy
{
    public class Sensor : InnoviObject
    {
        public int Id { get; set; } 
        public int AccountId { get; set; }
        public int FolderId { get; set; }
        public string Name { get; set; }
        public eSensorType SensorType { get; set; }
        public eSensorStatus Status { get; set; }
        public eStreamType StreamType { get; set; }
        public Coordinate GeoLocation { get; set; }
        public List<Coordinate> GeoArea { get; set; }
        public List<KeyValuePair<string, string>> Tags { get; set; }
        public string LoginKey { get; set; }
        public bool IsRecording { get; set; }
        public bool CreateEventImage { get; set; }
        public bool CreateEventClip { get; set; }
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

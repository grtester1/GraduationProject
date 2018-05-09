using System;
using System.Collections.Generic;
using System.Text;

namespace DummyProxy
{
    public class Sensor
    {

        internal int sensorId { get; private set; }

        private int accountId { get; set; }

        private int folderId { get; set; }

        public string Name { get; internal set; }

        public eSensorType SensorType { get; internal set; }

        public eSensorStatus Status { get; internal set; }

        public eStreamType StreamType { get; internal set; }

        public Coordinate GeoLocation { get; internal set; }

        public List<Coordinate> GeoArea { get; internal set; }

        public List<KeyValuePair<string, string>> Tags { get; internal set; }

        public string LoginKey { get; internal set; }

        public bool IsRecording { get; internal set; }

        public bool IsEventImageCreationRequired { get; internal set; }

        public bool IsEventClipCreationRequired { get; internal set; }

        public bool IsEnabledByUser { get; internal set; }

        public bool IsOptimizationEnabled { get; internal set; }

        public uint AlarmInterval { get; internal set; }

        public string StreamUrl { get; internal set; }

        public InnoviObjectCollection<SensorEvent> SensorEvents { get; internal set; }


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

        internal Sensor()
        {
            Name = "Dummy Camera";

            SensorType = Sensor.eSensorType.Ccd;

            Status = Sensor.eSensorStatus.Active;

            StreamType = Sensor.eStreamType.Rtsp;

            GeoLocation = null;

            GeoArea = null;

            Tags = null;

            LoginKey = "12345";

            IsRecording = true;

            IsEventImageCreationRequired = false;

            IsEventClipCreationRequired = false;

            IsEnabledByUser = true;

            IsOptimizationEnabled = true;

            AlarmInterval = 5;

            StreamUrl = "blabla";

        }
    }
}

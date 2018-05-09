using System;
using System.Collections.Generic;
using System.Text;

namespace DummyProxy
{
    public class SensorEvent
    {
        private string eventId { get; set; }
        internal int sensorId { get; private set; }
        private int accountId { get; set; }
        public string SensorName { get; internal set; }
        public ulong StartTime { get; private set; }
        public eObjectType ObjectType { get; private set; }
        public int RuleId { get; private set; }
        public string ImagePath { get; private set; }
        public string ClipPath { get; private set; }
        public Sensor.eSensorEventTag Tag { get; private set; }
        public eBehaviorType RuleName { get; private set; }

        internal SensorEvent()
        {
            eventId = "1";
            sensorId = 0;
        accountId = 0; 
        SensorName = "Dummy Camera";
            StartTime = 0;
            ObjectType = eObjectType.Motorcycle;
            RuleId = 0;
            ImagePath = "";
            ClipPath = "";
            Tag = Sensor.eSensorEventTag.None;
            RuleName = eBehaviorType.Crossing;

    }

        public enum eBehaviorType
        {
            Undefined,
            Moving,
            Crossing,
            Occupancy,
            Stopped,
            Mask,
            Vqm,
            Mrd,
            Grouping,
            Anomaly
        }

        public enum eObjectType
        {
            Undefined,
            Unknown,
            Person,
            Vehicle,
            Object,
            Bicycle,
            Motorcycle
        }
    }
}

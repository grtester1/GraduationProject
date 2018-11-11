using System;
using System.Threading.Tasks;
using InnoviApiProxy;

namespace AgentVI.Models
{
    public class EventModel
    {
        public string SensorName { get; private set; }
        public SensorEvent.eBehaviorType SensorEventRuleName { get; private set; }
        public ulong SensorEventDateTime { get; private set; }
        public string SensorEventImage { get; private set; }
        public string SensorEventClip { get; private set; }
        public SensorEvent.eObjectType SensorEventObjectType { get; private set; }
        public Sensor.eSensorEventTag SensorEventTag { get; private set; }
        private Sensor m_SensorHolder;
        private Lazy<Sensor> _SensorLazyHelper;
        private Lazy<Sensor> SensorLazyHelper
        {
            get => _SensorLazyHelper;
            set
            {
                _SensorLazyHelper = value;
                Task.Factory.StartNew(() => m_SensorHolder = _SensorLazyHelper.Value);
            }
        }
        public Sensor Sensor => m_SensorHolder == null ? SensorLazyHelper.Value : m_SensorHolder;
        public SensorEvent SensorEvent { get; private set; }

        internal static EventModel FactoryMethod(SensorEvent i_SensorEvent)
        {
            EventModel res = new EventModel()
            {
                SensorName = i_SensorEvent.SensorName,
                SensorEventClip = i_SensorEvent.ClipPath,
                SensorEventDateTime = i_SensorEvent.StartTime,
                SensorEventImage = i_SensorEvent.ImagePath,
                SensorEventRuleName = i_SensorEvent.RuleName,
                SensorEventObjectType = i_SensorEvent.ObjectType,
                SensorEventTag = i_SensorEvent.Tag,
                SensorLazyHelper = new Lazy<Sensor>(() => i_SensorEvent.EventSensor),
                SensorEvent = i_SensorEvent
            };
            return res;
        }
    }
}
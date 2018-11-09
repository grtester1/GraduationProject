using System;
using System.ComponentModel;
using InnoviApiProxy;
using Xamarin.Forms;
using AgentVI.Utils;

namespace AgentVI.Models
{
    public class EventModel
    {
        private string m_SensorName;
        public string SensorName
        {
            get { return m_SensorName; }
            private set
            {
                m_SensorName = value;
            }
        }

        private SensorEvent.eBehaviorType m_SensorEventRuleName;
        public SensorEvent.eBehaviorType SensorEventRuleName
        {
            get { return m_SensorEventRuleName; }
            private set
            {
                m_SensorEventRuleName = value;
            }
        }

        private ulong m_SensorEventDateTime;
        public ulong SensorEventDateTime
        {
            get { return m_SensorEventDateTime; }
            private set
            {
                m_SensorEventDateTime = value;
            }
        }

        private string m_SensorEventImage;
        public string SensorEventImage
        {
            get { return m_SensorEventImage; }
            private set
            {
                m_SensorEventImage = value;
            }
        }

        private string m_SensorEventClip;
        public string SensorEventClip
        {
            get { return m_SensorEventClip; }
            private set
            {
                m_SensorEventClip = value;
            }
        }

        private SensorEvent.eObjectType m_SensorEventObjectType;
        public SensorEvent.eObjectType SensorEventObjectType
        {
            get { return m_SensorEventObjectType; }
            private set
            {
                m_SensorEventObjectType = value;
            }
        }

        private Sensor.eSensorEventTag m_SensorEventTag;
        public Sensor.eSensorEventTag SensorEventTag
        {
            get { return m_SensorEventTag; }
            private set
            {
                m_SensorEventTag = value;
            }
        }


        private Lazy<Sensor> m_SensorLazyHelper;
        private Lazy<Sensor> SensorLazyHelper
        {
            get
            {
                return m_SensorLazyHelper;
            }
            set
            {
                m_SensorLazyHelper = value;
                Sensor = null;
            }
        }

        private Sensor m_Sensor;
        public Sensor Sensor
        {
            get { return m_SensorLazyHelper.Value; }
            private set
            {
                m_Sensor = value;
            }
        }


        private SensorEvent m_SensorEvent;
        public SensorEvent SensorEvent
        {
            get { return m_SensorEvent; }
            private set
            {
                m_SensorEvent = value;
            }
        }



        internal static EventModel FactoryMethod(SensorEvent i_SensorEvent)
        {
            Console.WriteLine("###Logger###   -   in EventModel.FactoryMethod main thread @ begin");
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
            Console.WriteLine("###Logger###   -   in EventModel.FactoryMethod main thread @ end");
            return res;
        }
    }
}
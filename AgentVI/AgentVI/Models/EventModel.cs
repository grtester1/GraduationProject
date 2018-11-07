using System;
using System.ComponentModel;
using InnoviApiProxy;
using Xamarin.Forms;
using AgentVI.Utils;

namespace AgentVI.Models
{
    public class EventModel : INotifyPropertyChanged
    {
        private string m_SensorName;
        public string SensorName
        {
            get { return m_SensorName; }
            private set
            {
                m_SensorName = value;
                OnPropertyChanged(nameof(SensorName));
            }
        }

        private SensorEvent.eBehaviorType m_SensorEventRuleName;
        public SensorEvent.eBehaviorType SensorEventRuleName
        {
            get { return m_SensorEventRuleName; }
            private set
            {
                m_SensorEventRuleName = value;
                OnPropertyChanged(nameof(SensorEventRuleName));
            }
        }

        private ulong m_SensorEventDateTime;
        public ulong SensorEventDateTime
        {
            get { return m_SensorEventDateTime; }
            private set
            {
                m_SensorEventDateTime = value;
                OnPropertyChanged(nameof(SensorEventDateTime));
            }
        }

        private string m_SensorEventImage;
        public string SensorEventImage
        {
            get { return m_SensorEventImage; }
            private set
            {
                m_SensorEventImage = value;
                OnPropertyChanged(nameof(SensorEventImage));
            }
        }

        private string m_SensorEventClip;
        public string SensorEventClip
        {
            get { return m_SensorEventClip; }
            private set
            {
                m_SensorEventClip = value;
                OnPropertyChanged(nameof(SensorEventClip));
            }
        }

        private SensorEvent.eObjectType m_SensorEventObjectType;
        public SensorEvent.eObjectType SensorEventObjectType
        {
            get { return m_SensorEventObjectType; }
            private set
            {
                m_SensorEventObjectType = value;
                OnPropertyChanged(nameof(SensorEventObjectType));
            }
        }

        private Sensor.eSensorEventTag m_SensorEventTag;
        public Sensor.eSensorEventTag SensorEventTag
        {
            get { return m_SensorEventTag; }
            private set
            {
                m_SensorEventTag = value;
                OnPropertyChanged(nameof(SensorEventTag));
            }
        }

        private Sensor m_Sensor;
        public Sensor Sensor
        {
            get { return m_Sensor; }
            private set
            {
                m_Sensor = value;
                OnPropertyChanged(nameof(Sensor));
            }
        }

        private SensorEvent m_SensorEvent;
        public SensorEvent SensorEvent
        {
            get { return m_SensorEvent; }
            private set
            {
                m_SensorEvent = value;
                OnPropertyChanged(nameof(SensorEvent));
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
                Sensor = i_SensorEvent.EventSensor,
                SensorEvent = i_SensorEvent
            };
            Console.WriteLine("###Logger###   -   in EventModel.FactoryMethod main thread @ end");
            return res;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
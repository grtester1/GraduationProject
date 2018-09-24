using System;
using System.ComponentModel;
using InnoviApiProxy;
using Xamarin.Forms;

namespace AgentVI.Models
{
    public class EventModel : INotifyPropertyChanged
    {
        private string m_SensorName;
        public string CamName
        {
            get { return m_SensorName; }
            private set
            {
                m_SensorName = value;
                OnPropertyChanged("SensorName");
            }
        }

        private SensorEvent.eBehaviorType m_RuleName;
        public SensorEvent.eBehaviorType RuleName
        {
            get { return m_RuleName; }
            private set
            {
                m_RuleName = value;
                OnPropertyChanged("RuleName");
            }
        }

        private ulong m_DateTime;
        public ulong DateTime
        {
            get { return m_DateTime; }
            private set
            {
                m_DateTime = value;
                OnPropertyChanged("DateTime");
            }
        }

        private string m_ImagePath;
        public string ImagePath
        {
            get { return m_ImagePath; }
            private set
            {
                m_ImagePath = value;
                OnPropertyChanged("SensorImage");
            }
        }

        private string m_ClipPath;
        public string ClipPath
        {
            get { return m_ClipPath; }
            private set
            {
                m_ClipPath = value;
                OnPropertyChanged("ClipPath");
            }
        }

        private Sensor.eSensorEventTag m_Tag;
        public Sensor.eSensorEventTag Tag
        {
            get { return m_Tag; }
            private set
            {
                m_Tag = value;
                OnPropertyChanged("SensorTag");
            }
        }

        private EventModel()
        {

        }

        internal static EventModel FactoryMethod(SensorEvent i_SensorEvent)
        {
            EventModel res = new EventModel()
            {
                CamName = i_SensorEvent.SensorName,
                ClipPath = i_SensorEvent.ClipPath,
                DateTime = i_SensorEvent.StartTime,
                ImagePath = i_SensorEvent.ImagePath,
                RuleName = i_SensorEvent.RuleName,
                Tag = i_SensorEvent.Tag
            };

            return res;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
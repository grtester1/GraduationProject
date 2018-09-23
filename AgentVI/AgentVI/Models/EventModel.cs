using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace AgentVI.Models
{
    public class EventModel : INotifyPropertyChanged
    {
        private string m_SensorName;
        public string CamName
        {
            get { return m_SensorName; }
            set
            {
                m_SensorName = value;
                OnPropertyChanged("SensorName");
            }
        }

        private string m_RuleName;
        public string RuleName
        {
            get { return m_RuleName; }
            set
            {
                m_RuleName = value;
                OnPropertyChanged("RuleName");
            }
        }

        private string m_DateTime;
        public string DateTime
        {
            get { return m_DateTime; }
            set
            {
                m_DateTime = value;
                OnPropertyChanged("DateTime");
            }
        }

        private string m_ImagePath;
        public string ImagePath
        {
            get { return m_ImagePath; }
            set
            {
                m_ImagePath = value;
                OnPropertyChanged("SensorImage");
            }
        }

        private string m_ClipPath;
        public string ClipPath
        {
            get { return m_ClipPath; }
            set
            {
                m_ClipPath = value;
                OnPropertyChanged("ClipPath");
            }
        }

        private string m_Tag;
        public string Tag
        {
            get { return m_Tag; }
            set
            {
                m_Tag = value;
                OnPropertyChanged("SensorTag");
            }
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
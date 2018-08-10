using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace AgentVI.Models
{
    public class EventModel : INotifyPropertyChanged
    {
        private string m_camName;
        public string CamName
        {
            get { return m_camName; }
            set
            {
                m_camName = value;
                OnPropertyChanged("CamName");
            }
        }

        private string m_ruleName;
        public string RuleName
        {
            get { return m_ruleName; }
            set
            {
                m_ruleName = value;
                OnPropertyChanged("RuleName");
            }
        }

        private string m_dateTime;
        public string DateTime
        {
            get { return m_dateTime; }
            set
            {
                m_dateTime = value;
                OnPropertyChanged("DateTime");
            }
        }

        private string m_camImage;
        public string CamImage
        {
            get { return m_camImage; }
            set
            {
                m_camImage = value;
                OnPropertyChanged("CamImage");
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
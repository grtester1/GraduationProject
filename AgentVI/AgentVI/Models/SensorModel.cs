#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace AgentVI.Models
{
    public class SensorModel : INotifyPropertyChanged
    {
        private SensorModel()
        {

        }

        internal static SensorModel FactoryMethod(Sensor i_Sensor)
        {
            SensorModel res = new SensorModel()
            {
                Sensor = i_Sensor,
                SensorName = i_Sensor.Name,
                SensorHealth = 5,//*****************************need change!!!
                SensorImage = ImageSource.FromStream(() => new System.IO.MemoryStream(i_Sensor.ReferenceImage))
            };

            return res.UpdateSensorsHealthStatus();
        }

        public enum ESensorColorHealth
        {
            Black,
            Grey,
            Red,
            Yellow,
            Green
        };
        public event PropertyChangedEventHandler PropertyChanged;

        private Sensor m_Sensor;
        public Sensor Sensor
        {
            get { return m_Sensor; }
            private set
            {
                m_Sensor = value;
                OnPropertyChanged("Sensor");
            }
        }

        private string m_SensorName;
        public string SensorName
        {
            get { return m_SensorName; }
            private set
            {
                m_SensorName = value;
                OnPropertyChanged("SensorName");
            }
        }

        private int m_SensorHealth;
        public int SensorHealth
        {
            get { return m_SensorHealth; }
            private set
            {
                m_SensorHealth = value;
                OnPropertyChanged("SensorHealth");
            }
        }

        private ESensorColorHealth m_SensorHealthStatus;
        public  ESensorColorHealth SensorHealthStatus
        {
            get { return m_SensorHealthStatus; }
            private set
            {
                m_SensorHealthStatus = value;
                OnPropertyChanged("SensorHealthStatus");
            }
        }

        private ImageSource m_SensorImage;
        public  ImageSource SensorImage
        {
            get { return m_SensorImage; }
            private set
            {
                m_SensorImage = value;
                OnPropertyChanged("SensorImage");
            }
        }

        private ESensorColorHealth GetEnumForValue(int i_value)
        {
            ESensorColorHealth res;

            if (i_value > 80)
            {
                res = ESensorColorHealth.Green;
            }
            else if (i_value > 60)
            {
                res = ESensorColorHealth.Yellow;
            }
            else if (i_value > 40)
            {
                res = ESensorColorHealth.Red;
            }
            else if (i_value > 20)
            {
                res = ESensorColorHealth.Grey;
            }
            else
            {
                res = ESensorColorHealth.Black;
            }

            return res;
        }

        private SensorModel UpdateSensorsHealthStatus()
        {
            SensorHealthStatus = GetEnumForValue(SensorHealth);
            return this;
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
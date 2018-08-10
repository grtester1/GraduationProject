
using DummyProxy;

//using InnoviApiProxy;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgentVI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CameraEventsPage : ContentPage
	{
        Sensor m_InPageSensor = null;

		public CameraEventsPage ()
		{
			InitializeComponent();
		}

        public CameraEventsPage(Sensor i_InputSensor):this()
        {
            m_InPageSensor = i_InputSensor;
            SensorNameLabel.Text = i_InputSensor.Name;
            SensorImage.Source = i_InputSensor.StreamUrl;
            SensorImageAddress.Text = "Image address: " + i_InputSensor.StreamUrl;
            SensorDateLabel.Text = i_InputSensor.StreamUrl;
            //SensorRuleNameLabel.Text = i_InputSensor.LiveViewStream;
        }
	}
}
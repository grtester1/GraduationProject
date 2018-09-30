using System;
using System.Collections.Generic;
using Xamarin.Forms;
using AgentVI.Models;

namespace AgentVI.Views
{
    public partial class HealthPage : ContentPage
    {
        private List<HealthModel> healthList;

        public HealthPage()
        {
            InitializeComponent();
            cameraNameHeader.Text = "no camera selected";




            healthList = new List<HealthModel>
            {
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "High Latency",  HealthDuration = "1:05" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "Resolution too low",  HealthDuration = "11:02" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "Resolution too low", HealthDuration = "27:31" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "High Latency", HealthDuration = "2:09" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "High Latency", HealthDuration = "4:00" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "Resolution too low", HealthDuration = "56:23" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "High Latency", HealthDuration = "78:59" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "High Latency",  HealthDuration = "1:05" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "Resolution too low",  HealthDuration = "11:02" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "Resolution too low", HealthDuration = "27:31" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "High Latency", HealthDuration = "2:09" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "High Latency", HealthDuration = "4:00" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "Resolution too low", HealthDuration = "56:23" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "High Latency", HealthDuration = "78:59" }
            };

            listView.ItemsSource = healthList;
        }

        public HealthPage(SensorModel sensor)
        {
            InitializeComponent();
            healthList = new List<HealthModel>();
            cameraNameHeader.Text = "Health for " + sensor.SensorName;
            Title = sensor.SensorName;

            healthList = new List<HealthModel>
            {
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "High Latency",  HealthDuration = "1:05" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "Resolution too low",  HealthDuration = "11:02" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "Resolution too low", HealthDuration = "27:31" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "High Latency", HealthDuration = "2:09" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "High Latency", HealthDuration = "4:00" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "Resolution too low", HealthDuration = "56:23" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "High Latency", HealthDuration = "78:59" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "High Latency",  HealthDuration = "1:05" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "Resolution too low",  HealthDuration = "11:02" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "Resolution too low", HealthDuration = "27:31" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "High Latency", HealthDuration = "2:09" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "High Latency", HealthDuration = "4:00" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "Resolution too low", HealthDuration = "56:23" },
                new HealthModel { HealthTime = new DateTime(1985, 11, 20 , 17, 23, 59), HealthDescription = "High Latency", HealthDuration = "78:59" }
            };
                
            listView.ItemsSource = healthList;
        }
    }
}
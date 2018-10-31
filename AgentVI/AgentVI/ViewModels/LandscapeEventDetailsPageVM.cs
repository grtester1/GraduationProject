using AgentVI.Converters;
using AgentVI.Interfaces;
using AgentVI.Models;
using AgentVI.Services;
using InnoviApiProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgentVI.ViewModels
{
    public class LandscapeEventDetailsPageVM : IBindableVM
    {
        private EventModel EventModel { get; set; }
        public string SensorEventClip => isLive ? EventModel.Sensor.LiveView : EventModel.SensorEventClip;
        public string FirstLineOverlay { get => new StringBuilder().Append(getActiveAccountName())
                                                                    .Append(",")
                                                                    .Append(getLeafNameOfCurrentFiltrationPath())
                                                                    .Append(",")
                                                                    .Append(EventModel.SensorName)
                                                                    .ToString();
                                        }
        public string SecondLineOverlay { get => new TimestampConverter()
                                                  .Convert(EventModel.SensorEventDateTime, typeof(ulong), null, null).ToString();
                                        }
        public bool IsPlayerVisible { get; set; } = true;
        private bool isLive;


        private LandscapeEventDetailsPageVM()
        {

        }

        public LandscapeEventDetailsPageVM(EventModel i_EventModel, bool i_IsLive = false):this()
        {
            isLive = i_IsLive;
            EventModel = i_EventModel;
        }

        private string getLeafNameOfCurrentFiltrationPath()
        {
            string res = String.Empty;
            List<Folder> currentPath = ServiceManager.Instance.FilterService.CurrentPath;
            if(currentPath.Count != 0)
            {
                res = currentPath[currentPath.Count - 1].Name;
            }
            return res;
        }

        private string getActiveAccountName()
        {
            return ServiceManager.Instance.FilterService.CurrentAccount.Name;
        }
    }
}

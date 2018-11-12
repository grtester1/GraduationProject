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
        private EventModel eventModel { get; set; }
        private Sensor streamingSensor { get; set; }
        public string SensorEventClip => isLive ? streamingSensor.LiveView : eventModel.SensorEventClip;
        public string FirstLineOverlay { get => new StringBuilder().Append(getActiveAccountName())
                                                                    .Append(",")
                                                                    .Append(getLeafNameOfCurrentFiltrationPath())
                                                                    .Append(",")
                                                                    .Append(isLive ? streamingSensor.Name : eventModel.SensorName)
                                                                    .ToString();
                                        }
        public string SecondLineOverlay { get => isLive? string.Empty : new TimestampConverter()
                                                  .Convert(eventModel.SensorEventDateTime, typeof(ulong), null, null).ToString();
                                        }
        public bool IsPlayerVisible { get; set; } = true;
        private bool isLive;


        private LandscapeEventDetailsPageVM()
        {

        }

        public LandscapeEventDetailsPageVM(EventModel i_EventModel):this()
        {
            isLive = false;
            eventModel = i_EventModel;
        }

        public LandscapeEventDetailsPageVM(Sensor i_StreamingSensor) : this()
        {
            isLive = true;
            streamingSensor = i_StreamingSensor;
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

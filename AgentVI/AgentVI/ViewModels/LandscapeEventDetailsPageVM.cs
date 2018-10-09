using AgentVI.Converters;
using AgentVI.Models;
using AgentVI.Services;
using InnoviApiProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgentVI.ViewModels
{
    public class LandscapeEventDetailsPageVM
    {
        private EventModel EventModel { get; set; }
        public string SensorEventClip { get => EventModel.SensorEventClip; }
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

        private LandscapeEventDetailsPageVM()
        {

        }

        public LandscapeEventDetailsPageVM(EventModel i_EventModel):this()
        {
            EventModel = i_EventModel;
        }

        private string getLeafNameOfCurrentFiltrationPath()
        {
            List<Folder> currentPath = ServiceManager.Instance.FilterService.CurrentPath;
            return currentPath[currentPath.Count - 1].Name;
        }

        private string getActiveAccountName()
        {
            return ServiceManager.Instance.FilterService.CurrentAccount.Name;
        }
    }
}

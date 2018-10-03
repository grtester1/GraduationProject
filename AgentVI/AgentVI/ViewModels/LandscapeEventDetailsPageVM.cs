using AgentVI.Converters;
using AgentVI.Models;
using AgentVI.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgentVI.ViewModels
{
    public class LandscapeEventDetailsPageVM
    {
        private EventModel EventModel { get; set; }
        public string SensorEventClip { get => EventModel.SensorEventClip; }
        public string FirstLineOverlay { get => new StringBuilder().Append(ServiceManager.Instance.LoginService.LoggedInUser.Username)
                                                                    .Append(",")
                                                                    .Append(ServiceManager.Instance.FilterService.GetLeafFolder())
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
    }
}

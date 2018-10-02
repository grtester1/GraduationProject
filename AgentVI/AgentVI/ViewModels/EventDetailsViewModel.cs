using AgentVI.Models;
using AgentVI.Converters;
using AgentVI.Utils;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AgentVI.ViewModels
{
    public class EventDetailsViewModel
    {
        private EventModel EventModel { get; set; }
        public string SensorName
        {
            get => EventModel.SensorName;
        }
        public string SensorEventRuleName
        {
            get => EventModel.SensorEventRuleName.convertEnumToString();
        }
        public string SensorEventDateTime
        {
            get => new TimestampConverter().Convert(EventModel.SensorEventDateTime,typeof(String), null, null).ToString();
        }
        public string SensorEventTag
        {
            get => EventModel.SensorEventTag.convertEnumToString();
        }
        public string SensorEventClipPath
        {//EventModel.SensorEventClip
            get => "https://vjs.zencdn.net/v/oceans.mp4";
        }
        public string SensorEventObjectType
        {
            get => new EnumObjectTypeSVGConverter().Convert(EventModel.SensorEventObjectType, EventModel.SensorEventObjectType.GetType() , null, null).ToString();
        }


        public EventDetailsViewModel(EventModel i_EventModel)
        {
            EventModel = i_EventModel;
        }
    }
}

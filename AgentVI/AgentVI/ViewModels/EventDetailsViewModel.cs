using AgentVI.Models;
using AgentVI.Converters;
using AgentVI.Utils;
using System;
using AgentVI.Interfaces;
using AgentVI.Views;
using System.Windows.Input;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;

namespace AgentVI.ViewModels
{
    public class EventDetailsViewModel : IBindableVM
    {
        public event EventHandler<UpdatedContentEventArgs> EventRouter;
        public DropdownMenuPage DropdownMenu { get; private set; }
        public string SensorName
        {
            get => EventModel.SensorName;
        }
        public string SensorEventRuleName
        {
            get => EventModel.SensorEventRuleName.convertEnumToString();
        }
        public string SensorEventBehavior
        {
            get => EventModel.SensorEventRuleName.BehaviorToString();
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
        {
            get => EventModel.SensorEventClip;
        }
        public string SensorEventObjectType
        {
            get => new EnumObjectTypeSVGConverter().Convert(EventModel.SensorEventObjectType, EventModel.SensorEventObjectType.GetType() , null, null).ToString();
        }
        public bool IsPlayerVisible { get; set; } = true;
        public bool IsClipAvailable
        {
            get => EventModel.SensorEvent.IsClipAvailable && IsPlayerVisible;
        }
        private EventModel EventModel { get; set; }


        public EventDetailsViewModel(EventModel i_EventModel)
        {
            EventModel = i_EventModel;
            DropdownMenu = DropdownMenuPage.FactoryMethod()
                .AddActionItem(new Tuple<string, Action>(
                    "Live", () => EventRouter?.Invoke(this, new UpdatedContentEventArgs(
                                                                UpdatedContentEventArgs.EContentUpdateType.Push,
                                                                new HealthStatPage()))))
                .AddActionItem(new Tuple<string, Action>(
                    "Health", () => EventRouter?.Invoke(this, new UpdatedContentEventArgs(
                                                                UpdatedContentEventArgs.EContentUpdateType.Push,
                                                                new HealthStatPage()))))
                .Build();
        }
    }
}

using AgentVI.Models;
using AgentVI.Converters;
using AgentVI.Utils;
using System;
using AgentVI.Interfaces;
using AgentVI.Views;
using System.Windows.Input;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;

namespace AgentVI.ViewModels
{
    public class EventDetailsViewModel : IBindableVM
    {
        public event EventHandler<UpdatedContentEventArgs> EventRouter;
        public DropdownMenuPage DropdownMenu { get; private set; }
        public string SensorName => EventModel.SensorName;
        public string SensorEventRuleName => EventModel.SensorEventRuleName.convertEnumToString();
        public string SensorEventBehavior=> EventModel.SensorEventRuleName.BehaviorToString();
        public string SensorEventDateTime => new TimestampConverter().Convert(EventModel.SensorEventDateTime, typeof(String), null, null).ToString();
        public string SensorEventTag=> EventModel.SensorEventTag.convertEnumToString();
        public string SensorEventClipPath => isLive ? EventModel.Sensor.LiveView : EventModel.SensorEventClip;
        public string SensorEventObjectType => new EnumObjectTypeSVGConverter().Convert(EventModel.SensorEventObjectType, EventModel.SensorEventObjectType.GetType(), null, null).ToString();
        public bool IsPlayerVisible { get; set; } = true;
        public bool IsClipAvailable => EventModel.SensorEvent.IsClipAvailable && IsPlayerVisible;
        private EventModel EventModel { get; set; }
        private bool isLive;

        public EventDetailsViewModel(EventModel i_EventModel, bool i_IsLive = false)
        {
            isLive = i_IsLive;
            EventModel = i_EventModel;
            DropdownMenuPage dropdownMenuPage = null;
            if (!i_IsLive)
            {
                dropdownMenuPage = DropdownMenuPage.FactoryMethod();
                dropdownMenuPage.AddActionItem(new Tuple<string, Action>(
                    "Live", async () =>
                    {
                        EventRouter?.Invoke(this, null);
                        EventDetailsPage eventDetailsPageBuf = null;
                        await Task.Factory.StartNew(() =>
                        {
                            eventDetailsPageBuf = new EventDetailsPage(EventModel, true);
                            eventDetailsPageBuf.RaiseContentViewUpdateEvent += eventsRouter;
                        });
                        EventRouter?.Invoke(this, new UpdatedContentEventArgs(
                                                                    UpdatedContentEventArgs.EContentUpdateType.Push,
                                                                    eventDetailsPageBuf, eventDetailsPageBuf.BindableViewModel));
                    }))
                .AddActionItem(new Tuple<string, Action>(
                    "Health", () => EventRouter?.Invoke(this, new UpdatedContentEventArgs(
                                                                UpdatedContentEventArgs.EContentUpdateType.Push,
                                                                new HealthStatPage()))))
                .Build();
            }
            DropdownMenu = dropdownMenuPage;
        }

        private void eventsRouter(object sender, UpdatedContentEventArgs e)
        {
            EventRouter?.Invoke(this, e);
        }
    }
}

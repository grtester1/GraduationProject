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
        public string SensorName
        {
            get
            {
                Console.WriteLine("###Logger###   -   in EventDetailsVM.SensorName @ begin get");
                var res = EventModel.SensorName;
                Console.WriteLine("###Logger###   -   in EventDetailsVM.SensorName @ end get");
                return res;
            }
        }
        public string SensorEventRuleName
        {
            get
            {
                Console.WriteLine("###Logger###   -   in EventDetailsVM.SensorEventRuleName @ begin get");
                var res = EventModel.SensorEventRuleName.convertEnumToString();
                Console.WriteLine("###Logger###   -   in EventDetailsVM.SensorEventRuleName @ begin get");
                return res;
            }
        }
        public string SensorEventBehavior
        {
            get
            {
                Console.WriteLine("###Logger###   -   in EventDetailsVM.SensorEventBehavior @ begin get");
                var res = EventModel.SensorEventRuleName.BehaviorToString();
                Console.WriteLine("###Logger###   -   in EventDetailsVM.SensorEventBehavior @ begin get");
                return res;
            }
        }
        public string SensorEventDateTime
        {
            get
            {
                Console.WriteLine("###Logger###   -   in EventDetailsVM.SensorEventDateTime @ begin get");
                var res = new TimestampConverter().Convert(EventModel.SensorEventDateTime, typeof(String), null, null).ToString();
                Console.WriteLine("###Logger###   -   in EventDetailsVM.SensorEventDateTime @ begin get");
                return res;
            }
        }
        public string SensorEventTag
        {
            get
            {
                Console.WriteLine("###Logger###   -   in EventDetailsVM.SensorEventTag @ begin get");
                var res = EventModel.SensorEventTag.convertEnumToString();
                Console.WriteLine("###Logger###   -   in EventDetailsVM.SensorEventTag @ begin get");
                return res;
            }
        }
        public string SensorEventClipPath
        {
            get
            {
                Console.WriteLine("###Logger###   -   in EventDetailsVM.SensorEventTag @ begin get");
                var res = isLive? EventModel.Sensor.LiveView : EventModel.SensorEventClip;
                Console.WriteLine("###Logger###   -   in EventDetailsVM.SensorEventTag @ begin get");
                return res;
            }
        }
        public string SensorEventObjectType
        {
            get
            {
                Console.WriteLine("###Logger###   -   in EventDetailsVM.SensorEventObjectType @ begin get");
                var res = new EnumObjectTypeSVGConverter().Convert(EventModel.SensorEventObjectType, EventModel.SensorEventObjectType.GetType(), null, null).ToString();
                Console.WriteLine("###Logger###   -   in EventDetailsVM.SensorEventObjectType @ begin get");
                return res;
            }
        }
        public bool IsPlayerVisible { get; set; } = true;
        public bool IsClipAvailable
        {
            get
            {
                Console.WriteLine("###Logger###   -   in EventDetailsVM.IsClipAvailable @ begin get");
                var res = EventModel.SensorEvent.IsClipAvailable && IsPlayerVisible;
                Console.WriteLine("###Logger###   -   in EventDetailsVM.IsClipAvailable @ begin get");
                return res;
            }
        }
        private EventModel EventModel { get; set; }
        private bool isLive;

        public EventDetailsViewModel(EventModel i_EventModel, bool i_IsLive = false)
        {
            Console.WriteLine("###Logger###   -   in EventDetailsVM.EventDetailsViewModel @ begin ctr");
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
            Console.WriteLine("###Logger###   -   in EventDetailsVM.EventDetailsViewModel @ end ctr");
        }

        private void eventsRouter(object sender, UpdatedContentEventArgs e)
        {
            EventRouter?.Invoke(this, e);
        }
    }
}

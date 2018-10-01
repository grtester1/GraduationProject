using AgentVI.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AgentVI.ViewModels
{
    public class EventDetailsViewModel
    {
        public EventModel EventModel { get; set; }

        public EventDetailsViewModel(EventModel i_EventModel)
        {
            EventModel = i_EventModel;
        }
    }
}

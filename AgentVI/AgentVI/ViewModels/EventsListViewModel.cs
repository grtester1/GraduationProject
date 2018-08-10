
using DummyProxy;

//using InnoviApiProxy;

using System;
using System.Collections.Generic;
using AgentVI.Services;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using AgentVI.Models;

namespace AgentVI.ViewModels
{
    public class EventsListViewModel
    {
        public ObservableCollection<EventModel> EventsList { get; set; }

        public EventsListViewModel()
        {
            EventsList = new ObservableCollection<EventModel>();
        }

		public void InitializeList(User i_loggedInUser)
        {
			if (i_loggedInUser != null)
            {
                //List<SensorEvent> userProxyList = i_loggedInUser.GetDefaultAccountEvents().ToList();
				/*InnoviObjectCollection<SensorEvent> userProxyList = i_loggedInUser.GetDefaultAccountEvents();
                if (userProxyList != null)
                {

                    foreach (SensorEvent camEvent in userProxyList)
                    {

                        EventModel eventModel = new EventModel();
						eventModel.CamName = camEvent.SensorName;
						//eventModel.DateTime = "6/2/2018 4:57:58 PM"; //camEvent.StartTime.ToString();
						DateTime DateTime = new DateTime((long)camEvent.StartTime);
						eventModel.DateTime = DateTime.ToString();
						eventModel.RuleName = camEvent.RuleName.ToString();
						eventModel.CamImage = camEvent.ImagePath;
						if (eventModel.CamImage == null || eventModel.CamImage == "")
                        {
                            eventModel.CamImage = "https://i.ytimg.com/vi/CKgEmWL1YrQ/maxresdefault.jpg";
                        }
                        EventsList.Add(eventModel);


                    }
                }
                else*/
                {
					EventsList.Add(new EventModel { CamName = "There is currently no event in the selected folder.", RuleName = "", DateTime = "", CamImage = "https://nondualityamerica.files.wordpress.com/2010/10/nothing-here-neon-300x200.jpg?w=375&h=175" });
                }
            }
            else
            {
                throw new Exception("Method InitializeFields/CamerasListVM was called with null param");
            }
        }

    }
}
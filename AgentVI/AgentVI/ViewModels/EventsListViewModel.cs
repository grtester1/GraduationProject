using System;
using System.Collections.Generic;
using AgentVI.Services;
using Xamarin.Forms;
using InnoviApiProxy;
//using DummyProxy;
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
                //List<SensorEvent> userProxyList = i_loggedInUser.GetDefaultAccountEvents();
                //if (userProxyList.Count > 0)
                //{
                   
                //foreach (SensorEvent camEvent in userProxyList)
                for (int i = 0; i < 10;i++)
                    {
                        //if(i > 10)
                        //{
                        //    break;
                        //}
                        EventModel eventModel = new EventModel();
                        eventModel.CamName = "Camera Name" ; //camEvent.Name;
                        eventModel.dateTime = "6/2/2018 4:57:58 PM"; //camEvent.StartTime.ToString();
                        eventModel.RuleName = "Rule name"; //camEvent.RuleId.ToString();
					    eventModel.CamImage = "https://i.ytimg.com/vi/CKgEmWL1YrQ/maxresdefault.jpg"; //camEvent.ImagePath;
                        if (eventModel.CamImage == null)
                        {
						    eventModel.CamImage = "https://i.ytimg.com/vi/CKgEmWL1YrQ/maxresdefault.jpg";
                        }
                        EventsList.Add(eventModel);
                        //i++;
                    }
                //}
                //else
                //{
				//   EventsList.Add(new EventModel { CamName = "There is currently no event in the selected folder.", RuleName = "", dateTime="" , CamImage = "https://i.ytimg.com/vi/CKgEmWL1YrQ/maxresdefault.jpg"; });
                //}
            }
            else
            {
                throw new Exception("Method InitializeFields/EventsListVM was called with null param");
            }
        }
    }
}
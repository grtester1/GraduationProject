using System;
using InnoviApiProxy;
using System.Collections.Generic;

namespace NadavTest
{
    class Program
    {
        static void Main(string[] args)
        {
            LoginResult loginResult = InnoviApiService.Login("ramot.n@gmail.com", "password");
            User user = loginResult.User;

            InnoviObjectCollection<Folder> lazyFolders =  user.GetDefaultAccountFolders();

            foreach (Folder folder in lazyFolders)
            {
                InnoviObjectCollection<Sensor> allSensors = folder.GetAllSensors();

                foreach (Sensor sensor in allSensors)
                {
                    string live = sensor.LiveView;

                    List<Sensor.Health> healthArray = sensor.SensorHealthArray;
                }

            }
           
            foreach (Folder folder in lazyFolders)
            {
                InnoviObjectCollection<SensorEvent> smartEvents = folder.FolderEvents;

                foreach (SensorEvent smartEvent in smartEvents)
                {
                    SensorEvent.eBehaviorType bType = smartEvent.RuleName;
                }
            }

            InnoviObjectCollection<Sensor> lazySensors = user.GetDefaultAccountSensors();
          
            InnoviObjectCollection<SensorEvent> lazyEvents = user.GetDefaultAccountEvents();

            List<SensorEvent> testList = new List<SensorEvent>();

            foreach (SensorEvent lazyEvent in lazyEvents)
            {
                testList.Add(lazyEvent);
                Console.WriteLine(lazyEvent.SensorName);
            }

            int i = 1;
        }
    }
}

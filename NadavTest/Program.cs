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
            
            //         LoginResult loginResult1 = User.Connect("eyJhbGciOiJIUzI1NiJ9.eyJhY2NvdW50SWQiOiI2Iiwicm9sZSI6IkFETUlOIiwidXNlclN0YXR1cyI6IkFDVElWRSIsInVzZXJUeXBlIjoiVVNFUiIsImV4cCI6MTUyNTg4NTU4MywidXNlcklkIjoiNTU1In0.jt - SDvzroj1 - dtjpU6O1zpklP_hZREb6RC8rSdCCP7g");
            InnoviObjectCollection<Sensor> lazySensors = user.GetDefaultAccountSensors();

            List<Sensor> sesnsorList = lazySensors.ToList();
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

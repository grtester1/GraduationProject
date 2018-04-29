using System;
using InnoviApiProxy;
using System.Collections.Generic;

namespace NadavTest
{
    class Program
    {
        static void Main(string[] args)
        {
            LoginResult loginResult = User.Login("ramot.n@gmail.com", "password");
          User user = loginResult.User;
            //         LoginResult loginResult1 = User.Connect("eyJhbGciOiJIUzI1NiJ9.eyJhY2NvdW50SWQiOiI2Iiwicm9sZSI6IkFETUlOIiwidXNlclN0YXR1cyI6IkFDVElWRSIsInVzZXJUeXBlIjoiVVNFUiIsImV4cCI6MTUyNTg4NTU4MywidXNlcklkIjoiNTU1In0.jt - SDvzroj1 - dtjpU6O1zpklP_hZREb6RC8rSdCCP7g");
            List<SensorEvent> myEvents = user.GetDefaultAccountEvents();
            List<Folder> folders = user.GetDefaultAccountFolders();
            foreach(var folder in folders)
            {
                List<Folder> subfolders = folder.Folders;

                if (subfolders == null)
                {
                    continue;
                }

                foreach (var subfolder in subfolders)
                {
                    List<Sensor> sensors = subfolder.Sensors;

                    foreach( var sensor in sensors)
                    {
                        List<SensorEvent> events = sensor.SensorEvents;
                    }
                }
            }
        }
    }
}

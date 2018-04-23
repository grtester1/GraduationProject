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
    //        LoginResult loginResult1 = User.Connect(Settings.AccessToken);
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
                }
            }
        }
    }
}

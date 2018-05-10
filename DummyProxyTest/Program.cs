using System;
using DummyProxy;

namespace DummyProxyTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            LoginResult loginResult = User.Login("ramot.n@gmail.com", "assword");
            string accessToken = Settings.AccessToken;
            User user = loginResult.User;
            InnoviObjectCollection<Folder> folders = user.GetDefaultAccountFolders();
            InnoviObjectCollection<Sensor> sensors = user.GetDefaultAccountSensors();
            InnoviObjectCollection<SensorEvent> events = user.GetDefaultAccountEvents();

            foreach (SensorEvent sensorEvent in events)
            {
                Console.WriteLine(sensorEvent.RuleName);
                Console.WriteLine(sensorEvent.SensorName);
            }

            int i = 1;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace DummyProxy
{
    public class User
    {
        private static User m_Instance;
        public string Username { get; private set; }
        public string UserEmail { get; private set; }
        public List<Account> Accounts { get; private set; }

        private User() { }

        internal User Fetch()
        {
            return m_Instance;
        }
        internal static LoginResult Login(string i_Email, string i_Password)
        {
            LoginResult result = new LoginResult();
            result.ErrorMessage = LoginResult.eErrorMessage.Empty;
            User user = createDummyUser();
            result.User = user;
            return result;
        }

        internal static LoginResult Connect(string i_AccessToken)
        {
            LoginResult result = new LoginResult();
            result.ErrorMessage = LoginResult.eErrorMessage.Empty;
            User user = createDummyUser();
            result.User = user;

            return result;
        }

        internal void Logout()
        {
            Settings.AccessToken = null;
            m_Instance = null;
        }

        public InnoviObjectCollection<Folder> GetDefaultAccountFolders()
        {
            List<Folder> folders = new List<Folder>();
            List<Folder> listHelper = new List<Folder>();

            Folder folder1 = new Folder();
            Folder folder2 = new Folder();
            Folder folder3= new Folder();
            Folder folder4 = new Folder();
            Folder folder5 = new Folder();
            Folder folder6 = new Folder();
            Folder folder7 = new Folder();
            Folder folder8 = new Folder();
            Folder folder9 = new Folder();
            Folder folder10 = new Folder();
            Folder folder11 = new Folder();
            Folder folder12 = new Folder();

            folder1.Name = "Dummy Folder 1";
             folder2.Name = "Dummy Folder 2";
            folder3.Name = "Dummy Folder 3";
            folder4.Name = "Dummy Folder 4";
            folder5.Name = "Dummy Folder 5";
            folder6.Name= "Dummy Folder 6";
            folder7.Name = "Dummy Folder 7";
            folder8.Name = "Dummy Folder 8";
            folder9.Name = "Dummy Folder 9";
            folder10.Name = "Dummy Folder 10";
            folder11.Name = "Dummy Folder 11";
            folder12.Name = "Dummy Folder 12";

            listHelper.Add(folder5);
            listHelper.Add(folder6);
            listHelper.Add(folder7);
            listHelper.Add(folder8);
            listHelper.Add(folder9);
            listHelper.Add(folder10);
            listHelper.Add(folder11);
            listHelper.Add(folder12);

            foreach (Folder folder in listHelper)
            {
                List<Sensor> sensors = new List<Sensor>();

                for (int i=0; i<10; i++)
                {
                    Sensor sensor = new Sensor();
                    sensor.Name += " " + i.ToString() ;
                    sensors.Add(sensor);
                }

                folder.Sensors = new InnoviObjectCollection<Sensor>(sensors);
            }

            List<Folder> folder1SubFolders = new List<Folder>();
            List<Folder> folder2SubFolders = new List<Folder>();
            List<Folder> folder3SubFolders = new List<Folder>();
            List<Folder> folder4SubFolders = new List<Folder>();

            folder1SubFolders.Add(folder5);
            folder1SubFolders.Add(folder6);

            folder2SubFolders.Add(folder7);
            folder2SubFolders.Add(folder8);

            folder3SubFolders.Add(folder9);
            folder3SubFolders.Add(folder10);

            folder4SubFolders.Add(folder11);
            folder4SubFolders.Add(folder12);

            folder1.Folders = new InnoviObjectCollection<Folder>(folder1SubFolders);
            folder2.Folders = new InnoviObjectCollection<Folder>(folder2SubFolders);
            folder3.Folders = new InnoviObjectCollection<Folder>(folder3SubFolders);
            folder4.Folders = new InnoviObjectCollection<Folder>(folder4SubFolders);

            folders.Add(folder1);
            folders.Add(folder2);
            folders.Add(folder3);
            folders.Add(folder4);

            return new InnoviObjectCollection<Folder>(folders);

        }

        public InnoviObjectCollection<SensorEvent> GetDefaultAccountEvents()
        {
            List<SensorEvent> events = new List<SensorEvent>();
            int sensorNameId = 1;

            for (int i=1; i<100; i++)
            {
                if (i % 10 == 0)
                {
                    sensorNameId++;
                }

                SensorEvent sensorEvent = new SensorEvent();
                sensorEvent.SensorName = "Dummy Camera " + sensorNameId.ToString();
                events.Add(sensorEvent);
            }

            return new InnoviObjectCollection<SensorEvent>(events);
        }

        public InnoviObjectCollection<Sensor> GetDefaultAccountSensors()
        {
            List<Sensor> sensors = new List<Sensor>();

            for (int i = 0; i < 10; i++)
            {
                Sensor sensor = new Sensor();
                sensor.Name += " " + i.ToString();
                sensors.Add(sensor);
                List<SensorEvent> sensorsEvents = new List<SensorEvent>();

                for (int k=0; k<10; k++)
                {
                    SensorEvent sensorEvent = new SensorEvent();
                    sensorEvent.SensorName = sensor.Name;
                    sensorsEvents.Add(sensorEvent);

                }

                sensor.SensorEvents = new InnoviObjectCollection<SensorEvent>(sensorsEvents);
            }

            return new InnoviObjectCollection<Sensor>(sensors);
        }

        private static User createDummyUser()
        {
            User user = new User();
            user.UserEmail = "ramot.n@gmail.com";
            user.Username = "Nadav";

            Account account = new Account();
            List<Account> accoutnList = new List<Account>();
            account.Name = "Dummy Account";
            account.Status = Account.eAccountStatus.Active;
            accoutnList.Add(account);
            user.Accounts = accoutnList;
            Settings.AccessToken = "eyJhbGciOiJIUzI1NiJ9.eyJhY2NvdW50SWQiOiI2Iiwicm9sZSI6IkFETUlOIiwidXNlclN0YXR1cyI6IkFDVElWRSIsInVzZXJUeXBlIjoiVVNFUiIsImV4cCI6MTUyNjYzMTIxMSwidXNlcklkIjoiNTU1In0.1qAdfjH_DUiJcEqYkAQ_UIlLaXb-P8CTlOxo9EFKEh8";
            return user;
        }
    }
}

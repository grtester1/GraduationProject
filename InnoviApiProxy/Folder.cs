using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace InnoviApiProxy
{
    public class Folder
    {
        [JsonProperty]
        private int parentId { get; set; }
        [JsonProperty]
        private int accountId { get; set; }
        [JsonProperty("id")]
        private int folderId { get; set; }
        [JsonProperty]
        public string Name { get; private set; }
        [JsonProperty]
        public int Depth { get; private set; }

        public List<Coordinate> GeoArea;

        internal Folder() { }

        public InnoviObjectCollection<Sensor> Sensors
        {
            get
            {
                return new InnoviObjectCollection<Sensor>(HttpUtils.GetFolderSensors, folderId);
            }
            set
            {
                Sensors = value;
            }
        }

        public InnoviObjectCollection<Folder> Folders
        {
            get
            {
                return new InnoviObjectCollection<Folder>(HttpUtils.GetFolders, folderId);
            }
            private set
            {
                Folders = value;
            }
        }

        public void ArmAllSensors()
        {
            throw new Exception("Not yet implemented");
        }

        public void DisarmAllSensors()
        {
            throw new Exception("Not yet implemented");
        }


    /*
        public List<Sensor> GetAllFolderSensors(Folder i_Folder)
        {
            List<Folder> subFolders = i_Folder.Folders.ToList();
            InnoviObjectCollection<Sensor> currentSensors = null;
            List<Sensor> allSensors = new List<Sensor>();

            if (subFolders == null)
            {
                currentSensors = i_Folder.Sensors;

                foreach (Sensor sensor in currentSensors)
                {
                    allSensors.Add(sensor);
                    // Do something with the sensor i.e present its data and insert it into some kind of collection
                }
            }
            else
            {
                foreach (Folder subfolder in subFolders)
                {
                    List<Sensor>  subfolderSensors = GetAllFolderSensors(subfolder);
                    allSensors.AddRange(subfolderSensors);
                }
            }

            return allSensors;
        }
        */
    }
}

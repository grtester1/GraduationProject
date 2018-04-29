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

        public List<Sensor> Sensors
        {
            get
            {
                return HttpUtils.GetSensors(folderId);
            }
            set
            {
                Sensors = value;
            }
        }

        public List<Folder> Folders
        {
            get
            {
                return HttpUtils.GetFolders(folderId);
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

        public List<Sensor> GetAllSensors()
        {
            List<Folder> subFolders = Folders;
            List<Sensor> sensors = new List<Sensor>();

            if (subFolders == null)
            {
                sensors = HttpUtils.GetFolderSensors(folderId);

            }
            else
            {
                foreach (Folder folder in subFolders)
                {
                    List<Sensor> currentSensors = folder.GetAllSensors();
                    sensors.AddRange(currentSensors);
                }
            }

            return sensors;
        }

    }
}

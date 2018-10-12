using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace InnoviApiProxy
{
    public class Folder : InnoviObject
    {
        [JsonProperty]
        private int parentId { get; set; }
        [JsonProperty]
        private int accountId { get; set; }
        [JsonProperty("id")]
        public int FolderId { get; private set; }
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
                return new InnoviObjectCollection<Sensor>(HttpUtils.GetFolderSensors, FolderId);
            }
            set
            {
                Sensors = value;
            }
        }

        public InnoviObjectCollection<SensorEvent> FolderEvents
        {
            get
            {
                return new InnoviObjectCollection<SensorEvent>(HttpUtils.GetFolderEvents, FolderId);
            }
        }


        public InnoviObjectCollection<Folder> Folders
        {
            get
            {
                return new InnoviObjectCollection<Folder>(HttpUtils.GetFolders, FolderId);
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

        public InnoviObjectCollection<Sensor> GetAllSensors()
        {
            return new InnoviObjectCollection<Sensor>(HttpUtils.GetAllFolderSensors, FolderId);
        }
    }
}

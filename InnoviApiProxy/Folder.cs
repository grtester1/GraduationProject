using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace InnoviApiProxy
{
    public class Folder
    {
        [JsonPropertyAttribute]
        public string Name { get; private set; }
        [JsonPropertyAttribute]
        public int Id { get; private set; }
        [JsonPropertyAttribute]
        public int ParentId { get; private set; }
        [JsonPropertyAttribute]
        public int AccountId { get; private set; }
        [JsonPropertyAttribute]
        public int Depth { get; private set; }

        public List<Coordinate> GeoArea;

        internal Folder() { }

        public List<Sensor> Sensors
        {
            get
            {
                return HttpUtils.GetSensors(Id);
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
                return HttpUtils.GetFolders(Id);
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

    }
}

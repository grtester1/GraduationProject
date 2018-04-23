using System;
using System.Collections.Generic;
using System.Text;

namespace InnoviApiProxy
{
    public class Folder
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int AccountId { get; set; }
        public int Depth { get; set; }

        public List<Coordinate> GeoArea;
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

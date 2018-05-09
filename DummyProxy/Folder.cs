using System;
using System.Collections.Generic;
using System.Text;

namespace DummyProxy
{
    public class Folder
    {

            private int parentId { get; set; }

            private int accountId { get; set; }

            private int folderId { get; set; }
          
            public string Name { get; internal set; }
    
            public int Depth { get; internal set; }

            public List<Coordinate> GeoArea;

            internal Folder() { }

            public InnoviObjectCollection<Sensor> Sensors { get; internal set; }


            public InnoviObjectCollection<Folder> Folders { get; internal set; }


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

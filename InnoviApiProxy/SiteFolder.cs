using System;
using System.Collections.Generic;
using System.Text;

namespace InnoviApiProxy
{
    public class SiteFolder : InnoviObject
    {
        public string Name { get; set; }
        public InnoviObjectCollection<Sensor> Sensors { get; set; }

        public void AddSensor(Sensor i_Sensor)
        {
            throw new Exception("Not yet implemented");
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

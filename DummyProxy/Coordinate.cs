using System;
using System.Collections.Generic;
using System.Text;

namespace DummyProxy
{
    public class Coordinate
    {

        public float Latitude { get; internal set; }

        public float Longitude { get; internal set; }
 
        public float Altitude { get; internal set; }

        internal Coordinate() { }
    }
}

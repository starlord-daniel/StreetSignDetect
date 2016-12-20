using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRService.Objects
{
    public class LocationObject
    {
        public Location[] locationList { get; set; }
        }

        public class Location
        {
            public string latitude { get; set; }
            public string longitude { get; set; }
        }

    
}
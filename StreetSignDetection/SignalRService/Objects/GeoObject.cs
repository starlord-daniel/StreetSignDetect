using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRService.Objects
{
    public class GeoObject
    {
        public string deviceId { get; set; }

        /// <summary>
        /// Latitude of the location
        /// </summary>
        public string lat { get; set; }

        /// <summary>
        /// Longitute of the location
        /// </summary>
        public string lon { get; set; }

        /// <summary>
        /// Street Sign
        /// </summary>
        public string streetSign { get; set; }
    }
}
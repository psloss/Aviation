using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaneLog.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public int PlaneId { get; set; }
        public Plane Plane { get; set; }
        public DateTime FlightDate { get; set; }

    }
}
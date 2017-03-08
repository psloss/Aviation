using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static PlaneLog.Models.Flight;
using static PlaneLog.Models.Plane;

namespace PlaneLog.Models
{
    public class Squawk
    {
        public int Id { get; set; }
        public int PlaneId { get; set; }
        public string Issue { get; set; }
        public DateTime DateRercorded { get; set; }
        public bool Resolved { get; set; }
        public DateTime DateResolved { get; set; }
        public string WhoResolved { get; set; }
        public decimal CostToResolve { get; set; }
        public string ContactPhone { get; set; }
        public string ContactName { get; set; }

    }
}
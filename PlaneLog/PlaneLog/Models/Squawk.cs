using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using static PlaneLog.Utils;
using static PlaneLog.Models.Plane;
using System.ComponentModel;

namespace PlaneLog.Models
{
    public class Squawk
    {
        public int Id { get; set; }
        public int PlaneId { get; set; }
        public Plane Plane { get; set; }
        public string Issue { get; set; }
        public DateTime DateRecorded { get; set; }
        public bool Resolved { get; set; }
        public DateTime? DateResolved { get; set; }
        public string WhoResolved { get; set; }
        public decimal? CostToResolve { get; set; }
        public string ContactPhone { get; set; }
        public string ContactName { get; set; }

    }
}
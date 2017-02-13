using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaneLog.Models
{
    public class Plane
    {
        public int Id { get; set; }
        public string TailNumber { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
    
    }
}
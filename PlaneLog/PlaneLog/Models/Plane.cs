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
        public string SerialNumber { get; set; }
        public int? YearManufactured { get; set; }
        public string EngineManufacturer { get; set; }
        public DateTime? AnnualDueDate { get; set; }
        public decimal? HoursLastAnnual { set; get; }
        public decimal? LastOilChangeHours { get; set; }

        public List<Flight> Flights { get; set; }

        public string AnnualDue { get { return AnnualDueDate == null ? string.Empty : AnnualDueDate.Value.ToString("MM/dd/yy"); } }
    }
}
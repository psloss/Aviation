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
        public DateTime? FlightDate { get; set; }
        public DateTime? TimeOut { get; set; }
        public DateTime? TimeIn { get; set; }
        public double? HobbsOut { get; set; }
        public double? HobbsIn { get; set; }
        public double? FuelOut { get; set; }
        public double? FuelIn { get; set; }
        public double? FuelPurchased { get; set; }
        public double? FuelCostGallon { get; set; }
        public double? FuelCostTotal { get; set; }
        public string Remarks { get; set; }
        public double? FuelUseHour { get; set; }

        public string TimeOutDate { get { return TimeOut == null ? string.Empty : TimeOut.Value.ToString("MM/dd/yy"); } }
    }
}
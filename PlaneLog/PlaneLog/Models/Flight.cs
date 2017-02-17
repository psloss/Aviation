using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static PlaneLog.Utils;

namespace PlaneLog.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public int PlaneId { get; set; }
        public Plane Plane { get; set; }
        public DateTime? FlightDate { get; set; }
        public decimal? HobbsOut { get; set; }
        public decimal? HobbsIn { get; set; }
        public decimal? FuelOut { get; set; }
        public decimal? FuelIn { get; set; }
        public decimal? FuelPurchased { get; set; }
        public decimal? FuelCostGallon { get; set; }
        public decimal? FuelCostTotal { get; set; }
        public int? AddedOil { get; set; }
        public int? OilChange { get; set; }
        public string Remarks { get; set; }
      

        public string FltDate { get { return FlightDate == null ? string.Empty : FlightDate.Value.ToString("MM/dd/yy"); } }
        public decimal HoursFlown { get { return (decimal)HobbsIn - (decimal)HobbsOut; } }
    //    public decimal FuelUsage { get { return (decimal)FuelOut - (decimal)FuelIn; } }
        public decimal FuelUsage { get { return FuelUsed(FuelOut, FuelIn, FuelPurchased); } }
        public decimal FuelUseHour { get { return (decimal)FuelUsage / HoursFlown; } }
        public string HobbsOForm { get { return FormNum(HobbsOut); } }
        public string HobbsIForm { get { return FormNum(HobbsIn); } }
        
        
        
    }

}


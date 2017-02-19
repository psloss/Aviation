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
    public class Flight
    {
        // private readonly decimal LastOilChangeHours;

        public int Id { get; set; }
        public int PlaneId { get; set; }
        public Plane Plane { get; set; }
        [DisplayName("Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yy}")]
        public DateTime? FlightDate { get; set; }

        [DisplayName("Hobbs\nOut")]
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public decimal? HobbsOut { get; set; }

        [DisplayName("Hobbs\nIn")]
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public decimal? HobbsIn { get; set; }

        [DisplayName("Fuel\nOut")]
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public decimal? FuelOut { get; set; }

        [DisplayName("Fuel\nIn")]
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public decimal? FuelIn { get; set; }

        [DisplayName("Fuel gals\nPurchased")]
        public decimal? FuelPurchased { get; set; }

        [DisplayName("Fuel Px\nGallon")]
        //     [DisplayFormat(DataFormatString ="{C2}")]
        public decimal? FuelCostGallon { get; set; }

        [DisplayName("Fuel Cost\nTotal")]
        public decimal? FuelCostTotal { get; set; }

        [DisplayName("Oil\nAdded")]
        public string AddedOil { get; set; }

        [DisplayName("Oil\nChanged")]
        public string OilChange { get; set; }

        public string Remarks { get; set; }

        [DisplayName("Flight\nDate")]
        public string FltDate { get { return FlightDate == null ? string.Empty : FlightDate.Value.ToString("MM/dd/yy"); } }
        public decimal HoursFlown { get { return (decimal)HobbsIn - (decimal)HobbsOut; } }
        //    public decimal FuelUsage { get { return (decimal)FuelOut - (decimal)FuelIn; } }
        public decimal FuelUsage { get { return FuelUsed(FuelOut, FuelIn, FuelPurchased); } }
        public decimal FuelUseHour { get { return (decimal)FuelUsage / HoursFlown; } }
        public string HobbsOForm { get { return FormNum(HobbsOut); } }
        public string HobbsIForm { get { return FormNum(HobbsIn); } }
        //       public string HoursSinceOilChange { get { return FormNum((decimal)HobbsIn - (decimal)LastOilChangeHours); } }

        [DisplayName("Gallons\n / Hour")]
        public decimal FuelPerHour { get { return (decimal) FuelUsage / (decimal) HoursFlown; } }
        
        

    }
    public class Util
    {
        // comment
    }
}


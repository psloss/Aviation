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
        public int Id { get; set; }
        public int PlaneId { get; set; }

        [DisplayName("Number")]
        public Plane Plane { get; set; }

        [DisplayName("Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yy}")]
        public DateTime? FlightDate { get; set; }

        [DisplayName("Hobbs\nOut")]
        [DisplayFormat(DataFormatString = "{0:n1}")]
        public decimal? HobbsOut { get; set; }

        [DisplayName("Hobbs\nIn")]
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public decimal? HobbsIn { get; set; }

        [DisplayName("Fuel \n Out")]
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public decimal? FuelOut { get; set; }

        [DisplayName("Fuel\nIn")]
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public decimal? FuelIn { get; set; }

        [DisplayName("Fuel Purch")]
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public decimal? FuelPurchased { get; set; }

        [DisplayName("Fuel $Gallon")]
        // [DisplayFormat(DataFormatString ="{C}")]
        public decimal? FuelCostGallon { get; set; }

        [DisplayName("Fuel Cost Total")]
        public decimal? FuelCostTotal { get { return FuelCostGallon * FuelPurchased; }  }

        [DisplayName("Oil \nAdded")]
        public bool AddedOil { get; set; }

        [DisplayName("Dipstick")]
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public decimal? OilDipstick { get; set; }

        [DisplayName("Oil \nChanged")]
        public bool OilChange { get; set; }

        [DisplayFormat ]
        public string Remarks { get; set; }

        // formatters for the fields above

        [DisplayName("Flight\nDate")]
        public string FltDate { get { return FlightDate == null ? string.Empty : FlightDate.Value.ToString("MM/dd/yy"); } }
        [DisplayName("Hours\nFlown")]
        public decimal HoursFlown { get { return (decimal)HobbsIn - (decimal)HobbsOut; } }

        [DisplayName("Fuel\nUsed")]
        public decimal FuelUsage { get { return FuelUsed(FuelOut, FuelIn, FuelPurchased); } }
        public decimal FuelUseHour { get { return (decimal)FuelUsage / HoursFlown; } }
        public string HobbsOForm { get { return FormNum(HobbsOut); } }
        public string HobbsIForm { get { return FormNum(HobbsIn); } }
        [DisplayName("Fuel $ Gal")]
        public string FuelCostGD { get { return FormDol(FuelCostGallon); } }
        [DisplayName("Total")]
        public string FuelCostTD { get { return FormDol(FuelCostTotal); } }

        [DisplayName("Gallons Hour")]
        public decimal FuelPerHour { get { return (decimal)FuelUsage / HoursFlown; } }

        private decimal FuelUsed(decimal? fuelout, decimal? fuelin, decimal? fuelpurch)
        {
            if (fuelpurch == null) { fuelpurch = 0; FuelCostGallon = 0;  }
            if (fuelout == null) { fuelout = 0; fuelin = 0; }
            decimal FuelUsed = (decimal)fuelout - (decimal)fuelin + (decimal)fuelpurch;
            return FuelUsed;
        }
        //[DisplayName("Fuel Cost")]
        //private decimal? fuelcosttotal;
        //public decimal? FuelCostTotal
        //{
        //    get
        //    {
        //        return (decimal)this.FuelCostGallon * (decimal)this.FuelPurchased;
        //    }
        //    set
        //    {
        //        this.fuelcosttotal = (decimal)FuelCostGallon * (decimal)FuelPurchased;
        //    }
        //}
        
        //[DisplayName("Oil Time")] /*Does planeid need to be included?*/
        //public decimal OilTime { get { return (decimal)HobbsIn - (decimal)Plane.LastOilChangeHours; } }

        [DisplayName("Remarks")]
        public string RemarksTrunc { get { return FormString(Remarks, 15); } }

        //    public decimal FuelUsage { get { return (decimal)FuelOut - (decimal)FuelIn; } }
    }
    // TODO Add updated fuel useage to sql file
    // TODO 
}



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

        [DisplayName("Hobbs Out")]
        [DisplayFormat(DataFormatString = "{0:n1}")]
        public decimal? HobbsOut { get; set; }

        [DisplayName("Hobbs In")]
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public decimal? HobbsIn { get; set; }

        [DisplayName("Fuel Out")]
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public decimal? FuelOut { get; set; }

        [DisplayName("Fuel In")]
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public decimal? FuelIn { get; set; }

        [DisplayName("Fuel Purch")]
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public decimal? FuelPurchased { get; set; }

        [DisplayName("Fuel $Gallon")]
        public decimal? FuelCostGallon { get; set; }

        [DisplayName("Oil Added")]
        public bool AddedOil { get; set; }

        [DisplayName("Dipstick")]
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public decimal? OilDipstick { get; set; }

        [DisplayName("Oil Changed")]
        public bool OilChange { get; set; }

        public string Remarks { get; set; }

        // formatters for the fields above

        [DisplayName("Flight Date")]
        public string FltDate { get { return FlightDate == null ? string.Empty : FlightDate.Value.ToString("MM/dd/yy"); } }
        [DisplayName("Hours Flown")]
        public decimal HoursFlown { get { return (decimal)HobbsIn - (decimal)HobbsOut; } }

        [DisplayName("Fuel Used")]
        public decimal FuelUsage { get { return FuelUsed(FuelOut, FuelIn, FuelPurchased); } }
    //    public decimal FuelUseHour { get { return (decimal)FuelUsage / HoursFlown; } }
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
            if (fuelpurch == null) { fuelpurch = 0; FuelCostGallon = 0; }
            if (fuelout == null) { fuelout = 0; fuelin = 0; }
            decimal FuelUsed = (decimal)fuelout - (decimal)fuelin + (decimal)fuelpurch;
            return FuelUsed;
        }
        // TODO fix this mess!!
        private decimal fuelCostTotal;
        [DisplayName("Fuel Cost")]
        public decimal? FuelCostTotal
        {
            get
            { return FuelCostGallon * FuelPurchased; }

            set
            {
                if (FuelCostGallon == null) FuelCostGallon = 0;
                if (FuelPurchased == null) FuelPurchased = 0;
                value = FuelCostGallon * FuelPurchased;
                fuelCostTotal = (decimal)value;
            }
        }

        private decimal fuelUseHour;
        public decimal? FuelUseHour
        {
            get { return FuelUsage / HoursFlown ; }

            set
            {
                if (FuelUseHour == null) FuelUseHour = 0;
                value = FuelUsage / HoursFlown;
                fuelUseHour = (decimal)value;
            }
        }

        private decimal flightHours;
        public decimal? FlightHours
        {
            get { return flightHours;  }
            set
            {
                if (FlightHours == null) FlightHours = 0;
                value = HobbsIn - HobbsOut;
                flightHours = (decimal)value;
            }

        }

        //[DisplayName("Oil Time")] /*Does planeid need to be included?*/
        //public decimal OilTime { get { return (decimal)HobbsIn - (decimal)Plane.LastOilChangeHours; } }

        [DisplayName("Remarks")]
        public string RemarksTrunc { get { return FormString(Remarks, 15); } }

    }
    // TODO Add updated fuel useage to sql file
    // TODO 
}



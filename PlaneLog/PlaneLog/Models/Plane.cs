using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlaneLog.Models
{
    public class Plane
    {

        public int Id { get; set; }

        private string tailNumber;
        [DisplayName("Tail Number")]
        public string TailNumber
        {
            get { return tailNumber; }
            set
            {
                if (value != null) value = value.ToUpper();
                tailNumber = value;
            }
        }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        [DisplayName("Serial\nNumber")]
        public string SerialNumber { get; set; }

        [DisplayName("Year\nMade")]
        public int? YearManufactured { get; set; }

        [DisplayName("Engine")]
        public string EngineManufacturer { get; set; }

        [DisplayName("Annual\nDue")]
        public DateTime? AnnualDueDate { get; set; }

        [DisplayName("Hours at\nAnnual")]
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public decimal? HoursLastAnnual { set; get; }


        // public decimal?  { get; set; }

        [DisplayName("Hours at Oil Change")]
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public decimal? LastOilChangeHours { get; set; }

        [DisplayName("Registration Expiration")]
        [DisplayFormat(DataFormatString = "MM/yy")]
        public DateTime? RegistrationExpirationDate { get; set; }

        public List<Flight> Flights { get; set; }

        [DisplayName("Hours at Annual")]
        public string AnnualDue { get { return AnnualDueDate == null ? string.Empty : AnnualDueDate.Value.ToString("MM/yy"); } }

        [DisplayName("Registration Expiration")]
        public string RegistrationDue { get { return RegistrationExpirationDate == null ? string.Empty : RegistrationExpirationDate.Value.ToString("MM/yy"); } }

        [DisplayName("Current Hours")]
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public decimal? EngineHours { get; set; }

        [DisplayName("Oil Hours")]
        public string OilHours { get { return (EngineHours - LastOilChangeHours).ToString(); } }
    }
}


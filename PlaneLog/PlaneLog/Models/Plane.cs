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
        [DisplayName("Tail\nNumber")]
        public string TailNumber { get; set; }
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
        public decimal? HoursLastAnnual { set; get; }
        [DisplayName("Hours at\nOil Change")]
        public decimal? LastOilChangeHours { get; set; }
        [DisplayName("Registration\nExpiration")]
        public DateTime? RegistrationExpirationDate { get; set; }

        public List<Flight> Flights { get; set; }

        [DisplayName("Hours at\nAnnual")]
        public string AnnualDue { get { return AnnualDueDate == null ? string.Empty : AnnualDueDate.Value.ToString("MM/yy"); } }
        [DisplayName("Registration\nExpiration")]
        public string RegistrationDue { get { return RegistrationExpirationDate == null ? string.Empty : RegistrationExpirationDate.Value.ToString("MM/yy"); } }
    }

      

    }


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using static PlaneLog.Models.Plane;

namespace PlaneLog
{
    public class Utils
    {
        public static string FormNum(decimal? inp)
        {
            string FormtNum = String.Format("{0:0.0}", inp);
            return FormtNum;
        }


      

        public static string FormDol(decimal? inp)
        {
            string FormDol = string.Format("$ {0:N2}", inp);
            return FormDol;
        }

        //[DisplayName("Hours\nFlown")]
        //public static decimal TimeFlown(decimal hobbsout, decimal hobbsin)
        //{
        //    decimal TimeFlown = hobbsin - hobbsout;
        //    return TimeFlown;
        //}

        //[DisplayName("Gallons\n\\ Hour")]
        //public static decimal FuelPerHour(decimal TimeFlown, decimal FuelUsed)
        //{
        //    decimal FuelPerHour = FuelUsed / TimeFlown;
        //    return FuelPerHour;
        //}
        //    public decimal HoursSinceOilChange { get { return (decimal)HobbsIn - LastOilChangeHours} }

    }
}

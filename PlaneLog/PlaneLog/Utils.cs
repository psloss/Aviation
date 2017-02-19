﻿using System;
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

        [DisplayName("Fuel\nUsed")]
        
        public static decimal FuelUsed(decimal? fuelout, decimal? fuelin, decimal? fuelpurch)
        {
            if (fuelpurch == null) { fuelpurch = 0; }
            decimal FuelUsed = (decimal)fuelout - (decimal)fuelin + (decimal)fuelpurch;
            return FuelUsed;
        }

        [DisplayName("Hours\nFlown")]
        public static decimal TimeFlown(decimal hobbsout, decimal hobbsin)
        {
            decimal TimeFlown = hobbsin - hobbsout;
            return TimeFlown;
        }

        [DisplayName("Gallons\n\\ Hour")]
        public static decimal FuelPerHour(decimal TimeFlown, decimal FuelUsed)
        {
            decimal FuelPerHour = FuelUsed / TimeFlown;
            return FuelPerHour;
        }
    //    public decimal HoursSinceOilChange { get { return (decimal)HobbsIn - LastOilChangeHours} }

    }
    }

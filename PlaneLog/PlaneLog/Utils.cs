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

        public static string FormDol(decimal? inp)
        {
            string FormDol = string.Format("${0:N2}", inp);
            return FormDol;
        }

        public static string FormString(string str, int lng)
        {
            if (str == null) str = " ";
            if (str.Length < lng)
            {
                str = str.PadLeft(lng);
            }
            string FormString = str.Substring(0, lng);
            return FormString;
        }
    }
}

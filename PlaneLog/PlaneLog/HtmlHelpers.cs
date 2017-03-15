using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaneLog.Helpers
{
    public static class HtmlHelpers
    {
        public static string Truncate(this HtmlHelper helper, string input, int length)
        {
            if (string.IsNullOrEmpty(input) == true) return null;
            if (input.Length <= length)
                return input;
            else
                return input.Substring(0, length) +  "<span style=\"color:red\"> ..more</span>";
        }

        public static decimal FormDollar(this HtmlHelper helper, decimal? input, int decimals)
        {
            if (string.IsNullOrEmpty(input.ToString()) == true) return 0;
            else
            {
                decimal FormDollar = Math.Round((decimal)input, decimals);
                return FormDollar;                
            }
        }

        public static string FormNumber(this HtmlHelper helper, decimal? input, int decimals)
        {
            if (string.IsNullOrEmpty(input.ToString()) == true) return "0.0";
            else
            {
                string FormNum = Math.Round((decimal)input, decimals).ToString();
                return FormNum;
            }
        }
        public static string FormDate(this HtmlHelper helper, DateTime? input)
        {
            if (input == null) return null;
            else
            {
                string FormDate = input.Value.ToString("MM/dd/yy");
                return FormDate;
            }
        }
    }
}
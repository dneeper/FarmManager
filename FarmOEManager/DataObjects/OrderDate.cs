using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FarmOEManager.WebServices.DataObjects
{
    public class OrderDate
    {
        public int Day;
        public int Month;
        public int Year;

        public OrderDate()
        {
        }

        public OrderDate(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        public override string ToString()
        {
            DateTime date = new DateTime(Year, Month, Day);
            return date.ToShortDateString();
        }
    }
}
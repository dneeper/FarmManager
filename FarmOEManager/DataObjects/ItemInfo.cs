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
    public class ItemInfo
    {
        public string Item;
        public string Type;
        public double Qty;

        public override string ToString()
        {
            return String.Format("{0} {1} {2} ", Type, Item, Qty);
        }
    }
}
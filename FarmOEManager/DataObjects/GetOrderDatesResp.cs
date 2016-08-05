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
    public class GetOrderDatesResp
    {
        public bool result;
        public string errorInfo;
        public List<OrderDateInfo> orderDateInfoList;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FarmOEManager.Activities;
using FarmOEManager.WebServices.DataObjects;
using FarmOEUtils;
using Newtonsoft.Json;

namespace FarmOEManager.WebServices
{
    public class GetOrderDates : WebServiceBase
    {
        public GetOrderDatesResp GetOrderDatesResp { get; private set; }
        public bool ValidConfiguration { get; private set; }
        private MemberData _MemberData;

        private static string URL =
            "http://www.ohiorawmilk.info/mobilemanager/MobileManager.svc/GetOrderDates/?farm={0}&group={1}";

        public GetOrderDates(ActivityEx parent, MemberData memberData, BackgroundTaskCompleteEventHandler onBackgroundTaskCompleteEvent) :
            base(parent, onBackgroundTaskCompleteEvent)
        {
            ValidConfiguration = true;
            _MemberData = memberData;
        }

        protected override bool Work()
        {
            try
            {
                var accountPreferences = new AccountPreferences(ParentActivity);

                if (accountPreferences.Valid == false)
                {
                    return ValidConfiguration = false;
                }

                var url = string.Format(URL, accountPreferences.Farm, _MemberData.Group);

                GetOrderDatesResp = InvokeWebServiceRequest<GetOrderDatesResp>(url);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
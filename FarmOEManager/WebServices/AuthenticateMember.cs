using System;
using System.Net;
using Android.App;
using FarmOEManager.Activities;
using FarmOEManager.WebServices.DataObjects;
using FarmOEUtils;
using Newtonsoft.Json;

namespace FarmOEManager.WebServices
{
    public class AuthenticateMember : WebServiceBase
    {
        public MemberData MemberData { get; private set; }
        public bool ValidConfiguration { get; private set; }

        private static string URL =
          "http://www.ohiorawmilk.info/mobilemanager/MobileManager.svc/AuthenticateUser/?farm={0}&firstName={1}&lastName={2}&password={3}&adminPassword={4}";

        public AuthenticateMember(ActivityEx parent, BackgroundTaskCompleteEventHandler onBackgroundTaskCompleteEvent) :
          base(parent, onBackgroundTaskCompleteEvent)
        {
            ValidConfiguration = true;
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

                var url = string.Format(URL, accountPreferences.Farm, accountPreferences.FirstName,
                  accountPreferences.LastName, accountPreferences.Password, accountPreferences.AdminPassword);

                MemberData = InvokeWebServiceRequest<MemberData>(url);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
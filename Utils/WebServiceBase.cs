using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using FarmOEUtils;
using Newtonsoft.Json;

namespace FarmOEUtils
{
    public abstract class WebServiceBase : BackgroundTask
    {
        private ProgressDialog _ProgressDialog;
        protected WebServiceBase(ActivityEx parent) : base(parent)
        {
        }

        protected WebServiceBase(ActivityEx parent,
          BackgroundTaskCompleteEventHandler backgroundTaskCompleteEventHandler) :
          base(parent, backgroundTaskCompleteEventHandler)
        {
        }

        protected override bool Setup()
        {
            _ProgressDialog = ProgressDialog.Show(ParentActivity, "",
              ParentActivity.GetString(Utils.Resource.String.progress_message));

            return true;
        }

        protected override void Complete(bool result)
        {
            _ProgressDialog.Cancel();
        }

        protected RespClass InvokeWebServiceRequest<RespClass>(string url)
        {
            using (var client = new WebClient())
            {
                var json = client.DownloadString(url);

                var stringReader = new System.IO.StringReader(json);
                var reader = new JsonTextReader(stringReader);
                var serializer = new JsonSerializer();

                return serializer.Deserialize<RespClass>(reader);
            }
        }
    }
}
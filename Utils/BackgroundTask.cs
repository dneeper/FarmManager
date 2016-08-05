using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android;

namespace FarmOEUtils
{
    public abstract class BackgroundTask
    {
        protected ActivityEx ParentActivity;

        private bool _Cancel = false;

        public delegate void BackgroundTaskCompleteEventHandler(object sender, BackgroundTaskEventArgs e);

        public event BackgroundTaskCompleteEventHandler CompleteEvent;

        protected BackgroundTask(ActivityEx parent)
        {
            ParentActivity = parent;
            ParentActivity.OnStopEvent += ParentActivity_OnStopEvent;
        }

        protected BackgroundTask(ActivityEx parent,
          BackgroundTaskCompleteEventHandler backgroundTaskCompleteEventHandler)
        {
            ParentActivity = parent;
            ParentActivity.OnStopEvent += ParentActivity_OnStopEvent;
            CompleteEvent += backgroundTaskCompleteEventHandler;
        }

        private void ParentActivity_OnStopEvent(object sender, EventArgs e)
        {
            _Cancel = true;
        }

        protected virtual bool Setup()
        {
            return true;
        }

        public bool Start()
        {
            if (Setup() == false)
            {
                return false;
            }

            ThreadPool.QueueUserWorkItem(o => InternalWorker());

            return true;
        }

        private void InternalWorker()
        {
            bool result = Work();

            ParentActivity.RunOnUiThread(() => { InternalComplete(result); });
        }

        private void InternalComplete(bool result)
        {
            if (_Cancel == false)
            {
                Complete(result);
                CompleteEvent?.Invoke(this, new BackgroundTaskEventArgs(result));
            }
        }

        protected abstract bool Work();

        protected virtual void Complete(bool result)
        {
        }

        public void Cancel()
        {
            _Cancel = true;
        }
    }


    public class BackgroundTaskEventArgs : EventArgs
    {
        public bool Result { get; private set; }

        public BackgroundTaskEventArgs(bool result)
        {
            Result = result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using Android.App;
using Android.Content;
using Java.Security;
using Android.Views;
using Utils;

namespace FarmOEUtils
{
    public class ActivityEx : Activity
    {
        public delegate void StopEventEventHandler(object sender, EventArgs e);
        public event StopEventEventHandler OnStopEvent;

        private bool _HomeButtonAsSoftBackButton;

        protected void SetHomeButtonAsSoftBackButton()
        {
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            _HomeButtonAsSoftBackButton = true;
        }

        protected override void OnStop()
        {
            OnStopEvent?.Invoke(this, new EventArgs());
            base.OnStop();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            if (item.ItemId == Android.Resource.Id.Home)
            {
                if (_HomeButtonAsSoftBackButton)
                {
                    if (OnBackButtonSelected(BackButtonType.ActionButton) == BaseRespType.PassToBase)
                    {
                        Finish();
                    }
                }
                else
                {
                    OnHomeButtonSelected();
                }
            }

            return base.OnOptionsItemSelected(item);
        }


        public override void OnBackPressed()
        {
            if (OnBackButtonSelected(BackButtonType.Hardware) == BaseRespType.PassToBase)
            {
                base.OnBackPressed();
            }
        }


        protected virtual void OnHomeButtonSelected()
        {
        }


        protected virtual BaseRespType OnBackButtonSelected(BackButtonType type)
        {
            return BaseRespType.PassToBase;
        }


        protected void ShowMsgBox(int titleResource, string message,
          EventHandler<DialogClickEventArgs> handler = null)
        {
            MessageBox.Show(this, titleResource, message, handler);
        }


        protected void ShowMsgBox(int titleResource, int messageResource,
          EventHandler<DialogClickEventArgs> handler = null)
        {
            MessageBox.Show(this, titleResource, messageResource, handler);
        }
    }


    public enum BackButtonType
    {
        ActionButton,
        Hardware
    }


    public enum BaseRespType
    {
        PassToBase,
        Ignore
    }
}

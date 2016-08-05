using System;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Test.Mock;
using FarmOEManager.Activities;
using FarmOEManager.WebServices;
using FarmOEManager.WebServices.DataObjects;
using FarmOEUtils;
using Utils;
using FarmOEManager.Adapters;
using Android.Support.V4.Widget;

namespace FarmOEManager
{
    [Activity(Label = "Farm Order Manager", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ActivityEx
    {
        private ListView _OrderDateList;
        private LinearLayout _ListHeader;
        private SwipeRefreshLayout _SwipeRefreshLayout;
        private MemberData _MemberData;
        private OrderDateAdapter _OrderDateAdapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            //Button button = FindViewById<Button>(Resource.Id.MyButton);

            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
            Button okButton = FindViewById<Button>(Resource.Id.okButton);
            okButton.Click += onOkButtonClick;

            _OrderDateList = FindViewById<ListView>(Resource.Id.orderDateList);

            _OrderDateList.ItemClick += OnOrderDateClick;

            var layoutInflater = (LayoutInflater)GetSystemService(Context.LayoutInflaterService);

            _ListHeader = (LinearLayout)layoutInflater.Inflate(Resource.Layout.ListHeader, null);

            _SwipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeLayout);

            _SwipeRefreshLayout.SetColorSchemeResources(Android.Resource.Color.HoloBlueBright, Android.Resource.Color.HoloBlueDark, Android.Resource.Color.HoloGreenLight, Android.Resource.Color.HoloGreenDark);
            _SwipeRefreshLayout.Refresh += _SwipeRefreshLayout_Refresh;

            //ActionBar.SetDisplayShowHomeEnabled(true);
            //ActionBar.SetDisplayHomeAsUpEnabled(true);

            AuthenticateUser();
        }

        private void _SwipeRefreshLayout_Refresh(object sender, EventArgs e)
        {
            RefreshOrderDates();
        }

        protected override void OnStart()
        {
            base.OnStart();

            //ActionBar.Title = "Action Me!";
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.ActionBarMenu, menu);

            return true;
        }

        private void AuthenticateUser()
        {
            var authenticate = new AuthenticateMember(this, OnAuthenticateCompleteEvent);

            authenticate.Start();
        }

        private void OnAuthenticateCompleteEvent(object sender, BackgroundTaskEventArgs e)
        {
            AuthenticateMember authenticateMember = sender as AuthenticateMember;

            if (e.Result)
            {
                if ((authenticateMember.MemberData != null) && (authenticateMember.MemberData.Valid == false))
                {
                    ShowMsgBox(Resource.String.ApplicationName, Resource.String.invalid_account,
                      (senderAlert, args) =>
                      {
                          StartAccountActivity();
                      });
                }
                else
                {
                    GetOrderDates(authenticateMember.MemberData);
                }
            }
            else
            {
                ShowMsgBox(Resource.String.ApplicationName, Resource.String.network_error);
            }
        }

        private void onOkButtonClick(object sender, System.EventArgs e)
        {
            Button okButton = sender as Button;
            okButton.Text = "Yep!";

            StartAccountActivity();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    break;

                case Resource.Id.setAccountMenuItem:
                    StartAccountActivity();
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if ( (requestCode == AccountActivity.GET_ACCOUNT_REQUEST) && (resultCode == Result.Ok) )
            {
                AuthenticateUser();
            }
        }


        private void StartAccountActivity()
        {
            var intent = new Intent(this, typeof(AccountActivity));

            StartActivityForResult(intent, AccountActivity.GET_ACCOUNT_REQUEST);
        }

        private void GetOrderDates(MemberData memberData)
        {
            _MemberData = memberData;

            var title = string.Format(GetString(Resource.String.currentOrderDates), _MemberData.Group);

            _ListHeader.FindViewById<TextView>(Resource.Id.titleText).Text = title;

            _OrderDateList.RemoveHeaderView(_ListHeader);
            _OrderDateList.AddHeaderView(_ListHeader, _ListHeader, false);

            var getOrderDates = new GetOrderDates(this, _MemberData, OnGetOrderDatesCompleteEvent);

            getOrderDates.Start();
        }

        private void RefreshOrderDates()
        {
            var getOrderDates = new GetOrderDates(this, _MemberData, OnRefreshOrderDatesCompleteEvent);

            getOrderDates.Start();
        }


        private void OnGetOrderDatesCompleteEvent(object sender, BackgroundTaskEventArgs e)
        {
            CreateUpdateOrderDateAdapter(sender as GetOrderDates);
        }


        private void OnRefreshOrderDatesCompleteEvent(object sender, BackgroundTaskEventArgs e)
        {
            CreateUpdateOrderDateAdapter(sender as GetOrderDates);

            _SwipeRefreshLayout.Refreshing = false;
        }

        private void CreateUpdateOrderDateAdapter(GetOrderDates getOrderDates)
        {
            if (_OrderDateAdapter == null)
            {
                _OrderDateAdapter = new OrderDateAdapter(this, getOrderDates.GetOrderDatesResp.orderDateInfoList);

                _OrderDateList.Adapter = _OrderDateAdapter;
            }
            else
            {
                _OrderDateAdapter.UpdateData(getOrderDates.GetOrderDatesResp.orderDateInfoList);
            }
        }

        private void OnOrderDateClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var orderDateInfo = _OrderDateAdapter.GetItem(e.Position - 1);

            var intent = new Intent(this, typeof(OrderSummaryActivity));

            intent.PutExtra(GlobalConstants.OrderDateInfo, orderDateInfo as IParcelable);
            intent.PutExtra(GlobalConstants.MemberData, _MemberData as IParcelable);

            StartActivity(intent);
        }
    }
}


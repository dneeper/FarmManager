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
using FarmOEManager.Adapters;
using FarmOEManager.WebServices;
using FarmOEManager.WebServices.DataObjects;
using FarmOEUtils;

namespace FarmOEManager.Activities
{
    [Activity(Label = "OrderSummaryActivity")]
    public class OrderSummaryActivity : ActivityEx
    {
        private ListView _MemberList;
        private ListView _ItemList;
        private OrderDateInfo _OrderDateInfo;
        private MemberData _MemberData;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.OrderSummaryView);

            _MemberList = FindViewById<ListView>(Resource.Id.memberList);
            _ItemList = FindViewById<ListView>(Resource.Id.itemList);

            _OrderDateInfo = this.Intent.GetParcelableExtra(GlobalConstants.OrderDateInfo) as OrderDateInfo;
            _MemberData = this.Intent.GetParcelableExtra(GlobalConstants.MemberData) as MemberData;

            Title = String.Format(GetString(Resource.String.orderSummaryTitle),
                _OrderDateInfo.ToString());

            var memberTitle = FindViewById<TextView>(Resource.Id.membersTitle);
            var memberTitleText = String.Format(memberTitle.Text, _OrderDateInfo.memberCount);
            memberTitle.Text = memberTitleText;

            ActionBar.SetDisplayHomeAsUpEnabled(true);
            SetHomeButtonAsSoftBackButton();

            GetOrderSummary();
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        private void GetOrderSummary()
        {
            var getOrderSummary = new GetOrderSummary(this, _MemberData, _OrderDateInfo.orderDate,
                OnGetOrderSummaryCompleteEvent);

            getOrderSummary.Start();
        }

        private void OnGetOrderSummaryCompleteEvent(object sender, BackgroundTaskEventArgs e)
        {
            var getOrderSummary = sender as GetOrderSummary;

            DisplayMemberList(getOrderSummary.GetOrderSummaryResp.memberList);
            DisplayItemList(getOrderSummary.GetOrderSummaryResp.itemInfoList);
        }

        private void DisplayMemberList(List<String> memberList)
        {
            var adapter = new GradientStringAdapter(this, memberList);

            _MemberList.Adapter = adapter;
        }

        private void DisplayItemList(List<ItemInfo> itemList)
        {
            var adapter = new GradientItemAdapter(this, itemList);

            _ItemList.Adapter = adapter;
        }

        protected override BaseRespType OnBackButtonSelected(BackButtonType type)
        {
            if (type == BackButtonType.ActionButton)
            {
                Finish();
                return BaseRespType.Ignore;
            }
            else
            {
                return BaseRespType.PassToBase;
            }
        }
    }
}
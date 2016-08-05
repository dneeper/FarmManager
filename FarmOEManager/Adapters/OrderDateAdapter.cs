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
using FarmOEManager.WebServices.DataObjects;

namespace FarmOEManager.Adapters
{
    public class OrderDateAdapter : BaseAdapter<OrderDateInfo>
    {
        private List<OrderDateInfo> _Items;
        private Activity _Context;

        public OrderDateAdapter(Activity context, List<OrderDateInfo> items)
        {
            _Items = items;
            _Context = context;
        }

        public void UpdateData(List<OrderDateInfo> items)
        {
            _Items = items;

            NotifyDataSetChanged();
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override OrderDateInfo this[int position]
        {
            get { return _Items[position]; }
        }

        public override int Count
        {
            get { return _Items.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _Items[position];
            View view = convertView;

            if (view == null)
            {
                view = _Context.LayoutInflater.Inflate(Resource.Layout.OrderDateView, null);
            }

            var date = new DateTime(item.orderDate.Year, item.orderDate.Month, item.orderDate.Day);

            view.FindViewById<TextView>(Resource.Id.OrderDate_DateView).Text = date.ToShortDateString();

            view.FindViewById<TextView>(Resource.Id.OrderDate_MemberCountView).Text =
                String.Format(_Context.Resources.GetString(Resource.String.memberElementText), item.memberCount);

            view.FindViewById<TextView>(Resource.Id.OrderDate_OrderCountView).Text =
                String.Format(_Context.Resources.GetString((Resource.String.orderElementText)), item.orderCount);

            return view;
        }
    }
}
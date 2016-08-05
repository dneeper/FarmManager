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
    public class GradientItemAdapter : BaseAdapter<ItemInfo>
    {
        private List<ItemInfo> _Items;
        private Activity _Context;

        public GradientItemAdapter(Activity context, List<ItemInfo> items)
        {
            _Items = items.OrderBy(x => x.Item).ToList();
            _Context = context;
        }

        public void UpdateData(List<ItemInfo> items)
        {
            _Items = items.OrderBy(x => x.Item).ToList();

            NotifyDataSetChanged();
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override ItemInfo this[int position]
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
                view = _Context.LayoutInflater.Inflate(Resource.Layout.GradientItemView, null);
            }

            view.FindViewById<TextView>(Resource.Id.gradientItemText).Text = item.Item;
            view.FindViewById<TextView>(Resource.Id.gradientItemQty).Text = item.Qty.ToString();

            return view;
        }
    }
}
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

namespace FarmOEManager.Adapters
{
    public class GradientStringAdapter : BaseAdapter<String>
    {
        private List<String> _Items;
        private Activity _Context;

        public GradientStringAdapter(Activity context, List<String> items)
        {
            _Items = items;
            _Context = context;
        }

        public void UpdateData(List<String> items)
        {
            _Items = items;

            NotifyDataSetChanged();
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override String this[int position]
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
                view = _Context.LayoutInflater.Inflate(Resource.Layout.GradientStringView, null);
            }

            view.FindViewById<TextView>(Resource.Id.gradientStringText).Text = item;

            return view;
        }
    }
}
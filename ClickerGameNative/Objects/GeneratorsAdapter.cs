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
using ClickerEngine.Enumerations;
using ClickerEngine.Generators;

namespace ClickerGame.AndroidNative
{
    public class GeneratorAdapter : BaseAdapter<Generator>
    {
        ObservableCollectionEx<Generator> items;
        Activity context;
        public GeneratorAdapter(Activity context, ObservableCollectionEx<Generator> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override Generator this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.Generators_row, null);
            view.FindViewById<TextView>(Resource.Id.Name).Text = item.Name;
            view.FindViewById<TextView>(Resource.Id.Description).Text = item.Description;
            view.FindViewById<TextView>(Resource.Id.Cost).Text = item.Price.ToString(ValueFormat.Literal);
            return view;
        }
    }
}
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

namespace ClickerGame.AndroidNative.Activities
{
    [Activity(Label = "GeneratorsActivity")]
    public class GeneratorsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Generators_layout);

            var listView = FindViewById<ListView>(Resource.Id.generators_listview); // get reference to the ListView in the layout

            // populate the listview with data
            listView.Adapter = new GeneratorAdapter(this, MainActivity._engine.GeneratorManager.Generators);
            //listView.ItemClick += OnListItemClick;  // to be defined
        }
    }
}
using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using BeersLibrary;
using Newtonsoft.Json;

namespace BeerAppAndroid
{
    [Activity(Label = "BeerAppAndroid", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity, SearchView.IOnQueryTextListener
    {
        SearchView beerSearchView;
        Button favouriteBeersButton;
        Button guesButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            beerSearchView = FindViewById<SearchView>(Resource.Id.beerSearchView);
            favouriteBeersButton = FindViewById<Button>(Resource.Id.favouriteBeersButton);
            guesButton = FindViewById<Button>(Resource.Id.guesBeerButton);

            beerSearchView.SetOnQueryTextListener(this);
            beerSearchView.SetQueryHint("Search a beer");

            favouriteBeersButton.Click += FavouriteBeersButton_Click;
            guesButton.Click += GuesButton_Click;

        }

        private void GuesButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(BeerActivity));
            intent.PutExtra("sender", "lucky");

            this.StartActivity(intent);
        }

        private void FavouriteBeersButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(BeerListActivity));
            intent.PutExtra("senderActivity", "favourite");

            this.StartActivity(intent);

        }

        public bool OnQueryTextChange(string newText)
        {
            return false;
        }

        public bool OnQueryTextSubmit(string query)
        {
            Intent intent = new Intent(this, typeof(BeerListActivity));
            intent.PutExtra("querystring", query);
            intent.PutExtra("senderActivity", "search");

            StartActivity(intent);
            return true;
        }
    }
}


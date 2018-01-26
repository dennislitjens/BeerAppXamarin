
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BeersLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BeerAppAndroid
{
    //[Activity(Label = "Beers", MainLauncher = true, Icon = "@mipmap/icon")]
    [Activity(Label = "Beers")]
    public class BeerListActivity : ListActivity
    {
        BeersManager beersManager;
        HttpClient client;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            client = new HttpClient();
            beersManager = new BeersManager();
            string querySearchBeer = Intent.GetStringExtra("querystring");
            bool senderIsFavouriteBeers = (Intent.GetStringExtra("senderActivity") == "favourite");

            if (senderIsFavouriteBeers)
            {
                FileHelper fileHelper = new FileHelper();
                string dbPath = fileHelper.GetLocalFilePath("beerapp_db");
                BeerLocalDatabase beerLocalDatabase = new BeerLocalDatabase(dbPath);
                if (!File.Exists(dbPath))
                {
                    new AlertDialog.Builder(this)
                        .SetPositiveButton("Ok", (sender, args) =>
                        {
                            Intent intent = new Intent(this, typeof(MainActivity));

                            StartActivity(intent);
                        })
                        .SetMessage("You don't have favourite beers")
                        .SetTitle("Oops!")
                        .Show();
                }else {
                    beersManager.Beers = await beerLocalDatabase.GetItemsAsync();
                    beersManager.InitLastIndex();
                    beersManager.MoveFirst();
                    ListAdapter = new BeersListManagerAdapter(this, Android.Resource.Layout.ActivityListItem, beersManager);
                }
            }
            else
            {
                try
                {
                    List<Beer> beers = new List<Beer>();
                    beers = await GetBeersAsync(querySearchBeer);
                    beersManager.InitLastIndex();
                    beersManager.MoveFirst();
                    ListAdapter = new BeersListManagerAdapter(this, Android.Resource.Layout.ActivityListItem, beersManager);
                }
                catch (Exception ex)
                {
                    new AlertDialog.Builder(this)
                        .SetPositiveButton("Ok", (sender, args) =>
                        {
                            Intent intent = new Intent(this, typeof(MainActivity));

                            StartActivity(intent);
                        })
                        .SetMessage("No beers found or no internet connection.")
                        .SetTitle("Oops!")
                        .Show();
                }
            }



            // Create your application here
        }

        //private void SetDeleteAction()
        //{
            
        //}

        //protected void OnSelection(object sender, SelectedItemChangedEventArgs e)
        //{
        //    if (e.SelectedItem == null)
        //    {
        //        return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
        //    }
        //    DisplayAlert("Item Selected", e.SelectedItem.ToString(), "Ok");
        //    //((ListView)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.
        //}

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            beersManager.MoveTo(position);
            BeerManagerSerializableAdapter adapter = new BeerManagerSerializableAdapter();
            adapter.Beers = beersManager.Beers;
            adapter.CurrentPosition = beersManager.CurrentPosition;

            Intent intent = new Intent(this, typeof(BeerActivity));
            intent.PutExtra("managerAdapter", JsonConvert.SerializeObject(adapter));

            StartActivity(intent);
        }

        public async Task<List<Beer>> GetBeersAsync(string searchText)
        {
            var response = await client.GetStringAsync("http://api.brewerydb.com/v2/search?q=" + searchText + "&type=beer&key=ea3f42048aa2b2e591a2be6861ca2f26");
            JObject jsonBeers = JObject.Parse(response);
            List<Beer> fetchedBeers = new List<Beer>();
            beersManager.Beers = new List<Beer>();
            foreach (var jsonBeer in jsonBeers["data"])
            {
                Beer convertedBeer = ConvertJsonBeerToBeerObject(jsonBeer);
                beersManager.Beers.Add(convertedBeer);
            }
            //beersTableView.ReloadData();
            return fetchedBeers;
        }

        public Beer ConvertJsonBeerToBeerObject(JToken jsonBeer)
        {
            Beer beer = new Beer();
            if (jsonBeer["name"] != null)
            {
                beer.Name = (string)jsonBeer["name"];
            }
            if (jsonBeer["description"] != null)
            {
                beer.Description = (string)jsonBeer["description"];
            }
            try
            {
                if (jsonBeer["labels"]["medium"] != null)
                {
                    beer.Photo = (string)jsonBeer["labels"]["medium"];
                }
            }
            catch (Exception ex)
            {
                beer.Photo = "";
            }

            if (jsonBeer["abv"] != null)
            {
                beer.AlcoholPercentage = (double)jsonBeer["abv"];
            }

            return beer;
        }
    }
}

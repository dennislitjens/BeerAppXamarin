
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BeersLibrary;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace BeerAppAndroid
{
    //[Activity(Label = "Beers", MainLauncher = true, Icon = "@mipmap/icon")]
    [Activity(Label = "Beer Activity")]
    public class BeerActivity : FragmentActivity
    {
        public BeersManager beersManager;
        BeerPagerAdapter beerPagerAdapter;
        ViewPager viewPager;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.BeerActivity);
            beersManager = new BeersManager();  
            if (Intent.GetStringExtra("sender") == null){
                try
                {
                    BeerManagerSerializableAdapter adapter = JsonConvert.DeserializeObject<BeerManagerSerializableAdapter>(Intent.GetStringExtra("managerAdapter"));
                    beersManager.Beers = adapter.Beers;
                    beersManager.CurrentPosition = adapter.CurrentPosition;
                    beersManager.InitLastIndex();
                }
                catch (ArgumentNullException ex)
                {
                    //beersManager.InitBeers();
                    beersManager.InitLastIndex();
                    beersManager.MoveFirst();
                }
            }else {
                beersManager.Beers = new List<Beer>();
                HttpClient client = new HttpClient(); 
                var response = await client.GetStringAsync("http://api.brewerydb.com/v2/beer/random?key=ea3f42048aa2b2e591a2be6861ca2f26");
                JObject jsonBeer = JObject.Parse(response);
                Beer convertedBeer = ConvertJsonBeerToBeerObject(jsonBeer["data"]);
                beersManager.Beers.Add(convertedBeer);
                beersManager.CurrentPosition = 0;
                beersManager.InitLastIndex();
            }
           
            beerPagerAdapter = new BeerPagerAdapter(SupportFragmentManager, beersManager);

            viewPager = FindViewById<ViewPager>(Resource.Id.beerPager);
            viewPager.Adapter = beerPagerAdapter;

            // Create your application here
        }
        protected string CreatePathToDatabase()
        {
            var docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var pathToDatabase = System.IO.Path.Combine(docsFolder, "db_sqlnet.db");
            return pathToDatabase;
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

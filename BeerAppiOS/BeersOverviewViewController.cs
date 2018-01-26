using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using BeersLibrary;
using Newtonsoft.Json.Linq;
using UIKit;

namespace BeerAppiOS
{
    public partial class BeersOverviewViewController : UIViewController
    {
        HttpClient client;
        BeersManager beersManager;
        public string searchQuery;
        public string sender;

        public BeersOverviewViewController() : base("BeersOverviewViewController", null)
        {
            client = new HttpClient();
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            Title = "Beers";

            beersManager = new BeersManager();
            List<Beer> beers = new List<Beer>();
            beersManager.Beers = new List<Beer>();
            BeerLocalDatabase beerLocalDatabase = InitializeBeerLocalDatabase();
            if (sender == "search"){
                try
                {
                    beers = await GetBeersAsync(searchQuery);
                    beersManager.InitLastIndex();
                    beersManager.MoveFirst();
                    beersTableView.Source = new BeersOverviewTableViewSource(beersManager, beerLocalDatabase);
                }
                catch (Exception ex)
                {
                    UIAlertView alert = new UIAlertView()
                    {
                        Title = "Oops!",
                        Message = "No beers found or no internet connection."
                    };
                    alert.AddButton("OK");
                    alert.Clicked += (handleAlertClicked);
                    alert.Show();
                }
            }else{
                List<Beer> beerList = await GetFavouriteBeersFromLocalDatabase();
                BeersOverviewTableViewSource beersOverviewTableViewSource = new BeersOverviewTableViewSource(beersManager, beerLocalDatabase);
                beersOverviewTableViewSource.canEditRows = true;
                beersTableView.Source = beersOverviewTableViewSource;
            }

        }

        private void handleAlertClicked(object sender, UIButtonEventArgs e)
        {
            HomeViewController homeViewController = new HomeViewController();
            AppDelegate appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
            appDelegate.RootNavigationController.PopViewController(true);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public async Task<List<Beer>> GetBeersAsync(string searchText)
        {
            var response = await client.GetStringAsync("http://api.brewerydb.com/v2/search?q=" + searchText + "&type=beer&key=ea3f42048aa2b2e591a2be6861ca2f26");
            JObject jsonBeers = JObject.Parse(response);
            List<Beer> fetchedBeers = new List<Beer>();
            foreach (var jsonBeer in jsonBeers["data"])
            {
                Beer convertedBeer = ConvertJsonBeerToBeerObject(jsonBeer);
                beersManager.Beers.Add(convertedBeer);
            }
            beersTableView.ReloadData();
            return fetchedBeers;
        }

        public async Task<List<Beer>> GetFavouriteBeersFromLocalDatabase()
        {
            BeerLocalDatabase beerLocalDatabase = InitializeBeerLocalDatabase();
            if (!File.Exists(beerLocalDatabase.dbPath))
            {
                UIAlertView noBeersFoundAlert = new UIAlertView("Oops", "You don't have favourite beers.", null, "Ok", null);

                noBeersFoundAlert.Show();
                return null;
            }
            else
            {
                beersManager.Beers = await beerLocalDatabase.GetItemsAsync();
                if (beersManager.Beers != null)
                {
                    beersManager.InitLastIndex();
                    beersManager.MoveFirst();
                    beersTableView.ReloadData();
                    return beersManager.Beers;
                }
                else
                {
                    UIAlertView alert = new UIAlertView()
                    {
                        Title = "Oops!",
                        Message = "You don't have favourite beers."
                    };
                    alert.AddButton("OK");
                    alert.Clicked += (handleAlertClicked);
                    alert.Show();
                    return null;
                }
            }
        }

        private BeerLocalDatabase InitializeBeerLocalDatabase()
        {
            FileHelper fileHelper = new FileHelper();
            string dbPath = fileHelper.GetLocalFilePath("beerapp_db");
            BeerLocalDatabase beerLocalDatabase = new BeerLocalDatabase(dbPath);
            return beerLocalDatabase;
        }

        public Beer ConvertJsonBeerToBeerObject(JToken jsonBeer)
        {
            Beer beer = new Beer();
            if(jsonBeer["name"] != null){
                beer.Name = (string)jsonBeer["name"];
            }
            if (jsonBeer["description"] != null)
            {
                beer.Description = (string)jsonBeer["description"];
            }
            try {
                if (jsonBeer["labels"]["medium"] != null)
                {
                    beer.Photo = (string)jsonBeer["labels"]["medium"];
                }
            }catch(Exception ex){
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


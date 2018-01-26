using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BeersLibrary;
using Newtonsoft.Json.Linq;
using UIKit;

namespace BeerAppiOS
{
    public partial class HomeViewController : UIViewController
    {
        Beer luckyBeer;

        public HomeViewController() : base("HomeViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            favouriteBeersButton.TouchUpInside += HandleFavouriteBeersTouchUpInside;
            luckyButton.TouchUpInside += HandleLuckyTouchUpInside;
            beerSearchView.SearchButtonClicked += HandleSearchButtonClicked;
        }

        private void HandleSearchButtonClicked(object sender, EventArgs e)
        {
            UISearchBar searchBar = (UISearchBar)sender;
            string searchQuery = searchBar.Text;

            BeersOverviewViewController beersOverviewViewController =
                new BeersOverviewViewController();
            beersOverviewViewController.searchQuery = searchQuery;
            beersOverviewViewController.sender = "search";
            AppDelegate appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
            appDelegate.RootNavigationController.PushViewController(beersOverviewViewController, true);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public void HandleFavouriteBeersTouchUpInside (object sender, EventArgs args){
            BeersOverviewViewController beersOverviewViewController =
                new BeersOverviewViewController();
            beersOverviewViewController.sender = "favourite";
            AppDelegate appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
            appDelegate.RootNavigationController.PushViewController(beersOverviewViewController, true);
        }

        public async void HandleLuckyTouchUpInside(object sender, EventArgs args)
        {
            BeersManager beersManager = new BeersManager();
            //beersManager.InitBeers();
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://api.brewerydb.com/v2/beer/random?key=ea3f42048aa2b2e591a2be6861ca2f26");
            JObject jsonBeer = JObject.Parse(response);
            luckyBeer = ConvertJsonBeerToBeerObject(jsonBeer["data"]);
            beersManager.Beers = new List<Beer>();
            beersManager.Beers.Add(luckyBeer);
            beersManager.MoveFirst();
            beersManager.InitLastIndex();
            BeerPagerViewController beerPagerViewController =
                new BeerPagerViewController(beersManager);

            AppDelegate appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
            appDelegate.RootNavigationController.PushViewController(beerPagerViewController, true);
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


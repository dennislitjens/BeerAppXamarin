using System;
using System.Collections.Generic;
using System.Net.Http;
using BeersLibrary;
using Foundation;
using Newtonsoft.Json.Linq;
using UIKit;

namespace BeerAppiOS
{
    public class BeerPagerViewController : UIViewController
    {
        UIPageViewController pageViewController;
        BeersManager beerManager;
        string sender;
        HttpClient client;

        public BeerPagerViewController(BeersManager beersManager)
        {
                this.beerManager = beersManager;
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {

            base.ViewDidLoad();

            // Perform any additional setup after loading the view
            pageViewController = new UIPageViewController(
                UIPageViewControllerTransitionStyle.PageCurl,
                UIPageViewControllerNavigationOrientation.Horizontal,
                UIPageViewControllerSpineLocation.Min);
            pageViewController.View.Frame = View.Bounds;
            View.AddSubview(pageViewController.View);

            BeerPagerViewControllerDataSource dataSource =
                new BeerPagerViewControllerDataSource(beerManager);
            pageViewController.DataSource = dataSource;

            BeerViewController firstCourseViewController =
                dataSource.GetFirstViewController();

            pageViewController.SetViewControllers(
                new UIViewController[] { firstCourseViewController },
                UIPageViewControllerNavigationDirection.Forward, false, null);
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

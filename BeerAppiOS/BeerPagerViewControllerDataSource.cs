using System;
using UIKit;
using BeersLibrary;

namespace BeerAppiOS
{
    public class BeerPagerViewControllerDataSource : UIPageViewControllerDataSource
    {
        BeersManager beersManager;

        public BeerPagerViewControllerDataSource(BeersManager beersManager)
        {
            this.beersManager = beersManager;
        }

        public BeerViewController GetFirstViewController()
        {
            return CreateBeersViewController();
        }

        public override UIViewController GetNextViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
        {
            BeerViewController returnBeerViewController = null;

            BeerViewController referenceBeerViewController =
                referenceViewController as BeerViewController;
            if(referenceBeerViewController != null)
            {
                beersManager.MoveTo(referenceBeerViewController.BeerPosition);
                if(beersManager.CanMoveNext)
                {
                    beersManager.MoveNext();
                    returnBeerViewController = CreateBeersViewController();
                }
            }
            return returnBeerViewController;
        }

        public override UIViewController GetPreviousViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
        {
            BeerViewController returnBeerViewController = null;

            BeerViewController referenceBeerViewController =
                referenceViewController as BeerViewController;
            if (referenceBeerViewController != null)
            {
                beersManager.MoveTo(referenceBeerViewController.BeerPosition);
                if (beersManager.CanMovePrev)
                {
                    beersManager.MovePrev();
                    returnBeerViewController = CreateBeersViewController();
                }
            }
            return returnBeerViewController;
        }

        private BeerViewController CreateBeersViewController()
        {
            BeerViewController beerViewController = new BeerViewController();
            beerViewController.Beer = beersManager.Current;
            beerViewController.BeerPosition = beersManager.CurrentPosition;

            return beerViewController;
        }


    }
}

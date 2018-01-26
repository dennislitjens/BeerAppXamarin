using System;
using Android.Support.V4.App;
using BeersLibrary;

namespace BeerAppAndroid
{
    public class BeerPagerAdapter : FragmentStatePagerAdapter
    {
        public BeerPagerAdapter(FragmentManager fm, BeersManager beersManager)
            :base(fm)
        {
            this.beersManager = beersManager;
        }

        BeersManager beersManager;

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            BeerFragment beerFragment = new BeerFragment();
            beersManager.CurrentPosition = position;
            beerFragment.Beer = beersManager.Current;

            return beerFragment;
        }

        public override int Count
        {
            get { return beersManager.Length; }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BeersLibrary;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using System.IO;
using System.Threading.Tasks;
using Android.App;

namespace BeerAppAndroid
{
    public class BeerFragment : Android.Support.V4.App.Fragment
    {
        TextView titleTextView;

        TextView descriptionTextView;

        ImageView beerImage;

        Button saveToFavouriteButton;

        string pathToDatabase;
        public Beer Beer { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);            
            View rootView = inflater.Inflate(Resource.Layout.BeerFragment, container, false);

            titleTextView = rootView.FindViewById<TextView>(Resource.Id.titleTextView);
            descriptionTextView = rootView.FindViewById<TextView>(Resource.Id.descriptionTextView);
            beerImage = rootView.FindViewById<ImageView>(Resource.Id.beerImage);
            saveToFavouriteButton = rootView.FindViewById<Button>(Resource.Id.heartButton);

            saveToFavouriteButton.Click += FavouriteBeersButton_Click;

            titleTextView.Text = Beer.Name;
            descriptionTextView.Text = Beer.Description;
            var imageBitMap = ResourceHelper.GetImageBitmapFromUrl(Beer.Photo);
            beerImage.SetImageBitmap(imageBitMap);
            CheckIfBeerIsInLocalDatabase();
            return rootView;
        }

        private async void FavouriteBeersButton_Click(object sender, EventArgs e)
        {
            FileHelper fileHelper = new FileHelper();
            string dbPath = fileHelper.GetLocalFilePath("beerapp_db");
            BeerLocalDatabase beerLocalDatabase = new BeerLocalDatabase(dbPath);
            if (File.Exists(dbPath))
            {
                beerLocalDatabase.CreateDatabase();
            }
            await beerLocalDatabase.SaveItemAsync(Beer);
            saveToFavouriteButton.Enabled = false;
            saveToFavouriteButton.Alpha = 0.5F;
            //if(isSucces == 0){
            //    new AlertDialog.Builder(Activity)
            //        .SetPositiveButton("Ok", (senderAlert, args) =>
            //        {
            //        })
            //        .SetMessage("The beer is added to your favourites.")
            //        .SetTitle("Cheers!")
            //        .Show();
            //}else {
            //    new AlertDialog.Builder(Activity)
            //        .SetPositiveButton("Ok", (senderAlert, args) =>
            //        {
            //        })
            //        .SetMessage("The beer could not be added to your favourites.")
            //        .SetTitle("Oops!")
            //        .Show();
            //}
        }

        private async void CheckIfBeerIsInLocalDatabase()
        {
            FileHelper fileHelper = new FileHelper();
            string dbPath = fileHelper.GetLocalFilePath("beerapp_db");
            BeerLocalDatabase beerLocalDatabase = new BeerLocalDatabase(dbPath);
            if (!File.Exists(dbPath))
            {
                beerLocalDatabase.CreateDatabase();
            }
            bool beerIsInDatabase = await beerLocalDatabase.CheckIfBeerIsInLocalDatabase(Beer.Name);
            if (beerIsInDatabase)
            {
                saveToFavouriteButton.Alpha = (float)0.5;
                saveToFavouriteButton.Enabled = false;
            }
        }
    }
}

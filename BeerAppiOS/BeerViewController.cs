using System;
using BeersLibrary;
using UIKit;
using SDWebImage;
using Foundation;
using System.IO;
using System.Threading.Tasks;

namespace BeerAppiOS
{
    public partial class BeerViewController : UIViewController
    {
        public Beer Beer { get; set; }
        public int BeerPosition { get; set; }

        public BeerViewController() : base("BeerViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            UpdateUI();
            CheckIfBeerIsInLocalDatabase();
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
            if(!beerIsInDatabase){
                heartButton.TouchUpInside += HandleHeartButtonTouchUpInside;
            }else {
                heartButton.Alpha = (float)0.5;
                heartButton.Enabled = false;
            }
        }

        private async void HandleHeartButtonTouchUpInside(object sender, EventArgs e)
        {
            FileHelper fileHelper = new FileHelper();
            string dbPath = fileHelper.GetLocalFilePath("beerapp_db");
            BeerLocalDatabase beerLocalDatabase = new BeerLocalDatabase(dbPath);
            if (!File.Exists(dbPath))
            {
                beerLocalDatabase.CreateDatabase();
            }
            await beerLocalDatabase.SaveItemAsync(Beer);
               heartButton.Alpha = (float)0.5;
               heartButton.Enabled = false;

            UIAlertView alert = new UIAlertView()
            {
                Title = "Cheers!",
                Message = Beer.Name + " is added to your favourites."
            };
            alert.AddButton("OK");
            alert.Show();
        }

        private void UpdateUI()
        {
            titleLabel.Text = Beer.Name + ": " + Beer.AlcoholPercentage;
            descriptionTextView.Text = Beer.Description;

            NSUrl imageUrl = new NSUrl(Beer.Photo);
            UIImage placeholderImage = UIImage.FromBundle("placeholder.png");
            beerImage.SetImage(imageUrl, placeholderImage);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}


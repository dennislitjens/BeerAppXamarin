using System;
using BeersLibrary;
using Foundation;
using UIKit;
using SDWebImage;

namespace BeerAppiOS
{
    public class BeersOverviewTableViewSource : UITableViewSource
    {
        BeersManager beersManager;
        NSString cellID = new NSString("BeerTableViewCell");
        BeerLocalDatabase beerLocalDatabase;
        public bool canEditRows = false;

        public BeersOverviewTableViewSource(BeersManager beersManager, BeerLocalDatabase beerLocalDatabase)
        {
            this.beersManager = beersManager;
            this.beerLocalDatabase = beerLocalDatabase;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell("BeerTableViewCell");

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, cellID);
            }

            beersManager.MoveTo(indexPath.Row);
            //beerImage = await ResourceHelper.LoadImage(beersManager.Current.Photo);
            cell.TextLabel.Text = beersManager.Current.Name;
            cell.DetailTextLabel.Text = "" + beersManager.Current.AlcoholPercentage;

            NSUrl imageUrl = new NSUrl(beersManager.Current.Photo);
            UIImage placeholderImage = UIImage.FromBundle("placeholder.png");
            cell.ImageView.SetImage(imageUrl, placeholderImage);

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            if (beersManager.Beers != null) {
                
                return beersManager.Length;
            }else{
                return 0;
            }

        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            beersManager.MoveTo(indexPath.Row);
            BeerPagerViewController beerPagerViewController =
                new BeerPagerViewController(beersManager);

            AppDelegate appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
            appDelegate.RootNavigationController.PushViewController(beerPagerViewController, true);
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, Foundation.NSIndexPath indexPath)
        {
            switch (editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:
                    // remove the item from the underlying data source
                    beersManager.MoveTo(indexPath.Row);
                    beerLocalDatabase.DeleteItemAsync(beersManager.Current);
                    beersManager.Beers.Remove(beersManager.Current);
                    // delete the row from the table
                    tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
                    break;
                case UITableViewCellEditingStyle.None:
                    Console.WriteLine("CommitEditingStyle:None called");
                    break;
            }
        }
        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return canEditRows; // return false if you wish to disable editing for a specific indexPath or for all rows
        }
        public override string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath)
        {   // Optional - default text is 'Delete'
            return "Trash (" + beersManager.Current.Name + ")";
        }
    }
}

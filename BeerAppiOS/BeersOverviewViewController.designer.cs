// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace BeerAppiOS
{
    [Register ("BeersOverviewViewController")]
    partial class BeersOverviewViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView beersTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (beersTableView != null) {
                beersTableView.Dispose ();
                beersTableView = null;
            }
        }
    }
}
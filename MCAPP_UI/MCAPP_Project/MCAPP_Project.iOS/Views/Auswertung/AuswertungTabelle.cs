
using System;
using System.Drawing;

using Foundation;
using MCAPP_Project.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using UIKit;

namespace MCAPP_Project.iOS.Views.Auswertung
{
    [MvxFromStoryboard]
    public partial class AuswertungTabelle : MvxTableViewController
    {
        public AuswertungTabelle(IntPtr handle) : base(handle)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        #region View lifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            TableView.RowHeight = UITableView.AutomaticDimension;
            TableView.EstimatedRowHeight = 40f;

            var source = new ThemaAuswertungSouce(TableView);
            TableView.Source = source;

            var set = this.CreateBindingSet<AuswertungTabelle, AuswertungTabelleViewModel>();
            set.Bind(source).To(vm => vm.Tables);

            // Button zum Anzeigen der Lösungen
            var button = new UIBarButtonItem(UIBarButtonSystemItem.FastForward);
            NavigationItem.SetRightBarButtonItem(button, false);
            set.Bind(button).To(vm => vm.SolutionButtonCommand);


            set.Apply();

            // Zurück-Button entfernen
            NavigationController.TopViewController.NavigationItem.SetHidesBackButton(true, true);




        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }

        #endregion
    }
}

using System;
using System.Drawing;

using Foundation;
using MCAPP_Project.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using UIKit;

namespace MCAPP_Project.iOS.Views
{
    [MvxFromStoryboard]
    public partial class QuestionTableView : MvxTableViewController
    {
        public QuestionTableView(IntPtr handle) : base(handle)
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
            var button = new UIBarButtonItem(UIBarButtonSystemItem.FastForward);
            NavigationItem.SetRightBarButtonItem(button, false);          

            TableView.RowHeight = UITableView.AutomaticDimension;
            TableView.EstimatedRowHeight = 500f;

            var source = new QuestionTableViewSource(TableView);
            TableView.Source = source;

            var set = this.CreateBindingSet<QuestionTableView, QuestionTableViewModel>();
            set.Bind(source).To(vm => vm.Tables);
            set.Bind(button).To(vm => vm.NextButtonCommand);
            set.Apply();
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
            Boolean found = false;

            foreach(UIViewController c in NavigationController.ViewControllers)
            {
                if (this.Equals(c))
                {
                    found = true;
                }
            }

            if (!found)
            {
                // hier kommt man an, wenn der "Zurück"-Button betätigt wurde.

                Console.WriteLine("zurueck");
            }




            base.ViewWillDisappear(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }

        #endregion
    }
}
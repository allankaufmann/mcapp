using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;


namespace MCAPP_Project.iOS.Views
{
    public class QuestionTableViewSource : MvxTableViewSource
    {
        public QuestionTableViewSource(UITableView tableView) : base(tableView)
        {

        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            return (QuestionTableViewCell)tableView.DequeueReusableCell("QuestionTableCell");
        }
    }
}
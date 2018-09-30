using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace MCAPP_Project.iOS.Views
{
    public class ThemenwahlViewSource : MvxTableViewSource
    {
        public ThemenwahlViewSource(UITableView tableView) : base(tableView)
        {

        }


        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {

            nint count = tableView.NumberOfRowsInSection(0);

            if (indexPath.LongRow == 0)
            {
                return (ThemenwahlUeberschriftCell)tableView.DequeueReusableCell("ThemenwahlUeberschrift");
            } else if (indexPath.LongRow == (count-1))
            {
                return (StartButtonCell)tableView.DequeueReusableCell("StartbuttonCell");
            } else
            {
                return (ThemenwahlCell)tableView.DequeueReusableCell("ThemenwahlCell");
            }


            
        }
    }
}
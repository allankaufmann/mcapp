using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace MCAPP_Project.iOS.Views.Auswertung
{
    public class ThemaAuswertungSouce : MvxTableViewSource
    {
        public ThemaAuswertungSouce(UITableView tableView) : base (tableView) 
        {
            
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            nint count = tableView.NumberOfRowsInSection(0);

            if (indexPath.LongRow == (count - 1))
            {
                ReStartButtonCell cell = (ReStartButtonCell)tableView.DequeueReusableCell("ReStartbuttonCell");
                return cell;
            }

            return (ThemaAuswertungCell)tableView.DequeueReusableCell("ThemaAuswertungCell");



        }


    }

}
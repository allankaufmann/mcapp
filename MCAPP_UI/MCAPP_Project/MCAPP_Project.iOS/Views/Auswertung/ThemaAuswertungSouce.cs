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
            return (ThemaAuswertungCell)tableView.DequeueReusableCell("ThemaAuswertungCell");
        }


    }

}
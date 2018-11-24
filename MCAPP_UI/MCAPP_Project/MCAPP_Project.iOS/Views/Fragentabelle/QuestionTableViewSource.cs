using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.ViewModels;
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
            QuestionViewModel view = (QuestionViewModel)item;

            if (indexPath.LongRow == 0)
            {
                return (QuestionTableViewThemaCell)tableView.DequeueReusableCell("QuestionTableCell");
            }
            else if (indexPath.LongRow == 1)
            {
                return (QuestionTableViewFrageCell)tableView.DequeueReusableCell("QuestionTableFrageCell");
            }
            else
            { 
                if (view.antwort.GetType() == typeof(Bildantwort))
                {
                    return (QuestionTableBildantwortTextCell)tableView.DequeueReusableCell("QuestionBildantwortCell");
                }

                return (QuestionTableAntwortTextCell)tableView.DequeueReusableCell("QuestionTableAntwortText");


            }            
        }




    }
}
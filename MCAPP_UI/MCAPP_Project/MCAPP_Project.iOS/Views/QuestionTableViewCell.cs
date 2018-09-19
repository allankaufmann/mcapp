using System;

using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;
using MvvmCross.Binding.BindingContext;
using MCAPP_Project.Core.ViewModels;

namespace MCAPP_Project.iOS.Views
{
    public partial class QuestionTableViewCell : MvxTableViewCell
    {
        //public static readonly NSString Key = new NSString("QuestionTableCell");
        //public static readonly UINib Nib;

        /*static QuestionTableViewCell()
        {
            Nib = UINib.FromName("QuestionTableCell", NSBundle.MainBundle);

        }*/

        protected QuestionTableViewCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<QuestionTableViewCell, QuestionViewModel>();
                set.Bind(ThemaText).To(vm => vm.Thema);
                set.Apply();

            });
            //Button.SetTitle("LaLa", UIControlState.Application);
        }

    }
}

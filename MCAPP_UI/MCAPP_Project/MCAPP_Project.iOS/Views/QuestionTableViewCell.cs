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
        public static readonly NSString Key = new NSString("QuestionTableViewCell");
        public static readonly UINib Nib;

        static QuestionTableViewCell()
        {
            Nib = UINib.FromName("QuestionTableViewCell", NSBundle.MainBundle);
        }

        protected QuestionTableViewCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<QuestionTableViewCell, QuestionTableViewModel>();
                set.Bind(ThemaText).To(vm => vm.Thema);
                set.Apply();

            });
        }
    }
}

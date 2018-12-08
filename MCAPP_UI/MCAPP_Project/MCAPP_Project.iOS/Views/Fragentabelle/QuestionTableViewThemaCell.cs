using System;

using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;
using MvvmCross.Binding.BindingContext;
using MCAPP_Project.Core.ViewModels;

namespace MCAPP_Project.iOS.Views
{
    public partial class QuestionTableViewThemaCell : MvxTableViewCell
    {

        protected QuestionTableViewThemaCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<QuestionTableViewThemaCell, QuestionViewModel>();
                set.Bind(ThemaText).To(vm => vm.Thema);
                set.Apply();
            });
        }

    }
}

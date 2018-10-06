using System;

using Foundation;
using MCAPP_Project.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace MCAPP_Project.iOS.Views
{
    public partial class QuestionTableViewFrageCell : MvxTableViewCell
    {


        protected QuestionTableViewFrageCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<QuestionTableViewFrageCell, QuestionViewModel>();
                set.Bind(FrageText).To(vm => vm.FrageText);
                set.Bind(FrageNrText).To(vm => vm.FrageNrText);
                set.Apply();

            });
        }
    }
}

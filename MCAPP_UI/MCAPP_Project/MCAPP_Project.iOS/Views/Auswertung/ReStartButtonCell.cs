using System;

using Foundation;
using MCAPP_Project.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace MCAPP_Project.iOS.Views
{
    public partial class ReStartButtonCell : MvxTableViewCell
    {


        protected ReStartButtonCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.

            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<ReStartButtonCell, AuswertungThemaViewModel>();
                set.Bind(ReStartButton).To(vm => vm.ReStartQuizCommand);
                set.Apply();
            });

        }
    }
}

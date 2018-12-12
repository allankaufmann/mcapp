using System;
using FFImageLoading.Work;
using Foundation;
using MCAPP_Project.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace MCAPP_Project.iOS.Views
{
    public partial class StartButtonCell : MvxTableViewCell
    {


        Object binding = null;


        protected StartButtonCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.

            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<StartButtonCell, ThemenwahlViewModel>();
                set.Bind(StartButton).To(vm => vm.StartQuizCommand);
                set.Apply();
                binding = set;
            });

        }
    }
}

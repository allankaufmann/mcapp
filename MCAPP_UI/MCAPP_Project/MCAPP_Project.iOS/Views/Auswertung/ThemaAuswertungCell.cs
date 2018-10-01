using System;

using Foundation;
using MCAPP_Project.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace MCAPP_Project.iOS.Views.Auswertung
{
    public partial class ThemaAuswertungCell : MvxTableViewCell
    {


        protected ThemaAuswertungCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.

            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<ThemaAuswertungCell, AuswertungThemaViewModel>();
                set.Bind(ThemaText).To(vm => vm.Thema.ThemaText);
                set.Bind(AuswertungText).To(vm => vm.auswertungText);
                set.Apply();
            });

        }
    }
}

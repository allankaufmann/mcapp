using System;

using Foundation;
using MCAPP_Project.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace MCAPP_Project.iOS.Views.Themenwahl
{
    public partial class AnzahlFragenCell : MvxTableViewCell
    {


        protected AnzahlFragenCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                var setx = this.CreateBindingSet<AnzahlFragenCell, ThemaViewModel>();
                setx.Bind(anzText).To(vm => vm.anzahlText).TwoWay();
                setx.Bind(stepper).For(v=>v.Value).To(vm => vm.AnzahlFrage).TwoWay();
                setx.Apply();                
            });



        }
    }
}

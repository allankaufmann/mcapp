using System;

using Foundation;
using MCAPP_Project.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace MCAPP_Project.iOS.Views
{
    public partial class QuestionTableAntwortTextCell : MvxTableViewCell
    {


        protected QuestionTableAntwortTextCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<QuestionTableAntwortTextCell, QuestionViewModel>();
                set.Bind(AntwortTextView).To(vm => vm.AntwortText);
                set.Bind(Schalter).To(vm => vm.AntwortAuswahl);
                set.Apply();

            });


        }

        partial void Auswahl_TouchUpInside(UIButton sender)
        {
            throw new NotImplementedException();
        }
    }
}

using System;

using Foundation;
using MCAPP_Project.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Core;
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
                
                // Schalter darf nur aktiviert sein, solange Quiz nicht beendet wurde
                set.Bind(Schalter).For(v => v.Enabled).To(vm => vm.Editable);

                // Schalter wird auf eingegebene Antwort gebunden
                set.Bind(Schalter).To(vm => vm.AntwortAuswahl);

                // Lösungsschalter wird auf Lösung gebunden
                SchalterLoesung.Enabled = false;
                set.Bind(SchalterLoesung).To(vm => vm.AntwortRichtig);
                set.Bind(LblLoesung).For(v => v.Hidden).To(vm => vm.Editable);
                set.Bind(SchalterLoesung).For(v => v.Hidden).To(vm => vm.Editable);

                set.Apply();
            });


        }
    }
}

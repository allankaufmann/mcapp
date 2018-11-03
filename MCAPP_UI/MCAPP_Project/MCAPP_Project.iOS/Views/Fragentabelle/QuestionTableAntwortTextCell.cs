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

        private IMvxInteraction<Boolean> _interaction;
        public IMvxInteraction<Boolean> Interaction
        {
            get => _interaction;
            set
            {
                if (_interaction != null)
                    _interaction.Requested -= OnInteractionRequested;

                _interaction = value;
                _interaction.Requested += OnInteractionRequested;
            }
        }

        private async void OnInteractionRequested(object sender, MvxValueEventArgs<Boolean> eventArgs)
        {
            var editable = eventArgs.Value;
            AntwortTextView.Editable = editable;
            /*var yesNoQuestion = eventArgs.Value;
            // show dialog
            var status = await ShowDialog(yesNoQuestion.Question);
            yesNoQuestion.YesNoCallback(status == DialogStatus.Yes);*/

        }





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


                var set2 = this.CreateBindingSet<QuestionTableAntwortTextCell, QuestionTableViewModel>();
                set2.Bind(this).For(view => view.Interaction).To(viewModel => viewModel.Interaction);
                set2.Apply();


            });


        }
    }
}

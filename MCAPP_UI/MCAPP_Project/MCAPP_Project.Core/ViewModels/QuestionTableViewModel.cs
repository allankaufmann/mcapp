using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Repositories;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MCAPP_Project.Core.ViewModels
{
    public class QuestionTableViewModel : MvxViewModel<Quiz>
    {
        readonly private IFragenRepository repo;

        readonly IMvxNavigationService navigationService;

        // Sollte nach Navigation gesetzt werden...

        Quiz quiz;

        public QuestionTableViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
            repo = Mvx.Resolve<IFragenRepository>();

            Tables = new ObservableCollection<QuestionViewModel>();

            NextButtonCommand = new MvxAsyncCommand(NextButton);
        }


        public ObservableCollection<QuestionViewModel> Tables { get; }

        public override void Prepare(Quiz parameter)
        {
            this.quiz = parameter;

            Tables.Add(new QuestionViewModel(parameter));
            Tables.Add(new QuestionViewModel(parameter));

            Frage frage = parameter.fragen[quiz.position];

            if (frage.antworten != null)
            {
                foreach (Textantwort a in frage.antworten)
                {
                    Tables.Add(new QuestionViewModel(parameter, a));
                }
            }
        }

        public IMvxAsyncCommand NextButtonCommand { get; }

        async Task NextButton()
        {
            int newpos = this.quiz.position+1;

            if (quiz.fragen.Count> (newpos))
            {
                _interaction.Raise(this.quiz.ended);

                this.quiz.position = this.quiz.position + 1;
                await navigationService.Navigate(typeof(QuestionTableViewModel), this.quiz);
            } else
            {
                await navigationService.Navigate(typeof(AuswertungTabelleViewModel), this.quiz);
            }            
        }

        private MvxInteraction<Boolean> _interaction = new MvxInteraction<Boolean>();
        public IMvxInteraction<Boolean> Interaction => _interaction;



    }
}

using MCAPP_Project.Core.Models;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MCAPP_Project.Core.ViewModels
{

    public class AuswertungTabelleViewModel : MvxViewModel<Quiz>
    {
        Quiz quiz;

        public ObservableCollection<AuswertungThemaViewModel> Tables { get; }

        IMvxNavigationService navigationService;

        public AuswertungTabelleViewModel(IMvxNavigationService navigationService)
        {
            Tables = new ObservableCollection<AuswertungThemaViewModel>();
            this.navigationService = navigationService;

            SolutionButtonCommand = new MvxAsyncCommand(SolutionButton);

        }

        async Task SolutionButton()
        {
            
            this.quiz.ended = true;
            this.quiz.position = 0;
            await navigationService.Navigate(typeof(QuestionTableViewModel), quiz);
        }



        public override void Prepare(Quiz parameter)
        {
            this.quiz = parameter;

            foreach (Thema t in quiz.gewaelteThemen)
            {
                Tables.Add(new AuswertungThemaViewModel(this.navigationService, t, parameter));
            }

            Tables.Add(new AuswertungThemaViewModel(this.navigationService));
        }


        public IMvxAsyncCommand SolutionButtonCommand { get; set; }


    }
}

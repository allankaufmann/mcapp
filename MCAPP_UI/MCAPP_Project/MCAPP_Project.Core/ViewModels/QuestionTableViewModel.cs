using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Repositories;
using MCAPP_Project.Core.Wrapper;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MCAPP_Project.Core.ViewModels
{
    public class QuestionTableViewModel : MvxViewModel<FragenWrapper>
    {
        private IFragenRepository repo;

        readonly IMvxNavigationService navigationService;

        // Sollte nach Navigation gesetzt werden...

        FragenWrapper wrapper;

        public QuestionTableViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
            repo = new DummyFragenRepository();

            Tables = new ObservableCollection<QuestionViewModel>();

            NextButtonCommand = new MvxAsyncCommand(NextButton);
        }


        public ObservableCollection<QuestionViewModel> Tables { get; }

        public override void Prepare(FragenWrapper parameter)
        {
            this.wrapper = parameter;

            Tables.Add(new QuestionViewModel(parameter));
            Tables.Add(new QuestionViewModel(parameter));

            Frage frage = parameter.fragen[wrapper.position];

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
            int newpos = this.wrapper.position+1;

            if (wrapper.fragen.Count> (newpos))
            {
                this.wrapper.position = this.wrapper.position + 1;
                await navigationService.Navigate(typeof(QuestionTableViewModel), this.wrapper);
            } else
            {
                await navigationService.Navigate(typeof(AuswertungTabelleViewModel), this.wrapper);
            }





            
        }


    }
}

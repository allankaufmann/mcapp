using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Repositories;
using MCAPP_Project.Core.Services;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MCAPP_Project.Core.ViewModels
{
    public class ThemenwahlViewModel : MvxViewModel
    {
        IFragenService fragenService;

        List<Thema> themenListe;

        readonly IMvxNavigationService navigationService;

        public ObservableCollection<ThemaViewModel> Tables { get; }


        public ThemenwahlViewModel(IMvxNavigationService navigationService)
        {
            this.Tables = new ObservableCollection<ThemaViewModel>();
            this.fragenService = new FrageService(new DummyFragenRepository());
            this.themenListe = fragenService.GetAllThemen();
            this.navigationService = navigationService;

            Tables.Add(new ThemaViewModel());

            foreach(Thema t in themenListe)
            {
                Tables.Add(new ThemaViewModel(t));
            }

            Tables.Add(new ThemaViewModel());

            StartQuizCommand = new MvxAsyncCommand(StartQuiz);

        }

        public IMvxAsyncCommand StartQuizCommand { get; }

        async Task StartQuiz()
        {
            await navigationService.Navigate(typeof(QuestionTableViewModel));
        }



    }
}

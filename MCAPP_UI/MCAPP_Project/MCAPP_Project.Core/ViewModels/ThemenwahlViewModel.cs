using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Repositories;
using MCAPP_Project.Core.Services;
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

            StartQuizCommand = new MvxAsyncCommand(StartQuiz, ThemaIstGewaehlt);


            Tables.Add(new ThemaViewModel(StartQuizCommand));

            foreach(Thema t in themenListe)
            {
                Tables.Add(new ThemaViewModel(StartQuizCommand, t));
            }

            Tables.Add(new ThemaViewModel(StartQuizCommand));
        }


        public IMvxAsyncCommand StartQuizCommand { get; }

        async Task StartQuiz()
        {
            List<Thema> gewaelteThemen = new List<Thema>();
            foreach (Thema t in themenListe)
            {
                if (t.ThemaGewaehlt)
                {
                    gewaelteThemen.Add(t);
                }
            }

            List<Frage> gezogeneFragen = new List<Frage>();
            gezogeneFragen = fragenService.GetFragen(gewaelteThemen);

            FragenWrapper wrapper = new FragenWrapper();
            wrapper.fragen = gezogeneFragen;
            wrapper.position = 0;


            await navigationService.Navigate(typeof(QuestionTableViewModel), wrapper);
        }


        public Boolean ThemaIstGewaehlt()
        {
            Boolean istGewaehlt = false;

            foreach (Thema t in themenListe)
            {
                if (t.ThemaGewaehlt)
                {
                    return true;
                }
            }
            return false;
        }



    }
}

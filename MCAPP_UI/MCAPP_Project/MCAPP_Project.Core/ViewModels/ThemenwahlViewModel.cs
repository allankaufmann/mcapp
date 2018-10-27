using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Repositories;
using MCAPP_Project.Core.Services;
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
    public class ThemenwahlViewModel : MvxViewModel
    {
        private readonly IFragenService fragenService;
        private readonly IQuizService quizService;
        private readonly IMCAPPWebService mcappwebservice;

        List<Thema> themenListe;

        readonly IMvxNavigationService navigationService;

        public ObservableCollection<ThemaViewModel> Tables { get; }


        public ThemenwahlViewModel(IMvxNavigationService navigationService)
        {
            this.mcappwebservice = Mvx.Resolve<IMCAPPWebService>();
            this.fragenService = Mvx.Resolve<IFragenService>();
            this.quizService = Mvx.IocConstruct<QuizService>();
            this.navigationService = navigationService;
            this.Tables = new ObservableCollection<ThemaViewModel>();
            this.StartQuizCommand = new MvxAsyncCommand(StartQuiz, ThemaIstGewaehlt);

            this.themenListe = fragenService.GetAllThemen();

            initViews();
        }

        private void initViews()
        {
            Tables.Add(new ThemaViewModel(StartQuizCommand));

            foreach (Thema t in themenListe)
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
            gezogeneFragen = await fragenService.GetFragen(gewaelteThemen, 10);

            Quiz quiz = quizService.CreateQuiz();
            quiz.fragen = gezogeneFragen;
            quiz.position = 0;
            quiz.gewaelteThemen = gewaelteThemen;

            await navigationService.Navigate(typeof(QuestionTableViewModel), quiz);
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

using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Services;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MCAPP_Project.Core.ViewModels
{
    public class AuswertungThemaViewModel : MvxViewModel
    {

        private Thema thema;

        private Quiz quiz;

        readonly IQuizService quizService;

        public IMvxAsyncCommand ReStartQuizCommand { get; }

        readonly IMvxNavigationService navigationService;

        public AuswertungThemaViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.ReStartQuizCommand = new MvxAsyncCommand(StartQuiz, startEnabled);
        }

        public AuswertungThemaViewModel(IMvxNavigationService navigationService, Thema thema, Quiz quiz)
        {
            this.navigationService = navigationService;
            //navigationService.Close(this);
            quizService = Mvx.Resolve<IQuizService>();
            this.ReStartQuizCommand = new MvxAsyncCommand(StartQuiz, startEnabled);

            this.thema = thema;
            this.quiz = quiz;

            if (!this.quiz.ended)
            {
                //this.quiz.ended = true;
                this.quiz = quizService.CreateAuswertung(thema, quiz);
            }

            
        }

        async Task StartQuiz()
        {
            await navigationService.Navigate(typeof(ThemenwahlViewModel));
        }

        public Boolean startEnabled()
        {
            return true;
        }

        public override void ViewDisappeared() 
        {
            base.ViewDisappeared();
            Console.WriteLine("hm....moment");
        }

        protected override void ReloadFromBundle(IMvxBundle state)
        {
            base.ReloadFromBundle(state);
            Console.WriteLine("hm....moment");
        }


        public String auswertungText
        {
            get
            {
                return this.quiz.AuswertungsTexte[this.thema];   
            }

           
        }



        public Thema Thema
        { get { return this.thema; } }

    }
}

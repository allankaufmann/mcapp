using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Services;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCAPP_Project.Core.ViewModels
{
    public class AuswertungThemaViewModel : MvxViewModel
    {

        private Thema thema;

        private Quiz quiz;

        readonly IQuizService quizService;


        public AuswertungThemaViewModel(Thema thema, Quiz quiz)
        {
            //navigationService.Close(this);
            quizService = Mvx.Resolve<IQuizService>();

            this.thema = thema;
            this.quiz = quiz;

            if (!this.quiz.ended)
            {
                this.quiz.ended = true;
                this.quiz = quizService.CreateAuswertung(thema, quiz);
            }

            
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

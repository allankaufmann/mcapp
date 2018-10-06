using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Services;
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
            quizService = Mvx.Resolve<IQuizService>();

            this.thema = thema;
            this.quiz = quiz;
            this.quiz = quizService.CreateAuswertung(thema, quiz);
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

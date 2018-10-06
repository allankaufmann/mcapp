using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Repositories;
using MCAPP_Project.Core.Services;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MCAPP_Project.Core.ViewModels
{
    public class QuestionViewModel : MvxViewModel
    {
        private Quiz quiz;

        private Textantwort antwort;

        private Frage frage;

        private IFragenService service = new FrageService(new DummyFragenRepository());


        public QuestionViewModel(Quiz parameter)
        {
            this.quiz = parameter;
            this.frage = parameter.fragen[quiz.position];
        }

        public QuestionViewModel(Quiz parameter, Textantwort antwort)
        {
            this.quiz = parameter;
            this.antwort = antwort;
        }

        public String FrageText
        {
            get { return this.frage.Fragetext;  }
        }

        public String AntwortText
        {
            get { return this.antwort.Text;  }
        }

        public Boolean AntwortAuswahl
        {
            get { return this.antwort.Auswahl; }
            set { this.antwort.Auswahl = value; }
        }


        public String Thema
        {
            get {
                if (frage!=null)
                {
                    Thema t = service.GetThema(frage.themaID);
                    return t.ThemaText;
                }

                return "";

            }
        }

        public String FrageNrText
        {
            get
            {              
                return "Frage " + (quiz.position+1) + " von " + quiz.fragen.Count;
            }
        }





    }
}

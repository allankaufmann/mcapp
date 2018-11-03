using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Repositories;
using MCAPP_Project.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
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

        readonly private IFragenService service;


        public QuestionViewModel(Quiz parameter)
        {
            this.quiz = parameter;
            this.frage = parameter.fragen[quiz.position];
            service = Mvx.Resolve<IFragenService>();
        }

        public QuestionViewModel(Quiz parameter, Textantwort antwort)
        {
            this.quiz = parameter;
            this.antwort = antwort;
            service = Mvx.Resolve<IFragenService>();
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
                    Thema t = service.GetThema(frage.thema_id);
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


        public bool Editable
        {
            get { return !this.quiz.ended;  }
        }


    }
}

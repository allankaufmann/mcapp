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

        private Textantwort antwort;

        private Frage frage;

        private IFragenService service = new FrageService(new DummyFragenRepository());


        public QuestionViewModel(Frage frage)
        {
            this.frage = frage;
        }

        public QuestionViewModel(Frage frage, Textantwort antwort)
        {
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





    }
}

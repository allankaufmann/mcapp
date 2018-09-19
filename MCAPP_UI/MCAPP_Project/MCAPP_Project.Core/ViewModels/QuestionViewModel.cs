using MCAPP_Project.Core.Models;
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


        public QuestionViewModel(Frage frage)
        {
            this.frage = frage;
        }

        public String FrageText
        {
            get { return this.frage.Fragetext;  }
        }

        public String AntwortText
        {
            get { return this.antwort.Text;  }
        }


        public String Thema
        {
            get { return "Externes Rechnungswesen"; }
        }





    }
}

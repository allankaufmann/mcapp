using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Repositories;
using MCAPP_Project.Core.Services;
using MCAPP_Project.Core.Wrapper;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MCAPP_Project.Core.ViewModels
{
    public class QuestionViewModel : MvxViewModel
    {
        private FragenWrapper wrapper;

        private Textantwort antwort;

        private Frage frage;

        private IFragenService service = new FrageService(new DummyFragenRepository());


        public QuestionViewModel(FragenWrapper parameter)
        {
            this.wrapper = parameter;
            this.frage = parameter.fragen[wrapper.position];
        }

        public QuestionViewModel(FragenWrapper parameter, Textantwort antwort)
        {
            this.wrapper = parameter;
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
                return "Frage " + (wrapper.position+1) + " von " + wrapper.fragen.Count;
            }
        }





    }
}
